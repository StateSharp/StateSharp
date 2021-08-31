using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StateSharp.Json
{
    public static class StateJsonConverter
    {
        public static string Serialize(object state)
        {
            if (state == null)
            {
                return "null";
            }

            var type = state.GetType();
            var interfaces = type.GetInterfaces();
            if (interfaces.Contains(typeof(IStateBase)))
            {
                if (interfaces.Contains(typeof(IStateDictionaryBase)))
                {
                    return Serialize((IStateDictionaryBase)state);
                }

                if (interfaces.Contains(typeof(IStateObjectBase)))
                {
                    return Serialize((IStateObjectBase)state);
                }

                if (interfaces.Contains(typeof(IStateStructureBase)))
                {
                    return Serialize((IStateStructureBase)state);
                }

                if (interfaces.Contains(typeof(IStateStringBase)))
                {
                    return Serialize((IStateStringBase)state);
                }

                throw new UnknownStateTypeException($"Unknown state type {type}");
            }

            throw new InvalidStateTypeException($"Type {type} is not a state type");
        }

        public static string Serialize(IStateDictionaryBase state)
        {
            if (state.GetState() == null)
            {
                return "null";
            }

            return $"{{{string.Join(',', state.GetState().Select(x => $"\"{x.Key}\":{Serialize(x.Value)}"))}}}";
        }

        public static string Serialize(IStateObjectBase state)
        {
            if (state.GetState() == null)
            {
                return "null";
            }

            return $"{{{string.Join(',', state.GetState().GetType().GetProperties().Select(x => $"\"{x.Name}\":{Serialize(x.GetValue(state.GetState()))}"))}}}";
        }

        public static string Serialize(IStateStructureBase state)
        {
            return SerializeStructure(state.GetState());
        }

        public static string Serialize(IStateStringBase state)
        {
            return SerializeString((string)state.GetState());
        }

        private static string SerializeStructure(object state)
        {
            var type = state.GetType();
            if (type.IsPrimitive)
            {
                return state.ToString();
            }

            if (type == typeof(string))
            {
                return SerializeString((string)state);
            }

            return $"{{{string.Join(',', type.GetProperties().Select(x => $"\"{x.Name}\":{SerializeStructure(x.GetValue(state))}"))}}}";
        }

        private static string SerializeString(string state)
        {
            return state == null ? "null" : $"\"{state}\"";
        }

        internal static T Deserialize<T>(IStateEventManager eventManager, string path, string json) where T : IStateBase
        {
            var type = typeof(T);
            var tokens = new Queue<char>(json);
            var interfaces = type.GetInterfaces();
            if (interfaces.Contains(typeof(IStateDictionaryBase)))
            {
                return (T)DeserializeDictionary(type, eventManager, path, tokens);
            }

            if (interfaces.Contains(typeof(IStateObjectBase)))
            {
                return (T)DeserializeObject(type, eventManager, path, tokens);
            }

            if (interfaces.Contains(typeof(IStateStructureBase)))
            {
                return (T)DeserializeStructure(type, eventManager, path, tokens);
            }

            if (interfaces.Contains(typeof(IStateStringBase)))
            {
                return (T)DeserializeString(type, eventManager, path, tokens);
            }

            throw new UnknownStateTypeException($"Unknown state type {type}");
        }

        private static IStateDictionaryBase DeserializeDictionary(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var stateType = type.GenericTypeArguments.Single();
            var state = Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(typeof(string), stateType));
            return (IStateDictionaryBase)Activator.CreateInstance(typeof(StateDictionary<>).MakeGenericType(stateType), eventManager, path, state);
        }

        private static IStateObjectBase DeserializeObject(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var stateType = type.GenericTypeArguments.Single();
            var state = Activator.CreateInstance(type);
            return (IStateObjectBase)Activator.CreateInstance(typeof(StateDictionary<>).MakeGenericType(stateType), eventManager, path, state);
        }

        private static IStateStructureBase DeserializeStructure(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var stateType = type.GenericTypeArguments.Single();
            var state = Activator.CreateInstance(type);
            return (IStateStructureBase)Activator.CreateInstance(typeof(StateDictionary<>).MakeGenericType(stateType), eventManager, path, state);
        }

        private static IStateStringBase DeserializeString(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            return null;
        }
    }
}
