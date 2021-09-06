using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateSharp.Core;
using StateSharp.Tests.State.State;

namespace StateSharp.Tests.UnitTests.Core.States.Structure
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void SetScore()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            manager.State.Score.Set(10);
            Assert.IsNotNull(manager.State);
            Assert.IsNotNull(manager.State.Score);
            Assert.AreEqual(10, manager.State.Score.State);
        }
    }
}
