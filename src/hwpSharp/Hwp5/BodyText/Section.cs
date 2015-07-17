using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HwpSharp.Hwp5.HwpType;
using OpenMcdf;

namespace HwpSharp.Hwp5.BodyText
{
    public class Section
    {
        public Hwp5DocumentInformation DocumentInformation { get; private set; }

        public List<Paragraph> Paragraphs { get; }

        public Section(Hwp5DocumentInformation docInfo)
        {
            DocumentInformation = docInfo;
            Paragraphs = new List<Paragraph>();
        }

        internal Section(CFStream stream, Hwp5DocumentInformation docInfo)
        {
            DocumentInformation = docInfo;
            Paragraphs = new List<Paragraph>();

            var bytes = Hwp5Document.GetRawBytesFromStream(stream, docInfo.FileHeader);
            var records = DataRecord.GetRecordsFromBytes(bytes, docInfo);

            var initRecords = new List<DataRecord>();
            foreach (var record in records)
            {
                if (record.Tag == TagEnum.ParagraphHeader)
                {
                    if (initRecords.Count > 0)
                    {
                        var paragraph = new Paragraph(initRecords);

                        Paragraphs.Add(paragraph);

                        initRecords = new List<DataRecord>();
                    }
                }
                initRecords.Add(record);
            }

            if (initRecords.Count > 0)
            {
                var paragraph = new Paragraph(initRecords);

                Paragraphs.Add(paragraph);
            }
        }
    }
}
