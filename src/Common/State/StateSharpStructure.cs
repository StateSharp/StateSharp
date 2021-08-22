using System;
using System.Collections.Generic;

namespace StateSharp.Common.State
{
    public class StateSharpStructure<T> : IStateSharpStructure<T> where T : struct
    {
        private readonly IStateSharpBase _parent;
        private readonly string _field;
        private readonly T _state;

        IStateSharpBase IStateSharpBase.Parent => _parent;
        string IStateSharpBase.Key => _field;
        StateSharpType IStateSharpBase.Type => StateSharpType.Object;

        public T State => _state;

        public StateSharpStructure(IStateSharpBase parent, string field)
        {
            _parent = parent;
            _field = field;
            _state = default;
        }

        public void SubscribeOnChange(Action<T> handler)
        {

        }

        public void UnsubscribeOnChange(Action<T> handler)
        {

        }

        string IStateSharpBase.GetPath(List<IStateSharpBase> callers)
        {
            callers.Add(this);
            return _parent.GetPath(callers);
        }

        public string GetPath()
        {
            return ((IStateSharpBase)this).GetPath(new List<IStateSharpBase>());
        }
    }
}