using System;
using System.Collections.Generic;
using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.Transactions;

namespace StateSharp.Core.States
{
    internal class StateString : IStateString
    {
        private IStateEventManager _eventManager;

        public StateString(IStateEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            State = null;
        }

        public StateString(IStateEventManager eventManager, string path, string state)
        {
            Path = path;
            _eventManager = eventManager;
            State = state;
        }

        public string Path { get; }

        public void SubscribeOnChange(Action<IStateEvent> handler)
        {
            _eventManager.Unsubscribe(Path, handler);
        }

        public void UnsubscribeOnChange(Action<IStateEvent> handler)
        {
            _eventManager.Subscribe(Path, handler);
        }

        void IStateBase.SetEventManager(IStateEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        IReadOnlyList<IStateBase> IStateBase.GetChildren()
        {
            return new List<IStateBase>();
        }

        IStateString IStateString.Copy(IStateEventManager eventManager)
        {
            return new StateString(eventManager, Path, State);
        }

        public string State { get; private set; }

        public string Init()
        {
            State = string.Empty;
            return State;
        }

        public void Set(string state)
        {
            State = state;
            _eventManager.Invoke(Path);
        }

        public IStateTransaction<IStateString> BeginTransaction()
        {
            return new StateTransaction<IStateString>(this, em => ((IStateString) this).Copy(em));
        }

        public void Commit(IStateTransaction<IStateString> transaction)
        {
            if (transaction.Owner != this)
            {
                throw new UnknownTransactionException("Object is not the owner of transaction");
            }

            State = transaction.State.State;

            _eventManager.Invoke(Path);
        }

        IStateBase IStateBase.Copy(IStateEventManager eventManager)
        {
            return ((IStateString) this).Copy(eventManager);
        }

        object IStateStringBase.GetState()
        {
            return State;
        }
    }
}