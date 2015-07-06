using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using hwpSharp.Common;
using OpenMcdf;

namespace hwpSharp.Hwp5
{
    /// <summary>
    /// Represents a hwp 5.0 document.
    /// </summary>
    public class Hwp5Document : IHwpDocument
    {
        /// <summary>
        /// This document is a HWP 5.0 document.
        /// </summary>
        public string HwpVersion => "5.0";

        /// <summary>
        /// Gets a file recognition header of this document.
        /// </summary>
        public Hwp5FileHeader FileHeader { get; private set; }

        /// <summary>
        /// Gets a document information of this document.
        /// </summary>
        public Hwp5DocumentInformation DocumentInformation
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a body text of this document.
        /// </summary>
        public Hwp5BodyText BodyText
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a summary information of this document.
        /// </summary>
        public Hwp5SummaryInformation SummaryInformation
        {
            get { throw new NotImplementedException(); }
            private set { throw new NotImplementedException(); }
        }

        internal Hwp5Document(CompoundFile compoundFile)
        {
            Load(compoundFile);
        }

        private void Load(CompoundFile compoundFile)
        {
            FileHeader = LoadFileHeader(compoundFile);
        }

        private static Hwp5FileHeader LoadFileHeader(CompoundFile compoundFile)
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

            var fileHeader = new Hwp5FileHeader(stream);

            return fileHeader;
        }

        /// <summary>
        /// Creates a <see cref="Hwp5Document"/> instance from a <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">A stream which contains a hwp 5 document.</param>
        public Hwp5Document(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            CompoundFile compoundFile;
            try
            {
                compoundFile = new CompoundFile(stream);
            }
            catch(CFFileFormatException exception)
            {
                throw new HwpFileFormatException("Specified document is not a hwp 5 document format.", exception);
            }

            Load(compoundFile);
        }

        /// <summary>
        /// Creates a <see cref="Hwp5Document"/> instance from a file.
        /// </summary>
        /// <param name="filename">A file name of a hwp 5 document.</param>
        public Hwp5Document(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }

            CompoundFile compoundFile;
            try
            {
                compoundFile = new CompoundFile(filename);
            }
            catch (CFFileFormatException exception)
            {
                throw new HwpFileFormatException("Specified document is not a hwp 5 document format.", exception);
            }

            Load(compoundFile);
        }
    }
}
