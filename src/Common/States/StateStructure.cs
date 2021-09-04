using System;
using System.Collections.Generic;
using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.Transactions;

namespace StateSharp.Core.States
{
    internal class StateStructure<T> : IStateStructure<T> where T : struct
    {
        private IStateEventManager _eventManager;

        public StateStructure(IStateEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            State = default;
        }

        public StateStructure(IStateEventManager eventManager, string path, T state)
        {
            Path = path;
            _eventManager = eventManager;
            State = state;
        }

        public string Path { get; }

        public T State { get; private set; }

        public void Set(T state)
        {
            State = state;
            _eventManager.Invoke(Path);
        }

        public IStateTransaction<IStateStructure<T>> BeginTransaction()
        {
            return new StateTransaction<IStateStructure<T>>(this, em => ((IStateStructure<T>) this).Copy(em));
        }

        public void Commit(IStateTransaction<IStateStructure<T>> transaction)
        {
            if (transaction.Owner != this)
            {
                throw new UnknownTransactionException("Object is not the owner of transaction");
            }

            State = transaction.State.State;

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

        IStateEventManager IStateBase.GetEventManager()
        {
            return _eventManager;
        }

        void IStateBase.SetEventManager(IStateEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        IReadOnlyList<IStateBase> IStateBase.GetChildren()
        {
            return new List<IStateBase>();
        }

        object IStateStructureBase.GetState()
        {
            return State;
        }

        IStateStructure<T> IStateStructure<T>.Copy(IStateEventManager eventManager)
        {
            return new StateStructure<T>(eventManager, Path, State);
        }

        IStateBase IStateBase.Copy(IStateEventManager eventManager)
        {
            return ((IStateStructure<T>) this).Copy(eventManager);
        }

        public T Set()
        {
            State = default;
            _eventManager.Invoke(Path);
            return State;
        }
    }
}
