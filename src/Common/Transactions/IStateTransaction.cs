using System;

namespace StateSharp.Core.Transactions
{
    public interface IStateTransaction
    {
        internal void Add(string path, Action action);
    }
}