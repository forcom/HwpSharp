using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwpSharp.Common
{
    /// <summary>
    /// Represents a Hwp document.
    /// </summary>
    public interface IHwpDocument
    {
        /// <summary>
        /// Represents the version of Hwp document.
        /// </summary>
        string HwpVersion { get; }
    }
}
