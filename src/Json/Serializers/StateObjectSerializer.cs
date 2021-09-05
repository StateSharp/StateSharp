using System;
using System.Collections.Generic;
using System.Linq;
using StateSharp.Core.States;

namespace StateSharp.Json.Serializers
{
    internal static class StateObjectSerializer
    {
        public static string Serialize(IStateObjectBase state)
        {
            if (state.GetState() == null)
            {
                return "null";
            }

            var properties = new List<string>();
            foreach (var property in state.GetState().GetType().GetProperties())
            {
                var interfaces = property.PropertyType.GetInterfaces();
                if (interfaces.Contains(typeof(IStateStringBase)))
                {
                    properties.Add($"\"{property.Name}\":{StateStringSerializer.Serialize((IStateStringBase)property.GetValue(state.GetState()))}");
                }
                else if (interfaces.Contains(typeof(IStateDictionaryBase)))
                {
                    properties.Add($"\"{property.Name}\":{StateDictionarySerializer.Serialize((IStateDictionaryBase)property.GetValue(state.GetState()))}");
                }
                else if (interfaces.Contains(typeof(IStateObjectBase)))
                {
                    properties.Add($"\"{property.Name}\":{StateObjectSerializer.Serialize((IStateObjectBase)property.GetValue(state.GetState()))}");
                }
                else if (interfaces.Contains(typeof(IStateStructureBase)))
                {
                    properties.Add($"\"{property.Name}\":{StateStructureSerializer.Serialize((IStateStructureBase)property.GetValue(state.GetState()))}");
                }
                else
                {
                    throw new Exception();
                }
            }

            return $"{{{string.Join(',', properties)}}}";
        }
    }
}
