using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StateSharp.Core;
using StateSharp.Core.Events;
using StateSharp.Tests.State.State;

namespace StateSharp.Tests.UnitTests.Core.Transactions.Structure
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void SetScore()
        {
            var moq = new Mock<Action<IStateEvent>>();
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            manager.State.Score.SubscribeOnChange(moq.Object);
            var transaction = manager.State.Score.BeginTransaction();
            transaction.State.Set(32);
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Never);
            manager.State.Score.Commit(transaction);
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Once);
            Assert.IsNotNull(manager.State.Score.State);
        }
    }
}
