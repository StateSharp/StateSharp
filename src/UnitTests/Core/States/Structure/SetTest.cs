using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace UnitTests.Core.States.Structure
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void SetScore()
        {
            var server = StateManagerConstructor.New<GameState>();
            server.State.Score.Set(10);
            Assert.IsNotNull(server.State);
            Assert.IsNotNull(server.State.Score);
            Assert.AreEqual(10, server.State.Score.State);
        }
    }
}