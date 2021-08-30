﻿using StateSharp.Core.Exceptions;
using StateSharp.Core.States;
using System;
using System.Linq;

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
            var stateType = type.GenericTypeArguments.Single();
            TypeValidator.HasDefaultConstructor(stateType);
            TypeValidator.HasNoFields(stateType);
            TypeValidator.HasNoMethods(type);
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
                        ObjectValidator.Validate(property.PropertyType);
                    }
                    else if (interfaces.Contains(typeof(IStateStructureBase)))
                    {
                        StructureValidator.Validate(property.PropertyType);
                    }
                    else
                    {
                        throw new ValidationException($"Unknown state type for property {property.Name} in type {type.FullName}");
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