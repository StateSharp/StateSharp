using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StateSharp.Core;
using StateSharp.Core.Events;
using StateSharp.Tests.State.State;

namespace StateSharp.Tests.UnitTests.Core.Events.Object
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
            manager.State.LocalPlayer.SubscribeOnChange(moq.Object);
            manager.State.LocalPlayer.Init();
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Once);
        }
    }
}
