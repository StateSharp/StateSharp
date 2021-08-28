using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace UnitTests.Core.States.Structure
{
    [TestClass]
    public class StateTest
    {
        [TestMethod]
        public void GetState()
        {
            var server = StateManagerConstructor.New<GameState>();
            Assert.IsNotNull(server.State);
            Assert.IsNotNull(server.State.Score);
            Assert.AreEqual(0, server.State.Score.State);
        }
    }
}