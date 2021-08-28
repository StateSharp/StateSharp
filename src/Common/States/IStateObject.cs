using StateSharp.Core.Transactions;

namespace StateSharp.Core.States
{
    public interface IStateObject<T> : IStateObjectBase where T : class
    {
        T State { get; }

        T Set();

        IStateTransaction<IStateObject<T>> BeginTransaction();
        void Commit(IStateTransaction<IStateObject<T>> transaction);
    }
}