using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hwpSharp.Common;

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
        /// Represents a file recognition header of this document.
        /// </summary>
        Hwp5FileHeader FileHeader { get; }

        /// <summary>
        /// Represents a document information of this document.
        /// </summary>
        Hwp5DocumentInformation DocumentInformation { get; }

        /// <summary>
        /// Represents a body text of this document.
        /// </summary>
        Hwp5BodyText BodyText { get; }

        /// <summary>
        /// Represents a summary information of this document.
        /// </summary>
        Hwp5SummaryInformation SummaryInformation { get; }
    }
}
