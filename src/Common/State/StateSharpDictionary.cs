using StateSharp.Common.Event;
using StateSharp.Common.Transaction;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            var result = StateSharpConstructor.ConstructInternal<T>(_eventManager, $"{Path}[{key}]");
            _state.Add(key, result);
            _eventManager.Invoke(Path, new StateSharpEvent($"{Path}[{key}]", null, result));
            return result;
        }

        public T Add(IStateSharpTransaction transaction, string key)
        {
            throw new NotImplementedException();
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

        public void Remove(IStateSharpTransaction transaction, string key)
        {
            throw new NotImplementedException();
        }

        public IStateSharpTransaction BeginTransaction()
        {
            return _eventManager.BeginTransaction();
        }

        public void Commit(IStateSharpTransaction transaction)
        {
            _eventManager.Commit(transaction);
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