using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using State.State;
using StateSharp.Core;
using StateSharp.Core.Events;
using System;

namespace StateSharp.UnitTests.Core.Events.Object
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void SetLocalPlayer()
        {
            var moq = new Mock<Action<IStateEvent>>();
            var manager = StateManagerConstructor.New<GameState>();
            manager.State.LocalPlayer.SubscribeOnChange(moq.Object);
            manager.State.LocalPlayer.Set();
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Once);
        }
    }
}