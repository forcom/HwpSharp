using System.Collections.Generic;
using hwpSharp.Common;
using hwpSharp.Hwp5.BodyText;
using OpenMcdf;

namespace hwpSharp.Hwp5
{
    /// <summary>
    /// Represents a hwp 5.0 body text.
    /// </summary>
    public class Hwp5BodyText : IBodyText
    {
        public Hwp5DocumentInformation DocumentInformation { get; private set; }

        public List<Section> Sections { get; }

        public Hwp5BodyText(Hwp5DocumentInformation docInfo)
        {
            DocumentInformation = docInfo;
            Sections = new List<Section>();
        }

        internal Hwp5BodyText(CFStorage storage, Hwp5DocumentInformation docInfo)
        {
            DocumentInformation = docInfo;
            Sections = new List<Section>();

            for (var i = 0; i < docInfo.DocumentProperty.SectionCount; ++i)
            {
                CFStream stream;
                try
                {
                    stream = storage.GetStream($"Section{i}");
                }
                catch (CFItemNotFound exception)
                {
                    throw new HwpCorruptedBodyTextException("The document does not have some sections. File may be corrupted.", exception);
                }

                var section = new Section(stream, docInfo);

                Sections.Add(section);
            }
        }
    }
}
