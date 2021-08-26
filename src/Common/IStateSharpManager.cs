using StateSharp.Common.Transaction;

namespace StateSharp.Common
{
    public interface IStateSharpManager<out T>
    {
        T State { get; }

        IStateSharpTransaction BeginTransaction();
        void Commit(IStateSharpTransaction transaction);
    }
}