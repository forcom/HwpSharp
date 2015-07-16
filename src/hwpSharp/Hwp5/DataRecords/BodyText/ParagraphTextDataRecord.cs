using System.Text;
using hwpSharp.Hwp5.HwpType;

namespace hwpSharp.Hwp5.DataRecords.BodyText
{
    public class ParagraphTextDataRecord : DataRecord
    {
        public string Text { get; set; }

        public ParagraphTextDataRecord(Dword size, byte[] bytes)
        {
            Tag = TagEnum.ParagraphHeader;
            Level = 0;
            Size = size;

            ParseRecord(bytes);
        }

        private void ParseRecord(byte[] bytes)
        {
            Text = Encoding.Unicode.GetString(bytes);
        }
    }
}