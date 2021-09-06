using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateSharp.Core;
using StateSharp.Tests.State.State;

namespace StateSharp.Tests.UnitTests.Core.States.Object
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void SetLocalPlayer()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            manager.State.LocalPlayer.Init();
            Assert.IsNotNull(manager.State.LocalPlayer.State);
        }
    }
}
