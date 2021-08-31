using System;
using System.Collections.Generic;
using StateSharp.Core.Events;

namespace StateSharp.Core.States
{
    public interface IStateBase
    {
        string Path { get; }

        void SubscribeOnChange(Action<IStateEvent> handler);
        void UnsubscribeOnChange(Action<IStateEvent> handler);

        internal void SetEventManager(IStateEventManager eventManager);
        internal IReadOnlyList<IStateBase> GetChildren();
        internal IStateBase Copy(IStateEventManager eventManager);
    }
}