using System;
using System.Linq;
using StateSharp.Core.Exceptions;
using StateSharp.Core.States;

namespace StateSharp.Core.Validators
{
    internal static class DictionaryValidator
    {
        internal static void Validate<T>() where T : IStateDictionaryBase
        {
            Validate(typeof(T));
        }

        internal static void Validate(Type type)
        {
            var elementType = type.GenericTypeArguments.Single();
            var interfaces = elementType.GetInterfaces();
            if (interfaces.Contains(typeof(IStateBase)))
            {
                if (interfaces.Contains(typeof(IStateDictionaryBase)))
                {
                    Validate(elementType);
                }
                else if (interfaces.Contains(typeof(IStateObjectBase)))
                {
                    ObjectValidator.Validate(elementType);
                }
                else if (interfaces.Contains(typeof(IStateStructureBase)))
                {
                    StructureValidator.Validate(elementType);
                }
                else
                {
                    throw new ValidationException(
                        $"Unknown state type for {elementType.FullName} in type {type.FullName}");
                }
            }
            else
            {
                throw new InvalidStateTypeException($"Type {elementType.FullName} is not a state type");
            }
        }
    }
}