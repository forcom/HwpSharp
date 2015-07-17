using System.Text;
using HwpSharp.Hwp5.HwpType;

namespace HwpSharp.Hwp5.DataRecords.BodyText
{
    public class ParagraphTextDataRecord : DataRecord
    {
        public string Text { get; set; }

        public ParagraphTextDataRecord(Dword size, byte[] bytes)
        {
            Tag = TagEnum.ParagraphText;
            Level = 1;
            Size = size;

            ParseRecord(bytes);
        }

        private void ParseRecord(byte[] bytes)
        {
            Text = Encoding.Unicode.GetString(bytes);
        }
    }
}