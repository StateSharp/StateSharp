using StateSharp.Core.Exceptions;
using System.Reflection;

namespace StateSharp.Core.Validators
{
    internal static class PropertyValidator
    {
        internal static void HasPublicGetAccessor(PropertyInfo property)
        {
            var getter = property.GetGetMethod();
            if (getter == null || getter.IsPublic == false)
            {
                throw new ValidationException($"Property {property.Name} get accessor is not public");
            }
        }

        internal static void HasNoSetAccessor(PropertyInfo property)
        {
            var setter = property.GetSetMethod();
            if (setter == null)
            {
                throw new ValidationException($"Property {property.Name} set accessor is not private");
            }
        }
    }
}