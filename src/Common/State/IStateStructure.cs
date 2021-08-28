using StateSharp.Core.Transaction;

namespace StateSharp.Core.State
{
    public interface IStateStructure<T> : IStateStructureBase where T : struct
    {
        T State { get; }

        T Set();
        void Set(T state);
        void Set(IStateTransaction transaction, T state);
    }
}