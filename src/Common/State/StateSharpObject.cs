using System;

namespace StateSharp.Common.State
{
    internal sealed class StateSharpObject<T> : IStateSharpObject<T>
    {
        public string Path { get; }
        public T State { get; }

        public StateSharpObject(string path)
        {
            Path = path;
            State = StateSharpConstructor.Construct<T>(Path);
        }

        public void SubscribeOnChange(Action<T> handler)
        {

        }

        public void UnsubscribeOnChange(Action<T> handler)
        {

        }
    }
}