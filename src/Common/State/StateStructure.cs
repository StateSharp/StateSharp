using System;
using StateSharp.Core.Event;
using StateSharp.Core.Transaction;

namespace StateSharp.Core.State
{
    internal class StateStructure<T> : IStateStructure<T> where T : struct
    {
        private readonly IStateEventManager _eventManager;

        public string Path { get; }

        public T State { get; private set; }

        public StateStructure(IStateEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            State = default;
        }

        public void Set(T state)
        {
            var e = new StateEvent(Path, State, state);
            State = state;
            _eventManager.Invoke(Path, e);
        }

        public void Set(IStateTransaction transaction, T state)
        {
            throw new NotImplementedException();
        }

        public IStateTransaction BeginTransaction()
        {
            return _eventManager.BeginTransaction();
        }

        public void Commit(IStateTransaction transaction)
        {
            _eventManager.Commit(transaction);
        }

        public void SubscribeOnChange(Action<IStateEvent> handler)
        {
            _eventManager.Subscribe(Path, handler);
        }

        public void UnsubscribeOnChange(Action<IStateEvent> handler)
        {
            _eventManager.Unsubscribe(Path, handler);
        }
    }
}