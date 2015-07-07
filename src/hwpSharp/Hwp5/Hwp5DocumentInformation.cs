using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hwpSharp.Common;
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

        public IEnumerable<DataRecord> Records { get; private set; }

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
        }

        private byte[] GetBytesFromStream(CFStream stream, Hwp5FileHeader fileHeader)
        {
            throw new NotImplementedException();
        }
    }
}
