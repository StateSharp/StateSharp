﻿using StateSharp.Core.Event;
using StateSharp.Core.State;
using StateSharp.Core.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StateSharp.Core
{
    public class StateManager<T> : IStateManager<T>, IStateEventManager where T : class
    {
        private readonly Dictionary<string, List<Action<IStateEvent>>> _handlers;
        private readonly StateObject<T> _state;

        public string Path => "State";
        public T State => _state.State;

        public StateManager()
        {
            _handlers = new Dictionary<string, List<Action<IStateEvent>>>();
            _state = new StateObject<T>(this, Path);
            _state.Set();
        }

        public IStateTransaction BeginTransaction()
        {
            return new StateTransaction();
        }

        public void Commit(IStateTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void Invoke(string path)
        {
            var matches = _handlers
                .Where(x => x.Key.StartsWith(path))
                .Select(x => x.Value)
                .SelectMany(x => x)
                .ToList();
            var splits = path.Split('.');
            for (var i = 2; i <= splits.Length; i++)
            {
                var p = string.Join('.', splits, 0, i - 1);
                if (splits[i - 1].EndsWith("]"))
                {
                    if (_handlers.TryGetValue($"{p}.{splits[i - 1].Split('[')[0]}", out var dictHandlers))
                    {
                        matches.AddRange(dictHandlers);
                    }
                    if (_handlers.TryGetValue($"{p}.{splits[i - 1]}", out var elemHandlers))
                    {
                        matches.AddRange(elemHandlers);
                    }
                }
                else
                {
                    if (_handlers.TryGetValue($"{p}.{splits[i - 1]}", out var handlers))
                    {
                        matches.AddRange(handlers);
                    }
                }
            }
            if (path != "State")
            {

                if (_handlers.TryGetValue("State", out var handlers))
                {
                    matches.AddRange(handlers);
                }
            }
            foreach (var handler in matches)
            {
                handler(new StateEvent(path));
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