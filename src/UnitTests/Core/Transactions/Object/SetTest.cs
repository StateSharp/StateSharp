using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using State.State;
using StateSharp.Core;
using StateSharp.Core.Events;

namespace StateSharp.UnitTests.Core.Transactions.Object
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void SetLocalPlayer()
        {
            var moq = new Mock<Action<IStateEvent>>();
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            manager.State.LocalPlayer.Init();
            manager.State.LocalPlayer.SubscribeOnChange(moq.Object);
            var transaction = manager.State.LocalPlayer.BeginTransaction();
            var state = transaction.State.Init();
            state.Position.Set(new Vector3(1, 2, 3));
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Never);
            manager.State.LocalPlayer.Commit(transaction);
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Once);
            Assert.IsNotNull(manager.State.LocalPlayer.State);
        }
    }
}