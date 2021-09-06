﻿using System;
using System.Collections.Generic;
using StateSharp.Core.Constructors;
using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.Services;
using StateSharp.Core.Transactions;

namespace StateSharp.Core.States
{
    internal sealed class StateObject<T> : IStateObject<T> where T : class
    {
        private IStateEventManager _eventManager;

        public StateObject(IStateEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            State = null;
        }

        public StateObject(IStateEventManager eventManager, string path, T state)
        {
            Path = path;
            _eventManager = eventManager;
            State = state;
        }

        public string Path { get; }

        public T State { get; private set; }

        public T Init()
        {
            State = StateConstructor.ConstructState<T>(_eventManager, Path);
            _eventManager.Invoke(Path);
            return State;
        }

        public IStateTransaction<IStateObject<T>> BeginTransaction()
        {
            return new StateTransaction<IStateObject<T>>(this, em => ((IStateObject<T>) this).Copy(em));
        }

        public void Commit(IStateTransaction<IStateObject<T>> transaction)
        {
            if (transaction.Owner != this)
            {
                throw new UnknownTransactionException("Object is not the owner of transaction");
            }

            State = transaction.State.State;

            var queue = new Queue<IStateBase>(((IStateBase) this).GetChildren());
            while (queue.TryDequeue(out var state))
            {
                state.SetEventManager(_eventManager);
                foreach (var child in state.GetChildren())
                {
                    queue.Enqueue(child);
                }
            }

            _eventManager.Invoke(Path);
        }

        public void SubscribeOnChange(Action<IStateEvent> handler)
        {
            _eventManager.Subscribe(Path, handler);
        }

        public void UnsubscribeOnChange(Action<IStateEvent> handler)
        {
            _eventManager.Unsubscribe(Path, handler);
        }

        IStateEventManager IStateBase.GetEventManager()
        {
            return _eventManager;
        }

        void IStateBase.SetEventManager(IStateEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        IReadOnlyList<IStateBase> IStateBase.GetChildren()
        {
            if (State == null)
            {
                return new List<IStateBase>();
            }

            var result = new List<IStateBase>();
            foreach (var property in typeof(T).GetProperties())
            {
                var value = (IStateBase) property.GetValue(State) ?? throw new NullReferenceException();
                result.Add(value);
            }

            return result;
        }

        object IStateObjectBase.GetState()
        {
            return State;
        }

        IStateObject<T> IStateObject<T>.Copy(IStateEventManager eventManager)
        {
            return new StateObject<T>(eventManager, Path, CopyService.CopyState(State, eventManager));
        }

        IStateBase IStateBase.Copy(IStateEventManager eventManager)
        {
            return ((IStateObject<T>) this).Copy(eventManager);
        }
    }
}