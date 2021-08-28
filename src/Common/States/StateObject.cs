using StateSharp.Core.Constructors;
using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.Transactions;
using System;
using System.Collections.Generic;

namespace StateSharp.Core.States
{
    internal sealed class StateObject<T> : IStateObject<T> where T : class
    {
        private readonly IStateEventManager _eventManager;

        public string Path { get; }

        public T State { get; private set; }

        public StateObject(IStateEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            State = default;
        }

        public T Set()
        {
            State = StateConstructor.ConstructState<T>(_eventManager, Path);
            _eventManager.Invoke(Path);
            return State;
        }

        public IStateTransaction<IStateObject<T>> BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void Commit(IStateTransaction<IStateObject<T>> transaction)
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

        IStateObject<T> IStateObject<T>.Copy()
        {
            throw new NotImplementedException();
        }

        IStateBase IStateBase.Copy()
        {
            return ((IStateObject<T>)this).Copy();
        }
    }
}