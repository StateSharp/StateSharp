using System;
using System.Collections.Generic;

namespace StateSharp.Common.State
{
    public interface IStateSharpDictionary<T>
    {
        IReadOnlyDictionary<string, T> State { get; }

        void Add(string key, T value);
        void Remove(string key);

        void SubscribeOnChange(Action<T> handler);
        void UnsubscribeOnChange(Action<T> handler);
    }
}