using System;
using System.IO;
using hwpSharp.Common;
using hwpSharp.Hwp5;
using Xunit;

namespace hwpSharp.Tests.Hwp5
{
    public class Hwp5DocumentTest
    {
        [Theory]
        [InlineData(@"../case/CompoundFile.xls", typeof(HwpFileFormatException), "Specified document does not have a FileHeader field.")]
        [InlineData(@"../case/Hwp3File.hwp", typeof(HwpFileFormatException), "Specified document is not a hwp 5 document format.")]
        [InlineData(@"../case/PdfFile.pdf", typeof(HwpFileFormatException), "Specified document is not a hwp 5 document format.")]
        [InlineData(null, typeof(ArgumentNullException), "Value cannot be null.")]
        [InlineData(@"Non-Exist Document", typeof(FileNotFoundException), "Could not find file")]
        public void ConstructorWithFileName_AbnormalHwp5Document(string filename, Type expectedException, string expectedMessage)
        {
            var ex = Record.Exception(() =>
            {
                new Hwp5Document(filename);
            });

            Assert.IsType(expectedException, ex);
            Assert.StartsWith(expectedMessage, ex.Message);
        }

        [Theory]
        [InlineData(@"../case/CompoundFile.xls", typeof(HwpFileFormatException), "Specified document does not have a FileHeader field.")]
        [InlineData(@"../case/Hwp3File.hwp", typeof(HwpFileFormatException), "Specified document is not a hwp 5 document format.")]
        [InlineData(@"../case/PdfFile.pdf", typeof(HwpFileFormatException), "Specified document is not a hwp 5 document format.")]
        [InlineData(null, typeof(ArgumentNullException), "Value cannot be null.")]
        public void ConstructorWithStream_AbnormalHwp5Document(string filename, Type expectedException, string expectedMessage)
        {
            var ex = Record.Exception(() =>
            {
                var stream = filename != null ? new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite) : null;
                new Hwp5Document(stream);
            });

            Assert.IsType(expectedException, ex);
            Assert.StartsWith(expectedMessage, ex.Message);
        }
    }
}
