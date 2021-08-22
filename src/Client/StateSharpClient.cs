using StateSharp.Common;
using StateSharp.Common.State;
using System;

namespace StateSharp.Client
{
    internal class StateSharpClient<T> : IStateSharpManager, IStateSharpClient<T>
    {
        public T State => _state.State;
        private readonly StateSharpObject<T> _state;

        public StateSharpClient()
        {
            _state = new StateSharpObject<T>(null, null);
        }

        public void Subscribe(string path, Action<object> handler)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(string path, Action<object> handler)
        {
            throw new NotImplementedException();
        }
    }
}