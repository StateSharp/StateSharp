using StateSharp.Common;
using StateSharp.Common.State;
using System;
using System.Net;
using System.Net.Sockets;

namespace StateSharp.Server
{
    internal class StateSharpServer<T> : IStateSharpManager, IStateSharpServer<T>
    {
        private readonly StateSharpObject<T> _state;
        private readonly TcpListener _listener;

        public string Path { get; }
        public T State => _state.State;

        public StateSharpServer(string root, IPAddress ipAddress, int port)
        {
            Path = root;
            _state = new StateSharpObject<T>(root);
            _listener = new TcpListener(ipAddress, port);
        }

        public void SubscribeOnChange(Action<T> handler)
        {
            Subscribe(Path, x => handler((T)x));
        }

        public void UnsubscribeOnChange(Action<T> handler)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(string path, Action<object> handler)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(string path, Action<object> handler)
        {
            throw new NotImplementedException();
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