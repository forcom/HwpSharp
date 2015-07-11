using System;
using System.Linq;
using hwpSharp.Common;
using hwpSharp.Hwp5.DataRecords;
using hwpSharp.Hwp5.HwpType;
using OpenMcdf;

namespace hwpSharp.Hwp5
{
    /// <summary>
    /// Represents a hwp 5.0 document information.
    /// </summary>
    public class Hwp5DocumentInformation : IDocumentInformation
    {
        public Hwp5FileHeader FileHeader { get; private set; }

        public DocumentPropertyDataRecord DocumentProperty { get; private set; }

        /// <summary>
        /// Creates a blank <see cref="Hwp5DocumentInformation"/> instance with specified file header.
        /// </summary>
        /// <param name="fileHeader">Hwp 5 file header</param>
        public Hwp5DocumentInformation(Hwp5FileHeader fileHeader)
        {
            FileHeader = fileHeader;
        }

        internal Hwp5DocumentInformation(CFStream stream, Hwp5FileHeader fileHeader)
        {
            FileHeader = fileHeader;

            var bytes = Hwp5Document.GetRawBytesFromStream(stream, fileHeader);

            var records = DataRecord.GetRecordsFromBytes(bytes);

            try
            {
                DocumentProperty = (DocumentPropertyDataRecord) records.Single(r => r.Tag == TagEnum.DocumentProperties);
            }
            catch (InvalidOperationException exception)
            {
                throw new HwpFileFormatException(
                    "This document does not contains a document property data record or contains more than one document property data record.",
                    exception);
            }
        }
    }
}
