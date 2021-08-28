using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using State.State;
using StateSharp.Core;
using StateSharp.Core.Events;
using System;

namespace UnitTests.Core.Events.Object
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void SetLocalPlayer()
        {
            var moq = new Mock<Action<IStateEvent>>();
            var server = StateManagerConstructor.New<GameState>();
            server.State.LocalPlayer.SubscribeOnChange(moq.Object);
            server.State.LocalPlayer.Set();
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Once);
        }
    }
}