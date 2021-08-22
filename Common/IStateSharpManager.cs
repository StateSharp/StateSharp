using System;

namespace StateSharp.Common
{
    public interface IStateSharpManager
    {
        void Subscribe(string filter, Action<object> handler);
        void Unsubscribe(string filter, Action<object> handler);
    }
}