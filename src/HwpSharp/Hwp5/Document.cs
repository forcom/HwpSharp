using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using HwpSharp.Common;
using HwpSharp.Hwp5.BodyText;
using HwpSharp.Hwp5.HwpType;
using OpenMcdf;

namespace HwpSharp.Hwp5
{
    /// <summary>
    /// Represents a hwp 5.0 document.
    /// </summary>
    public class Document : IHwpDocument
    {
        /// <summary>
        /// This document is a HWP 5.0 document.
        /// </summary>
        public string HwpVersion => "5.0";

        /// <summary>
        /// Gets a file recognition header of this document.
        /// </summary>
        public FileHeader FileHeader { get; private set; }

        /// <summary>
        /// Gets a document information of this document.
        /// </summary>
        public DocumentInformation.DocumentInformation DocumentInformation { get; private set; }

        /// <summary>
        /// Gets a body text of this document.
        /// </summary>
        public BodyText.BodyText BodyText { get; private set; }

        /// <summary>
        /// Gets a summary information of this document.
        /// </summary>
        public SummaryInformation SummaryInformation
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }

        internal Document(CompoundFile compoundFile)
        {
            Load(compoundFile);
        }

        private void Load(CompoundFile compoundFile)
        {
            FileHeader = LoadFileHeader(compoundFile);
            DocumentInformation = LoadDocumentInformation(compoundFile);
            if (!FileHeader.Published)
            {
                BodyText = LoadBodyText(compoundFile);
            }
            else
            {
                BodyText = LoadViewText(compoundFile);
                compoundFile.Commit();
                compoundFile.Save("distribution-released.hwp");
            }
        }

        private BodyText.BodyText LoadViewText(CompoundFile compoundFile)
        {
            CFStorage storage, bodyTextStorage;
            try
            {
                storage = compoundFile.RootStorage.GetStorage("ViewText");
                compoundFile.RootStorage.Delete("BodyText");
                bodyTextStorage = compoundFile.RootStorage.AddStorage("BodyText");
            }
            catch (CFItemNotFound exception)
            {
                throw new HwpFileFormatException("Specified document does not have any BodyText fields.", exception);
            }

            for (int i = 0; i < DocumentInformation.DocumentProperty.SectionCount; ++i)
            {
                try
                {
                    var stream = storage.GetStream($"Section{i}");
                    var bytes = GetRawBytesFromStream(stream, DocumentInformation.FileHeader, DocumentInformation.FileHeader.Published);

                    var newStream = bodyTextStorage.AddStream($"Section{i}");
                    newStream.SetData(bytes);
                }
                catch (CFItemNotFound exception)
                {
                    throw new HwpCorruptedBodyTextException("The document does not have some sections. File may be corrupted.", exception);
                }
            }

            compoundFile.RootStorage.Delete("ViewText");
            return LoadBodyText(compoundFile);
        }

        private BodyText.BodyText LoadBodyText(CompoundFile compoundFile)
        {
            CFStorage storage;
            try
            {
                storage = compoundFile.RootStorage.GetStorage("BodyText");
            }
            catch (CFItemNotFound exception)
            {
                throw new HwpFileFormatException("Specified document does not have any BodyText fields.", exception);
            }

            var bodyText = new BodyText.BodyText(storage, DocumentInformation);

            return bodyText;
        }

        private DocumentInformation.DocumentInformation LoadDocumentInformation(CompoundFile compoundFile)
        {
            CFStream stream;
            try
            {
                stream = compoundFile.RootStorage.GetStream("DocInfo");
            }
            catch (CFItemNotFound exception)
            {
                throw new HwpFileFormatException("Specified document does not have a DocInfo field.", exception);
            }

            var docInfo = new DocumentInformation.DocumentInformation(stream, FileHeader);

            return docInfo;
        }

        private static FileHeader LoadFileHeader(CompoundFile compoundFile)
        {
            CFStream stream;
            try
            {
                stream = compoundFile.RootStorage.GetStream("FileHeader");
            }
            catch (CFItemNotFound exception)
            {
                throw new HwpFileFormatException("Specified document does not have a FileHeader field.", exception);
            }

            var fileHeader = new FileHeader(stream);
            stream.SetData(fileHeader.RawBytes);

            return fileHeader;
        }

        /// <summary>
        /// Creates a <see cref="Document"/> instance from a <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">A stream which contains a hwp 5 document.</param>
        public Document(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            try
            {
                using (var compoundFile = new CompoundFile(stream, CFSUpdateMode.Update, CFSConfiguration.Default))
                {
                    Load(compoundFile);
                }
            }
            catch(CFFileFormatException exception)
            {
                throw new HwpFileFormatException("Specified document is not a hwp 5 document format.", exception);
            }
        }

        /// <summary>
        /// Creates a <see cref="Document"/> instance from a file.
        /// </summary>
        /// <param name="filename">A file name of a hwp 5 document.</param>
        public Document(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }

            try
            {
                using (var compoundFile = new CompoundFile(filename, CFSUpdateMode.Update, CFSConfiguration.Default))
                {
                    Load(compoundFile);
                }
            }
            catch (CFFileFormatException exception)
            {
                throw new HwpFileFormatException("Specified document is not a hwp 5 document format.", exception);
            }
        }

        internal static byte[] GetRawBytesFromStream(CFStream stream, FileHeader fileHeader, bool isDistribution = false)
        {
            var streamBytes = stream.GetData();

            if (fileHeader.PasswordEncrypted)
            {
                throw new HwpUnsupportedFormatException("Does not support a password encrypted document.");
            }

            if (fileHeader.Published && isDistribution)
            {
                using (var dataStream = new MemoryStream(streamBytes, false))
                {
                    int length;
                    var recordBytes = new byte[260];
                    dataStream.Read(recordBytes, 0, recordBytes.Length);
                    var record = DataRecord.GetRecordFromBytes(recordBytes, out length);

                    var seed = record.RawBytes;

                    {
                        var random = new Common.Random(seed.ToDWord()).GetEnumerator();

                        uint n = 0;
                        byte key = 0;
                        for (var i = 0; i < 256; ++i, --n)
                        {
                            if (n == 0)
                            {
                                random.MoveNext();
                                key = (byte) (random.Current & 0xFF);
                                random.MoveNext();
                                n = (random.Current & 0xF) + 1;
                            }
                            if (i >= 4)
                            {
                                seed[i] ^= key;
                            }
                        }
                    }

                    var offset = 4 + (seed[0] & 0x0F);
                    var shaKey = new byte[16];
                    Array.Copy(seed, offset, shaKey, 0, shaKey.Length);

                    using (
                        var aes = new AesManaged
                        {
                            KeySize = 128,
                            BlockSize = 128,
                            Mode = CipherMode.ECB,
                            Padding = PaddingMode.None
                        })
                    {
                        using (var cryptoStream = new CryptoStream(dataStream, aes.CreateDecryptor(shaKey, shaKey), CryptoStreamMode.Read))
                        {
                            using (var decryptedStream = new MemoryStream())
                            {
                                cryptoStream.CopyTo(decryptedStream);
                                streamBytes = decryptedStream.ToArray();
                            }
                        }
                    }
                }
            }
            else if (fileHeader.Compressed)
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
