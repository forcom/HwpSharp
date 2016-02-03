using System;
using System.Collections.Generic;
using System.Linq;
using HwpSharp.Common;
using HwpSharp.Hwp5.DocumentInformation.DataRecords;
using HwpSharp.Hwp5.HwpType;
using OpenMcdf;

namespace HwpSharp.Hwp5.DocumentInformation
{
    /// <summary>
    /// Represents a hwp 5.0 document information.
    /// </summary>
    public class DocumentInformation : IDocumentInformation
    {
        public IList<DataRecord> DataRecords { get; }

        public FileHeader FileHeader { get; }

        public DocumentProperty DocumentProperty { get; }

        public IdMapping IdMappings { get; }

        /// <summary>
        /// Creates a blank <see cref="DocumentInformation"/> instance with specified file header.
        /// </summary>
        /// <param name="fileHeader">Hwp 5 file header</param>
        public DocumentInformation(FileHeader fileHeader)
        {
            FileHeader = fileHeader;
        }

        internal DocumentInformation(CFStream stream, FileHeader fileHeader)
        {
            FileHeader = fileHeader;

            var bytes = Document.GetRawBytesFromStream(stream, fileHeader);

            var records = DataRecord.GetRecordsFromBytes(bytes);

            try
            {
                DataRecords = records as IList<DataRecord> ?? records.ToList();
                DocumentProperty = (DocumentProperty) DataRecords.Single(r => r.TagId == DocumentProperty.DocumentPropertiesTagId);
                IdMappings = (IdMapping) DataRecords.Single(r => r.TagId == IdMapping.IdMappingsTagId);
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
