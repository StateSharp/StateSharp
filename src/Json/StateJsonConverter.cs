using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.States;
using StateSharp.Json.Exceptions;

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
                    return Serialize((IStateDictionaryBase) state);
                }

                if (interfaces.Contains(typeof(IStateObjectBase)))
                {
                    return Serialize((IStateObjectBase) state);
                }

                if (interfaces.Contains(typeof(IStateStructureBase)))
                {
                    return Serialize((IStateStructureBase) state);
                }

                if (interfaces.Contains(typeof(IStateStringBase)))
                {
                    return Serialize((IStateStringBase) state);
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
            return SerializeString((string) state.GetState());
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
                return SerializeString((string) state);
            }

            return $"{{{string.Join(',', type.GetProperties().Select(x => $"\"{x.Name}\":{SerializeStructure(x.GetValue(state))}"))}}}";
        }

        private static string SerializeString(string state)
        {
            return state == null ? "null" : $"\"{state}\"";
        }

        internal static T Deserialize<T>(IStateEventManager eventManager, string path, string json) where T : IStateBase
        {
            return (T) Deserialize(typeof(T), eventManager, path, new Queue<char>(json));
        }

        internal static object Deserialize(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var interfaces = type.GetInterfaces();
            if (interfaces.Contains(typeof(IStateDictionaryBase)))
            {
                return DeserializeDictionary(type, eventManager, path, tokens);
            }

            if (interfaces.Contains(typeof(IStateObjectBase)))
            {
                return DeserializeObject(type, eventManager, path, tokens);
            }

            if (interfaces.Contains(typeof(IStateStructureBase)))
            {
                return DeserializeStructure(type, eventManager, path, tokens);
            }

            if (interfaces.Contains(typeof(IStateStringBase)))
            {
                return DeserializeString(type, eventManager, path, tokens);
            }

            throw new UnknownStateTypeException($"Unknown state type {type}");
        }

        private static IStateDictionaryBase DeserializeDictionary(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var stateType = type.GenericTypeArguments.Single();
            var state = Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(typeof(string), stateType));

            if (tokens.Dequeue() == '{')
            {
                throw new DeserializationException($"Could not serialize json for {type.FullName}");
            }

            while (true)
            {
                var field = ReadField(type, tokens);
                if (tokens.Dequeue() != ':')
                {
                    throw new DeserializationException($"Could not serialize json for {type.FullName}");
                }

                var value = Deserialize(type.GenericTypeArguments.Single(), eventManager, $"{path}[{field}]", tokens);
                state.GetType().GetMethod("Add").Invoke(state, new[] {field, value});

                if (tokens.Peek() == ',')
                {
                    continue;
                }

                if (tokens.Peek() == '}')
                {
                    break;
                }

                throw new DeserializationException($"Could not serialize json for {type.FullName}");
            }

            return (IStateDictionaryBase) Activator.CreateInstance(typeof(StateDictionary<>).MakeGenericType(stateType), eventManager, path, state);
        }

        private static IStateObjectBase DeserializeObject(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var stateType = type.GenericTypeArguments.Single();
            var state = Activator.CreateInstance(stateType);
            return (IStateObjectBase) Activator.CreateInstance(typeof(StateDictionary<>).MakeGenericType(stateType), eventManager, path, state);
        }

        private static IStateStructureBase DeserializeStructure(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var stateType = type.GenericTypeArguments.Single();
            var state = Activator.CreateInstance(stateType);
            var constructors = typeof(StateStructure<>).MakeGenericType(stateType).GetConstructors();
            return (IStateStructureBase) Activator.CreateInstance(typeof(StateStructure<>).MakeGenericType(stateType), eventManager, path, state);
        }

        private static IStateStringBase DeserializeString(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            return null;
        }

        private static string ReadField(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            if (tokens.Dequeue() == '"')
            {
                throw new DeserializationException($"Could not serialize json for {type.FullName}");
            }

            var token = tokens.Dequeue();
            while (token != '"')
            {
                builder.Append(token);
                token = tokens.Dequeue();
            }

            return builder.ToString();
        }
    }
}
