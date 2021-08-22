using System.Net;

namespace StateSharp.Server
{
    public static class StateSharpServerConstructor
    {
        public static IStateSharpServer<T> New<T>(string root, IPAddress ipAddress, int port)
        {
            return new StateSharpServer<T>(root, ipAddress, port);
        }
    }
}