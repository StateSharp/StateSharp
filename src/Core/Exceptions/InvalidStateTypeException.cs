using System;

namespace StateSharp.Core.Exceptions
{
    public class InvalidStateTypeException : Exception
    {
        public InvalidStateTypeException(string message) : base(message)
        { }
    }
}
