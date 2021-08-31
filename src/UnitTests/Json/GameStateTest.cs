using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;
using StateSharp.Json;

namespace StateSharp.UnitTests.Json
{
    [TestClass]
    public class GameStateTest
    {
        [TestMethod]
        public void GameStateSerialize()
        {
            var manager = StateManagerConstructor.New<GameState>();
            var json = StateJsonConverter.Serialize(manager);
            Assert.AreEqual("null", json);
        }
    }
}