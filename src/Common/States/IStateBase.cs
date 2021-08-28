using System;
using StateSharp.Core.Events;
using StateSharp.Core.Transactions;

namespace StateSharp.Core.States
{
    public interface IStateBase
    {
        string Path { get; }

        IStateTransaction BeginTransaction();
        void Commit(IStateTransaction transaction);

        void SubscribeOnChange(Action<IStateEvent> handler);
        void UnsubscribeOnChange(Action<IStateEvent> handler);
    }
}