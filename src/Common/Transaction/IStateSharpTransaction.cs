using System;

namespace StateSharp.Common.Transaction
{
    public interface IStateSharpTransaction
    {
        internal void Add(string path, Action action);
    }
}