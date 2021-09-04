using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.Complex;
using StateSharp.Core;
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

        [TestMethod]
        public void BaseInitializedTest()
        {
            var manager = StateManagerConstructor.New<ComplexObject>();
            var state = StateJsonConverter.Deserialize<IStateObject<ComplexObject>>(manager.GetEventManager(), manager.Path, "{\"String\":null,\"Object\":null,\"Structure\":{\"String\":null,\"Vector3\":{\"X\":0,\"Y\":0,\"Z\":0},\"Bool\":false,\"Byte\":0,\"Sbyte\":0,\"Char\":'\0',\"Decimal\":0,\"Double\":0,\"Float\":0,\"Int\":0,\"Uint\":0,\"Nint\":0,\"Nuint\":0,\"Long\":0,\"Ulong\":0,\"Short\":0,\"Ushort\":0},\"StringDictionary\":null,\"PrimativeDictionary\":null,\"StructureDictionary\":null,\"ObjectDictionary\":null}");
        }
    }
}
