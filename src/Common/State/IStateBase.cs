using System;
using StateSharp.Core.Event;
using StateSharp.Core.Transaction;

namespace StateSharp.Core.State
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