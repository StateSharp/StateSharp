using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace StateSharp.UnitTests.Core.States.Object
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void SetLocalPlayer()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.State.LocalPlayer.Set();
            Assert.IsNotNull(manager.State.LocalPlayer.State);
        }
    }
}