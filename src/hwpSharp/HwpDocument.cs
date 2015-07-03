using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using hwpSharp.Common;

namespace hwpSharp
{
    /// <summary>
    /// Represents a Hwp document reader.
    /// </summary>
    public static class HwpReader
    {
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
            throw new NotImplementedException();
        }
    }
}
