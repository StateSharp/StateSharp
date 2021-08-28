using StateSharp.Core.Events;
using System;

namespace StateSharp.Core.States
{
    public interface IStateBase
    {
        string Path { get; }

        void SubscribeOnChange(Action<IStateEvent> handler);
        void UnsubscribeOnChange(Action<IStateEvent> handler);
    }
}