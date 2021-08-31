using StateSharp.Core.Events;
using StateSharp.Core.Transactions;

namespace StateSharp.Core.States
{
    public interface IStateString : IStateStringBase
    {
        string State { get; }

        string Init();

        IStateTransaction<IStateString> BeginTransaction();
        void Commit(IStateTransaction<IStateString> transaction);

        internal new IStateString Copy(IStateEventManager eventManager);
    }
}