using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace UnitTests.Core.States.Object
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void SetLocalPlayer()
        {
            var server = StateManagerConstructor.New<GameState>();
            server.State.LocalPlayer.Set();
            Assert.IsNotNull(server.State.LocalPlayer.State);
        }
    }
}