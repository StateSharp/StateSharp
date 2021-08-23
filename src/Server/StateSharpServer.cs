using StateSharp.Common;
using System.Net;
using System.Net.Sockets;

namespace StateSharp.Server
{
    internal class StateSharpServer<T> : IStateSharpServer<T>
    {
        private readonly IStateSharpManager<T> _state;
        private readonly TcpListener _listener;

        public T State => _state.State;

        public StateSharpServer(IPAddress ipAddress, int port)
        {
            _state = StateSharpManagerConstructor.New<T>();
            _listener = new TcpListener(ipAddress, port);
        }

        public void Start()
        {
            _listener.Start();
        }

        public void Stop()
        {
            _listener.Stop();
        }
    }
}