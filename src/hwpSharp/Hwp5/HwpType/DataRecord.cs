using System;

namespace hwpSharp.Hwp5.HwpType
{
    /// <summary>
    /// Represents a data record of hwp 5 document.
    /// </summary>
    public abstract class DataRecord
    {
        internal const int HwpTagBegin = 0x10;

        /// <summary>
        /// Gets the tag id of data record.
        /// </summary>
        public TagEnum Tag { get; protected set; }

        /// <summary>
        /// Gets the level of data record.
        /// </summary>
        public int Level { get; protected set; }

        /// <summary>
        /// Gets the size of data record.
        /// </summary>
        public Dword Size { get; protected set; }

        protected DataRecord(byte[] recordBytes)
        {
            if (recordBytes == null)
            {
                throw new ArgumentNullException(nameof(recordBytes));
            }

            TagEnum tag;
            var tagParsed = Enum.TryParse($"{recordBytes[3]*0x100 + (recordBytes[2] >> 6)}", out tag);
            if (!tagParsed)
            {
                throw new ArgumentException(nameof(recordBytes));
            }
            Tag = tag;

            Level = ((recordBytes[2] & 0x3f) << 4) + recordBytes[1] >> 4;

            Size = ((recordBytes[1] & 0xfu) << 8) + recordBytes[0];
            if (Size == 0xfff)
            {
                Size = recordBytes[4];
            }
        }
    }
}
