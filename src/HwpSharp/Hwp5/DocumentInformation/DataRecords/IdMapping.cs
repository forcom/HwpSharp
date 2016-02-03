using System;
using System.Collections.Generic;
using HwpSharp.Hwp5.HwpType;
using Int32 = HwpSharp.Hwp5.HwpType.Int32;

namespace HwpSharp.Hwp5.DocumentInformation.DataRecords
{
    public class IdMapping : DataRecord
    {
        public const uint IdMappingsTagId = HwpTagBegin + 1;

        public Int32[] IdMappingCounts { get; }

        public Int32 BinaryDataCount => IdMappingCounts[0];

        public Int32 KoreanFontCount => IdMappingCounts[1];

        public Int32 EnglishFontCount => IdMappingCounts[2];

        public Int32 ChineseFontCount => IdMappingCounts[3];

        public Int32 JapaneseFontCount => IdMappingCounts[4];

        public Int32 OtherFontCount => IdMappingCounts[5];

        public Int32 SymbolFontCount => IdMappingCounts[6];

        public Int32 UserFontCount => IdMappingCounts[7];

        public Int32 BorderBackgroundCount => IdMappingCounts[8];

        public Int32 CharacterShapeCount => IdMappingCounts[9];

        public Int32 TabDefinitionCount => IdMappingCounts[10];

        public Int32 ParagraphNumberCount => IdMappingCounts[11];

        public Int32 ListHeaderTableCount => IdMappingCounts[12];

        public Int32 ParagraphShapeCount => IdMappingCounts[13];

        public Int32 StyleCount => IdMappingCounts[14];

        public Int32 MemoShapeCount
        {
            get
            {
                if (IdMappingCounts.Length < 15)
                {
                    throw new NotSupportedException();
                }

                return IdMappingCounts[15];
            }
        }

        public Int32 TrackChangeCount
        {
            get
            {
                if (IdMappingCounts.Length < 16)
                {
                    throw new NotSupportedException();
                }

                return IdMappingCounts[16];
            }
        }

        public Int32 TrackChangeUserCount
        {
            get
            {
                if (IdMappingCounts.Length < 17)
                {
                    throw new NotSupportedException();
                }

                return IdMappingCounts[17];
            }
        }

        public IdMapping(uint level, byte[] bytes, DocumentInformation _ = null)
            : base(IdMappingsTagId, level, (uint) bytes.Length)
        {
            var mappings = new List<Int32>();
            for (var pos = 0; pos < bytes.Length; pos += 4)
            {
                mappings.Add(bytes.ToInt32(pos));
            }
            IdMappingCounts = mappings.ToArray();
        }
    }
}
