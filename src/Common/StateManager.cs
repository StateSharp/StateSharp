using System;
using System.Collections.Generic;
using StateSharp.Core.Collections;
using StateSharp.Core.Events;
using StateSharp.Core.States;
using StateSharp.Core.Transactions;
using StateSharp.Core.Validators;

namespace StateSharp.Core
{
    public class StateManager<T> : IStateManager<T>, IStateEventManager where T : class
    {
        private readonly PathTree<Action<IStateEvent>> _handlers;
        private readonly StateObject<T> _state;

        public StateManager()
        {
            _handlers = new PathTree<Action<IStateEvent>>();
            _state = new StateObject<T>(this, Path);
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

        public string Path => "State";
        public T State => _state.State;

        public T Init()
        {
            return _state.Init();
        }

        public IStateTransaction<IStateObject<T>> BeginTransaction()
        {
            return _state.BeginTransaction();
        }

        public void Commit(IStateTransaction<IStateObject<T>> transaction)
        {
            _state.Commit(transaction);
        }

        public void Validate()
        {
            ObjectValidator.Validate<StateObject<T>>();
        }

        public void SubscribeOnChange(Action<IStateEvent> handler)
        {
            Subscribe(Path, handler);
        }

        public void UnsubscribeOnChange(Action<IStateEvent> handler)
        {
            Unsubscribe(Path, handler);
        }

        void IStateBase.SetEventManager(IStateEventManager eventManager)
        {
            ((IStateBase) _state).SetEventManager(eventManager);
        }

        IReadOnlyList<IStateBase> IStateBase.GetChildren()
        {
            return ((IStateBase) _state).GetChildren();
        }

        IStateObject<T> IStateObject<T>.Copy(IStateEventManager eventManager)
        {
            return ((IStateObject<T>) _state).Copy(eventManager);
        }

        IStateBase IStateBase.Copy(IStateEventManager eventManager)
        {
            return ((IStateObject<T>) _state).Copy(eventManager);
        }

        object IStateObjectBase.GetState()
        {
            return ((IStateObjectBase) _state).GetState();
        }
    }
}