using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace UnitTests.Core.States.Dictionary
{
    [TestClass]
    public class RemoveTest
    {
        [TestMethod]
        public void RemoveUser()
        {
            var server = StateManagerConstructor.New<GameState>();
            server.State.RemotePlayers.Set();
            server.State.RemotePlayers.Add("User1");
            server.State.RemotePlayers.Remove("User1");
            Assert.AreEqual(0, server.State.RemotePlayers.State.Count);
        }
    }
}