using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace StateSharp.UnitTests.Core.States.Dictionary
{
    [TestClass]
    public class RemoveTest
    {
        [TestMethod]
        public void RemoveRemotePlayer()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Set();
            manager.State.RemotePlayers.Set();
            manager.State.RemotePlayers.Add("User1");
            manager.State.RemotePlayers.Remove("User1");
            Assert.AreEqual(0, manager.State.RemotePlayers.State.Count);
        }
    }
}