using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.Transactions;
using System;
using System.Collections.Generic;

namespace StateSharp.Core.States
{
    internal class StateStructure<T> : IStateStructure<T> where T : struct
    {
        private readonly IStateEventManager _eventManager;

        public string Path { get; }

        public T State { get; private set; }

        public StateStructure(IStateEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            State = default;
        }

        public T Set()
        {
            State = default;
            _eventManager.Invoke(Path);
            return State;
        }

        public void Set(T state)
        {
            State = state;
            _eventManager.Invoke(Path);
        }

        public IStateTransaction<IStateStructure<T>> BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void Commit(IStateTransaction<IStateStructure<T>> transaction)
        {
            if (transaction.Owner != this) throw new UnknownTransactionException();
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

        IStateStructure<T> IStateStructure<T>.Copy()
        {
            throw new NotImplementedException();
        }

        IStateBase IStateBase.Copy()
        {
            return ((IStateStructure<T>)this).Copy();
        }
    }
}