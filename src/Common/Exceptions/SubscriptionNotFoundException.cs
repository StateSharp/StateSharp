using System;

namespace StateSharp.Core.Exceptions
{
    public class SubscriptionNotFoundException : Exception
    {
        public SubscriptionNotFoundException(string message) : base(message)
        { }
    }
}