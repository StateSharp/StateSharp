using State.State;

namespace State.Complex
{
    public struct ComplexStructure
    {
        // String
        public string String { get; private set; }

        // Struct
        public Vector3 Vector3 { get; private set; }

        // Primatives
        public bool Bool { get; private set; }
        public byte Byte { get; private set; }
        public sbyte Sbyte { get; private set; }
        public char Char { get; private set; }
        public decimal Decimal { get; private set; }
        public double Double { get; private set; }
        public float Float { get; private set; }
        public int Int { get; private set; }
        public uint Uint { get; private set; }
        public nint Nint { get; private set; }
        public nuint Nuint { get; private set; }
        public long Long { get; private set; }
        public ulong Ulong { get; private set; }
        public short Short { get; private set; }
        public ushort Ushort { get; private set; }
    }
}
