using System;
using System.Collections.Generic;
using System.Linq;
using hwpSharp.Common;
using hwpSharp.Hwp5.DataRecords.DocumentInformation;

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
        public TagEnum Tag { get; internal set; }

        /// <summary>
        /// Gets the level of data record.
        /// </summary>
        public int Level { get; internal set; }

        /// <summary>
        /// Gets the size of data record.
        /// </summary>
        public Dword Size { get; internal set; }

        internal static DataRecord ParseHeaderBytes(byte[] headerBytes)
        {
            if (headerBytes == null)
            {
                throw new ArgumentNullException(nameof(headerBytes));
            }

            TagEnum tag;
            var tagParsed = Enum.TryParse($"{(headerBytes[1] & 0x3)*0x100 + headerBytes[0]}", out tag);
            if (!tagParsed)
            {
                tag = TagEnum.Unknown;
            }

            // 10 bits for level, but 0 <= level <= 3 in the spec.
            var level = headerBytes[2] & 0xf;

            Dword size = (uint) (headerBytes[3]*0x10u + (headerBytes[2] >> 4));

            return new DataRecordImpl(tag, level, size);
        }

        internal static DataRecord CreateRecordFromHeader(DataRecord header, byte[] bytes)
        {
            switch (header.Tag)
            {
                case TagEnum.DocumentProperties:
                    return new DocumentPropertyDataRecord(header.Size, bytes);
                default:
                    return null;
            }
        }

        internal static IEnumerable<DataRecord> GetRecordsFromBytes(byte[] bytes)
        {
            var records = new List<DataRecord>();

            var pos = 0;
            while (pos < bytes.Length)
            {
                if (pos + 4 > bytes.Length)
                {
                    throw new HwpCorruptedDataRecordException("Unable to read data record header.");
                }

                var headerBytes = bytes.Skip(pos).Take(4).ToArray();
                var header = ParseHeaderBytes(headerBytes);
                pos += 4;
                if (header.Size == 0xfff)
                {
                    header.Size = bytes.Skip(pos).ToDword();
                }
                var recordBytes = bytes.Skip(pos).Take((int) (uint) header.Size).ToArray();
                var record = CreateRecordFromHeader(header, recordBytes);

                if (record != null)
                {
                    records.Add(record);
                }

                pos += (int) (uint) header.Size;
            }

            return records;
        }
    }

    internal class DataRecordImpl : DataRecord
    {
        public DataRecordImpl(TagEnum tag, int level, Dword size)
        {
            Tag = tag;
            Level = level;
            Size = size;
        }
    }
}
