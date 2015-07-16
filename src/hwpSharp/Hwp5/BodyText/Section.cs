using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hwpSharp.Hwp5.HwpType;
using OpenMcdf;

namespace hwpSharp.Hwp5.BodyText
{
    public class Section
    {
        public Hwp5DocumentInformation DocumentInformation { get; private set; }

        public Section(Hwp5DocumentInformation docInfo)
        {
            DocumentInformation = docInfo;
        }

        internal Section(CFStream stream, Hwp5DocumentInformation docInfo)
        {
            DocumentInformation = docInfo;

            var bytes = Hwp5Document.GetRawBytesFromStream(stream, docInfo.FileHeader);
            var records = DataRecord.GetRecordsFromBytes(bytes);

            throw new NotImplementedException();
        }
    }
}
