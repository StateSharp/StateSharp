using System.Net;
using System.Net.Sockets;
using StateSharp.Core;

namespace StateSharp.Server
{
    internal class StateServer<T> : IStateServer<T> where T : class
    {
        private readonly TcpListener _listener;
        private readonly IStateManager<T> _state;

        public StateServer(IPAddress ipAddress, int port)
        {
            _state = StateManagerConstructor.New<T>();
            _listener = new TcpListener(ipAddress, port);
        }

        public T State => _state.State;

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