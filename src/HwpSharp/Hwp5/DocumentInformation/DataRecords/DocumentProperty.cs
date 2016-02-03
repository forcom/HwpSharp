using HwpSharp.Hwp5.HwpType;

namespace HwpSharp.Hwp5.DocumentInformation.DataRecords
{
    public class DocumentProperty : DataRecord
    {
        public const uint DocumentPropertiesTagId = HwpTagBegin;

        public UInt16 SectionCount { get; set; }
        public UInt16 StartPageNumber { get; set; }
        public UInt16 StartFootNoteNumber { get; set; }
        public UInt16 StartEndNoteNumber { get; set; }
        public UInt16 StartPictureNumber { get; set; }
        public UInt16 StartTableNumber { get; set; }
        public UInt16 StartEquationNumber { get; set; }
        public UInt32 ListId { get; set; }
        public UInt32 ParagraphId { get; set; }
        public UInt32 CharacterUnitPosition { get; set; }

        public DocumentProperty(uint level, byte[] bytes, DocumentInformation _ = null)
            : base(DocumentPropertiesTagId, level, (uint) bytes.Length)
        {
            SectionCount = bytes.ToUInt16();
            StartPageNumber = bytes.ToUInt16(2);
            StartFootNoteNumber = bytes.ToUInt16(4);
            StartEndNoteNumber = bytes.ToUInt16(6);
            StartPictureNumber = bytes.ToUInt16(8);
            StartTableNumber = bytes.ToUInt16(10);
            StartEquationNumber = bytes.ToUInt16(12);
            ListId = bytes.ToUInt32(14);
            ParagraphId = bytes.ToUInt32(18);
            CharacterUnitPosition = bytes.ToUInt32(22);
        }
    }
}
