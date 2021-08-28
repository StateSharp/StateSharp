using StateSharp.Core.Events;
using System;
using System.Collections.Generic;

namespace StateSharp.Core.States
{
    public interface IStateBase
    {
        string Path { get; }

        void SubscribeOnChange(Action<IStateEvent> handler);
        void UnsubscribeOnChange(Action<IStateEvent> handler);

        internal IReadOnlyList<IStateBase> GetFields();
        internal IStateBase Copy();
    }
}