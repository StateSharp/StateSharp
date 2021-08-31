using StateSharp.Core;

namespace StateSharp.Client
{
    internal class StateClient<T> : IStateClient<T> where T : class
    {
        private readonly IStateManager<T> _state;

        public StateClient()
        {
            _state = StateManagerConstructor.New<T>();
        }

        public T State => _state.State;
    }
}