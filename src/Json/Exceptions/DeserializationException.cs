using System;

namespace StateSharp.Json.Exceptions
{
    public class DeserializationException : Exception
    {
        public DeserializationException(string message) : base(message)
        { }
    }
}
