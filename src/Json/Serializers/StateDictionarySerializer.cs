using System;
using System.Linq;
using StateSharp.Core.States;

namespace StateSharp.Json.Serializers
{
    internal static class StateDictionarySerializer
    {
        public static string Serialize(IStateDictionaryBase state)
        {
            if (state.GetState() == null)
            {
                return "null";
            }

            var stateType = state.GetType().GenericTypeArguments.Single();

            var interfaces = stateType.GetInterfaces();
            if (interfaces.Contains(typeof(IStateDictionaryBase)))
            {
                return $"{{{string.Join(',', state.GetState().Select(x => $"\"{x.Key}\":{StateDictionarySerializer.Serialize((IStateDictionaryBase)x.Value)}"))}}}";
            }
            if (interfaces.Contains(typeof(IStateObjectBase)))
            {
                return $"{{{string.Join(',', state.GetState().Select(x => $"\"{x.Key}\":{StateObjectSerializer.Serialize((IStateObjectBase)x.Value)}"))}}}";
            }
            if (interfaces.Contains(typeof(IStateStringBase)))
            {
                return $"{{{string.Join(',', state.GetState().Select(x => $"\"{x.Key}\":{StateStringSerializer.Serialize((IStateStringBase)x.Value)}"))}}}";
            }
            if (interfaces.Contains(typeof(IStateStructureBase)))
            {
                return $"{{{string.Join(',', state.GetState().Select(x => $"\"{x.Key}\":{StateStructureSerializer.Serialize((IStateStructureBase)x.Value)}"))}}}";
            }

            throw new Exception();
        }
    }
}
