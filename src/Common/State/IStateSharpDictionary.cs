using StateSharp.Common.Event;
using System;
using System.Collections.Generic;

namespace StateSharp.Common.State
{
    public interface IStateSharpDictionary<T> : IStateSharpDictionaryBase where T : IStateSharpBase
    {
        IReadOnlyDictionary<string, T> State { get; }

        T Add(string key);
        void Remove(string key);

        void SubscribeOnChange(Action<IStateSharpEvent> handler);
        void UnsubscribeOnChange(Action<IStateSharpEvent> handler);
    }
}