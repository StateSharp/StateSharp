using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace StateSharp.UnitTests.Core.States
{
    [TestClass]
    public class GetPathTest
    {
        [TestMethod]
        public void State()
        {
            var manager = StateManagerConstructor.New<GameState>();
            Assert.AreEqual("State", manager.Path);
        }

        [TestMethod]
        public void StateScore()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Set();
            Assert.AreEqual("State.Score", manager.State.Score.Path);
        }

        [TestMethod]
        public void StateLocalPlayer()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Set();
            Assert.AreEqual("State.LocalPlayer", manager.State.LocalPlayer.Path);
        }

        [TestMethod]
        public void StateRemotePlayers()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Set();
            Assert.AreEqual("State.RemotePlayers", manager.State.RemotePlayers.Path);
        }

        [TestMethod]
        public void StateRemotePlayersUsername()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Set();
            manager.State.RemotePlayers.Set();
            var user1 = manager.State.RemotePlayers.Add("User1");
            Assert.AreEqual("State.RemotePlayers[User1]", user1.Path);
        }

        [TestMethod]
        public void StateRemotePlayersUsernamePosition()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Set();
            manager.State.RemotePlayers.Set();
            var user1 = manager.State.RemotePlayers.Add("User1");
            user1.Set();
            Assert.AreEqual("State.RemotePlayers[User1].Position", user1.State.Position.Path);
        }
    }
}