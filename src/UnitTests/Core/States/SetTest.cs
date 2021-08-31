using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace StateSharp.UnitTests.Core.States
{
    [TestClass]
    public class StateTest
    {
        [TestMethod]
        public void GetState()
        {
            var manager = StateManagerConstructor.New<GameState>();
            Assert.IsNull(manager.State);
            manager.Init();
            Assert.IsNotNull(manager.State);
            Assert.IsNotNull(manager.State.Score);
            Assert.IsNotNull(manager.State.LocalPlayer);
            Assert.IsNotNull(manager.State.RemotePlayers);
        }
    }
}