using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using State.State;
using StateSharp.Core;
using StateSharp.Core.Events;

namespace UnitTests.Core.Events.Dictionary
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void SetRemotePlayers()
        {
            var moq = new Mock<Action<IStateEvent>>();
            var server = StateManagerConstructor.New<GameState>();
            server.State.RemotePlayers.SubscribeOnChange(moq.Object);
            server.State.RemotePlayers.Set();
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Once);
        }
    }
}