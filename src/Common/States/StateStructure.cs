using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.Transactions;
using System;
using System.Collections.Generic;

namespace StateSharp.Core.States
{
    internal class StateStructure<T> : IStateStructure<T> where T : struct
    {
        private IStateEventManager _eventManager;

        public string Path { get; }

        public T State { get; private set; }

        public StateStructure(IStateEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            State = default;
        }

        internal StateStructure(IStateEventManager eventManager, string path, T state)
        {
            Path = path;
            _eventManager = eventManager;
            State = state;
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
            return new StateTransaction<IStateStructure<T>>(this, em => ((IStateStructure<T>)this).Copy(em));
        }

        public void Commit(IStateTransaction<IStateStructure<T>> transaction)
        {
            if (transaction.Owner != this) throw new UnknownTransactionException();

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

        void IStateBase.SetEventManager(IStateEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        IReadOnlyList<IStateBase> IStateBase.GetChildren()
        {
            return new List<IStateBase>();
        }

        IStateStructure<T> IStateStructure<T>.Copy(IStateEventManager eventManager)
        {
            return new StateStructure<T>(eventManager, Path, State);
        }

        IStateBase IStateStructure<T>.FromJson(string json)
        {
            throw new NotImplementedException();
        }

        IStateBase IStateBase.FromJson(string json)
        {
            throw new NotImplementedException();
        }

        string IStateBase.ToJson()
        {
            throw new NotImplementedException();
        }

        IStateBase IStateBase.Copy(IStateEventManager eventManager)
        {
            return ((IStateStructure<T>)this).Copy(eventManager);
        }
    }
}