using StateSharp.Core.Events;
using StateSharp.Core.States;
using System;
using System.Collections.Generic;

namespace StateSharp.Json.Deserializers
{
    internal static class StateStringDeserializer
    {
        public static IStateStringBase Deserialize(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            if (tokens.Peek() == 'n')
            {
                CommonDeserializer.ReadNull(type, tokens);
                return new StateString(eventManager, path);
            }

            var value = CommonDeserializer.ReadString(typeof(string), tokens);
            return new StateString(eventManager, path, value);
        }
    }
}
