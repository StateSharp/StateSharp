using StateSharp.Core.Events;
using StateSharp.Core.States;
using StateSharp.Json.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StateSharp.Json.Deserializers
{
    internal static class StateObjectDeserializer
    {
        public static IStateObjectBase Deserialize(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var stateType = type.GenericTypeArguments.Single();

            if (tokens.Peek() == 'n')
            {
                CommonDeserializer.ReadNull(type, tokens);
                return (IStateObjectBase)Activator.CreateInstance(typeof(StateObject<>).MakeGenericType(stateType), eventManager, path);
            }

            var state = Activator.CreateInstance(stateType);

            if (tokens.Dequeue() != '{')
            {
                throw new DeserializationException($"Could not serialize json for {type.FullName}");
            }

            while (true)
            {
                var name = CommonDeserializer.ReadName(type, tokens);
                if (tokens.Dequeue() != ':')
                {
                    throw new DeserializationException($"Could not serialize json for {type.FullName}");
                }

                var property = stateType.GetProperty(name);
                var value = StateJsonConverter.Deserialize(property.PropertyType, eventManager, $"{path}.{name}", tokens);
                property.SetValue(state, value);

                if (tokens.Peek() == ',')
                {
                    tokens.Dequeue();
                    continue;
                }

                if (tokens.Peek() == '}')
                {
                    tokens.Dequeue();
                    break;
                }

                throw new DeserializationException($"Could not serialize json for {type.FullName}");
            }

            return (IStateObjectBase)Activator.CreateInstance(typeof(StateObject<>).MakeGenericType(stateType), eventManager, path, state);
        }
    }
}
