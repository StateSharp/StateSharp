using StateSharp.Common.Event;
using System;
using System.Linq;

namespace StateSharp.Common.State
{
    internal static class StateSharpConstructor
    {

        internal static T Construct<T>(IStateSharpEventManager eventManager, string path)
        {
            return (T)Construct(typeof(T), eventManager, path);
        }

        internal static object Construct(Type type, IStateSharpEventManager eventManager, string path)
        {
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
    }
}