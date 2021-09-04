using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.Complex;
using StateSharp.Core;
using StateSharp.Json;

namespace StateSharp.UnitTests.Json.Serialize
{
    [TestClass]
    public class ComplexStateTest
    {
        [TestMethod]
        public void NullTest()
        {
            var manager = StateManagerConstructor.New<ComplexObject>();
            Assert.AreEqual("null", StateJsonConverter.Serialize(manager));
        }

        [TestMethod]
        public void BaseInitializedTest()
        {
            var manager = StateManagerConstructor.New<ComplexObject>();
            manager.Init();
            Assert.AreEqual("{\"String\":null,\"Object\":null,\"Structure\":{\"String\":null,\"Vector3\":{\"X\":0,\"Y\":0,\"Z\":0},\"Bool\":false,\"Byte\":0,\"Sbyte\":0,\"Char\":'\0',\"Decimal\":0,\"Double\":0,\"Float\":0,\"Int\":0,\"Uint\":0,\"Nint\":0,\"Nuint\":0,\"Long\":0,\"Ulong\":0,\"Short\":0,\"Ushort\":0},\"StringDictionary\":null,\"PrimativeDictionary\":null,\"StructureDictionary\":null,\"ObjectDictionary\":null}", StateJsonConverter.Serialize(manager));
        }
    }
}
