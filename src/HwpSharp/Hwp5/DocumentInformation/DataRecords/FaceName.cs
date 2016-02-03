using System;
using System.Diagnostics;
using System.Text;
using HwpSharp.Common;
using HwpSharp.Hwp5.HwpType;
using Byte = HwpSharp.Hwp5.HwpType.Byte;

namespace HwpSharp.Hwp5.DocumentInformation.DataRecords
{
    public class FaceName : DataRecord
    {
        // 파일 형식 문서에 내용이 빠진 부분은 다음을 참조함
        // https://github.com/mete0r/pyhwp/tree/develop/pyhwp/hwp5/binmodel/tagid19_face_name.py

        public const uint FaceNameTagId = HwpTagBegin + 3;

        [Flags]
        public enum FontProperty : byte
        {
            Unknown = 0x0,
            Ttf = 0x1,
            Hft = 0x2,
            DefaultFont = 0x20,
            FaceStyle = 0x40,
            AlternativeFont = 0x80,
        }

        public enum AlternativeFont : byte
        {
            Unknown = 0,
            Ttf = 1,
            Hft = 2,
        }

        public struct FaceStyle
        {
            public enum FamilyType : byte
            {
                Any = 0,
                None = 1,
                TextDisplay = 2,
                Script = 3,
                Decorative = 4,
                Pictorial = 5,
            }

            public enum SerifStyle : byte
            {
                Any = 0,
                None = 1,
                Cove = 2,
                ObtuseCove = 3,
                SquareCove = 4,
                ObtuseSquareCove = 5,
                Square = 6,
                Thin = 7,
                Bone = 8,
                Exaggerated = 9,
                Triangle = 10,
                NormalSans = 11,
                ObtuseSans = 12,
                PerpSans = 13,
                Flared = 14,
                Rounded = 15,
            }

            public enum Weight : byte
            {
                Any = 0,
                None = 1,
                VeryLight = 2,
                Light = 3,
                Thin = 4,
                Book = 5,
                Medium = 6,
                Demi = 7,
                Bold = 8,
                Heavy = 9,
                Black = 10,
                Nord = 11,
            }

            public enum Proportion : byte
            {
                Any = 0,
                None = 1,
                OldStyle = 2,
                Modern = 3,
                EvenWidth = 4,
                Expanded = 5,
                Condensed = 6,
                VeryExpanded = 7,
                VeryCondensed = 8,
                Monospaced = 9,
            }

            public enum Contrast : byte
            {
                Any = 0,
                None = 1,
                NoContrast = 2,
                VeryLow = 3,
                Low = 4,
                MediumLow = 5,
                Medium = 6,
                MediumHigh = 7,
                High = 8,
                VeryHigh = 9,
            }

            public enum StrokeVariation : byte
            {
                Any = 0,
                None = 1,
                GradualDiagonal = 2,
                GradualTranspositonal = 3,
                GradualVertical = 4,
                GradualHorizontal = 5,
                RapidVertial = 6,
                RapidHorizontal = 7,
                InstantVertical = 8,
            }

            public enum ArmStyle : byte
            {
                Any = 0,
                None = 1,
                StraightHorizontal = 2,
                StraightWedge = 3,
                StraightVertical = 4,
                StraightSigleSerif = 5,
                StraightDoubleSerif = 6,
                BentHorizontal = 7,
                BentWedge = 8,
                BentVertical = 9,
                BentSingleSerif = 10,
                BentDoubleSerif = 11,
            }

            public enum LetterForm : byte
            {
                Any = 0,
                None = 1,
                NormalContact = 2,
                NormalWeighted = 3,
                NormalBoxed = 4,
                NormalFlattened = 5,
                NormalRounded = 6,
                NormalOffCenter = 7,
                NormalSquare = 8,
                ObliqueContact = 9,
                ObliqueWeighted = 10,
                ObliqueBoxed = 11,
                ObliqueFlattened = 12,
                ObliqueRounded = 13,
                ObliqueOffCenter = 14,
                ObliqueSquare = 15,
            }

            public enum MiddleLine : byte
            {
                Any = 0,
                None = 1,
                StandardTrimmed = 2,
                StandardPointed = 3,
                StandardSerifed = 4,
                HighTrimmed = 5,
                HighPointed = 6,
                HighSerifed = 7,
                ConstantTrimmed = 8,
                ConstantPointed = 9,
                ConstantSerifed = 10,
                LowTrimmed = 11,
                LowPointed = 12,
                LowSerifed = 13,
            }

            public enum XHeight : byte
            {
                Any = 0,
                None = 1,
                ConstantSmall = 2,
                ConstantStandard = 3,
                ConstantLarge = 4,
                DuckingSmall = 5,
                DuckingStandard = 6,
                DuckingLarge = 7,
            }

            public FamilyType FamilyTypeValue { get; }

            public SerifStyle SerifStyleValue { get; }

            public Weight WeightValue { get; }

            public Proportion ProportionValue { get; }

            public Contrast ContrastValue { get; }

            public StrokeVariation StrokeVariationValue { get; }

            public ArmStyle ArmStyleValue { get; }

            public LetterForm LetterFormValue { get; }

            public MiddleLine MiddleLineValue { get; }

            public XHeight XHeightValue { get; }

