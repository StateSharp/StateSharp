using StateSharp.Core.States;

namespace StateSharp.Core.Transactions
{
    public interface IStateTransaction<out T> where T : IStateBase
    {
        public T State { get; }
        internal IStateBase Owner { get; }
    }
}