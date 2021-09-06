using System;

namespace StateSharp.Core.Exceptions
{
    public class UnknownTransactionException : Exception
    {
        public UnknownTransactionException(string message) : base(message)
        { }
    }
}
