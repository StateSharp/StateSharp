using StateSharp.Core.Events;
using StateSharp.Core.States;
using StateSharp.Json.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StateSharp.Json.Deserializers
{
    internal static class StateDictionaryDeserializer
    {
        public static IStateDictionaryBase Deserialize(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var stateType = type.GenericTypeArguments.Single();

            if (tokens.Peek() == 'n')
            {
                CommonDeserializer.ReadNull(type, tokens);
                return (IStateDictionaryBase)Activator.CreateInstance(typeof(StateDictionary<>).MakeGenericType(stateType), eventManager, path);
            }

            var state = Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(typeof(string), stateType));

            if (tokens.Dequeue() != '{')
            {
                throw new DeserializationException($"Could not serialize json for {type.FullName}");
            }

            while (true)
            {
                var field = CommonDeserializer.ReadString(type, tokens);
                if (tokens.Dequeue() != ':')
                {
                    throw new DeserializationException($"Could not serialize json for {type.FullName}");
                }

                var value = StateJsonConverter.Deserialize(type.GenericTypeArguments.Single(), eventManager, $"{path}[{field}]", tokens);
                state.GetType().GetMethod("Add").Invoke(state, new[] { field, value });

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

            return (IStateDictionaryBase)Activator.CreateInstance(typeof(StateDictionary<>).MakeGenericType(stateType), eventManager, path, state);
        }
    }
}
