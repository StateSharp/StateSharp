﻿using StateSharp.Core.Events;
using StateSharp.Core.Transactions;

namespace StateSharp.Core.States
{
    public interface IStateStructure<T> : IStateStructureBase where T : struct
    {
        T State { get; }

        void Set(T state);

        IStateTransaction<IStateStructure<T>> BeginTransaction();
        void Commit(IStateTransaction<IStateStructure<T>> transaction);

        internal new IStateStructure<T> Copy(IStateEventManager eventManager);
    }
}