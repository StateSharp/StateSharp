using StateSharp.Core.Transaction;

namespace StateSharp.Core
{
    public interface IStateManager<out T> where T : class
    {
        string Path { get; }
        T State { get; }

        IStateTransaction BeginTransaction();
        void Commit(IStateTransaction transaction);
    }
}