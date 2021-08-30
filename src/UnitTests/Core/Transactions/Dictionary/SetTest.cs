﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using State.State;
using StateSharp.Core;
using StateSharp.Core.Events;
using System;

namespace StateSharp.UnitTests.Core.Transactions.Dictionary
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void SetRemotePlayers()
        {
            var moq = new Mock<Action<IStateEvent>>();
            var manager = StateManagerConstructor.New<GameState>();
            manager.State.RemotePlayers.SubscribeOnChange(moq.Object);
            var transaction = manager.State.RemotePlayers.BeginTransaction();
            transaction.State.Set();
            transaction.State.Add("User1");
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Never);
            manager.State.RemotePlayers.Commit(transaction);
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Once);
            Assert.IsNotNull(manager.State.LocalPlayer.State);
        }
    }
}