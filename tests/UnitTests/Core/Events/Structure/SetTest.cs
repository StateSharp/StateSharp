using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StateSharp.Core;
using StateSharp.Core.Events;
using StateSharp.Tests.State.State;

namespace StateSharp.Tests.UnitTests.Core.Events.Structure
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
            manager.State.Score.Set(10);
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Once);
        }
    }
}
