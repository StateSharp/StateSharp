using System.Linq;
using System.Text;
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

            return $"{{{string.Join(',', state.GetState().Select(x => $"\"{x.Key}\": {Serialize(x.Value)}"))}}}";
        }

        public static string Serialize(IStateObjectBase state)
        {
            if (state.GetState() == null)
            {
                return "null";
            }

            var builder = new StringBuilder();
            builder.Append("{");
            foreach (var property in state.GetState().GetType().GetProperties())
            {
                builder.Append($"\"{property.Name}\": {Serialize(property.GetValue(state.GetState()))}");
            }

            builder.Append("}");
            return builder.ToString();
        }

        public static string Serialize(IStateStructureBase state)
        {
            return SerializeStructure(state.GetState());
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
                return $"\"{state}\"";
            }

            var builder = new StringBuilder();
            builder.Append("{");
            foreach (var property in type.GetProperties())
            {
                builder.Append($"\"{property.Name}\": {SerializeStructure(property.GetValue(state))}");
            }

            builder.Append("}");
            return builder.ToString();
        }
    }
}