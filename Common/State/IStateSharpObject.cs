using System;

namespace StateSharp.Common.State
{
    public interface IStateSharpObject<T>
    {
        T State { get; }
        void SubscribeOnChange(Action<T> handler);
        void UnsubscribeOnChange(Action<T> handler);
    }
}