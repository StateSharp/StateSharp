﻿using StateSharp.Core.Constructors;
using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StateSharp.Core.States
{
    internal sealed class StateDictionary<T> : IStateDictionary<T> where T : IStateBase
    {
        private Dictionary<string, T> _state;
        private IStateEventManager _eventManager;

        public string Path { get; }
        public IReadOnlyDictionary<string, T> State => _state == null ? null : new ReadOnlyDictionary<string, T>(_state);

        public StateDictionary(IStateEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            _state = null;
        }

        public StateDictionary(IStateEventManager eventManager, string path, Dictionary<string, T> state)
        {
            Path = path;
            _eventManager = eventManager;
            _state = state;
        }

        Dictionary<string, T> IStateDictionary<T>.GetState()
        {
            return _state;
        }

        public IReadOnlyDictionary<string, T> Set()
        {
            _state = new Dictionary<string, T>();
            _eventManager.Invoke(Path);
            return State;
        }

        public T Add(string key)
        {
            var result = StateConstructor.ConstructInternal<T>(_eventManager, $"{Path}[{key}]");
            _state.Add(key, result);
            _eventManager.Invoke($"{Path}[{key}]");
            return result;
        }

        public void Remove(string key)
        {
            if (_state.Remove(key) == false)
            {
                throw new KeyNotFoundException(key);
            }
            _eventManager.Invoke($"{Path}[{key}]");
        }

        public void Clear()
        {
            _state.Clear();
            _eventManager.Invoke(Path);
        }

        public IStateTransaction<IStateDictionary<T>> BeginTransaction()
        {
            return new StateTransaction<IStateDictionary<T>>(this, em => ((IStateDictionary<T>)this).Copy(em));
        }

        public void Commit(IStateTransaction<IStateDictionary<T>> transaction)
        {
            if (transaction.Owner != this) throw new UnknownTransactionException("Object is not the owner of transaction");

            _state = transaction.State.GetState();

            var queue = new Queue<IStateBase>(((IStateBase)this).GetChildren());
            while (queue.TryDequeue(out var state))
            {
                state.SetEventManager(_eventManager);
                foreach (var child in state.GetChildren())
                {
                    queue.Enqueue(child);
                }
            }

            _eventManager.Invoke(Path);
        }

        public void SubscribeOnChange(Action<IStateEvent> handler)
        {
            _eventManager.Subscribe(Path, handler);
        }

        public void UnsubscribeOnChange(Action<IStateEvent> handler)
        {
            _eventManager.Unsubscribe(Path, handler);
        }

        void IStateBase.SetEventManager(IStateEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        IReadOnlyList<IStateBase> IStateBase.GetChildren()
        {
            return State.Values.Cast<IStateBase>().ToList();
        }

        IStateDictionary<T> IStateDictionary<T>.Copy(IStateEventManager eventManager)
        {
            if (_state == null) return new StateDictionary<T>(eventManager, Path, null);
            var state = new Dictionary<string, T>();
            foreach (var (key, value) in State)
            {
                state.Add(key, (T)value.Copy(eventManager));
            }
            return new StateDictionary<T>(eventManager, Path, state);
        }

        IStateBase IStateDictionary<T>.FromJson(string json)
        {
            throw new NotImplementedException();
        }

        IStateBase IStateBase.FromJson(string json)
        {
            return ((IStateDictionary<T>)this).FromJson(json);
        }

        string IStateBase.ToJson()
        {
            throw new NotImplementedException();
        }

        IStateBase IStateBase.Copy(IStateEventManager eventManager)
        {
            return ((IStateDictionary<T>)this).Copy(eventManager);
        }
    }
}