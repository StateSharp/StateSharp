using System;

namespace StateSharp.Core.Exceptions
{
    public class UnknownStateTypeException : Exception
    {
        public UnknownStateTypeException(string message) : base(message)
        { }
    }
}
