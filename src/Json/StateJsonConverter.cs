using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.States;
using StateSharp.Json.Exceptions;
using StateSharp.Json.Serializers.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            if (tokens.Peek() == 'n')
            {
                ReadNull(type, tokens);
                return (IStateDictionaryBase)Activator.CreateInstance(typeof(StateDictionary<>).MakeGenericType(stateType), eventManager, path);
            }

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

            if (tokens.Peek() == 'n')
            {
                ReadNull(type, tokens);
                return (IStateObjectBase)Activator.CreateInstance(typeof(StateObject<>).MakeGenericType(stateType), eventManager, path);
            }

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

            return (IStateObjectBase)Activator.CreateInstance(typeof(StateObject<>).MakeGenericType(stateType), eventManager, path, state);
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
            if (tokens.Peek() == 'n')
            {
                ReadNull(type, tokens);
                return new StateString(eventManager, path);
            }

            var value = ReadString(typeof(string), tokens);
            return new StateString(eventManager, path, value);
        }

        private static object DeserializeStruct(Type type, Queue<char> tokens)
        {
            if (type == typeof(string))
            {
                if (tokens.Peek() == 'n')
                {
                    ReadNull(type, tokens);
                    return null;
                }
                return ReadString(type, tokens);
            }

            if (type == typeof(decimal))
            {
                return DeserializePrimative(type, tokens);
            }

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
                return char.Parse(builder.ToString().Replace("'", ""));
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

        private static void ReadNull(Type type, Queue<char> tokens)
        {
            if (tokens.Dequeue() != 'n')
            {
                throw new DeserializationException($"Could not serialize json for {type.FullName}");
            }

            if (tokens.Dequeue() != 'u')
            {
                throw new DeserializationException($"Could not serialize json for {type.FullName}");
            }

            if (tokens.Dequeue() != 'l')
            {
                throw new DeserializationException($"Could not serialize json for {type.FullName}");
            }

            if (tokens.Dequeue() != 'l')
            {
                throw new DeserializationException($"Could not serialize json for {type.FullName}");
            }
        }
    }
}
