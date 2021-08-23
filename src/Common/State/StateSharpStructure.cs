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

        public void SubscribeOnChange(Action<IStateSharpEvent> handler)
        {

        }

        public void UnsubscribeOnChange(Action<IStateSharpEvent> handler)
        {

        }
    }
}