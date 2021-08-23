using StateSharp.Common;

namespace StateSharp.Client
{
    internal class StateSharpClient<T> : IStateSharpClient<T>
    {
        private readonly IStateSharpManager<T> _state;

        public T State => _state.State;

        public StateSharpClient()
        {
            _state = StateSharpManagerConstructor.New<T>();
        }
    }
}