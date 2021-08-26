using StateSharp.Common.Event;
using System;
using System.Linq;

namespace StateSharp.Common.State
{
    internal static class StateSharpConstructor
    {
        internal static T ConstructState<T>(IStateSharpEventManager eventManager, string path)
        {
            return (T)ConstructState(typeof(T), eventManager, path);
        }

        internal static object ConstructState(Type type, IStateSharpEventManager eventManager, string path)
        {
            Console.WriteLine(type);
            var result = Activator.CreateInstance(type);
            foreach (var property in type.GetProperties())
            {
                var interfaces = property.PropertyType.GetInterfaces();
                if (interfaces.Contains(typeof(IStateSharpBase)))
                {
                    if (interfaces.Contains(typeof(IStateSharpDictionaryBase)))
                    {
                        property.SetValue(result, Activator.CreateInstance(typeof(StateSharpDictionary<>).MakeGenericType(property.PropertyType.GenericTypeArguments), eventManager, $"{path}.{property.Name}"));
                    }
                    else if (interfaces.Contains(typeof(IStateSharpObjectBase)))
                    {
                        property.SetValue(result, Activator.CreateInstance(typeof(StateSharpObject<>).MakeGenericType(property.PropertyType.GenericTypeArguments), eventManager, $"{path}.{property.Name}"));
                    }
                    else if (interfaces.Contains(typeof(IStateSharpStructureBase)))
                    {
                        property.SetValue(result, Activator.CreateInstance(typeof(StateSharpStructure<>).MakeGenericType(property.PropertyType.GenericTypeArguments), eventManager, $"{path}.{property.Name}"));
                    }
                    else
                    {
                        throw new Exception($"Unknown type for {property.Name} in {type}");
                    }
                }
                else
                {
                    throw new Exception($"Unknown type for {property.Name} in {type}");
                }
            }
            return result;
        }


        internal static T ConstructInternal<T>(IStateSharpEventManager eventManager, string path) where T : IStateSharpBase
        {
            return (T)ConstructInternal(typeof(T), eventManager, path);
        }

        internal static object ConstructInternal(Type type, IStateSharpEventManager eventManager, string path)
        {
            object result;
            var interfaces = type.GetInterfaces();
            if (interfaces.Contains(typeof(IStateSharpDictionaryBase)))
            {
                result = Activator.CreateInstance(typeof(StateSharpDictionary<>).MakeGenericType(type.GenericTypeArguments), eventManager, path);
            }
            else if (interfaces.Contains(typeof(IStateSharpObjectBase)))
            {
                result = Activator.CreateInstance(typeof(StateSharpObject<>).MakeGenericType(type.GenericTypeArguments), eventManager, path);
            }
            else if (interfaces.Contains(typeof(IStateSharpStructureBase)))
            {
                result = Activator.CreateInstance(typeof(StateSharpStructure<>).MakeGenericType(type.GenericTypeArguments), eventManager, path);
            }
            else
            {
                throw new Exception($"Unknown type {type}");
            }
            return result;
        }
    }
}