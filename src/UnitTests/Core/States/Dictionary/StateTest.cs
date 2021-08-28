using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace UnitTests.Core.States.Dictionary
{
    [TestClass]
    public class StateTest
    {
        [TestMethod]
        public void GetState()
        {
            var server = StateManagerConstructor.New<GameState>();
            server.State.RemotePlayers.Set();
            Assert.AreEqual(0, server.State.RemotePlayers.State.Count);
        }
    }
}