using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateSharp.Core;
using StateSharp.Tests.State.State;

namespace StateSharp.Tests.UnitTests.Core.Validators
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
