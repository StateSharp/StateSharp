using StateSharp.Common;
using StateSharp.Common.State;
using System;
using System.Net;
using System.Net.Sockets;

namespace StateSharp.Server
{
    public class StateSharpServer<T> : IStateSharpManager, IStateSharpServer<T>
    {
        public T State => _state.State;
        private readonly TcpListener _listener;
        private readonly StateSharpObject<T> _state;

        public StateSharpServer(IPAddress ipAddress, int port)
        {
            _state = new StateSharpObject<T>(null);
            _listener = new TcpListener(ipAddress, port);
        }

        public void Set(T value)
        {
            _state.Set(value);
        }

        public void SubscribeOnChange(Action<T> handler)
        {
            Subscribe(".", x => handler((T)x));
        }

        public void UnsubscribeOnChange(Action<T> handler)
        {
            _state.UnsubscribeOnChange(handler);
        }

        public void Subscribe(string filter, Action<object> handler)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(string filter, Action<object> handler)
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