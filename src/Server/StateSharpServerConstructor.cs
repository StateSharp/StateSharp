using System.Net;

namespace StateSharp.Server
{
    public static class StateSharpServerConstructor
    {
        public static IStateSharpServer<T> New<T>(IPAddress ipAddress, int port)
        {
            return new StateSharpServer<T>(ipAddress, port);
        }
    }
}