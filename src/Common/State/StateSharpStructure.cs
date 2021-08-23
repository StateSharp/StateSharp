using System;

namespace StateSharp.Common.State
{
    public class StateSharpStructure<T> : IStateSharpStructure<T> where T : struct
    {
        public string Path { get; }
        public T State { get; private set; }

        public StateSharpStructure(string path)
        {
            Path = path;
            State = default;
        }

        public void SubscribeOnChange(Action<T> handler)
        {

        }

        public void UnsubscribeOnChange(Action<T> handler)
        {

        }
    }
}