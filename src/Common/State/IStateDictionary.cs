using StateSharp.Core.Transaction;
using System.Collections.Generic;

namespace StateSharp.Core.State
{
    public interface IStateDictionary<T> : IStateDictionaryBase where T : IStateBase
    {
        IReadOnlyDictionary<string, T> State { get; }

        IReadOnlyDictionary<string, T> Set();
        T Add(string key);
        T Add(IStateTransaction transaction, string key);
        void Remove(string key);
        void Remove(IStateTransaction transaction, string key);
    }
}