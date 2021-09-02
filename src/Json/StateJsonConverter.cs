using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.States;
using StateSharp.Json.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return (T)Deserialize(typeof(T), eventManager, path, new Queue<char>(json));
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

            if (tokens.Dequeue() != '{')
            {
                throw new DeserializationException($"Could not serialize json for {type.FullName}");
            }

            while (true)
            {
                var field = ReadString(type, tokens);
                if (tokens.Dequeue() != ':')
                {
                    throw new DeserializationException($"Could not serialize json for {type.FullName}");
                }

                var value = Deserialize(type.GenericTypeArguments.Single(), eventManager, $"{path}[{field}]", tokens);
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

        private static IStateObjectBase DeserializeObject(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var stateType = type.GenericTypeArguments.Single();
            var state = Activator.CreateInstance(stateType);

            if (tokens.Dequeue() != '{')
            {
                throw new DeserializationException($"Could not serialize json for {type.FullName}");
            }

            while (true)
            {
                var field = ReadString(type, tokens);
                if (tokens.Dequeue() != ':')
                {
                    throw new DeserializationException($"Could not serialize json for {type.FullName}");
                }

                var property = stateType.GetProperty(field);
                var value = Deserialize(property.PropertyType, eventManager, $"{path}.{field}", tokens);
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

            return (IStateObjectBase)Activator.CreateInstance(typeof(StateDictionary<>).MakeGenericType(stateType), eventManager, path, state);
        }

        private static IStateStructureBase DeserializeStructure(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var stateType = type.GenericTypeArguments.Single();

            if (stateType.IsPrimitive)
            {
                return (IStateStructureBase)Activator.CreateInstance(typeof(StateStructure<>).MakeGenericType(stateType), eventManager, path, DeserializePrimative(stateType, tokens));
            }

            return (IStateStructureBase)Activator.CreateInstance(typeof(StateStructure<>).MakeGenericType(stateType), eventManager, path, DeserializeStruct(stateType, tokens));
        }

        private static IStateStringBase DeserializeString(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var value = ReadString(typeof(string), tokens);
            return new StateString(eventManager, path, value);
        }

        private static object DeserializeStruct(Type type, Queue<char> tokens)
        {
            var state = Activator.CreateInstance(type);

            if (tokens.Dequeue() != '{')
            {
                throw new DeserializationException($"Could not serialize json for {type.FullName}");
            }

            while (true)
            {
                var field = ReadString(type, tokens);
                if (tokens.Dequeue() != ':')
                {
                    throw new DeserializationException($"Could not serialize json for {type.FullName}");
                }

                var property = type.GetProperty(field);
                if (property.PropertyType.IsPrimitive)
                {
                    property.SetValue(state, DeserializePrimative(property.PropertyType, tokens));
                }
                else
                {
                    property.SetValue(state, DeserializeStruct(property.PropertyType, tokens));
                }

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

            return state;
        }

        private static object DeserializePrimative(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            while (tokens.Peek() != ',' && tokens.Peek() != '}')
            {
                builder.Append(tokens.Dequeue());
            }

            if (type == typeof(bool))
            {
                return bool.Parse(builder.ToString());
            }

            if (type == typeof(byte))
            {
                return byte.Parse(builder.ToString());
            }

            if (type == typeof(sbyte))
            {
                return sbyte.Parse(builder.ToString());
            }

            if (type == typeof(char))
            {
                return char.Parse(builder.ToString());
            }

            if (type == typeof(decimal))
            {
                return decimal.Parse(builder.ToString());
            }

            if (type == typeof(double))
            {
                return double.Parse(builder.ToString());
            }

            if (type == typeof(float))
            {
                return float.Parse(builder.ToString());
            }

            if (type == typeof(int))
            {
                return int.Parse(builder.ToString());
            }

            if (type == typeof(uint))
            {
                return uint.Parse(builder.ToString());
            }

            if (type == typeof(nint))
            {
                return nint.Parse(builder.ToString());
            }

            if (type == typeof(nuint))
            {
                return nuint.Parse(builder.ToString());
            }

            if (type == typeof(long))
            {
                return long.Parse(builder.ToString());
            }

            if (type == typeof(ulong))
            {
                return ulong.Parse(builder.ToString());
            }

            if (type == typeof(short))
            {
                return short.Parse(builder.ToString());
            }

            if (type == typeof(ushort))
            {
                return ushort.Parse(builder.ToString());
            }

            throw new DeserializationException($"Unknown primative {type.FullName}");
        }

        private static string ReadString(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            if (tokens.Dequeue() != '"')
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
