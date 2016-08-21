using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HwpSharp.Hwp5;
using HwpSharp.Hwp5.BodyText.DataRecords;

namespace HwpSharp.Tests.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var doc = new Document(@"..\case\Hwp5\distribution.hwp");
            foreach (var section in doc.BodyText.Sections)
            {
                if (section == null)
                    continue;
                foreach (var record in section.DataRecords)
                {
                    if (record?.TagId != ParagraphText.ParagraphTextTagId)
                        continue;
                    System.Console.WriteLine(((ParagraphText) record)?.Text);
                }
            }
        }
    }
}
