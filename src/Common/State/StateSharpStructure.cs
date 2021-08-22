using System;

namespace StateSharp.Common.State
{
    public class StateSharpStructure<T> : IStateSharpStructure<T> where T : struct
    {
        private readonly T _state;

        public string Path { get; }
        public T State => _state;

        public StateSharpStructure(string path)
        {
            Path = path;
            //_state = default;
        }

        public void SubscribeOnChange(Action<T> handler)
        {

        }

        public void UnsubscribeOnChange(Action<T> handler)
        {

        }
    }
}