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
        public void GameStateNull()
        {
            var manager = StateManagerConstructor.New<GameState>();
            var json = StateJsonConverter.Serialize(manager);
            Assert.AreEqual("null", json);
        }

        [TestMethod]
        public void GameStateInitialized()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            var json = StateJsonConverter.Serialize(manager);
            Assert.AreEqual("{\"Score\": 0,\"LocalPlayer\": null,\"RemotePlayers\": null}", json);
        }
    }
}