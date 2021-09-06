using System.Collections.Generic;
using StateSharp.Core.Events;
using StateSharp.Core.Transactions;

namespace StateSharp.Core.States
{
    public interface IStateDictionary<T> : IStateDictionaryBase where T : IStateBase
    {
        IReadOnlyDictionary<string, T> State { get; }

        IReadOnlyDictionary<string, T> Init();
        T Add(string key);
        void Remove(string key);
        void Clear();

        IStateTransaction<IStateDictionary<T>> BeginTransaction();
        void Commit(IStateTransaction<IStateDictionary<T>> transaction);

        internal Dictionary<string, T> GetStateRef();
        internal new IStateDictionary<T> Copy(IStateEventManager eventManager);
    }
}
