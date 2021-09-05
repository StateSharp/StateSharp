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
        public static IStateStructureBase Deserialize(Type type, IStateEventManager eventManager, string path, Queue<char> tokens)
        {
            var stateType = type.GenericTypeArguments.Single();

            if (stateType.IsPrimitive)
            {
                return (IStateStructureBase)Activator.CreateInstance(typeof(StateStructure<>).MakeGenericType(stateType), eventManager, path, DeserializePrimative(stateType, tokens));
            }

            return (IStateStructureBase)Activator.CreateInstance(typeof(StateStructure<>).MakeGenericType(stateType), eventManager, path, DeserializeStruct(stateType, tokens));
        }
        private static object DeserializeStruct(Type type, Queue<char> tokens)
        {
            if (type == typeof(string))
            {
                if (tokens.Peek() == 'n')
                {
                    CommonDeserializer.ReadNull(type, tokens);
                    return null;
                }
                return CommonDeserializer.ReadString(type, tokens);
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
                var field = CommonDeserializer.ReadString(type, tokens);
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
    }
}
