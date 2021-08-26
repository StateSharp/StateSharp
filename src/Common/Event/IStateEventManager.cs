using StateSharp.Core.Transaction;
using System;

namespace StateSharp.Core.Event
{
    internal interface IStateEventManager
    {
        IStateTransaction BeginTransaction();
        void Commit(IStateTransaction transaction);
        void Invoke(string path);
        void Subscribe(string path, Action<IStateEvent> handler);
        void Unsubscribe(string path, Action<IStateEvent> handler);
    }
}