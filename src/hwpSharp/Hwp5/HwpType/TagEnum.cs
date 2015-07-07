using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwpSharp.Hwp5.HwpType
{
    /// <summary>
    /// Specifies a tag ID of a hwp 5 data record.
    /// </summary>
    public enum TagEnum
    {
        /// <summary>
        /// Document property.
        /// </summary>
        DocumentProperties = DataRecord.HwpTagBegin,
        IdMappings = DataRecord.HwpTagBegin + 1,
        BinDate = DataRecord.HwpTagBegin + 2
    }
}
