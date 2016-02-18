using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HwpSharp.Hwp5.BodyText.DataRecords;
using HwpSharp.Hwp5.HwpType;
using OpenMcdf;

namespace HwpSharp.Hwp5.BodyText
{
    public class Section
    {
        public DocumentInformation.DocumentInformation DocumentInformation { get; private set; }
        
        public List<DataRecord> DataRecords { get; }

        public Section(DocumentInformation.DocumentInformation docInfo)
        {
            DocumentInformation = docInfo;
            DataRecords = new List<DataRecord>();
        }

        internal Section(CFStream stream, DocumentInformation.DocumentInformation docInfo)
        {
            DocumentInformation = docInfo;

            var bytes = Document.GetRawBytesFromStream(stream, docInfo.FileHeader, docInfo.FileHeader.Published);
            DataRecords = new List<DataRecord>(DataRecord.GetRecordsFromBytes(bytes, docInfo));
        }
    }
}
