using StateSharp.Common;
using StateSharp.Common.State;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace StateSharp.Server
{
    internal class StateSharpServer<T> : IStateSharpManager, IStateSharpServer<T>
    {
        IStateSharpBase IStateSharpBase.Parent => null;
        string IStateSharpBase.Key => ((IStateSharpBase)_state).Key;
        StateSharpType IStateSharpBase.Type => StateSharpType.Object;

        private readonly StateSharpObject<T> _state;
        private readonly TcpListener _listener;

        public T State => _state.State;

        public StateSharpServer(string root, IPAddress ipAddress, int port)
        {
            _state = new StateSharpObject<T>(this, root);
            _listener = new TcpListener(ipAddress, port);
        }

        public void SubscribeOnChange(Action<T> handler)
        {
            Subscribe(GetPath(), x => handler((T)x));
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

        string IStateSharpBase.GetPath(List<IStateSharpBase> callers)
        {
            return PathService.GetPath(callers);
        }

        public string GetPath()
        {
            return ((IStateSharpBase)this).GetPath(new List<IStateSharpBase>());
        }
    }
}