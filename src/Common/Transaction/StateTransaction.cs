using System;
using System.Collections.Generic;

namespace StateSharp.Core.Transaction
{
    internal class StateTransaction : IStateTransaction
    {
        private readonly List<Tuple<string, Action>> _actions;

        public StateTransaction()
        {
            _actions = new List<Tuple<string, Action>>();
        }

        void IStateTransaction.Add(string path, Action action)
        {
            _actions.Add(new Tuple<string, Action>(path, action));
        }
    }
}