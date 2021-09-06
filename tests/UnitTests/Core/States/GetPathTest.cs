using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateSharp.Core;
using StateSharp.Tests.State.State;

namespace StateSharp.Tests.UnitTests.Core.States
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
            manager.Init();
            Assert.AreEqual("State.Score", manager.State.Score.Path);
        }

        [TestMethod]
        public void StateLocalPlayer()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            Assert.AreEqual("State.LocalPlayer", manager.State.LocalPlayer.Path);
        }

        [TestMethod]
        public void StateRemotePlayers()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            Assert.AreEqual("State.RemotePlayers", manager.State.RemotePlayers.Path);
        }

        [TestMethod]
        public void StateRemotePlayersUsername()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            manager.State.RemotePlayers.Init();
            var user1 = manager.State.RemotePlayers.Add("User1");
            Assert.AreEqual("State.RemotePlayers[User1]", user1.Path);
        }

        [TestMethod]
        public void StateRemotePlayersUsernamePosition()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            manager.State.RemotePlayers.Init();
            var user1 = manager.State.RemotePlayers.Add("User1");
            user1.Init();
            Assert.AreEqual("State.RemotePlayers[User1].Position", user1.State.Position.Path);
        }
    }
}
