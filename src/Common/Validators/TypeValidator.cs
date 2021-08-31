using System;
using System.Linq;
using System.Reflection;
using StateSharp.Core.Exceptions;

namespace StateSharp.Core.Validators
{
    internal static class TypeValidator
    {
        internal static void IsReferenceType(Type type)
        {
            if (type.IsValueType)
            {
                throw new ValidationException($"Type {type.FullName} is a value type");
            }
        }

        internal static void IsValueType(Type type)
        {
            if (type.IsValueType == false)
            {
                throw new ValidationException($"Type {type.FullName} is a reference type");
            }
        }

        internal static void HasOnlyDefaultConstructor(Type type)
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
            if (type.GetMethods(BindingFlags.DeclaredOnly).Any())
            {
                throw new ValidationException($"Type {type.FullName} contains methods");
            }
        }
    }
}
