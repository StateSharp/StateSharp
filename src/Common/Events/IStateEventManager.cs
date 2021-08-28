using System;

namespace StateSharp.Core.Events
{
    internal interface IStateEventManager
    {
        void Invoke(string path);
        void Subscribe(string path, Action<IStateEvent> handler);
        void Unsubscribe(string path, Action<IStateEvent> handler);
    }
}