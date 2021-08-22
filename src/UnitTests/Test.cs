using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateSharp.Server;
using System.Net;
using UnitTests.State;

namespace UnitTests
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Basic()
        {
            var server = new StateSharpServer<GameState>(IPAddress.Any, 8080);
        }
    }
}
