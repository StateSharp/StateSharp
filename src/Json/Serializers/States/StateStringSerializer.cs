using StateSharp.Core.States;

namespace StateSharp.Json.Serializers.States
{
    internal static class StateStringSerializer
    {
        public static string Serialize(IStateStringBase state)
        {
            return state.GetState() == null ? "null" : $"\"{state.GetState()}\"";
        }
    }
}
