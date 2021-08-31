using System;
using System.Linq;
using StateSharp.Core.Events;
using StateSharp.Core.Exceptions;
using StateSharp.Core.States;

namespace StateSharp.Core.Constructors
{
    internal static class StateConstructor
    {
        internal static T ConstructState<T>(IStateEventManager eventManager, string path)
        {
            return (T) ConstructState(typeof(T), eventManager, path);
        }

        internal static object ConstructState(Type type, IStateEventManager eventManager, string path)
        {
            var result = Activator.CreateInstance(type);
            foreach (var property in type.GetProperties())
            {
                var interfaces = property.PropertyType.GetInterfaces();
                if (interfaces.Contains(typeof(IStateBase)))
                {
                    if (interfaces.Contains(typeof(IStateDictionaryBase)))
                    {
                        property.SetValue(result, Activator.CreateInstance(typeof(StateDictionary<>).MakeGenericType(property.PropertyType.GenericTypeArguments), eventManager, $"{path}.{property.Name}"));
                    }
                    else if (interfaces.Contains(typeof(IStateObjectBase)))
                    {
                        property.SetValue(result, Activator.CreateInstance(typeof(StateObject<>).MakeGenericType(property.PropertyType.GenericTypeArguments), eventManager, $"{path}.{property.Name}"));
                    }
                    else if (interfaces.Contains(typeof(IStateStructureBase)))
                    {
                        property.SetValue(result, Activator.CreateInstance(typeof(StateStructure<>).MakeGenericType(property.PropertyType.GenericTypeArguments), eventManager, $"{path}.{property.Name}"));
                    }
                    else if (interfaces.Contains(typeof(IStateStringBase)))
                    {
                        property.SetValue(result, new StateString(eventManager, $"{path}.{property.Name}"));
                    }
                    else
                    {
                        throw new UnknownStateTypeException($"Unknown state type {type}");
                    }
                }
                else
                {
                    throw new InvalidStateTypeException($"Type {type} is not a state type");
                }
            }

            return result;
        }

        internal static T ConstructInternal<T>(IStateEventManager eventManager, string path) where T : IStateBase
        {
            return (T) ConstructInternal(typeof(T), eventManager, path);
        }

        internal static object ConstructInternal(Type type, IStateEventManager eventManager, string path)
        {
            object result;
            var interfaces = type.GetInterfaces();
            if (interfaces.Contains(typeof(IStateDictionaryBase)))
            {
                result = Activator.CreateInstance(typeof(StateDictionary<>).MakeGenericType(type.GenericTypeArguments), eventManager, path);
            }
            else if (interfaces.Contains(typeof(IStateObjectBase)))
            {
                result = Activator.CreateInstance(typeof(StateObject<>).MakeGenericType(type.GenericTypeArguments), eventManager, path);
            }
            else if (interfaces.Contains(typeof(IStateStructureBase)))
            {
                result = Activator.CreateInstance(typeof(StateStructure<>).MakeGenericType(type.GenericTypeArguments), eventManager, path);
            }
            else
            {
                throw new UnknownStateTypeException($"Unknown state type {type}");
            }

            return result;
        }
    }
}
