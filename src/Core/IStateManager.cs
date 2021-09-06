using StateSharp.Core.States;

namespace StateSharp.Core
{
    public interface IStateManager<T> : IStateObject<T> where T : class
    {
        void Validate();
    }
}
