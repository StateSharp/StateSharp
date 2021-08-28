﻿using StateSharp.Core.Constructors;
using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StateSharp.Core.States
{
    internal sealed class StateDictionary<T> : IStateDictionary<T> where T : IStateBase
    {
        private Dictionary<string, T> _state;
        private readonly IStateEventManager _eventManager;

        public string Path { get; }
        public IReadOnlyDictionary<string, T> State => new ReadOnlyDictionary<string, T>(_state);

        public StateDictionary(IStateEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            _state = default;
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

        public IStateTransaction<IStateDictionary<T>> BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void Commit(IStateTransaction<IStateDictionary<T>> transaction)
        {
            if (transaction.Owner != this) throw new UnknownTransactionException();
        }

        public IStateTransaction<T> BeginAddTransaction()
        {
            throw new NotImplementedException();
        }

        public void Commit(IStateTransaction<T> transaction)
        {
            throw new NotImplementedException();
        }

        public void SubscribeOnChange(Action<IStateEvent> handler)
        {
            _eventManager.Subscribe(Path, handler);
        }

        public void UnsubscribeOnChange(Action<IStateEvent> handler)
        {
            _eventManager.Unsubscribe(Path, handler);
        }

        IReadOnlyList<IStateBase> IStateBase.GetFields()
        {
            throw new NotImplementedException();
        }

        IStateDictionary<T> IStateDictionary<T>.Copy()
        {
            throw new NotImplementedException();
        }

        IStateBase IStateBase.Copy()
        {
            return ((IStateDictionary<T>)this).Copy();
        }
    }
}