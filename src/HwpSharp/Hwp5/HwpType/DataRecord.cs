using System;
using System.Collections.Generic;
using System.Linq;
using HwpSharp.Common;
using HwpSharp.Hwp5.BodyText.DataRecords;
using HwpSharp.Hwp5.DocumentInformation.DataRecords;

namespace HwpSharp.Hwp5.HwpType
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
        public uint TagId { get; internal set; }

        /// <summary>
        /// Gets the level of data record.
        /// </summary>
        public uint Level { get; internal set; }

        /// <summary>
        /// Gets the size of data record.
        /// </summary>
        public DWord Size { get; internal set; }

        public byte[] RawBytes { get; internal set; }

        protected DataRecord(uint tagId, uint level, DWord size, byte[] bytes)
        {
            TagId = tagId;
            Level = level;
            Size = size;

            RawBytes = bytes;
        }

        internal static DataRecord GetRecordFromBytes(byte[] bytes, out int length,
            DocumentInformation.DocumentInformation docInfo = null)
        {
            return GetRecordFromBytes(bytes, 0, out length, docInfo);
        }

        internal static DataRecord GetRecordFromBytes(byte[] bytes, int offset, out int length,
            DocumentInformation.DocumentInformation docInfo = null)
        {
            if (offset >= bytes.Length)
            {
                throw new IndexOutOfRangeException();
            }

            length = 0;

            var header = bytes.ToDWord(offset);
            length += 4;

            var tagId = header & 0x3FF;
            var level = (header >> 10) & 0x3FF;
            var size = header >> 20;
            if (size == 0xfff)
            {
                size = bytes.ToDWord(offset + length);
                length += 4;
            }

            var recordBytes = bytes.Skip(offset + length).Take((int) size).ToArray();
            length += (int) size;

            return DataRecordFactory.Create(tagId, level, recordBytes, docInfo);
        }

        internal static IEnumerable<DataRecord> GetRecordsFromBytes(byte[] bytes, DocumentInformation.DocumentInformation docInfo = null)
        {
            var records = new List<DataRecord>();

            var pos = 0;
            while (pos < bytes.Length)
            {
                int length;
                var record = GetRecordFromBytes(bytes, pos, out length);
                if (record != null)
                {
                    records.Add(record);
                }

                pos += length;
            }

            return records;
        }

        protected internal byte[] GetHeaderBytes()
        {
            if (Size >= 0xfff)
            {
                DWord header = 0xfff | (Level << 10) | TagId;
                var result = new byte[8];
                header.ToBytes().CopyTo(result, 0);
                Size.ToBytes().CopyTo(result, 4);
                return result;
            }
            else
            {
                DWord header = (Size << 20) | (Level << 10) | TagId;
                return header.ToBytes();
            }
        }

        protected internal byte[] GetBytes()
        {
            var headerBytes = GetHeaderBytes();

            if (RawBytes == null || RawBytes.Length == 0)
            {
                return headerBytes;
            }

            var result = new byte[headerBytes.Length + RawBytes.Length];
            headerBytes.CopyTo(result, 0);
            RawBytes.CopyTo(result, headerBytes.Length);
            return result;
        }
    }

    public static class DataRecordFactory
    {
        private static readonly Dictionary<uint, Type> DataRecordTypes = new Dictionary<uint, Type>
        {
            {DocumentProperty.DocumentPropertiesTagId, typeof (DocumentProperty)},
            {IdMapping.IdMappingsTagId, typeof (IdMapping)},
            {BinData.BinDataTagId, typeof (BinData)},
            {FaceName.FaceNameTagId, typeof (FaceName)},
            {BorderFill.BorderFillTagId, typeof (BorderFill)},

            {ParagraphHeader.ParagraphHeaderTagId, typeof (ParagraphHeader)},
            {ParagraphText.ParagraphTextTagId, typeof (ParagraphText)},
        };

        public static void RegisterType(uint tagId, Type type)
        {
            DataRecordTypes[tagId] = type;
        }

        public static DataRecord Create(uint tagId, uint level, byte[] data, DocumentInformation.DocumentInformation docInfo = null)
        {
            if (!DataRecordTypes.ContainsKey(tagId))
            {
                return new DataRecordImpl(tagId, level, data);
            }

            var ctor =
                DataRecordTypes[tagId].GetConstructor(new[]
                {typeof (uint), typeof (byte[]), typeof (DocumentInformation.DocumentInformation)});
            if (ctor == null)
            {
                throw new HwpDataRecordConstructorException();
            }
            return ctor.Invoke(new object[] {level, data, docInfo}) as DataRecord;
        }
    }

    internal class DataRecordImpl : DataRecord
    {
        public byte[] Data { get; private set; }

        public DataRecordImpl(uint tagId, uint level, byte[] data = null)
            : base(tagId, level, (uint) (data?.Length ?? 0), data)
        {
            Data = data;
        }
    }
}
