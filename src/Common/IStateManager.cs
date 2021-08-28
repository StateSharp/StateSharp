using StateSharp.Core.States;
using StateSharp.Core.Transactions;

namespace StateSharp.Core
{
    public interface IStateManager<T> where T : class
    {
        string Path { get; }
        T State { get; }

        IStateTransaction<IStateObject<T>> BeginTransaction();
        void Commit(IStateTransaction<IStateObject<T>> transaction);
    }
}