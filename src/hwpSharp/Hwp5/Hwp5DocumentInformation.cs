using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
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

            var bytes = GetBytesFromStream(stream, fileHeader);

            var records = GetRecordsFromBytes(bytes);

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

        private IEnumerable<DataRecord> GetRecordsFromBytes(byte[] bytes)
        {
            var records = new List<DataRecord>();

            var pos = 0;
            while (pos < bytes.Length)
            {
                if (pos + 4 > bytes.Length)
                {
                    throw new HwpCorruptedDataRecordException("Unable to read data record header.");
                }

                var headerBytes = bytes.Skip(pos).Take(4).ToArray();
                var header = DataRecord.ParseHeaderBytes(headerBytes);
                pos += 4;
                if (header.Size == 0xfff)
                {
                    header.Size = bytes.Skip(pos).ToDword();
                }
                var recordBytes = bytes.Skip(pos).Take((int) (uint) header.Size).ToArray();
                var record = CreateRecordFromHeader(header, recordBytes);

                if (record != null)
                {
                    records.Add(record);
                }

                pos += (int) (uint) header.Size;
            }

            return records;
        }

        private static DataRecord CreateRecordFromHeader(DataRecord header, byte[] bytes)
        {
            switch (header.Tag)
            {
                case TagEnum.DocumentProperties:
                    return new DocumentPropertyDataRecord(header.Size, bytes);
                default:
                    return null;
            }
        }

        private static byte[] GetBytesFromStream(CFStream stream, Hwp5FileHeader fileHeader)
        {
            var streamBytes = stream.GetData();

            if (fileHeader.PasswordEncrypted)
            {
                throw new HwpUnsupportedFormatException("Does not support a password encrypted document.");
            }

            if (fileHeader.Compressed)
            {
                using (var dataStream = new MemoryStream(streamBytes, false))
                {
                    using (var zipStream = new DeflateStream(dataStream, CompressionMode.Decompress))
                    {
                        using (var decStream = new MemoryStream())
                        {
                            zipStream.CopyTo(decStream);

                            streamBytes = decStream.ToArray();
                        }
                    }
                }
            }

            return streamBytes;
        }
    }
}
