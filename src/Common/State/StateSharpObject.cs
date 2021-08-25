using StateSharp.Common.Event;
using StateSharp.Common.Transaction;
using System;

namespace StateSharp.Common.State
{
    internal sealed class StateSharpObject<T> : IStateSharpObject<T>
    {
        private readonly IStateSharpEventManager _eventManager;

        public string Path { get; }
        public T State { get; }

        public StateSharpObject(IStateSharpEventManager eventManager, string path)
        {
            Path = path;
            _eventManager = eventManager;
            State = StateSharpConstructor.Construct<T>(eventManager, Path);
        }

        public IStateSharpTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void Commit(IStateSharpTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void SubscribeOnChange(Action<IStateSharpEvent> handler)
        {
            _eventManager.Subscribe(Path, handler);
        }

        public void UnsubscribeOnChange(Action<IStateSharpEvent> handler)
        {
            _eventManager.Unsubscribe(Path, handler);
        }
    }
}