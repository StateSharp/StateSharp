using StateSharp.Tests.State.State;

namespace StateSharp.Tests.State.Complex
{
    public struct ComplexStructure
    {
        public ComplexStructure(string @string, Vector3 @vector3, bool @bool, byte @byte, sbyte @sbyte, char @char, decimal @decimal, double @double, float @float, int @int, uint @uint, long @long, ulong @ulong, short @short, ushort @ushort)
        {
            String = @string;
            Vector3 = vector3;
            Bool = @bool;
            Byte = @byte;
            Sbyte = @sbyte;
            Char = @char;
            Decimal = @decimal;
            Double = @double;
            Float = @float;
            Int = @int;
            Uint = @uint;
            Long = @long;
            Ulong = @ulong;
            Short = @short;
            Ushort = @ushort;
        }

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
        public long Long { get; private set; }
        public ulong Ulong { get; private set; }
        public short Short { get; private set; }
        public ushort Ushort { get; private set; }
    }
}
