using StateSharp.Core.Exceptions;
using System;
using System.Linq;

namespace StateSharp.Core.Validators
{
    internal static class TypeValidator
    {
        internal static void HasDefaultConstructor(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) == null)
            {
                throw new ValidationException($"Type {type.FullName} does not implement parameterless constructor");
            }
        }

        internal static void HasNoFields(Type type)
        {
            if (type.GetFields().Any())
            {
                throw new ValidationException($"Type {type.FullName} contains fields");
            }
        }

        internal static void HasNoMethods(Type type)
        {
            if (type.GetMethods().Any())
            {
                throw new ValidationException($"Type {type.FullName} contains methods");
            }
        }
    }
}