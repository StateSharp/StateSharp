using System;
using System.Collections.Generic;

namespace StateSharp.Common.State
{
    public interface IStateSharpDictionary<T> : IStateSharpDictionaryBase
    {
        IReadOnlyDictionary<string, T> State { get; }

        T Add(string key);
        void Remove(string key);

        void SubscribeOnChange(Action<T> handler);
        void UnsubscribeOnChange(Action<T> handler);
    }
}