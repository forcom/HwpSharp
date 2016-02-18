using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using HwpSharp.Common;
using HwpSharp.Hwp5.HwpType;
using Byte = HwpSharp.Hwp5.HwpType.Byte;
using Int16 = HwpSharp.Hwp5.HwpType.Int16;
using Int32 = HwpSharp.Hwp5.HwpType.Int32;
using UInt16 = HwpSharp.Hwp5.HwpType.UInt16;

namespace HwpSharp.Hwp5.DocumentInformation.DataRecords
{
    public class BorderFill : DataRecord
    {
        public const uint BorderFillTagId = HwpTagBegin + 4;

        [DebuggerDisplay("Type = {Type}")]
        public struct BorderProperty
        {
            public enum DiagonalType
            {
                None,
                Slash,
                BackSlash,
                Cross,
                Center,
            }

            public enum SlashDiagonal : ushort
            {
                None = 0,
                Slash = (1 << 3),
                SlashTop = SlashBottom + (1 << 10),
                SlashRight = SlashLeft + (1 << 10),
                SlashBottom = (1 << 2) + Slash,
                SlashLeft = Slash + (1 << 4),
                SlashCrooked = Slash + (1 << 8),
            }

            public enum BackSlashDiagonal : ushort
            {
                None = 0,
                BackSlash = (1 << 6),
                BackSlashTop = BackSlashBottom + (1 << 12),
                BackSlashLeft = BackSlashRight + (1 << 12),
                BackSlashBottom = (1 << 5) + BackSlash,
                BackSlashRight = BackSlash + (1 << 7),
                BackSlashCrooked = BackSlash + (1 << 10),
            }

            public enum CenterLine : ushort
            {
                None = 0,
                HorizontalCenter = (1 << 8) + (1 << 13),
                VerticalCenter = (1 << 9) + (1 << 13),
                CrossCenter = (1 << 8) + (1 << 9) + (1 << 13),
            }

            public bool CubeEffect { get; }

            public bool ShadeEffect { get; }

            public DiagonalType Type { get; }

            public SlashDiagonal SlashDiagonalType { get; }

            public BackSlashDiagonal BackSlashDiagonalType { get; }

            public CenterLine CenterLineType { get; }

            internal BorderProperty(UInt16 property)
            {
                CubeEffect = (property & 1) != 0;

                ShadeEffect = (property & 2) != 0;

                SlashDiagonal slash;
                SlashDiagonalType = Enum.TryParse($"{property & 0x091C}", out slash) ? slash : SlashDiagonal.None;

                BackSlashDiagonal backSlash;
                BackSlashDiagonalType = Enum.TryParse($"{property & 0x14E0}", out backSlash)
                    ? backSlash
                    : BackSlashDiagonal.None;

                CenterLine center;
                CenterLineType = Enum.TryParse($"{property & 0x2300}", out center) ? center : CenterLine.None;

                if (center != CenterLine.None)
                {
                    Type = DiagonalType.Center;
                }
                else if (slash != SlashDiagonal.None && backSlash != BackSlashDiagonal.None)
                {
                    Type = DiagonalType.Cross;
                }
                else if (slash != SlashDiagonal.None)
                {
                    Type = DiagonalType.Slash;
                }
                else if (backSlash != BackSlashDiagonal.None)
                {
                    Type = DiagonalType.BackSlash;
                }
                else
                {
                    Type = DiagonalType.None;
                }
            }

            public BorderProperty(bool cubeEffect = false, bool shadeEffect = false,
                DiagonalType type = DiagonalType.None, SlashDiagonal slash = SlashDiagonal.None,
                BackSlashDiagonal backSlash = BackSlashDiagonal.None, CenterLine center = CenterLine.None)
            {
                CubeEffect = cubeEffect;
                ShadeEffect = shadeEffect;
                Type = type;
                SlashDiagonalType = slash;
                BackSlashDiagonalType = backSlash;
                CenterLineType = center;
            }
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public enum Weight : byte
        {
            Mm0_1 = 0,
            Mm0_12 = 1,
            Mm0_15 = 2,
            Mm0_2 = 3,
            Mm0_25 = 4,
            Mm0_3 = 5,
            Mm0_4 = 6,
            Mm0_5 = 7,
            Mm0_6 = 8,
            Mm0_7 = 9,
            Mm1_0 = 10,
            Mm1_5 = 11,
            Mm2_0 = 12,
            Mm3_0 = 13,
            Mm4_0 = 14,
            Mm5_0 = 15,
        }
        
