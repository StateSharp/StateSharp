using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace UnitTests.Core.States
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
        public void StateScore()
        {
            var server = StateManagerConstructor.New<GameState>();
            Assert.AreEqual("State.Score", server.State.Score.Path);
        }

        [TestMethod]
        public void StateLocalPlayer()
        {
            var server = StateManagerConstructor.New<GameState>();
            Assert.AreEqual("State.LocalPlayer", server.State.LocalPlayer.Path);
        }

        [TestMethod]
        public void StateRemotePlayers()
        {
            var server = StateManagerConstructor.New<GameState>();
            Assert.AreEqual("State.RemotePlayers", server.State.RemotePlayers.Path);
        }

        [TestMethod]
        public void StateRemotePlayersUsername()
        {
            var server = StateManagerConstructor.New<GameState>();
            server.State.RemotePlayers.Set();
            var user1 = server.State.RemotePlayers.Add("User1");
            Assert.AreEqual("State.RemotePlayers[User1]", user1.Path);
        }

        [TestMethod]
        public void StateRemotePlayersUsernamePosition()
        {
            var server = StateManagerConstructor.New<GameState>();
            server.State.RemotePlayers.Set();
            var user1 = server.State.RemotePlayers.Add("User1");
            user1.Set();
            Assert.AreEqual("State.RemotePlayers[User1].Position", user1.State.Position.Path);
        }
    }
}