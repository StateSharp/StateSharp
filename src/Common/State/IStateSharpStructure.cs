using System;

namespace StateSharp.Common.State
{
    public interface IStateSharpStructure<T> : IStateSharpStructureBase where T : struct
    {
        void SubscribeOnChange(Action<T> handler);
        void UnsubscribeOnChange(Action<T> handler);
    }
}