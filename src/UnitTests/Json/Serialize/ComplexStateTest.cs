using Microsoft.VisualStudio.TestTools.UnitTesting;
using State.Complex;
using State.State;
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

        [TestMethod]
        public void FullyInitializedTest()
        {
            var manager = StateManagerConstructor.New<ComplexObject>();
            manager.Init();
            manager.State.Object.Init();
            manager.State.ObjectDictionary.Init();
            manager.State.PrimativeDictionary.Init();
            manager.State.StringDictionary.Init();
            manager.State.StructureDictionary.Init();
            Assert.AreEqual("{\"String\":null,\"Object\":{\"String\":null,\"Object\":null,\"Structure\":{\"String\":null,\"Vector3\":{\"X\":0,\"Y\":0,\"Z\":0},\"Bool\":false,\"Byte\":0,\"Sbyte\":0,\"Char\":'\0',\"Decimal\":0,\"Double\":0,\"Float\":0,\"Int\":0,\"Uint\":0,\"Nint\":0,\"Nuint\":0,\"Long\":0,\"Ulong\":0,\"Short\":0,\"Ushort\":0},\"StringDictionary\":null,\"PrimativeDictionary\":null,\"StructureDictionary\":null,\"ObjectDictionary\":null},\"Structure\":{\"String\":null,\"Vector3\":{\"X\":0,\"Y\":0,\"Z\":0},\"Bool\":false,\"Byte\":0,\"Sbyte\":0,\"Char\":'\0',\"Decimal\":0,\"Double\":0,\"Float\":0,\"Int\":0,\"Uint\":0,\"Nint\":0,\"Nuint\":0,\"Long\":0,\"Ulong\":0,\"Short\":0,\"Ushort\":0},\"StringDictionary\":{},\"PrimativeDictionary\":{},\"StructureDictionary\":{},\"ObjectDictionary\":{}}", StateJsonConverter.Serialize(manager));
        }

        [TestMethod]
        public void FullyInitializedWithValuesTest()
        {
            var manager = StateManagerConstructor.New<ComplexObject>();
            manager.Init();
            manager.State.Object.Init();
            manager.State.Object.State.String.Set("Nested Object");
            manager.State.ObjectDictionary.Init();
            var obj1 = manager.State.ObjectDictionary.Add("Object1");
            obj1.Init();
            manager.State.PrimativeDictionary.Init();
            var pri1 = manager.State.PrimativeDictionary.Add("Primative1");
            pri1.Set(123);
            manager.State.StringDictionary.Init();
            var str1 = manager.State.StringDictionary.Add("String1");
            str1.Set("foo");
            manager.State.Structure.Set(new ComplexStructure("struct", new Vector3(1, 2, 3), true, 1, 2, 'c', 3m, 0.4, 0.5f, 6, 7, 8, 9, 10, 11));
            manager.State.StructureDictionary.Init();
            var struct1 = manager.State.StructureDictionary.Add("Structure1");
            struct1.Set(new ComplexStructure("struct1", new Vector3(1, 2, 3), true, 1, 2, 'c', 3m, 0.4, 0.5f, 6, 7, 8, 9, 10, 11));
            Assert.AreEqual("{\"String\":null,\"Object\":{\"String\":\"Nested Object\",\"Object\":null,\"Structure\":{\"String\":null,\"Vector3\":{\"X\":0,\"Y\":0,\"Z\":0},\"Bool\":false,\"Byte\":0,\"Sbyte\":0,\"Char\":'\0',\"Decimal\":0,\"Double\":0,\"Float\":0,\"Int\":0,\"Uint\":0,\"Long\":0,\"Ulong\":0,\"Short\":0,\"Ushort\":0},\"StringDictionary\":null,\"PrimativeDictionary\":null,\"StructureDictionary\":null,\"ObjectDictionary\":null},\"Structure\":{\"String\":\"struct\",\"Vector3\":{\"X\":1,\"Y\":2,\"Z\":3},\"Bool\":true,\"Byte\":1,\"Sbyte\":2,\"Char\":'c',\"Decimal\":3,\"Double\":0.4,\"Float\":0.5,\"Int\":6,\"Uint\":7,\"Long\":8,\"Ulong\":9,\"Short\":10,\"Ushort\":11},\"StringDictionary\":{\"String1\":\"foo\"},\"PrimativeDictionary\":{\"Primative1\":123},\"StructureDictionary\":{\"Structure1\":{\"String\":\"struct1\",\"Vector3\":{\"X\":1,\"Y\":2,\"Z\":3},\"Bool\":true,\"Byte\":1,\"Sbyte\":2,\"Char\":'c',\"Decimal\":3,\"Double\":0.4,\"Float\":0.5,\"Int\":6,\"Uint\":7,\"Long\":8,\"Ulong\":9,\"Short\":10,\"Ushort\":11}},\"ObjectDictionary\":{\"Object1\":{\"String\":null,\"Object\":null,\"Structure\":{\"String\":null,\"Vector3\":{\"X\":0,\"Y\":0,\"Z\":0},\"Bool\":false,\"Byte\":0,\"Sbyte\":0,\"Char\":'\0',\"Decimal\":0,\"Double\":0,\"Float\":0,\"Int\":0,\"Uint\":0,\"Long\":0,\"Ulong\":0,\"Short\":0,\"Ushort\":0},\"StringDictionary\":null,\"PrimativeDictionary\":null,\"StructureDictionary\":null,\"ObjectDictionary\":null}}}", StateJsonConverter.Serialize(manager));
        }
    }
}