            public FaceStyle(FamilyType familyType = FamilyType.Any, SerifStyle serifStyle = SerifStyle.Any,
                Weight weight = Weight.Any, Proportion proportion = Proportion.Any, Contrast contrast = Contrast.Any,
                StrokeVariation strokeVariation = StrokeVariation.Any, ArmStyle armStyle = ArmStyle.Any,
                LetterForm letterForm = LetterForm.Any, MiddleLine middleLine = MiddleLine.Any,
                XHeight xHeight = XHeight.Any)
            {
                FamilyTypeValue = familyType;
                SerifStyleValue = serifStyle;
                WeightValue = weight;
                ProportionValue = proportion;
                ContrastValue = contrast;
                StrokeVariationValue = strokeVariation;
                ArmStyleValue = armStyle;
                LetterFormValue = letterForm;
                MiddleLineValue = middleLine;
                XHeightValue = xHeight;
            }
        }

        public FontProperty Property { get; }

        public string FontName { get; }

        private readonly AlternativeFont _alternativeFontType;

        public AlternativeFont AlternativeFontType
        {
            get
            {
                if (!Property.HasFlag(FontProperty.AlternativeFont))
                {
                    throw new HwpUnsupportedProperty();
                }
                return _alternativeFontType;
            }
        }

        private readonly string _alternativeFontName;

        public string AlternativeFontName
        {
            get
            {
                if (!Property.HasFlag(FontProperty.AlternativeFont))
                {
                    throw new HwpUnsupportedProperty();
                }
                return _alternativeFontName;
            }
        }

        private readonly FaceStyle _faceStyle;

        public FaceStyle FaceStyleValue
        {
            get
            {
                if (!Property.HasFlag(FontProperty.FaceStyle))
                {
                    throw new HwpUnsupportedProperty();
                }
                return _faceStyle;
            }
        }

        private readonly string _defaultFontName;

        public string DefaultFontName
        {
            get
            {
                if (!Property.HasFlag(FontProperty.DefaultFont))
                {
                    throw new HwpUnsupportedProperty();
                }
                return _defaultFontName;
            }
        }

        public FaceName(uint level, byte[] bytes, DocumentInformation _ = null)
            : base(FaceNameTagId, level, (uint) bytes.Length)
        {
            var pos = 0;

            FontProperty property;
            Property = Enum.TryParse($"{bytes[pos]}", out property) ? property : FontProperty.Unknown;
            pos += 1;

            var nameLength = bytes.ToWord(pos);
            pos += 2;

            FontName = Encoding.Unicode.GetString(bytes, pos, 2*nameLength);
            pos += 2*nameLength;

            if (property.HasFlag(FontProperty.AlternativeFont))
            {
                AlternativeFont alternative;
                _alternativeFontType = Enum.TryParse($"{bytes[pos]}", out alternative)
                    ? alternative
                    : AlternativeFont.Unknown;
                pos += 1;

                var alternativeLength = bytes.ToWord(pos);
                pos += 2;

                _alternativeFontName = Encoding.Unicode.GetString(bytes, pos, 2*alternativeLength);
                pos += 2*alternativeLength;
            }

            if (property.HasFlag(FontProperty.FaceStyle))
            {

                FaceStyle.FamilyType familyType;
                familyType = Enum.TryParse($"{bytes[pos++]}", out familyType) ? familyType : FaceStyle.FamilyType.Any;

                FaceStyle.SerifStyle serifStyle;
                serifStyle = Enum.TryParse($"{bytes[pos++]}", out serifStyle) ? serifStyle : FaceStyle.SerifStyle.Any;

                FaceStyle.Weight weight;
                weight = Enum.TryParse($"{bytes[pos++]}", out weight) ? weight : FaceStyle.Weight.Any;

                FaceStyle.Proportion proportion;
                proportion = Enum.TryParse($"{bytes[pos++]}", out proportion) ? proportion : FaceStyle.Proportion.Any;

                FaceStyle.Contrast contrast;
                contrast = Enum.TryParse($"{bytes[pos++]}", out contrast) ? contrast : FaceStyle.Contrast.Any;

                FaceStyle.StrokeVariation strokeVariation;
                strokeVariation = Enum.TryParse($"{bytes[pos++]}", out strokeVariation)
                    ? strokeVariation
                    : FaceStyle.StrokeVariation.Any;

                FaceStyle.ArmStyle armStyle;
                armStyle = Enum.TryParse($"{bytes[pos++]}", out armStyle) ? armStyle : FaceStyle.ArmStyle.Any;

                FaceStyle.LetterForm letterForm;
                letterForm = Enum.TryParse($"{bytes[pos++]}", out letterForm) ? letterForm : FaceStyle.LetterForm.Any;

                FaceStyle.MiddleLine middleLine;
                middleLine = Enum.TryParse($"{bytes[pos++]}", out middleLine) ? middleLine : FaceStyle.MiddleLine.Any;

                FaceStyle.XHeight xHeight;
                xHeight = Enum.TryParse($"{bytes[pos++]}", out xHeight) ? xHeight : FaceStyle.XHeight.Any;

                _faceStyle = new FaceStyle(familyType, serifStyle, weight, proportion, contrast, strokeVariation,
                    armStyle, letterForm, middleLine, xHeight);
            }

            if (property.HasFlag(FontProperty.DefaultFont))
            {
                var defaultLength = bytes.ToWord(pos);
                pos += 2;

                _defaultFontName = Encoding.Unicode.GetString(bytes, pos, 2*defaultLength);
                pos += 2*defaultLength;
            }
        }
    }
}
