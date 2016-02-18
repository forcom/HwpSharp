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
        public void Main(string[] args)
        {
            foreach(var filename in System.IO.Directory.EnumerateFiles(@"..\psat", "*.hwp"))
            {
                System.Console.WriteLine(filename);
                new Document($@"..\psat\{filename}");
            }
        }
    }
}
