using System.Net;

namespace StateSharp.Networking.Server
{
    public static class StateServerConstructor
    {
        public static IStateServer<T> New<T>(IPAddress ipAddress, int port) where T : class
        {
            return new StateServer<T>(ipAddress, port);
        }
    }
}
