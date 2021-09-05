namespace StateSharp.Json.Serializers
{
    internal static class CommonSerializer
    {
        public static string Serialize(string state)
        {
            return state == null ? "null" : $"\"{state}\"";
        }
    }
}
