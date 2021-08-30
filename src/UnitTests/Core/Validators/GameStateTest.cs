using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;

namespace StateSharp.UnitTests.Core.Validators
{
    [TestClass]
    public class GameStateTest
    {
        [TestMethod]
        public void SetRemotePlayers()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Validate();
        }
    }
}