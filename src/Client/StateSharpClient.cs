using StateSharp.Common;
using StateSharp.Common.State;
using System;

namespace StateSharp.Client
{
    public class StateSharpClient<T> : IStateSharpManager, IStateSharpClient<T>
    {
        public T State => _state.State;
        private readonly StateSharpObject<T> _state;

        public StateSharpClient()
        {
            _state = new StateSharpObject<T>(null);
        }

        public void Set(T value)
        {
            throw new NotImplementedException();
        }

        public void SubscribeOnChange(Action<T> handler)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeOnChange(Action<T> handler)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(string filter, Action<object> handler)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(string filter, Action<object> handler)
        {
            throw new NotImplementedException();
        }
    }
}