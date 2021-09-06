using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateSharp.Core;
using StateSharp.Tests.State.State;

namespace StateSharp.Tests.UnitTests.Core.States.Dictionary
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void SetRemotePlayers()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            manager.State.RemotePlayers.Init();
            Assert.AreEqual(0, manager.State.RemotePlayers.State.Count);
        }
    }
}
