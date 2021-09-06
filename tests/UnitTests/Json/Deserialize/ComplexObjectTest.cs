using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateSharp.Core;
using StateSharp.Core.States;
using StateSharp.Json;
using StateSharp.Tests.State.Complex;

namespace StateSharp.Tests.UnitTests.Json.Deserialize
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
            var state = StateJsonConverter.Deserialize<IStateObject<ComplexObject>>(manager.GetEventManager(), manager.Path, "{\"String\":null,\"Object\":null,\"Structure\":{\"String\":null,\"Vector3\":{\"X\":0,\"Y\":0,\"Z\":0},\"Bool\":false,\"Byte\":0,\"Sbyte\":0,\"Char\":'\0',\"Decimal\":0,\"Double\":0,\"Float\":0,\"Int\":0,\"Uint\":0,\"Long\":0,\"Ulong\":0,\"Short\":0,\"Ushort\":0},\"StringDictionary\":null,\"PrimativeDictionary\":null,\"StructureDictionary\":null,\"ObjectDictionary\":null}");
        }

        [TestMethod]
        public void FullyInitializedTest()
        {
            var manager = StateManagerConstructor.New<ComplexObject>();
            var state = StateJsonConverter.Deserialize<IStateObject<ComplexObject>>(manager.GetEventManager(), manager.Path, "{\"String\":null,\"Object\":{\"String\":null,\"Object\":null,\"Structure\":{\"String\":null,\"Vector3\":{\"X\":0,\"Y\":0,\"Z\":0},\"Bool\":false,\"Byte\":0,\"Sbyte\":0,\"Char\":'\0',\"Decimal\":0,\"Double\":0,\"Float\":0,\"Int\":0,\"Uint\":0,\"Long\":0,\"Ulong\":0,\"Short\":0,\"Ushort\":0},\"StringDictionary\":null,\"PrimativeDictionary\":null,\"StructureDictionary\":null,\"ObjectDictionary\":null},\"Structure\":{\"String\":null,\"Vector3\":{\"X\":0,\"Y\":0,\"Z\":0},\"Bool\":false,\"Byte\":0,\"Sbyte\":0,\"Char\":'\0',\"Decimal\":0,\"Double\":0,\"Float\":0,\"Int\":0,\"Uint\":0,\"Long\":0,\"Ulong\":0,\"Short\":0,\"Ushort\":0},\"StringDictionary\":{},\"PrimativeDictionary\":{},\"StructureDictionary\":{},\"ObjectDictionary\":{}}");
        }

        [TestMethod]
        public void FullyInitializedWithValuesTest()
        {
            var manager = StateManagerConstructor.New<ComplexObject>();
            var state = StateJsonConverter.Deserialize<IStateObject<ComplexObject>>(manager.GetEventManager(), manager.Path, "{\"String\":null,\"Object\":{\"String\":\"Nested Object\",\"Object\":null,\"Structure\":{\"String\":null,\"Vector3\":{\"X\":0,\"Y\":0,\"Z\":0},\"Bool\":false,\"Byte\":0,\"Sbyte\":0,\"Char\":'\0',\"Decimal\":0,\"Double\":0,\"Float\":0,\"Int\":0,\"Uint\":0,\"Long\":0,\"Ulong\":0,\"Short\":0,\"Ushort\":0},\"StringDictionary\":null,\"PrimativeDictionary\":null,\"StructureDictionary\":null,\"ObjectDictionary\":null},\"Structure\":{\"String\":\"struct\",\"Vector3\":{\"X\":1,\"Y\":2,\"Z\":3},\"Bool\":true,\"Byte\":1,\"Sbyte\":2,\"Char\":'c',\"Decimal\":3,\"Double\":0.4,\"Float\":0.5,\"Int\":6,\"Uint\":7,\"Long\":8,\"Ulong\":9,\"Short\":10,\"Ushort\":11},\"StringDictionary\":{\"String1\":\"foo\"},\"PrimativeDictionary\":{\"Primative1\":123},\"StructureDictionary\":{\"Structure1\":{\"String\":\"struct1\",\"Vector3\":{\"X\":1,\"Y\":2,\"Z\":3},\"Bool\":true,\"Byte\":1,\"Sbyte\":2,\"Char\":'c',\"Decimal\":3,\"Double\":0.4,\"Float\":0.5,\"Int\":6,\"Uint\":7,\"Long\":8,\"Ulong\":9,\"Short\":10,\"Ushort\":11}},\"ObjectDictionary\":{\"Object1\":{\"String\":null,\"Object\":null,\"Structure\":{\"String\":null,\"Vector3\":{\"X\":0,\"Y\":0,\"Z\":0},\"Bool\":false,\"Byte\":0,\"Sbyte\":0,\"Char\":'\0',\"Decimal\":0,\"Double\":0,\"Float\":0,\"Int\":0,\"Uint\":0,\"Long\":0,\"Ulong\":0,\"Short\":0,\"Ushort\":0},\"StringDictionary\":null,\"PrimativeDictionary\":null,\"StructureDictionary\":null,\"ObjectDictionary\":null}}}");
        }
    }
}
