using HwpSharp.Hwp5;
using Xunit;

namespace HwpSharp.Tests.Hwp5
{
    public class Hwp5DocumentInformationTest
    {
        [Theory]
        [InlineData(@"../case/Hwp5/BlogForm_BookReview.hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/BlogForm_MovieReview.hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/BlogForm_Recipe.hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/BookReview.hwp", 2, 1)]
        [InlineData(@"../case/Hwp5/calendar_monthly.hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/calendar_year.hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/classical_literature.hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/english.hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/Hyper(hwp2010).hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/interview.hwp", 2, 1)]
        [InlineData(@"../case/Hwp5/KTX.hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/NewYear_s_Day.hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/request.hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/shortcut.hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/sungeo.hwp", 13, 1)]
        [InlineData(@"../case/Hwp5/Textmail.hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/treatise sample.hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/Worldcup_FIFA2010_32.hwp", 1, 1)]
        [InlineData(@"../case/Hwp5/multisection.hwp", 3, 1)]
        public void DocumentProperty_NormalHwp5Document(string filename, ushort expectedSectionCount, ushort expectedStartPageNumber)
        {
            var document = new Hwp5Document(filename);

            Assert.Equal(expectedSectionCount, (ushort) document.DocumentInformation.DocumentProperty.SectionCount);
            Assert.Equal(expectedStartPageNumber, (ushort) document.DocumentInformation.DocumentProperty.StartPageNumber);
        }
    }
}
