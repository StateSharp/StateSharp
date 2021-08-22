using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StateSharp.Common.State
{
    internal sealed class StateSharpDictionary<T> : IStateSharpDictionary<T>
    {
        private readonly IStateSharpBase _parent;
        private readonly string _field;
        private readonly Dictionary<string, T> _state;

        IStateSharpBase IStateSharpBase.Parent => _parent;
        string IStateSharpBase.Key => _field;
        StateSharpType IStateSharpBase.Type => StateSharpType.Dictionary;

        public IReadOnlyDictionary<string, T> State => new ReadOnlyDictionary<string, T>(_state);

        public StateSharpDictionary(IStateSharpBase parent, string field)
        {
            _parent = parent;
            _field = field;
            _state = new Dictionary<string, T>();
        }

        public T Add(string key)
        {
            var type = typeof(T);
            var interfaces = type.GetInterfaces();
            if (interfaces.Contains(typeof(IStateSharpDictionaryBase)))
            {
                var result = Activator.CreateInstance(
                    typeof(StateSharpDictionary<>).MakeGenericType(type.GenericTypeArguments), this,
                    key);
                return (T)result;
            }
            else if (interfaces.Contains(typeof(IStateSharpObjectBase)))
            {
                var result = Activator.CreateInstance(
                    typeof(StateSharpObject<>).MakeGenericType(type.GenericTypeArguments), this,
                    key);
                return (T)result;
            }
            else if (interfaces.Contains(typeof(IStateSharpStructureBase)))
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new Exception($"Unknown type {type}");
            }
        }

        public void Remove(string key)
        {

        }

        public void SubscribeOnChange(Action<T> handler)
        {

        }

        public void UnsubscribeOnChange(Action<T> handler)
        {

        }

        string IStateSharpBase.GetPath(List<IStateSharpBase> callers)
        {
            callers.Add(this);
            return _parent.GetPath(callers);
        }

        public string GetPath()
        {
            return ((IStateSharpBase)this).GetPath(new List<IStateSharpBase>());
        }
    }
}