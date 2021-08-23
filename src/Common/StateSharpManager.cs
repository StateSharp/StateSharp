using StateSharp.Common.Event;
using StateSharp.Common.State;
using System;
using System.Collections.Generic;

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

        public void Invoke(string path, IStateSharpEvent param)
        {

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