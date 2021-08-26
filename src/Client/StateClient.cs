using StateSharp.Core;

namespace StateSharp.Client
{
    internal class StateClient<T> : IStateClient<T>
    {
        private readonly IStateManager<T> _state;

        public T State => _state.State;

        public StateClient()
        {
            _state = StateManagerConstructor.New<T>();
        }
    }
}