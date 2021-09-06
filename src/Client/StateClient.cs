using StateSharp.Core;
using StateSharp.Core.Events;
using StateSharp.Core.States;
using StateSharp.Core.Transactions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace StateSharp.Networking.Client
{
    internal class StateClient<T> : IStateClient<T> where T : class
    {
        private readonly TcpClient _client;
        private readonly IStateManager<T> _manager;

        public StateClient()
        {
            _client = new TcpClient();
            _manager = StateManagerConstructor.New<T>();
        }

        public void Validate()
        {
            _manager.Validate();
        }

        public bool Connected => _client.Connected;

        public void Connect(string hostname, int port)
        {
            _client.Connect(hostname, port);
        }

        public void Close()
        {
            _client.Close();
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
    }
}
