using System;
using System.Collections.Generic;
using System.Linq;
using StateSharp.Core.Event;
using StateSharp.Core.State;
using StateSharp.Core.Transaction;

namespace StateSharp.Core
{
    public class StateManager<T> : IStateManager<T>, IStateEventManager
    {
        private readonly Dictionary<string, List<Action<IStateEvent>>> _handlers;
        private readonly StateObject<T> _state;

        public T State => _state.State;

        public StateManager()
        {
            _handlers = new Dictionary<string, List<Action<IStateEvent>>>();
            _state = new StateObject<T>(this, "State");
        }

        public IStateTransaction BeginTransaction()
        {
            return new StateTransaction();
        }

        public void Commit(IStateTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void Invoke(string path, IStateEvent param)
        {
            var matches = _handlers
                .Where(x => x.Key.StartsWith(path))
                .Select(x => x.Value)
                .SelectMany(x => x)
                .ToList();
            var splits = path.Split('.');
            for (var i = 1; i < splits.Length - 1; i++)
            {
                var p = string.Join('.', splits, 0, i);
                if (_handlers.TryGetValue(p, out var handlers))
                {
                    matches.AddRange(handlers);
                }
            }
            foreach (var handler in matches)
            {
                handler(param);
            }
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
                    handler,
                });
            }
        }

        public void Unsubscribe(string path, Action<IStateEvent> handler)
        {
            if (_handlers.TryGetValue(path, out var handlers))
            {
                if (handlers.Remove(handler))
                {
                    throw new NullReferenceException("Subscription not found");
                }
            }
            throw new KeyNotFoundException(path);
        }
    }
}