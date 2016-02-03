using System.Text;
using HwpSharp.Hwp5.HwpType;

namespace HwpSharp.Hwp5.BodyText.DataRecords
{
    public class ParagraphText : DataRecord
    {
        public const uint ParagraphTextTagId = HwpTagBegin + 51;
        public string Text { get; set; }

        public ParagraphText(uint level, byte[] bytes, DocumentInformation.DocumentInformation _ = null)
            : base(ParagraphTextTagId, level, (uint) bytes.Length)
        {
            Text = Encoding.Unicode.GetString(bytes);
        }
    }
}