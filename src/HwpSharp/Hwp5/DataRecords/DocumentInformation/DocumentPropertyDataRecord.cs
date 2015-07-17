using System.Linq;
using HwpSharp.Common;
using HwpSharp.Hwp5.HwpType;

namespace HwpSharp.Hwp5.DataRecords.DocumentInformation
{
    public class DocumentPropertyDataRecord : DataRecord
    {
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

        public DocumentPropertyDataRecord(Dword size, byte[] bytes)
        {
            Tag = TagEnum.DocumentProperties;
            Level = 0;
            Size = size;

            if (size != 26)
            {
                throw new HwpCorruptedDataRecordException("Size of DocumentPropertyDataRecord must be 26 bytes.");
            }

            ParseRecord(bytes);
        }

        private void ParseRecord(byte[] bytes)
        {
            SectionCount = bytes.ToUInt16();
            StartPageNumber = bytes.Skip(2).ToUInt16();
            StartFootNoteNumber = bytes.Skip(4).ToUInt16();
            StartEndNoteNumber = bytes.Skip(6).ToUInt16();
            StartPictureNumber = bytes.Skip(8).ToUInt16();
            StartTableNumber = bytes.Skip(10).ToUInt16();
            StartEquationNumber = bytes.Skip(12).ToUInt16();
            ListId = bytes.Skip(14).ToUInt32();
            ParagraphId = bytes.Skip(18).ToUInt32();
            CharacterUnitPosition = bytes.Skip(22).ToUInt32();
        }
    }
}
