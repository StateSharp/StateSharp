using System.Net;
using System.Net.Sockets;
using StateSharp.Core;

namespace StateSharp.Server
{
    internal class StateServer<T> : IStateServer<T>
    {
        private readonly IStateManager<T> _state;
        private readonly TcpListener _listener;

        public T State => _state.State;

        public StateServer(IPAddress ipAddress, int port)
        {
            _state = StateManagerConstructor.New<T>();
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