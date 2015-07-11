using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hwpSharp.Common;
using hwpSharp.Hwp5.HwpType;

namespace hwpSharp.Hwp5.DataRecords
{
    public class DocumentPropertyDataRecord : DataRecord
    {
        public HwpType.UInt16 SectionCount { get; set; }
        public HwpType.UInt16 StartPageNumber { get; set; }
        public HwpType.UInt16 StartFootNoteNumber { get; set; }
        public HwpType.UInt16 StartEndNoteNumber { get; set; }
        public HwpType.UInt16 StartPictureNumber { get; set; }
        public HwpType.UInt16 StartTableNumber { get; set; }
        public HwpType.UInt16 StartEquationNumber { get; set; }
        public HwpType.UInt32 ListId { get; set; }
        public HwpType.UInt32 ParagraphId { get; set; }
        public HwpType.UInt32 CharacterUnitPosition { get; set; }

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
            SectionCount = (ushort) (bytes[3]*0x100u + bytes[4]);
        }
    }
}
