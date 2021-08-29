﻿using StateSharp.Core.Events;
using StateSharp.Core.Transactions;

namespace StateSharp.Core.States
{
    public interface IStateStructure<T> : IStateStructureBase where T : struct
    {
        T State { get; }

        T Set();
        void Set(T state);

        IStateTransaction<IStateStructure<T>> BeginTransaction();
        void Commit(IStateTransaction<IStateStructure<T>> transaction);

        internal new IStateStructure<T> Copy(IStateEventManager eventManager);
        internal new IStateBase FromJson(string json);
    }
}