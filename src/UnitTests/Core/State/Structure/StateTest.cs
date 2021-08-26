using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace UnitTests.Core.State.Structure
{
    [TestClass]
    public class StateTest
    {
        [TestMethod]
        public void GetState()
        {
            var server = StateManagerConstructor.New<GameState>();
            Assert.IsNotNull(server.State);
            Assert.IsNotNull(server.State.RemotePlayers);
        }
    }
}