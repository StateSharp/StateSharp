using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.Complex;
using StateSharp.Core.States;
using StateSharp.Json;

namespace StateSharp.UnitTests.Json.Deserialize
{
    [TestClass]
    public class ComplexObjectTest
    {
        [TestMethod]
        public void DeserializeComplexObjectTest()
        {
            var state = StateJsonConverter.Deserialize<IStateObject<ComplexObject>>(null, "State", "null");
            Assert.IsNull(state.State);
        }
    }
}
