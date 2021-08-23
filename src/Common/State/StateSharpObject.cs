﻿using StateSharp.Common.Event;
using System;

namespace StateSharp.Common.State
{
    internal sealed class StateSharpObject<T> : IStateSharpObject<T>
    {
        private readonly IStateSharpEventManager _eventManager;

        public string Path { get; }
        public T State { get; }

        public StateSharpObject(IStateSharpEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            State = StateSharpConstructor.Construct<T>(eventManager, Path);
        }

        public void SubscribeOnChange(Action<IStateSharpEvent> handler)
        {

        }

        public void UnsubscribeOnChange(Action<IStateSharpEvent> handler)
        {

        }
    }
}