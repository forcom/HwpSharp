using System.IO;
using System.Linq;
using HwpSharp.Common;
using HwpSharp.Hwp5;

namespace HwpSharp
{
    /// <summary>
    /// Represents a Hwp document reader.
    /// </summary>
    public static class HwpReader
    {
        private static class Signature
        {
            public static readonly byte[] Hwp3 =
            {
                0x48, 0x57, 0x50, 0x20, 0x44, 0x6f, 0x63, 0x75, 0x6d, 0x65, 0x6e, 0x74, 0x20, 0x46, 0x69, 0x6c,
                0x65, 0x20, 0x56, 0x33, 0x2e, 0x30, 0x30, 0x20, 0x1a, 0x01, 0x02, 0x03, 0x04, 0x05
            };

            public static readonly byte[] CompoundFile =
            {
                0xd0, 0xcf, 0x11, 0xe0, 0xa1, 0xb1, 0x1a, 0xe1
            };
        }

        private static class FileType
        {
            public const string Hwp3 = @"HWP 3.0";
            public const string CompoundFile = @"Compound File";
        }

        /// <summary>
        /// Returns a <see cref="IHwpDocument"/> from a hwp document file.
        /// </summary>
        /// <param name="filename">Path to a hwp document.</param>
        /// <returns><see cref="IHwpDocument"/> instance.</returns>
        public static IHwpDocument Read(string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Read(stream);
            }
        }

        /// <summary>
        /// Returns a <see cref="IHwpDocument"/> from a stream.
        /// </summary>
        /// <param name="stream">Stream of a hwp document.</param>
        /// <returns><see cref="IHwpDocument"/> instance.</returns>
        public static IHwpDocument Read(Stream stream)
        {
            var fileType = GetFileType(stream);
            if (fileType == FileType.CompoundFile)
            {
                return new Document(stream);
            }
            throw new HwpFileFormatException("File type is imcompatible.");
        }

        private static string GetFileType(Stream stream)
        {
            var memoryStream = ReadFromStream(stream);

            if (IsHwp30Format(memoryStream))
            {
                return FileType.Hwp3;
            }

            if (IsCompoundFileFormat(memoryStream))
            {
                return FileType.CompoundFile;
            }

            throw new HwpFileFormatException("File type is unknown.");
        }

        private static MemoryStream ReadFromStream(Stream stream)
        {
            try
            {
                var memoryStream = new MemoryStream();
                while (true)
                {
                    var buf = new byte[8192];
                    var read = stream.Read(buf, 0, buf.Length);
                    if (read > 0)
                    {
                        memoryStream.Write(buf, 0, read);
                    }
                    else
                    {
                        break;
                    }
                }
                memoryStream.Position = 0;
                return memoryStream;
            }
            finally
            {
                stream.Close();
            }
        }

        private static bool IsHwp30Format(Stream stream)
        {
            var read = 0;
            try
            {
                var firstBytes = new byte[30];
                read = stream.Read(firstBytes, 0, 30);
                return Signature.Hwp3.SequenceEqual(firstBytes);
            }
            finally
            {
                if (read > 0)
                {
                    stream.Seek(-read, SeekOrigin.Current);
                }
            }
        }

        private static bool IsCompoundFileFormat(Stream stream)
        {
            var read = 0;
            try
            {
                var firstBytes = new byte[8];
                read = stream.Read(firstBytes, 0, 8);

                return Signature.CompoundFile.SequenceEqual(firstBytes);
            }
            finally
            {
                if (read > 0)
                {
                    stream.Seek(-read, SeekOrigin.Current);
                }
            }
        }
    }
}