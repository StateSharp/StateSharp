using System;
using System.Linq;
using StateSharp.Core.Exceptions;
using StateSharp.Core.States;

namespace StateSharp.Core.Validators
{
    internal static class ObjectValidator
    {
        internal static void Validate<T>() where T : IStateObjectBase
        {
            Validate(typeof(T));
        }

        internal static void Validate(Type type)
        {
            var stateType = type.GenericTypeArguments.Single();
            TypeValidator.IsReferenceType(stateType);
            TypeValidator.HasOnlyDefaultConstructor(stateType);
            TypeValidator.HasNoFields(stateType);
            TypeValidator.HasNoMethods(stateType);
            foreach (var property in stateType.GetProperties())
            {
                var interfaces = property.PropertyType.GetInterfaces();
                if (interfaces.Contains(typeof(IStateBase)))
                {
                    if (interfaces.Contains(typeof(IStateDictionaryBase)))
                    {
                        DictionaryValidator.Validate(property.PropertyType);
                    }
                    else if (interfaces.Contains(typeof(IStateObjectBase)))
                    {
                        Validate(property.PropertyType);
                    }
                    else if (interfaces.Contains(typeof(IStateStructureBase)))
                    {
                        StructureValidator.Validate(property.PropertyType);
                    }
                    else
                    {
                        throw new ValidationException(
                            $"Unknown state type for property {property.Name} in type {type.FullName}");
                    }
                }
                else
                {
                    throw new InvalidStateTypeException($"Property {property.Name} is not a state type");
                }
            }
        }
    }
}