using StateSharp.Common.Event;
using StateSharp.Common.Transaction;
using System;

namespace StateSharp.Common.State
{
    public interface IStateSharpBase
    {
        string Path { get; }

        IStateSharpTransaction BeginTransaction();
        void Commit(IStateSharpTransaction transaction);

        void SubscribeOnChange(Action<IStateSharpEvent> handler);
        void UnsubscribeOnChange(Action<IStateSharpEvent> handler);
    }
}