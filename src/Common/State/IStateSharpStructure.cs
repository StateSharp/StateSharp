using StateSharp.Common.Transaction;

namespace StateSharp.Common.State
{
    public interface IStateSharpStructure<T> : IStateSharpStructureBase where T : struct
    {
        void Set(T state);
        void Set(IStateSharpTransaction transaction, T state);
    }
}