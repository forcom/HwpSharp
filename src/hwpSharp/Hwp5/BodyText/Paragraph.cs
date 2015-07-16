using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hwpSharp.Hwp5.DataRecords.BodyText;
using hwpSharp.Hwp5.HwpType;

namespace hwpSharp.Hwp5.BodyText
{
    public class Paragraph
    {
        public ParagraphHeaderDataRecord ParagraphHeader { get; }
        public ParagraphTextDataRecord ParagraphText { get; }

        public Paragraph()
        {
            throw new NotImplementedException();
        }

        internal Paragraph(IEnumerable<DataRecord> records)
        {
            foreach (var record in records)
            {
                switch (record.Tag)
                {
                    case TagEnum.ParagraphHeader:
                        if (ParagraphHeader != null)
                        {
                            throw new ArgumentException("Duplicated ParagraphHeaderDataRecord");
                        }
                        ParagraphHeader = record as ParagraphHeaderDataRecord;
                        break;
                    case TagEnum.ParagraphText:
                        if (ParagraphText != null)
                        {
                            throw new ArgumentException("Duplicated ParagraphTextDataRecord");
                        }
                        ParagraphText = record as ParagraphTextDataRecord;
                        break;
                    default:
                        throw new ArgumentException("Invalid DataRecord for Paragraph");
                }
            }
        }
    }
}
