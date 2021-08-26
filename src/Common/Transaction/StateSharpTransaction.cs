using System;
using System.Collections.Generic;

namespace StateSharp.Common.Transaction
{
    internal class StateSharpTransaction : IStateSharpTransaction
    {
        private readonly List<Tuple<string, Action>> _actions;

        public StateSharpTransaction()
        {
            _actions = new List<Tuple<string, Action>>();
        }

        void IStateSharpTransaction.Add(string path, Action action)
        {
            _actions.Add(new Tuple<string, Action>(path, action));
        }
    }
}