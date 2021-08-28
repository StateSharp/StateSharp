using StateSharp.Core.Collections;
using StateSharp.Core.Events;
using StateSharp.Core.States;
using StateSharp.Core.Transactions;
using System;

namespace StateSharp.Core
{
    public class StateManager<T> : IStateManager<T>, IStateEventManager where T : class
    {
        private readonly PathTree<Action<IStateEvent>> _handlers;
        private readonly StateObject<T> _state;

        public string Path => "State";
        public T State => _state.State;

        public StateManager()
        {
            _handlers = new PathTree<Action<IStateEvent>>();
            _state = new StateObject<T>(this, Path);
            _state.Set();
        }

        public void Invoke(string path)
        {
            var param = new StateEvent(path);
            foreach (var handler in _handlers.GetMatches(path))
            {
                handler(param);
            }
        }

        public void Subscribe(string path, Action<IStateEvent> handler)
        {
            _handlers.Add(path, handler);
        }

        public void Unsubscribe(string path, Action<IStateEvent> handler)
        {
            _handlers.Remove(path, handler);
        }

        public IStateTransaction<IStateObject<T>> BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void Commit(IStateTransaction<IStateObject<T>> transaction)
        {
            throw new NotImplementedException();
        }
    }
}