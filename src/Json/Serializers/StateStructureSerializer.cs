using StateSharp.Core.States;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StateSharp.Json.Serializers
{
    internal static class StateStructureSerializer
    {
        private static readonly Dictionary<Type, Func<object, string>> Serializers = new()
        {
            { typeof(bool), state => Serialize((bool)state) },
            { typeof(byte), state => Serialize((byte)state) },
            { typeof(sbyte), state => Serialize((sbyte)state) },
            { typeof(char), state => Serialize((char)state) },
            { typeof(decimal), state => Serialize((decimal)state) },
            { typeof(double), state => Serialize((double)state) },
            { typeof(float), state => Serialize((float)state) },
            { typeof(int), state => Serialize((int)state) },
            { typeof(uint), state => Serialize((uint)state) },
            { typeof(long), state => Serialize((long)state) },
            { typeof(ulong), state => Serialize((ulong)state) },
            { typeof(short), state => Serialize((short)state) },
            { typeof(ushort), state => Serialize((ushort)state) },
            { typeof(string), state => Serialize((string)state) },
        };

        public static string Serialize(IStateStructureBase state)
        {
            var stateType = state.GetType().GenericTypeArguments.Single();

            if (Serializers.TryGetValue(stateType, out var serializer))
            {
                return serializer(state.GetState());
            }

            return Serialize(state.GetState());
        }

        public static string Serialize(object state)
        {
            var properties = new List<string>();
            foreach (var property in state.GetType().GetProperties())
            {
                if (Serializers.TryGetValue(property.PropertyType, out var serializer))
                {
                    properties.Add($"\"{property.Name}\":{serializer(property.GetValue(state))}");
                    continue;
                }

                properties.Add($"\"{property.Name}\":{Serialize(property.GetValue(state))}");
            }

            return $"{{{string.Join(',', properties)}}}";
        }

        public static string Serialize(bool state)
        {
            return state.ToString().ToLower();
        }

        public static string Serialize(byte state)
        {
            return state.ToString();
        }

        public static string Serialize(sbyte state)
        {
            return state.ToString();
        }

        public static string Serialize(char state)
        {
            return $"'{state}'";
        }

        public static string Serialize(decimal state)
        {
            return state.ToString(CultureInfo.InvariantCulture);
        }

        public static string Serialize(double state)
        {
            return state.ToString(CultureInfo.InvariantCulture);
        }

        public static string Serialize(float state)
        {
            return state.ToString(CultureInfo.InvariantCulture);
        }

        public static string Serialize(int state)
        {
            return state.ToString();
        }

        public static string Serialize(uint state)
        {
            return state.ToString();
        }

        public static string Serialize(long state)
        {
            return state.ToString();
        }

        public static string Serialize(ulong state)
        {
            return state.ToString();
        }

        public static string Serialize(short state)
        {
            return state.ToString();
        }

        public static string Serialize(ushort state)
        {
            return state.ToString();
        }

        public static string Serialize(string state)
        {
            return CommonSerializer.Serialize(state);
        }
    }
}
