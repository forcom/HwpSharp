using System.Collections.Generic;
using HwpSharp.Common;
using OpenMcdf;

namespace HwpSharp.Hwp5.BodyText
{
    /// <summary>
    /// Represents a hwp 5.0 body text.
    /// </summary>
    public class BodyText : IBodyText
    {
        public DocumentInformation.DocumentInformation DocumentInformation { get; private set; }

        public List<Section> Sections { get; }

        public BodyText(DocumentInformation.DocumentInformation docInfo)
        {
            DocumentInformation = docInfo;
            Sections = new List<Section>();
        }

        internal BodyText(CFStorage storage, DocumentInformation.DocumentInformation docInfo)
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
