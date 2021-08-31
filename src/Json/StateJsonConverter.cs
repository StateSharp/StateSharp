using System.Linq;
using StateSharp.Core.Exceptions;
using StateSharp.Core.States;

namespace StateSharp.Json
{
    public static class StateJsonConverter
    {
        public static string Serialize(object state)
        {
            if (state == null)
            {
                return "null";
            }

            var type = typeof(object);
            var interfaces = state.GetType().GetInterfaces();
            if (interfaces.Contains(typeof(IStateBase)))
            {
                if (interfaces.Contains(typeof(IStateDictionaryBase)))
                {
                    return Serialize((IStateDictionaryBase) state);
                }

                if (interfaces.Contains(typeof(IStateObjectBase)))
                {
                    return Serialize((IStateObjectBase) state);
                }

                if (interfaces.Contains(typeof(IStateStructureBase)))
                {
                    return Serialize((IStateStructureBase) state);
                }

                if (interfaces.Contains(typeof(IStateStringBase)))
                {
                    return Serialize((IStateStringBase) state);
                }

                throw new UnknownStateTypeException($"Unknown state type {type}");
            }

            throw new InvalidStateTypeException($"Type {type} is not a state type");
        }

        public static string Serialize(IStateDictionaryBase state)
        {
            if (state.GetState() == null)
            {
                return "null";
            }

            return $"{{{string.Join(',', state.GetState().Select(x => $"\"{x.Key}\":{Serialize(x.Value)}"))}}}";
        }

        public static string Serialize(IStateObjectBase state)
        {
            if (state.GetState() == null)
            {
                return "null";
            }

            return
                $"{{{string.Join(',', state.GetState().GetType().GetProperties().Select(x => $"\"{x.Name}\":{Serialize(x.GetValue(state.GetState()))}"))}}}";
        }

        public static string Serialize(IStateStructureBase state)
        {
            return SerializeStructure(state.GetState());
        }

        public static string Serialize(IStateStringBase state)
        {
            return SerializeString((string) state.GetState());
        }

        private static string SerializeStructure(object state)
        {
            var type = state.GetType();
            if (type.IsPrimitive)
            {
                return state.ToString();
            }

            if (type == typeof(string))
            {
                return SerializeString((string) state);
            }

            return
                $"{{{string.Join(',', type.GetProperties().Select(x => $"\"{x.Name}\":{SerializeStructure(x.GetValue(state))}"))}}}";
        }

        private static string SerializeString(string state)
        {
            return state == null ? "null" : $"\"{state}\"";
        }
    }
}