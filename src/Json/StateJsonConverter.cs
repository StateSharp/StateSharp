using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.States;
using StateSharp.Json.Deserializers;
using StateSharp.Json.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StateSharp.Json
{
    public static class StateJsonConverter
    {
        public static string Serialize(IStateDictionaryBase state)
        {
            return StateDictionarySerializer.Serialize(state);
        }
        public static string Serialize(IStateObjectBase state)
        {
            return StateObjectSerializer.Serialize(state);
        }
        public static string Serialize(IStateStringBase state)
        {
            return StateStringSerializer.Serialize(state);
        }
        public static string Serialize(IStateStructureBase state)
        {
            return StateStructureSerializer.Serialize(state);
        }

        internal static T Deserialize<T>(IStateEventManager eventManager, string path, string json) where T : IStateBase
        {
            return (T)Deserialize(typeof(T), eventManager, path, new Queue<char>(json));
        }

        internal static object Deserialize(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var interfaces = type.GetInterfaces();
            if (interfaces.Contains(typeof(IStateDictionaryBase)))
            {
                return StateDictionaryDeserializer.Deserialize(type, eventManager, path, tokens);
            }

            if (interfaces.Contains(typeof(IStateObjectBase)))
            {
                return StateObjectDeserializer.Deserialize(type, eventManager, path, tokens);
            }

            if (interfaces.Contains(typeof(IStateStringBase)))
            {
                return StateStringDeserializer.Deserialize(type, eventManager, path, tokens);
            }

            if (interfaces.Contains(typeof(IStateStructureBase)))
            {
                return StateStructureDeserializer.Deserialize(type, eventManager, path, tokens);
            }

            throw new UnknownStateTypeException($"Unknown state type {type}");
        }
    }
}
