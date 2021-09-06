using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateSharp.Core;
using StateSharp.Tests.State.State;

namespace StateSharp.Tests.UnitTests.Core.States.Dictionary
{
    [TestClass]
    public class RemoveTest
    {
        [TestMethod]
        public void RemoveRemotePlayer()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            manager.State.RemotePlayers.Init();
            manager.State.RemotePlayers.Add("User1");
            manager.State.RemotePlayers.Remove("User1");
            Assert.AreEqual(0, manager.State.RemotePlayers.State.Count);
        }
    }
}
