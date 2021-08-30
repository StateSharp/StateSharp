using StateSharp.Core.States;
using System.Linq;
using System.Text;

namespace StateSharp.Json
{
    public static class StateJsonConverter
    {
        public static string Parse(object state)
        {
            return string.Empty;
        }

        public static string Parse<T>(IStateDictionary<T> state) where T : IStateBase
        {
            return $"{{{string.Join(',', state.State.Select(x => $"\"{x.Key}\": {Parse(x.Value)}"))}}}";
        }

        public static string Parse<T>(IStateObject<T> state) where T : class
        {
            var builder = new StringBuilder();
            builder.Append("{");
            foreach (var property in state.State.GetType().GetProperties())
            {
                builder.Append($"\"{property.Name}\": {Parse(property.GetValue(state))}");
            }
            builder.Append("}");
            return builder.ToString();
        }

        public static string Parse<T>(IStateStructure<T> state) where T : struct
        {
            return string.Empty;
        }
    }
}