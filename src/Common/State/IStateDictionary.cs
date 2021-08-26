using System.Collections.Generic;
using StateSharp.Core.Transaction;

namespace StateSharp.Core.State
{
    public interface IStateDictionary<T> : IStateDictionaryBase where T : IStateBase
    {
        IReadOnlyDictionary<string, T> State { get; }

        T Add(string key);
        T Add(IStateTransaction transaction, string key);
        void Remove(string key);
        void Remove(IStateTransaction transaction, string key);
    }
}