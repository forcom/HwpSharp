using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using hwpSharp.Common;
using hwpSharp.Hwp5;
using Xunit;

namespace hwpSharp.Tests.Hwp5
{
    public class Hwp5FileHeaderTest
    {
        [Theory]
        [InlineData(@"../case/Hwp5/BlogForm_BookReview.hwp", "5.0.3.0")]
        [InlineData(@"../case/Hwp5/BlogForm_MovieReview.hwp", "5.0.3.0")]
        [InlineData(@"../case/Hwp5/BlogForm_Recipe.hwp", "5.0.3.0")]
        [InlineData(@"../case/Hwp5/BookReview.hwp", "5.0.3.0")]
        [InlineData(@"../case/Hwp5/calendar_monthly.hwp", "5.0.3.0")]
        [InlineData(@"../case/Hwp5/calendar_year.hwp", "5.0.3.0")]
        [InlineData(@"../case/Hwp5/classical_literature.hwp", "5.0.3.0")]
        [InlineData(@"../case/Hwp5/english.hwp", "5.0.3.2")]
        [InlineData(@"../case/Hwp5/Hyper(hwp2010).hwp", "5.0.3.3")]
        [InlineData(@"../case/Hwp5/interview.hwp", "5.0.3.0")]
        [InlineData(@"../case/Hwp5/KTX.hwp", "5.0.3.0")]
        [InlineData(@"../case/Hwp5/NewYear_s_Day.hwp", "5.0.3.2")]
        [InlineData(@"../case/Hwp5/request.hwp", "5.0.3.2")]
        [InlineData(@"../case/Hwp5/shortcut.hwp", "5.0.3.0")]
        [InlineData(@"../case/Hwp5/sungeo.hwp", "5.0.3.0")]
        [InlineData(@"../case/Hwp5/Textmail.hwp", "5.0.3.0")]
        [InlineData(@"../case/Hwp5/treatise sample.hwp", "5.0.3.0")]
        [InlineData(@"../case/Hwp5/Worldcup_FIFA2010_32.hwp", "5.0.3.0")]
        public void Version_NormalHwp5Document(string filename, string expected)
        {
            var document = new Hwp5Document(filename);

            Assert.Equal(expected, document.FileHeader.FileVersion.ToString());
        }
    }
}
