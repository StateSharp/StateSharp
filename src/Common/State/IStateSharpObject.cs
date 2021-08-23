using StateSharp.Common.Event;
using System;

namespace StateSharp.Common.State
{
    public interface IStateSharpObject<T> : IStateSharpObjectBase
    {
        T State { get; }
        void SubscribeOnChange(Action<IStateSharpEvent> handler);
        void UnsubscribeOnChange(Action<IStateSharpEvent> handler);
    }
}