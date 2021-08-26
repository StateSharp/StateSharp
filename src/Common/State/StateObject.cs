using System;
using StateSharp.Core.Event;
using StateSharp.Core.Transaction;

namespace StateSharp.Core.State
{
    internal sealed class StateObject<T> : IStateObject<T>
    {
        private readonly IStateEventManager _eventManager;

        public string Path { get; }
        public T State { get; }

        public StateObject(IStateEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            State = StateConstructor.ConstructState<T>(eventManager, Path);
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