        [DebuggerDisplay("Type = {Type}, Weight = {Weight}, Color = {Color}")]
        public struct Border
        {
            public enum LineType : byte
            {
                None = 0,
                Solid = 1,
                Dash = 2,
                Dot = 3,
                DashDot = 4,
                DashDotDot = 5,
                LongDash = 6,
                Circle = 7,
                DoubleLine = 8,
                ThinThick = 9,
                ThickThin = 10,
                ThinThickThin = 11,
                Wave = 12,
                DoubleWave = 13,
                Thick3D = 14,
                Thick3DInverse = 15,
                Solid3D = 16,
                Solid3DInverse = 17,
            }

            public LineType Type { get; }

            public Weight Weight { get; }

            public Color Color { get; }

            internal Border(UInt8 type, UInt8 weight, Color color)
            {
                LineType lineType;
                Type = Enum.TryParse($"{type}", out lineType) ? lineType : LineType.Solid;

                Weight weightValue;
                Weight = Enum.TryParse($"{weight}", out weightValue) ? weightValue : Weight.Mm0_1;

                Color = color;
            }

            public Border(LineType type = LineType.Solid, Weight weight = Weight.Mm0_1, Color color = default(Color))
            {
                Type = type;
                Weight = weight;
                Color = color;
            }
        }

        [DebuggerDisplay("Type = {Type}, Weight = {Weight}, Color = {Color}")]
        public struct Diagonal
        {
            public enum DiagonalType : byte
            {
                Slash = 0,
                BackSlash = 1,
                CrookedSlash = 2,
            }

            public DiagonalType Type { get; }

            public Weight Weight { get; }

            public Color Color { get; }

            internal Diagonal(UInt8 type, UInt8 weight, Color color)
            {
                DiagonalType diagonal;
                Type = Enum.TryParse($"{type}", out diagonal) ? diagonal : DiagonalType.Slash;

                Weight weightValue;
                Weight = Enum.TryParse($"{weight}", out weightValue) ? weightValue : Weight.Mm0_1;

                Color = color;
            }

            public Diagonal(DiagonalType type = DiagonalType.Slash, Weight weight = Weight.Mm0_1, Color color = default(Color))
            {
                Type = type;
                Weight = weight;
                Color = color;
            }
        }

        public struct Fill
        {
            [Flags]
            public enum FillType
            {
                None = 0,
                Solid = 1,
                Image = 2,
                Gradation = 4,
            }
            
            [DebuggerDisplay("Background = {Background}, Foreground = {Foreground}, Pattern = {Pattern}")]
            public struct SolidFill
            {
                public enum PatternType
                {
                    None = 0,
                    Horizontal = 1,
                    Vertical = 2,
                    BackSlash = 3,
                    Slash = 4,
                    Plus = 5,
                    Cross = 6,
                }

                public Color Background { get; }

                public Color Foreground { get; }

                public PatternType Pattern { get; }

                internal SolidFill(Color background, Color foreground, Int32 pattern)
                {
                    Background = background;

                    Foreground = foreground;

                    PatternType type;
                    Pattern = Enum.TryParse($"{pattern}", out type) ? type : PatternType.None;
                }

                public SolidFill(Color background = default(Color), Color foreground = default(Color),
                    PatternType pattern = PatternType.None)
                {
                    Background = background;
                    Foreground = foreground;
                    Pattern = pattern;
                }
            }

            public struct GradationFill
            {
                public enum GradationType
                {
                    None = 0,
                    Stripe = 1,
                    Circular = 2,
                    Cone = 3,
                    Square = 4,
                }

                public struct GradationColor
                {
                    public Int32? Position { get; }

                    public Color Color { get; }

                    public GradationColor(Int32? position, Color color)
                    {
                        Position = position;
                        Color = color;
                    }
                }

                public GradationType Type { get; }

                public Int16 Degree { get; }

                public Int16 CenterX { get; }

                public Int16 CenterY { get; }

                public Int16 Blur { get; }

                public IList<GradationColor> Colors { get; }

                internal GradationFill(byte[] bytes, int pos = 0)
                {
                    GradationType type;
                    Type = Enum.TryParse($"{bytes.ToInt16(pos)}", out type) ? type : GradationType.None;
                    pos += 2;

                    Degree = bytes.ToInt16(pos);
                    pos += 2;

                    CenterX = bytes.ToInt16(pos);
                    pos += 2;

                    CenterY = bytes.ToInt16(pos);
                    pos += 2;

                    Blur = bytes.ToInt16(pos);
                    pos += 2;

                    var num = bytes.ToInt16(pos);
                    pos += 2;
                    Colors = new List<GradationColor>();

                    List<Int32> positions = null;
                    if (num > 2)
                    {
                        positions = new List<Int32>();
                        for (var i = 0; i < num; i++)
                        {
                            positions.Add(bytes.ToInt32(pos));
                            pos += 4;
                        }
                    }

                    for (var i = 0; i < num; i++)
                    {
                        Colors.Add(new GradationColor(positions?[i], bytes.ToColor(pos)));
                        pos += 4;
                    }
                }

