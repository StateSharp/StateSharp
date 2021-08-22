using System;

namespace StateSharp.Common.State
{
    public sealed class StateSharpObject<T> : StateSharpBase, IStateSharpObject<T>
    {
        public T State { get; private set; }

        public StateSharpObject(StateSharpBase parent) : base(parent)
        { }

        public void Set(T value)
        {
            State = value;
        }

        public void SubscribeOnChange(Action<T> handler)
        {

        }

        public void UnsubscribeOnChange(Action<T> handler)
        {

        }
    }
}