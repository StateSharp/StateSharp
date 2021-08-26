using System;

namespace StateSharp.Core.Transaction
{
    public interface IStateTransaction
    {
        internal void Add(string path, Action action);
    }
}