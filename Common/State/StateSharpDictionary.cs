using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StateSharp.Common.State
{
    public class StateSharpDictionary<T> : StateSharpBase, IStateSharpDictionary<T> where T : Dictionary<string, T>
    {
        private readonly Dictionary<string, T> _state;

        public IReadOnlyDictionary<string, T> State => new ReadOnlyDictionary<string, T>(_state);

        internal StateSharpDictionary(StateSharpBase parent) : base(parent)
        {
            _state = new Dictionary<string, T>();
        }

        public void Add(string key, T value)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void SubscribeOnChange(Action<T> handler)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeOnChange(Action<T> handler)
        {
            throw new NotImplementedException();
        }

    }
}