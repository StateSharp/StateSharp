using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StateSharp.Core;
using StateSharp.Core.Events;
using StateSharp.Tests.State.State;

namespace StateSharp.Tests.UnitTests.Core.Events.Dictionary
{
    [TestClass]
    public class RemoveTest
    {
        [TestMethod]
        public void RemoveUser()
        {
            var moq = new Mock<Action<IStateEvent>>();
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            manager.State.RemotePlayers.Init();
            manager.State.RemotePlayers.Add("User1");
            manager.State.RemotePlayers.SubscribeOnChange(moq.Object);
            manager.State.RemotePlayers.Remove("User1");
            moq.Verify(x => x(It.IsAny<IStateEvent>()), Times.Once);
        }
    }
}
