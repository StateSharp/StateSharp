using StateSharp.Core.Transactions;
using System.Collections.Generic;

namespace StateSharp.Core.States
{
    public interface IStateDictionary<T> : IStateDictionaryBase where T : IStateBase
    {
        IReadOnlyDictionary<string, T> State { get; }

        IReadOnlyDictionary<string, T> Set();
        T Add(string key);
        void Remove(string key);

        IStateTransaction<IStateDictionary<T>> BeginTransaction();
        void Commit(IStateTransaction<IStateDictionary<T>> transaction);
        IStateTransaction<T> BeginAddTransaction();
        void Commit(IStateTransaction<T> transaction);

        internal new IStateDictionary<T> Copy();
    }
}