using System;
using StateSharp.Core.Transactions;

namespace StateSharp.Core.Events
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