using System;
using HwpSharp.Hwp5.HwpType;

namespace HwpSharp.Hwp5.BodyText.DataRecords
{
    public class ParagraphHeader : DataRecord
    {
        public const uint ParagraphHeaderTagId = HwpTagBegin + 50;

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

        public ParagraphHeader(uint level, byte[] bytes,
            DocumentInformation.DocumentInformation docInfo = null)
            : base(ParagraphHeaderTagId, level, (uint) bytes.Length)
        {
            Length = bytes.ToUInt32();
            if ((Length & 0x80000000u) != 0)
            {
                Length &= 0x7fffffffu;
            }

            ControlMask = bytes.ToUInt32(4);
            
            ParagraphShapeId = bytes.ToUInt16(8);

            ParagraphStyleId = bytes[10];

            ColumnKind columnKind;
            if (!Enum.TryParse(bytes[11].ToString(), out columnKind))
            {
                columnKind = ColumnKind.None;
            }
            ColumnType = columnKind;

            CharacterShapeCount = bytes.ToUInt16(12);

            ParagraphRangeCount = bytes.ToUInt16(14);

            LineAlignCount = bytes.ToUInt16(16);

            ParagraphId = bytes.ToUInt32(18);

            if (bytes.Length >= 24)
            {
                HistoryMergeParagraphFlag = bytes.ToUInt16(22);
            }
        }
    }
}
