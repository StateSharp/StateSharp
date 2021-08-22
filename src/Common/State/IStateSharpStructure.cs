using System;

namespace StateSharp.Common.State
{
    public interface IStateSharpStructure<T> : IStateSharpStructureBase
    {
        void SubscribeOnChange(Action<T> handler);
        void UnsubscribeOnChange(Action<T> handler);
    }
}