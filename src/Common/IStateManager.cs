using StateSharp.Core.Transaction;

namespace StateSharp.Core
{
    public interface IStateManager<out T>
    {
        T State { get; }

        IStateTransaction BeginTransaction();
        void Commit(IStateTransaction transaction);
    }
}