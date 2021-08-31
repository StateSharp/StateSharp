using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Core;
using StateSharp.Json;

namespace StateSharp.UnitTests.Json.Serialize
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
            Assert.AreEqual("{\"Name\":null,\"Score\":0,\"LocalPlayer\":null,\"RemotePlayers\":null}", json);
        }

        [TestMethod]
        public void GameStateFullyInitialized()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            manager.State.Name.Set("Game1");
            manager.State.Score.Set(3);
            manager.State.LocalPlayer.Init();
            manager.State.LocalPlayer.State.Position.Set(new Vector3(1, 2, 3));
            manager.State.RemotePlayers.Init();
            var user1 = manager.State.RemotePlayers.Add("User1");
            user1.Init();
            user1.State.Position.Set(new Vector3(2, 3, 4));
            var json = StateJsonConverter.Serialize(manager);
            Assert.AreEqual("{\"Name\":\"Game1\",\"Score\":3,\"LocalPlayer\":{\"Position\":{\"X\":1,\"Y\":2,\"Z\":3}},\"RemotePlayers\":{\"User1\":{\"Position\":{\"X\":2,\"Y\":3,\"Z\":4}}}}", json);
        }
    }
}
