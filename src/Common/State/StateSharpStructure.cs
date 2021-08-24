using StateSharp.Common.Event;
using System;

namespace StateSharp.Common.State
{
    internal class StateSharpStructure<T> : IStateSharpStructure<T> where T : struct
    {
        private readonly IStateSharpEventManager _eventManager;

        public string Path { get; }
        public T State { get; private set; }

        public StateSharpStructure(IStateSharpEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            State = default;
        }

        public void Set(T state)
        {
            var e = new StateSharpEvent(Path, State, state);
            State = state;
            _eventManager.Invoke(Path, e);
        }

        public void SubscribeOnChange(Action<IStateSharpEvent> handler)
        {
            _eventManager.Subscribe(Path, handler);
        }

        public void UnsubscribeOnChange(Action<IStateSharpEvent> handler)
        {
            _eventManager.Unsubscribe(Path, handler);
        }
    }
}