namespace hwpSharp.HwpType
{
    /// <summary>
    ///     Unsigned 1 byte (0-255).
    /// </summary>
    public struct Byte
    {
        private readonly byte _value;

        private Byte(byte value)
        {
            _value = value;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="byte" /> to a <see cref="Byte" />.
        /// </summary>
        /// <param name="value">The <see cref="byte" /> to convert.</param>
        /// <returns>A new <see cref="Byte" /> with the specified value.</returns>
        public static implicit operator Byte(byte value)
        {
            return new Byte(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="Byte" /> to a <see cref="byte" />.
        /// </summary>
        /// <param name="value">The <see cref="Byte" /> to convert.</param>
        /// <returns>A <see cref="byte" /> that is the specified <see cref="Byte" />'s value.</returns>
        public static implicit operator byte(Byte value)
        {
            return value._value;
        }
    }

    /// <summary>
    ///     'unsigned int' on the 16bit compiler.
    /// </summary>
    public struct Word
    {
        private readonly ushort _value;

        private Word(ushort value)
        {
            _value = value;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="ushort" /> to a <see cref="Word" />.
        /// </summary>
        /// <param name="value">The <see cref="byte" /> to convert.</param>
        /// <returns>A new <see cref="Word" /> with the specified value.</returns>
        public static implicit operator Word(ushort value)
        {
            return new Word(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="Word" /> to a <see cref="ushort" />.
        /// </summary>
        /// <param name="value">The <see cref="Word" /> to convert.</param>
        /// <returns>A <see cref="ushort" /> that is the specified <see cref="Word" />'s value.</returns>
        public static implicit operator ushort(Word value)
        {
            return value._value;
        }
    }

    /// <summary>
    ///     'unsigned long' on the 16bit compiler.
    /// </summary>
    public struct Dword
    {
        private readonly uint _value;

        private Dword(uint value)
        {
            _value = value;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="uint" /> to a <see cref="Dword" />.
        /// </summary>
        /// <param name="value">The <see cref="uint" /> to convert.</param>
        /// <returns>A new <see cref="Dword" /> with the specified value.</returns>
        public static implicit operator Dword(uint value)
        {
            return new Dword(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="Dword" /> to a <see cref="uint" />.
        /// </summary>
        /// <param name="value">The <see cref="Dword" /> to convert.</param>
        /// <returns>A <see cref="uint" /> that is the specified <see cref="Dword" />'s value.</returns>
        public static implicit operator uint(Dword value)
        {
            return value._value;
        }
    }

    /// <summary>
    ///     Unicode based character, default code in HWP.
    /// </summary>
    public struct Wchar
    {
        private readonly char _value;

        private Wchar(char value)
        {
            _value = value;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="char" /> to a <see cref="Wchar" />.
        /// </summary>
        /// <param name="value">The <see cref="char" /> to convert.</param>
        /// <returns>A new <see cref="Wchar" /> with the specified value.</returns>
        public static implicit operator Wchar(char value)
        {
            return new Wchar(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="Wchar" /> to a <see cref="char" />.
        /// </summary>
        /// <param name="value">The <see cref="Wchar" /> to convert.</param>
        /// <returns>A <see cref="char" /> that is the specified <see cref="Wchar" />'s value.</returns>
        public static implicit operator char(Wchar value)
        {
            return value._value;
        }
    }

    /// <summary>
    ///     HWP internal unit, represented as 1/1700 inch.
    /// </summary>
    public struct HwpUnit
    {
        private readonly uint _value;

        private HwpUnit(uint value)
        {
            _value = value;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="uint" /> to a <see cref="HwpUnit" />.
        /// </summary>
        /// <param name="value">The <see cref="uint" /> to convert.</param>
        /// <returns>A new <see cref="HwpUnit" /> with the specified value.</returns>
        public static implicit operator HwpUnit(uint value)
        {
            return new HwpUnit(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="HwpUnit" /> to a <see cref="uint" />.
        /// </summary>
        /// <param name="value">The <see cref="HwpUnit" /> to convert.</param>
        /// <returns>A <see cref="uint" /> that is the specified <see cref="HwpUnit" />'s value.</returns>
        public static implicit operator uint(HwpUnit value)
        {
            return value._value;
        }
    }

    /// <summary>
    ///     HWP internal unit, represented as 1/1700 inch with sign.
    /// </summary>
    public struct SHwpUnit
    {
        private readonly int _value;

        private SHwpUnit(int value)
        {
            _value = value;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="int" /> to a <see cref="SHwpUnit" />.
        /// </summary>
        /// <param name="value">The <see cref="int" /> to convert.</param>
        /// <returns>A new <see cref="SHwpUnit" /> with the specified value.</returns>
        public static implicit operator SHwpUnit(int value)
        {
            return new SHwpUnit(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="SHwpUnit" /> to a <see cref="int" />.
        /// </summary>
        /// <param name="value">The <see cref="SHwpUnit" /> to convert.</param>
        /// <returns>A <see cref="int" /> that is the specified <see cref="SHwpUnit" />'s value.</returns>
        public static implicit operator int(SHwpUnit value)
        {
            return value._value;
        }
    }

    /// <summary>
    ///     'unsigned __int8'
    /// </summary>
    public struct UInt8
    {
        private readonly byte _value;

        private UInt8(byte value)
        {
            _value = value;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="byte" /> to a <see cref="UInt8" />.
        /// </summary>
        /// <param name="value">The <see cref="byte" /> to convert.</param>
        /// <returns>A new <see cref="UInt8" /> with the specified value.</returns>
        public static implicit operator UInt8(byte value)
        {
            return new UInt8(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="UInt8" /> to a <see cref="byte" />.
        /// </summary>
        /// <param name="value">The <see cref="UInt8" /> to convert.</param>
        /// <returns>A <see cref="byte" /> that is the specified <see cref="UInt8" />'s value.</returns>
        public static implicit operator byte(UInt8 value)
        {
            return value._value;
        }
    }

    /// <summary>
    ///     'unsigned __int16'
    /// </summary>
    public struct UInt16
    {
        private readonly ushort _value;

        private UInt16(ushort value)
        {
            _value = value;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="ushort" /> to a <see cref="UInt16" />.
        /// </summary>
        /// <param name="value">The <see cref="ushort" /> to convert.</param>
        /// <returns>A new <see cref="UInt16" /> with the specified value.</returns>
        public static implicit operator UInt16(ushort value)
        {
            return new UInt16(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="UInt16" /> to a <see cref="ushort" />.
        /// </summary>
        /// <param name="value">The <see cref="UInt16" /> to convert.</param>
        /// <returns>A <see cref="ushort" /> that is the specified <see cref="UInt16" />'s value.</returns>
        public static implicit operator ushort(UInt16 value)
        {
            return value._value;
        }
    }

    /// <summary>
    ///     'unsigned __int32'
    /// </summary>
    public struct UInt32
    {
        private readonly uint _value;

        private UInt32(uint value)
        {
            _value = value;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="uint" /> to a <see cref="UInt32" />.
        /// </summary>
        /// <param name="value">The <see cref="uint" /> to convert.</param>
        /// <returns>A new <see cref="UInt32" /> with the specified value.</returns>
        public static implicit operator UInt32(uint value)
        {
            return new UInt32(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="UInt32" /> to a <see cref="uint" />.
        /// </summary>
        /// <param name="value">The <see cref="UInt32" /> to convert.</param>
        /// <returns>A <see cref="uint" /> that is the specified <see cref="UInt32" />'s value.</returns>
        public static implicit operator uint(UInt32 value)
        {
            return value._value;
        }
    }

    /// <summary>
    ///     'signed __int8'
    /// </summary>
    public struct Int8
    {
        private readonly sbyte _value;

        private Int8(sbyte value)
        {
            _value = value;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="sbyte" /> to a <see cref="Int8" />.
        /// </summary>
        /// <param name="value">The <see cref="sbyte" /> to convert.</param>
        /// <returns>A new <see cref="Int8" /> with the specified value.</returns>
        public static implicit operator Int8(sbyte value)
        {
            return new Int8(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="Int8" /> to a <see cref="sbyte" />.
        /// </summary>
        /// <param name="value">The <see cref="Int8" /> to convert.</param>
        /// <returns>A <see cref="sbyte" /> that is the specified <see cref="Int8" />'s value.</returns>
        public static implicit operator sbyte(Int8 value)
        {
            return value._value;
        }
    }

    /// <summary>
    ///     'signed __int16'
    /// </summary>
    public struct Int16
    {
        private readonly short _value;

        private Int16(short value)
        {
            _value = value;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="short" /> to a <see cref="Int16" />.
        /// </summary>
        /// <param name="value">The <see cref="short" /> to convert.</param>
        /// <returns>A new <see cref="Int16" /> with the specified value.</returns>
        public static implicit operator Int16(short value)
        {
            return new Int16(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="Int16" /> to a <see cref="short" />.
        /// </summary>
        /// <param name="value">The <see cref="Int16" /> to convert.</param>
        /// <returns>A <see cref="short" /> that is the specified <see cref="Int16" />'s value.</returns>
        public static implicit operator short(Int16 value)
        {
            return value._value;
        }
    }

    /// <summary>
    ///     'signed __int32'
    /// </summary>
    public struct Int32
    {
        private readonly int _value;

        private Int32(int value)
        {
            _value = value;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="int" /> to a <see cref="Int32" />.
        /// </summary>
        /// <param name="value">The <see cref="int" /> to convert.</param>
        /// <returns>A new <see cref="Int32" /> with the specified value.</returns>
        public static implicit operator Int32(int value)
        {
            return new Int32(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="Int32" /> to a <see cref="int" />.
        /// </summary>
        /// <param name="value">The <see cref="Int32" /> to convert.</param>
        /// <returns>A <see cref="int" /> that is the specified <see cref="Int32" />'s value.</returns>
        public static implicit operator int(Int32 value)
        {
            return value._value;
        }
    }

    /// <summary>
    ///     Same as INT16
    /// </summary>
    public struct HwpUnit16
    {
        private readonly short _value;

        private HwpUnit16(short value)
        {
            _value = value;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="short" /> to a <see cref="HwpUnit16" />.
        /// </summary>
        /// <param name="value">The <see cref="short" /> to convert.</param>
        /// <returns>A new <see cref="HwpUnit16" /> with the specified value.</returns>
        public static implicit operator HwpUnit16(short value)
        {
            return new HwpUnit16(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="HwpUnit16" /> to a <see cref="short" />.
        /// </summary>
        /// <param name="value">The <see cref="HwpUnit16" /> to convert.</param>
        /// <returns>A <see cref="short" /> that is the specified <see cref="HwpUnit16" />'s value.</returns>
        public static implicit operator short(HwpUnit16 value)
        {
            return value._value;
        }
    }

    /// <summary>
    ///     Represents RGB value(0x00bbggrr) as decimal.
    /// </summary>
    public struct ColorRef
    {
        public byte Red { get; }
        public byte Green { get; }
        public byte Blue { get; }

        private ColorRef(uint value)
        {
            Red = (byte) (value & 0xff);
            Green = (byte) ((value >> 8) & 0xff);
            Blue = (byte) ((value >> 16) & 0xff);
        }

        public ColorRef(byte rr, byte gg, byte bb)
        {
            Red = rr;
            Green = gg;
            Blue = bb;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="uint" /> to a <see cref="ColorRef" />.
        /// </summary>
        /// <param name="value">The <see cref="uint" /> to convert.</param>
        /// <returns>A new <see cref="ColorRef" /> with the specified value.</returns>
        public static implicit operator ColorRef(uint value)
        {
            return new ColorRef(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="ColorRef" /> to a <see cref="uint" />.
        /// </summary>
        /// <param name="value">The <see cref="ColorRef" /> to convert.</param>
        /// <returns>A <see cref="uint" /> that is the specified <see cref="ColorRef" />'s value.</returns>
        public static implicit operator uint(ColorRef value)
        {
            return value.Red + value.Green*0x100u + value.Blue*0x10000u;
        }
    }
}