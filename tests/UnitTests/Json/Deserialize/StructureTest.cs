using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateSharp.Core.States;
using StateSharp.Json;
using StateSharp.Tests.State.State;

namespace StateSharp.Tests.UnitTests.Json.Deserialize
{
    [TestClass]
    public class StructureTest
    {
        [TestMethod]
        public void Vector3Test()
        {
            var state = StateJsonConverter.Deserialize<IStateStructure<Vector3>>(null, "State", "{\"X\":1,\"Y\":2,\"Z\":3}");
            Assert.AreEqual(1, state.State.X);
            Assert.AreEqual(2, state.State.Y);
            Assert.AreEqual(3, state.State.Z);
        }
    }
}
