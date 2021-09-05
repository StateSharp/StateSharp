using StateSharp.Json.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StateSharp.Json.Deserializers
{
    internal static class CommonDeserializer
    {
        public static string ReadString(Type type, Queue<char> tokens)
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

        public static void ReadNull(Type type, Queue<char> tokens)
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
