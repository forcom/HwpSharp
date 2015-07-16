using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hwpSharp.Common;
using hwpSharp.Hwp5.HwpType;

namespace hwpSharp.Hwp5.DataRecords.BodyText
{
    public class ParagraphHeaderDataRecord : DataRecord
    {
        [Flags]
        public enum ColumnKind : byte
        {
            None = 0x00,
            Area = 0x01,
            MultiColumn = 0x02,
            Page = 0x04,
            Column = 0x08
        }

        public HwpType.UInt32 Length { get; set; }
        public HwpType.UInt32 ControlMask { get; set; }
        public HwpType.UInt16 ParagraphShapeId { get; set; }
        public UInt8 ParagraphStyleId { get; set; }
        public ColumnKind ColumnType { get; set; }
        public HwpType.UInt16 CharacterShapeCount { get; set; }
        public HwpType.UInt16 ParagraphRangeCount { get; set; }
        public HwpType.UInt16 LineAlignCount { get; set; }
        public HwpType.UInt32 ParagraphId { get; set; }
        public HwpType.UInt16 HistoryMergeParagraphFlag { get; set; }

        public ParagraphHeaderDataRecord(Dword size, byte[] bytes, Hwp5DocumentInformation docInfo)
        {
            Tag = TagEnum.ParagraphHeader;
            Level = 0;
            Size = size;

            ParseRecord(bytes, docInfo);
        }

        private void ParseRecord(byte[] bytes, Hwp5DocumentInformation docInfo)
        {
            Length = bytes.ToUInt32();
            if ((Length & 0x80000000u) != 0)
            {
                Length &= 0x7fffffffu;
            }

            ControlMask = bytes.Skip(4).ToUInt32();

            // TODO : Replace ID with ParagraphShapeDataRecord
            ParagraphShapeId = bytes.Skip(8).ToUInt16();

            // TODO : Replace ID with StyleDataRecord
            ParagraphStyleId = bytes[10];

            ColumnKind columnKind;
            if (!Enum.TryParse(bytes[11].ToString(), out columnKind))
            {
                columnKind = ColumnKind.None;
            }
            ColumnType = columnKind;

            CharacterShapeCount = bytes.Skip(12).ToUInt16();

            ParagraphRangeCount = bytes.Skip(14).ToUInt16();

            LineAlignCount = bytes.Skip(16).ToUInt16();

            ParagraphId = bytes.Skip(18).ToUInt32();

            if (bytes.Length >= 24)
            {
                HistoryMergeParagraphFlag = bytes.Skip(22).ToUInt16();
            }
        }
    }
}
