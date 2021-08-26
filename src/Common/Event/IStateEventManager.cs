using System;
using StateSharp.Core.Transaction;

namespace StateSharp.Core.Event
{
    internal interface IStateEventManager
    {
        IStateTransaction BeginTransaction();
        void Commit(IStateTransaction transaction);
        void Invoke(string path, IStateEvent param);
        void Subscribe(string path, Action<IStateEvent> handler);
        void Unsubscribe(string path, Action<IStateEvent> handler);
    }
}