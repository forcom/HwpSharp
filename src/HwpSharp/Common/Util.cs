using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HwpSharp.Hwp5.HwpType;

namespace HwpSharp.Common
{
    public class Random : IEnumerable<DWord>
    {
        public DWord Seed { get; private set; }

        public Random(DWord seed)
        {
            Seed = seed;
        }

        public IEnumerator<DWord> GetEnumerator()
        {
            while (true)
            {
                Seed = (Seed*214013 + 2531011) & 0xFFFFFFFF;
                yield return (Seed >> 16) & 0x7FFF;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
