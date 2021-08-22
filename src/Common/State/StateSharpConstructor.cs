using System;
using System.Linq;

namespace StateSharp.Common.State
{
    internal static class StateSharpConstructor
    {

        internal static T Construct<T>(IStateSharpBase parent)
        {
            return (T)Construct(typeof(T), parent);
        }

        internal static object Construct(Type type, IStateSharpBase parent)
        {
            var result = Activator.CreateInstance(type);
            foreach (var property in type.GetProperties())
            {
                var interfaces = property.PropertyType.GetInterfaces();
                if (interfaces.Contains(typeof(IStateSharpBase)))
                {
                    if (interfaces.Contains(typeof(IStateSharpDictionaryBase)))
                    {
                        property.SetValue(result, Activator.CreateInstance(typeof(StateSharpDictionary<>).MakeGenericType(property.PropertyType.GenericTypeArguments), parent, property.Name));
                    }
                    else if (interfaces.Contains(typeof(IStateSharpObjectBase)))
                    {
                        property.SetValue(result, Activator.CreateInstance(typeof(StateSharpObject<>).MakeGenericType(type), parent, property.Name));
                    }
                    else if (interfaces.Contains(typeof(IStateSharpStructureBase)))
                    {

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