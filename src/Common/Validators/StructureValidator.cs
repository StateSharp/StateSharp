using StateSharp.Core.States;
using System;
using System.Linq;

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
            var stateType = type.GenericTypeArguments.Single();
            TypeValidator.HasDefaultConstructor(stateType);
            TypeValidator.HasNoFields(stateType);
            TypeValidator.HasNoMethods(type);
            foreach (var property in stateType.GetProperties())
            {

            }
        }
    }
}