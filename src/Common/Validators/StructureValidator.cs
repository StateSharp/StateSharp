using StateSharp.Core.States;
using System;
using System.Linq;
using System.Reflection;

namespace StateSharp.Core.Validators
{
    internal static class StructureValidator
    {
        internal static void Validate<T>() where T : IStateStructureBase
        {
            Validate(typeof(T));
        }

        internal static void Validate(Type type)
        {
            ValidateStruct(type.GenericTypeArguments.Single());
        }

        private static void ValidateStruct(Type type)
        {
            if (type.IsPrimitive) return;

            TypeValidator.IsValueType(type);
            TypeValidator.HasNoFields(type);
            TypeValidator.HasNoMethods(type);
            foreach (var property in type.GetProperties())
            {
                Validate(property);
            }
        }

        private static void Validate(PropertyInfo property)
        {
            PropertyValidator.HasPublicGetAccessor(property);

            ValidateStruct(property.PropertyType);
        }
    }
}