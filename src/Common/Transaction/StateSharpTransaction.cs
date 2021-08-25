using System;

namespace StateSharp.Common.Transaction
{
    internal class StateSharpTransaction : IStateSharpTransaction
    {
        void IStateSharpTransaction.Add(string path, Action action)
        {
            throw new System.NotImplementedException();
        }
    }
}