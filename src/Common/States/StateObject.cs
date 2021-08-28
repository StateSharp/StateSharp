using System;
using StateSharp.Core.Events;
using StateSharp.Core.Transactions;

namespace StateSharp.Core.States
{
    internal sealed class StateObject<T> : IStateObject<T> where T : class
    {
        private readonly IStateEventManager _eventManager;

        public string Path { get; }
        public T State { get; private set; }

        public StateObject(IStateEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            State = default;
        }

        public T Set()
        {
            State = StateConstructor.ConstructState<T>(_eventManager, Path);
            return State;
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