using System;

namespace StateSharp.Common.State
{
    public interface IStateSharpList<T>
    {
        void Add(T value);
        void Remove(T value);

        void SubscribeOnChange(Action<T> handler);
        void UnsubscribeOnChange(Action<T> handler);
    }
}