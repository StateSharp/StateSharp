using StateSharp.Common.Transaction;
using System.Collections.Generic;

namespace StateSharp.Common.State
{
    public interface IStateSharpDictionary<T> : IStateSharpDictionaryBase where T : IStateSharpBase
    {
        IReadOnlyDictionary<string, T> State { get; }

        T Add(string key);
        T Add(IStateSharpTransaction transaction, string key);
        void Remove(string key);
        void Remove(IStateSharpTransaction transaction, string key);
    }
}