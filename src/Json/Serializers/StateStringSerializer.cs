using StateSharp.Core.States;

namespace StateSharp.Json.Serializers
{
    internal static class StateStringSerializer
    {
        public static string Serialize(IStateStringBase state)
        {
            return CommonSerializer.Serialize((string)state.GetState());
        }
    }
}
