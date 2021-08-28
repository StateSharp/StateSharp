using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace StateSharp.UnitTests.Core.States.Dictionary
{
    [TestClass]
    public class AddTest
    {
        [TestMethod]
        public void AddRemotePlayer()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.State.RemotePlayers.Set();
            var user1 = manager.State.RemotePlayers.Add("User1");
            Assert.AreEqual(user1, manager.State.RemotePlayers.State["User1"]);
        }
    }
}