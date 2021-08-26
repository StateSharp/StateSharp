using StateSharp.Common.Transaction;
using System;

namespace StateSharp.Common.Event
{
    internal interface IStateSharpEventManager
    {
        IStateSharpTransaction BeginTransaction();
        void Commit(IStateSharpTransaction transaction);
        void Invoke(string path, IStateSharpEvent param);
        void Subscribe(string path, Action<IStateSharpEvent> handler);
        void Unsubscribe(string path, Action<IStateSharpEvent> handler);
    }
}