using System;
using System.Collections.Generic;
using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.States;

namespace StateSharp.Core.Transactions
{
    internal class StateTransaction<T> : IStateTransaction<T>, IStateEventManager where T : IStateBase
    {
        private readonly IDictionary<string, List<Action<IStateEvent>>> _handlers;
        private readonly IStateBase _owner;

        public StateTransaction(IStateBase owner, Func<IStateEventManager, T> constructor)
        {
            _owner = owner;
            _handlers = new Dictionary<string, List<Action<IStateEvent>>>();
            State = constructor(this);
        }

        public void Invoke(string path)
        {
            // Do not invoke inside transaction.
        }

        public void Subscribe(string path, Action<IStateEvent> handler)
        {
            if (_handlers.TryGetValue(path, out var handlers))
            {
                handlers.Add(handler);
            }
            else
            {
                _handlers.Add(path, new List<Action<IStateEvent>>
                {
                    handler
                });
            }
        }

        public void Unsubscribe(string path, Action<IStateEvent> handler)
        {
            if (_handlers.TryGetValue(path, out var handlers))
            {
                if (handlers.Remove(handler))
                {
                    return;
                }

                throw new SubscriptionNotFoundException($"Subscription not found for {handler}");
            }

            throw new SubscriptionNotFoundException($"Subscription not found for {path}");
        }

        public T State { get; }
        IStateBase IStateTransaction<T>.Owner => _owner;
    }
}
