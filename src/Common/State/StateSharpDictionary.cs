using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StateSharp.Common.State
{
    internal sealed class StateSharpDictionary<T> : IStateSharpDictionary<T> where T : IStateSharpBase
    {
        private readonly Dictionary<string, T> _state;

        public string Path { get; }
        public IReadOnlyDictionary<string, T> State => new ReadOnlyDictionary<string, T>(_state);

        public StateSharpDictionary(string path)
        {
            Path = path;
            _state = new Dictionary<string, T>();
        }

        public T Add(string key)
        {
            var type = typeof(T);
            var interfaces = type.GetInterfaces();
            if (interfaces.Contains(typeof(IStateSharpDictionaryBase)))
            {
                var result = Activator.CreateInstance(typeof(StateSharpDictionary<>).MakeGenericType(type.GenericTypeArguments), $"{Path}[{key}]");
                _state.Add(key, (T)result);
                return (T)result;
            }
            if (interfaces.Contains(typeof(IStateSharpObjectBase)))
            {
                var result = Activator.CreateInstance(typeof(StateSharpObject<>).MakeGenericType(type.GenericTypeArguments), $"{Path}[{key}]");
                _state.Add(key, (T)result);
                return (T)result;
            }
            if (interfaces.Contains(typeof(IStateSharpStructureBase)))
            {
                var result = Activator.CreateInstance(typeof(StateSharpStructure<>).MakeGenericType(type.GenericTypeArguments), $"{Path}[{key}]");
                _state.Add(key, (T)result);
                return (T)result;
            }
            throw new Exception($"Unknown type {type}");
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
    }
}