using StateSharp.Core;
using StateSharp.Core.Events;
using StateSharp.Core.States;
using StateSharp.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace StateSharp.Networking.Server
{
    internal class StateServer<T> : IStateServer<T> where T : class
    {
        private readonly TcpListener _listener;
        private readonly List<TcpClient> _clients;
        private readonly IStateManager<T> _manager;

        public StateServer(IPAddress ipAddress, int port)
        {
            _manager = StateManagerConstructor.New<T>();
            _clients = new List<TcpClient>();
            _listener = new TcpListener(ipAddress, port);
        }

        public void Validate()
        {
            _manager.Validate();
        }

        public string Path => _manager.Path;

        public void SubscribeOnChange(Action<IStateEvent> handler)
        {
            _manager.SubscribeOnChange(handler);
        }

        public void UnsubscribeOnChange(Action<IStateEvent> handler)
        {
            _manager.UnsubscribeOnChange(handler);
        }

        IStateEventManager IStateBase.GetEventManager()
        {
            return _manager.GetEventManager();
        }

        void IStateBase.SetEventManager(IStateEventManager eventManager)
        {
            _manager.SetEventManager(eventManager);
        }

        IReadOnlyList<IStateBase> IStateBase.GetChildren()
        {
            return _manager.GetChildren();
        }

        IStateObject<T> IStateObject<T>.Copy(IStateEventManager eventManager)
        {
            return _manager.Copy(eventManager);
        }

        public T State => _manager.State;

        public T Init()
        {
            return _manager.Init();
        }

        public IStateTransaction<IStateObject<T>> BeginTransaction()
        {
            return _manager.BeginTransaction();
        }

        public void Commit(IStateTransaction<IStateObject<T>> transaction)
        {
            _manager.Commit(transaction);
        }

        IStateBase IStateBase.Copy(IStateEventManager eventManager)
        {
            return _manager.Copy(eventManager);
        }

        object IStateObjectBase.GetState()
        {
            return _manager.GetState();
        }

        public void Start()
        {
            _listener.Start();
        }

        public bool Pending()
        {
            return _listener.Pending();
        }

        public void AcceptClient()
        {
            _clients.Add(_listener.AcceptTcpClient());
        }

        public void AcceptClients()
        {
            while (_listener.Pending())
            {
                _clients.Add(_listener.AcceptTcpClient());
            }
        }

        public void Stop()
        {
            _listener.Stop();
        }
    }
}
