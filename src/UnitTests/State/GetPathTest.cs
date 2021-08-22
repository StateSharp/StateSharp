using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.State;
using StateSharp.Server;
using System.Net;

namespace UnitTests.State
{
    [TestClass]
    public class GetPathTest
    {
        [TestMethod]
        public void StatePlayers()
        {
            var server = StateSharpServerConstructor.New<GameState>("State", IPAddress.Any, 8080);
            Assert.AreEqual("State.Players", server.State.Players.GetPath());
        }

        [TestMethod]
        public void StatePlayersUsername()
        {
            var server = StateSharpServerConstructor.New<GameState>("State", IPAddress.Any, 8080);
            var user1 = server.State.Players.Add("User1");
            Assert.AreEqual("State.Players[User1]", user1.GetPath());
        }
    }
}