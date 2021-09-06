using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateSharp.Core;
using StateSharp.Tests.State.State;

namespace StateSharp.Tests.UnitTests.Core.States.Dictionary
{
    [TestClass]
    public class AddTest
    {
        [TestMethod]
        public void AddRemotePlayer()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            manager.State.RemotePlayers.Init();
            var user1 = manager.State.RemotePlayers.Add("User1");
            Assert.AreEqual(user1, manager.State.RemotePlayers.State["User1"]);
        }
    }
}
