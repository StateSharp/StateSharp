using StateSharp.Core.Events;
using StateSharp.Core.Transactions;

namespace StateSharp.Core.States
{
    public interface IStateObject<T> : IStateObjectBase where T : class
    {
        T State { get; }

        T Init();

        IStateTransaction<IStateObject<T>> BeginTransaction();
        void Commit(IStateTransaction<IStateObject<T>> transaction);

        internal new IStateObject<T> Copy(IStateEventManager eventManager);
    }
}