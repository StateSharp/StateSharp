using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using State.State;
using StateSharp.Core;
using StateSharp.Core.Events;
using System;

namespace UnitTests.Core.Events.Structure
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void SetScore()
        {
            var moq = new Mock<Action<IStateEvent>>();
            var server = StateManagerConstructor.New<GameState>();
            server.State.Score.SubscribeOnChange(moq.Object);
            server.State.Score.Set(10);
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Once);
        }
    }
}