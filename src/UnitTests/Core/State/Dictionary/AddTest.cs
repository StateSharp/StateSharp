using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace UnitTests.Core.State.Dictionary
{
    [TestClass]
    public class AddTest
    {
        [TestMethod]
        public void AddUser()
        {
            var server = StateManagerConstructor.New<GameState>();
            server.State.RemotePlayers.Set();
            var user1 = server.State.RemotePlayers.Add("User1");
            Assert.AreEqual(user1, server.State.RemotePlayers.State["User1"]);
        }
    }
}