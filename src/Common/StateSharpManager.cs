using StateSharp.Common.Event;
using StateSharp.Common.State;
using StateSharp.Common.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StateSharp.Common
{
    public class StateSharpManager<T> : IStateSharpManager<T>, IStateSharpEventManager
    {
        private readonly Dictionary<string, List<Action<IStateSharpEvent>>> _handlers;
        private readonly StateSharpObject<T> _state;

        public T State => _state.State;

        public StateSharpManager()
        {
            _handlers = new Dictionary<string, List<Action<IStateSharpEvent>>>();
            _state = new StateSharpObject<T>(this, "State");
        }

        public IStateSharpTransaction BeginTransaction()
        {
            return new StateSharpTransaction();
        }

        public void Commit(IStateSharpTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void Invoke(string path, IStateSharpEvent param)
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

        public void Subscribe(string path, Action<IStateSharpEvent> handler)
        {
            if (_handlers.TryGetValue(path, out var handlers))
            {
                handlers.Add(handler);
            }
            else
            {
                _handlers.Add(path, new List<Action<IStateSharpEvent>>
                {
                    handler,
                });
            }
        }

        public void Unsubscribe(string path, Action<IStateSharpEvent> handler)
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