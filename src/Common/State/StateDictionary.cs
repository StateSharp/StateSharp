using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using StateSharp.Core.Event;
using StateSharp.Core.Transaction;

namespace StateSharp.Core.State
{
    internal sealed class StateDictionary<T> : IStateDictionary<T> where T : IStateBase
    {
        private readonly Dictionary<string, T> _state;
        private readonly IStateEventManager _eventManager;

        public string Path { get; }
        public IReadOnlyDictionary<string, T> State => new ReadOnlyDictionary<string, T>(_state);

        public StateDictionary(IStateEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            _state = new Dictionary<string, T>();
        }

        public T Add(string key)
        {
            var result = StateConstructor.ConstructInternal<T>(_eventManager, $"{Path}[{key}]");
            _state.Add(key, result);
            _eventManager.Invoke(Path, new StateEvent($"{Path}[{key}]", null, result));
            return result;
        }

        public T Add(IStateTransaction transaction, string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            if (_state.TryGetValue(key, out var value))
            {
                _state.Remove(key);
                _eventManager.Invoke(Path, new StateEvent($"{Path}[{key}]", value, null));
            }
            throw new KeyNotFoundException(key);
        }

        public void Remove(IStateTransaction transaction, string key)
        {
            throw new NotImplementedException();
        }

        public IStateTransaction BeginTransaction()
        {
            return _eventManager.BeginTransaction();
        }

        public void Commit(IStateTransaction transaction)
        {
            _eventManager.Commit(transaction);
        }

        public void SubscribeOnChange(Action<IStateEvent> handler)
        {
            _eventManager.Subscribe(Path, handler);
        }

        public void UnsubscribeOnChange(Action<IStateEvent> handler)
        {
            _eventManager.Unsubscribe(Path, handler);
        }
    }
}