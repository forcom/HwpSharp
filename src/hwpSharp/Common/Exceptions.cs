using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwpSharp.Common
{
    /// <summary>
    /// The exception that is thrown when a file or stream does not contains a HWP document, or HwpSharp cannot recognized a HWP document.
    /// </summary>
    public class HwpFileFormatException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HwpFileFormatException"/> class.
        /// </summary>
        public HwpFileFormatException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HwpFileFormatException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public HwpFileFormatException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HwpFileFormatException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public HwpFileFormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
