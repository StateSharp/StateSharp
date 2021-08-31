using System;
using System.Collections.Generic;
using StateSharp.Core.Events;
using StateSharp.Core.Transactions;

namespace StateSharp.Core.States
{
    internal class StateString : IStateString
    {
        public string Path { get; }

        public void SubscribeOnChange(Action<IStateEvent> handler)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeOnChange(Action<IStateEvent> handler)
        {
            throw new NotImplementedException();
        }

        void IStateBase.SetEventManager(IStateEventManager eventManager)
        {
            throw new NotImplementedException();
        }

        IReadOnlyList<IStateBase> IStateBase.GetChildren()
        {
            throw new NotImplementedException();
        }

        IStateString IStateString.Copy(IStateEventManager eventManager)
        {
            throw new NotImplementedException();
        }

        public string State { get; }

        public string Init()
        {
            throw new NotImplementedException();
        }

        public IStateTransaction<IStateString> BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void Commit(IStateTransaction<IStateString> transaction)
        {
            throw new NotImplementedException();
        }

        IStateBase IStateBase.Copy(IStateEventManager eventManager)
        {
            throw new NotImplementedException();
        }

        object IStateStringBase.GetState()
        {
            throw new NotImplementedException();
        }
    }
}