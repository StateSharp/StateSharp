using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace UnitTests.Core.State
{
    [TestClass]
    public class GetPathTest
    {
        [TestMethod]
        public void State()
        {
            var server = StateManagerConstructor.New<GameState>();
            Assert.AreEqual("State", server.Path);
        }

        [TestMethod]
        public void StatePlayers()
        {
            var server = StateManagerConstructor.New<GameState>();
            Assert.AreEqual("State.RemotePlayers", server.State.RemotePlayers.Path);
        }

        [TestMethod]
        public void StatePlayersUsername()
        {
            var server = StateManagerConstructor.New<GameState>();
            server.State.RemotePlayers.Set();
            var user1 = server.State.RemotePlayers.Add("User1");
            Assert.AreEqual("State.RemotePlayers[User1]", user1.Path);
        }

        [TestMethod]
        public void StatePlayersUsernamePosition()
        {
            var server = StateManagerConstructor.New<GameState>();
            server.State.RemotePlayers.Set();
            var user1 = server.State.RemotePlayers.Add("User1");
            user1.Set();
            Assert.AreEqual("State.RemotePlayers[User1].Position", user1.State.Position.Path);
        }
    }
}