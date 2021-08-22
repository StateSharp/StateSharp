using System;

namespace StateSharp.Common
{
    public interface IStateSharpManager
    {
        void Subscribe(string path, Action<object> handler);
        void Unsubscribe(string path, Action<object> handler);
    }
}