                public GradationFill(GradationType type = GradationType.None, Int16 degree = default(Int16),
                    Int16 centerX = default(Int16), Int16 centerY = default(Int16), Int16 blur = default(Int16),
                    IEnumerable<GradationColor> colors = null)
                {
                    Type = type;
                    Degree = degree;
                    CenterX = centerX;
                    CenterY = centerY;
                    Blur = blur;
                    Colors = colors?.ToList() ?? new List<GradationColor>();
                }
            }

            public struct ImageFill
            {
                public enum ImageFillType : byte
                {
                    Tile = 0,
                    TileTop = 1,
                    TileBottom = 2,
                    TileLeft = 3,
                    TileRight = 4,
                    Fit = 5,
                    CenterCenter = 6,
                    CenterTop = 7,
                    CenterBottom = 8,
                    LeftCenter = 9,
                    LeftTop = 10,
                    LeftBottom = 11,
                    RightCenter = 12,
                    RightTop = 13,
                    RightBottom = 14,
                    None = 15,
                }

                public struct Image
                {
                    [Flags]
                    public enum ImageEffect
                    {
                        None = 0,
                        Grayscale = 1,
                        BlackWhite = 2,
                        Pattern = 4,
                    }

                    public Int8 Brightness { get; }

                    public Int8 Contrast { get; }

                    public ImageEffect Effect { get; }

                    public UInt16 BinItemId { get; }

                    internal Image(Int8 brightness, Int8 contrast, Byte effect, UInt16 binItemId)
                    {
                        Brightness = brightness;
                        Contrast = contrast;

                        ImageEffect imageEffect;
                        Effect = Enum.TryParse($"{effect}", out imageEffect) ? imageEffect : ImageEffect.None;

                        BinItemId = binItemId;
                    }

                    public Image(Int8 brightness, Int8 contrast, ImageEffect effect, UInt16 binItemId)
                    {
                        Brightness = brightness;
                        Contrast = contrast;
                        Effect = effect;
                        BinItemId = binItemId;
                    }
                }

                public ImageFillType Type { get; }

                public Image ImageData { get; }

                public DWord AdditionalGradationSize { get; }

                public Byte GradationCenter { get; }

                public ImageFill(ImageFillType type, Image image, DWord gradationSize, Byte gradationCenter)
                {
                    Type = type;
                    ImageData = image;
                    AdditionalGradationSize = gradationSize;
                    GradationCenter = gradationCenter;
                }
            }

            public FillType Type { get; }

            private readonly SolidFill _solid;
            public SolidFill Solid
            {
                get
                {
                    if (!Type.HasFlag(FillType.Solid))
                    {
                        throw new HwpUnsupportedProperty();
                    }
                    return _solid;
                }
            }

            private readonly GradationFill _gradation;
            public GradationFill Gradation
            {
                get
                {
                    if (!Type.HasFlag(FillType.Gradation))
                    {
                        throw new HwpUnsupportedProperty();
                    }
                    return _gradation;
                }
            }

            private readonly ImageFill _image;
            public ImageFill Image
            {
                get
                {
                    if (!Type.HasFlag(FillType.Image))
                    {
                        throw new HwpUnsupportedProperty();
                    }
                    return _image;
                }
            }

            public byte[] AdditionalFill { get; }
        }


        public BorderProperty Property { get; }

        public Border Left { get; }

        public Border Right { get; }

        public Border Top { get; }

        public Border Bottom { get; }

        public Diagonal DiagonalProperty { get; }

        public BorderFill(uint level, byte[] bytes, DocumentInformation _ = null)
            : base(BorderFillTagId, level, (uint) bytes.Length, bytes)
        {
            var pos = 0;

            Property = new BorderProperty(bytes.ToUInt16());
            pos += 2;

            Left = new Border(bytes[pos], bytes[pos + 1], bytes.ToColor(pos + 2));
            pos += 6;

            Right = new Border(bytes[pos], bytes[pos + 1], bytes.ToColor(pos + 2));
            pos += 6;

            Top = new Border(bytes[pos], bytes[pos + 1], bytes.ToColor(pos + 2));
            pos += 6;

            Bottom = new Border(bytes[pos], bytes[pos + 1], bytes.ToColor(pos + 2));
            pos += 6;

            DiagonalProperty = new Diagonal(bytes[pos], bytes[pos + 1], bytes[pos + 2]);
            pos += 6;
        }
    }
}
