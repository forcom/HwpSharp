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
        public Hwp5DocumentInformation DocumentInformation { get; private set; }

        /// <summary>
        /// Gets a body text of this document.
        /// </summary>
        public Hwp5BodyText BodyText { get; private set; }

        /// <summary>
        /// Gets a summary information of this document.
        /// </summary>
        public Hwp5SummaryInformation SummaryInformation { get; private set; }

        protected Hwp5Document(CompoundFile compoundFile)
        {
            Load(compoundFile);
        }

        private void Load(CompoundFile compoundFile)
        {
            var fileHeader = LoadFileHeader(compoundFile);
        }

        private static Hwp5FileHeader LoadFileHeader(CompoundFile compoundFile)
        {
            var stream = compoundFile.RootStorage.GetStream("FileHeader");

            var fileHeader = new Hwp5FileHeader(stream);

            return fileHeader;
        }

        /// <summary>
        /// Creates a <see cref="Hwp5Document"/> instance from a <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">A stream which contains a hwp 5 document.</param>
        public Hwp5Document(Stream stream)
        {
            var compoundFile = new CompoundFile(stream);

            Load(compoundFile);
        }

        /// <summary>
        /// Creates a <see cref="Hwp5Document"/> instance from a file.
        /// </summary>
        /// <param name="filename">A file name of a hwp 5 document.</param>
        public Hwp5Document(string filename)
        {
            var compoundFile = new CompoundFile(filename);

            Load(compoundFile);
        }
    }
}
