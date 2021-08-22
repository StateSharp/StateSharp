using System;

namespace StateSharp.Common.State
{
    public interface IStateSharpStruct<T>
    {
        void SubscribeOnChange(Action<T> handler);
        void UnsubscribeOnChange(Action<T> handler);
    }
}