using StateSharp.Common.Event;
using System;

namespace StateSharp.Common.State
{
    public interface IStateSharpStructure<T> : IStateSharpStructureBase where T : struct
    {
        void Set(T state);
        void SubscribeOnChange(Action<IStateSharpEvent> handler);
        void UnsubscribeOnChange(Action<IStateSharpEvent> handler);
    }
}