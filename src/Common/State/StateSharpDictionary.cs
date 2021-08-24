using StateSharp.Common.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StateSharp.Common.State
{
    internal sealed class StateSharpDictionary<T> : IStateSharpDictionary<T> where T : IStateSharpBase
    {
        private readonly Dictionary<string, T> _state;
        private readonly IStateSharpEventManager _eventManager;

        public string Path { get; }
        public IReadOnlyDictionary<string, T> State => new ReadOnlyDictionary<string, T>(_state);

        public StateSharpDictionary(IStateSharpEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            _state = new Dictionary<string, T>();
        }

        public T Add(string key)
        {
            object result;
            var type = typeof(T);
            var interfaces = type.GetInterfaces();
            if (interfaces.Contains(typeof(IStateSharpDictionaryBase)))
            {
                result = Activator.CreateInstance(typeof(StateSharpDictionary<>).MakeGenericType(type.GenericTypeArguments), _eventManager, $"{Path}[{key}]");
            }
            else if (interfaces.Contains(typeof(IStateSharpObjectBase)))
            {
                result = Activator.CreateInstance(typeof(StateSharpObject<>).MakeGenericType(type.GenericTypeArguments), _eventManager, $"{Path}[{key}]");
            }
            else if (interfaces.Contains(typeof(IStateSharpStructureBase)))
            {
                result = Activator.CreateInstance(typeof(StateSharpStructure<>).MakeGenericType(type.GenericTypeArguments), _eventManager, $"{Path}[{key}]");
            }
            else
            {
                throw new Exception($"Unknown type {type}");
            }
            _state.Add(key, (T)result);
            _eventManager.Invoke(Path, new StateSharpEvent($"{Path}[{key}]", null, result));
            return (T)result;
        }

        public void Remove(string key)
        {
            if (_state.TryGetValue(key, out var value))
            {
                _state.Remove(key);
                _eventManager.Invoke(Path, new StateSharpEvent($"{Path}[{key}]", value, null));
            }
            throw new KeyNotFoundException(key);
        }

        public void SubscribeOnChange(Action<IStateSharpEvent> handler)
        {
            _eventManager.Subscribe(Path, handler);
        }

        public void UnsubscribeOnChange(Action<IStateSharpEvent> handler)
        {
            _eventManager.Unsubscribe(Path, handler);
        }
    }
}