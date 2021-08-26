using StateSharp.Core;
using System.Net;
using System.Net.Sockets;

namespace StateSharp.Server
{
    internal class StateServer<T> : IStateServer<T> where T : class
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