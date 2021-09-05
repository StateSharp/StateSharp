using StateSharp.Core.Events;
using StateSharp.Core.States;
using StateSharp.Json.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateSharp.Json.Deserializers
{
    internal static class StateStructureDeserializer
    {
        private static readonly Dictionary<Type, Func<Type, Queue<char>, object>> Deserializers = new()
        {
            { typeof(bool), (type, tokens) => DeserializeBool(type, tokens) },
            { typeof(byte), (type, tokens) => DeserializeByte(type, tokens) },
            { typeof(sbyte), (type, tokens) => DeserializeSbyte(type, tokens) },
            { typeof(char), (type, tokens) => DeserializeChar(type, tokens) },
            { typeof(decimal), (type, tokens) => DeserializeDecimal(type, tokens) },
            { typeof(double), (type, tokens) => DeserializeDouble(type, tokens) },
            { typeof(float), (type, tokens) => DeserializeFloat(type, tokens) },
            { typeof(int), (type, tokens) => DeserializeInt(type, tokens) },
            { typeof(uint), (type, tokens) => DeserializeUint(type, tokens) },
            { typeof(long), (type, tokens) => DeserializeLong(type, tokens) },
            { typeof(ulong), (type, tokens) => DeserializeUlong(type, tokens) },
            { typeof(short), (type, tokens) => DeserializeShort(type, tokens) },
            { typeof(ushort), (type, tokens) => DeserializeUshort(type, tokens) },
            { typeof(string), (type, tokens) => DeserializeString(type, tokens) },
        };

        public static IStateStructureBase Deserialize(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var stateType = type.GenericTypeArguments.Single();

            if (Deserializers.TryGetValue(stateType, out var deserializer))
            {
                return (IStateStructureBase)Activator.CreateInstance(typeof(StateStructure<>).MakeGenericType(stateType), eventManager, path, deserializer(stateType, tokens));
            }

            return (IStateStructureBase)Activator.CreateInstance(typeof(StateStructure<>).MakeGenericType(stateType), eventManager, path, DeserializeStruct(stateType, tokens));
        }

        public static object DeserializeStruct(Type type, Queue<char> tokens)
        {
            var state = Activator.CreateInstance(type);

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

                var property = type.GetProperty(name);
                if (Deserializers.TryGetValue(property.PropertyType, out var deserializer))
                {
                    property.SetValue(state, deserializer(property.PropertyType, tokens));
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

        public static bool DeserializeBool(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            while (tokens.Peek() != ',' && tokens.Peek() != '}')
            {
                builder.Append(tokens.Dequeue());
            }
            return bool.Parse(builder.ToString());
        }

        public static byte DeserializeByte(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            while (tokens.Peek() != ',' && tokens.Peek() != '}')
            {
                builder.Append(tokens.Dequeue());
            }
            return byte.Parse(builder.ToString());
        }

        public static sbyte DeserializeSbyte(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            while (tokens.Peek() != ',' && tokens.Peek() != '}')
            {
                builder.Append(tokens.Dequeue());
            }
            return sbyte.Parse(builder.ToString());
        }

        public static char DeserializeChar(Type type, Queue<char> tokens)
        {
            if (tokens.Dequeue() != '\'')
            {
                throw new Exception();
            }

            var result = tokens.Dequeue();

            if (tokens.Dequeue() != '\'')
            {
                throw new Exception();
            }

            return result;
        }

        public static decimal DeserializeDecimal(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            while (tokens.Peek() != ',' && tokens.Peek() != '}')
            {
                builder.Append(tokens.Dequeue());
            }
            return decimal.Parse(builder.ToString());
        }

        public static double DeserializeDouble(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            while (tokens.Peek() != ',' && tokens.Peek() != '}')
            {
                builder.Append(tokens.Dequeue());
            }
            return double.Parse(builder.ToString());
        }

        public static float DeserializeFloat(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            while (tokens.Peek() != ',' && tokens.Peek() != '}')
            {
                builder.Append(tokens.Dequeue());
            }
            return float.Parse(builder.ToString());
        }

        public static int DeserializeInt(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            while (tokens.Peek() != ',' && tokens.Peek() != '}')
            {
                builder.Append(tokens.Dequeue());
            }
            return int.Parse(builder.ToString());
        }

        public static uint DeserializeUint(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            while (tokens.Peek() != ',' && tokens.Peek() != '}')
            {
                builder.Append(tokens.Dequeue());
            }
            return uint.Parse(builder.ToString());
        }

        public static long DeserializeLong(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            while (tokens.Peek() != ',' && tokens.Peek() != '}')
            {
                builder.Append(tokens.Dequeue());
            }
            return long.Parse(builder.ToString());
        }

        public static ulong DeserializeUlong(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            while (tokens.Peek() != ',' && tokens.Peek() != '}')
            {
                builder.Append(tokens.Dequeue());
            }
            return ulong.Parse(builder.ToString());
        }

        public static short DeserializeShort(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            while (tokens.Peek() != ',' && tokens.Peek() != '}')
            {
                builder.Append(tokens.Dequeue());
            }
            return short.Parse(builder.ToString());
        }

        public static ushort DeserializeUshort(Type type, Queue<char> tokens)
        {
            var builder = new StringBuilder();
            while (tokens.Peek() != ',' && tokens.Peek() != '}')
            {
                builder.Append(tokens.Dequeue());
            }
            return ushort.Parse(builder.ToString());
        }

        public static string DeserializeString(Type type, Queue<char> tokens)
        {
            if (tokens.Peek() == 'n')
            {
                CommonDeserializer.ReadNull(type, tokens);
                return null;
            }
            return CommonDeserializer.ReadString(type, tokens);
        }
    }
}
