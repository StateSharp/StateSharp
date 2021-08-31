using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using State.State;
using StateSharp.Core;
using StateSharp.Core.Events;
using System;

namespace StateSharp.UnitTests.Core.Events.Dictionary
{
    [TestClass]
    public class RemoveTest
    {
        [TestMethod]
        public void RemoveUser()
        {
            var moq = new Mock<Action<IStateEvent>>();
            var manager = StateManagerConstructor.New<GameState>();
            manager.Set();
            manager.State.RemotePlayers.Set();
            manager.State.RemotePlayers.Add("User1");
            manager.State.RemotePlayers.SubscribeOnChange(moq.Object);
            manager.State.RemotePlayers.Remove("User1");
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Once);
        }
    }
}