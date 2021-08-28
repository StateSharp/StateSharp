using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using State.State;
using StateSharp.Core;
using StateSharp.Core.Events;

namespace UnitTests.Core.Events.Dictionary
{
    [TestClass]
    public class RemoveTest
    {
        [TestMethod]
        public void RemoveUser()
        {
            var moq = new Mock<Action<IStateEvent>>();
            var server = StateManagerConstructor.New<GameState>();
            server.State.RemotePlayers.Set();
            server.State.RemotePlayers.Add("User1");
            server.State.RemotePlayers.SubscribeOnChange(moq.Object);
            server.State.RemotePlayers.Remove("User1");
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Once);
        }
    }
}