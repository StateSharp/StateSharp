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
        private static readonly Dictionary<Type, Func<Type, IStateEventManager, string, Queue<char>, object>> Deserializers = new()
        {
            { typeof(IStateDictionaryBase), Deserialize },
            { typeof(IStateObjectBase), StateObjectDeserializer.Deserialize },
            { typeof(IStateStringBase), StateStringDeserializer.Deserialize },
            { typeof(IStateStructureBase), StateStructureDeserializer.Deserialize },
        };

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

            var deserializer = Deserializers.Single(x => stateType.GetInterfaces().Contains(x.Key)).Value;
            var addMethod = state.GetType().GetMethod("Add");

            while (true)
            {
                var name = CommonDeserializer.ReadName(type, tokens);
                if (tokens.Dequeue() != ':')
                {
                    throw new DeserializationException($"Could not serialize json for {type.FullName}");
                }

                var value = deserializer(stateType, eventManager, $"{path}[{name}]", tokens);
                addMethod.Invoke(state, new[] { name, value });

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
