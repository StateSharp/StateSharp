using System.Net;

namespace StateSharp.Server
{
    public static class StateServerConstructor
    {
        public static IStateServer<T> New<T>(IPAddress ipAddress, int port)
        {
            return new StateServer<T>(ipAddress, port);
        }
    }
}