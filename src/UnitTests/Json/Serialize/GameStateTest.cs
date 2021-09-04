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
        public void NullTest()
        {
            var manager = StateManagerConstructor.New<GameState>();
            Assert.AreEqual("null", StateJsonConverter.Serialize(manager));
        }

        [TestMethod]
        public void InitializedTest()
        {
            var manager = StateManagerConstructor.New<GameState>();
            manager.Init();
            manager.State.Name.Set("Lobby");
            manager.State.Score.Set(10);
            var localPlayer = manager.State.LocalPlayer.Init();
            localPlayer.Position.Set(new Vector3(0, 1, 2));
            manager.State.RemotePlayers.Init();
            var user1 = manager.State.RemotePlayers.Add("User1");
            user1.Init();
            user1.State.Position.Set(new Vector3(1, 2, 3));
            var user2 = manager.State.RemotePlayers.Add("User2");
            user2.Init();
            user2.State.Position.Set(new Vector3(2, 3, 4));
            Assert.AreEqual("{\"Name\":\"Lobby\",\"Score\":10,\"LocalPlayer\":{\"Position\":{\"X\":0,\"Y\":1,\"Z\":2}},\"RemotePlayers\":{\"User1\":{\"Position\":{\"X\":1,\"Y\":2,\"Z\":3}},\"User2\":{\"Position\":{\"X\":2,\"Y\":3,\"Z\":4}}}}", StateJsonConverter.Serialize(manager));
        }
    }
}
