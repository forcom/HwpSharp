using System;
using System.Diagnostics;
using System.Text;
using HwpSharp.Common;
using HwpSharp.Hwp5.HwpType;
using UInt16 = HwpSharp.Hwp5.HwpType.UInt16;

namespace HwpSharp.Hwp5.DocumentInformation.DataRecords
{
    public class BinData : DataRecord
    {
        public const uint BinDataTagId = HwpTagBegin + 2;

        public enum TypeProperty : byte
        {
            Link = 0,
            Enbedding = 1,
            Storage = 2,
        }

        public enum CompressionProperty : byte
        {
            StorageDefault = 0,
            Compress = 1,
            NotCompress = 2,
        }

        public enum StateProperty : byte
        {
            Never = 0,
            Success = 1,
            Failed = 2,
            Ignored = 3,
        }

        [DebuggerDisplay("Type={Type}, Compression={Compression}, State={State}")]
        public struct BinDataProperty
        {
            public TypeProperty Type { get; }

            public CompressionProperty Compression { get; }

            public StateProperty State { get; }

            public BinDataProperty(UInt16 value)
            {
                TypeProperty type;
                Type = Enum.TryParse($"{value & 0x7}", out type) ? type : TypeProperty.Link;

                CompressionProperty compression;
                Compression = Enum.TryParse($"{(value >> 4) & 0x3}", out compression)
                    ? compression
                    : CompressionProperty.StorageDefault;

                StateProperty state;
                State = Enum.TryParse($"{(value >> 8) & 0x3}", out state) ? state : StateProperty.Never;
            }

            public BinDataProperty(TypeProperty type, CompressionProperty compression, StateProperty state)
            {
                Type = type;
                Compression = compression;
                State = state;
            }

            public static implicit operator UInt16(BinDataProperty binData)
            {
                return
                    (UInt16)
                        (((ushort) binData.State << 8) + ((ushort) binData.Compression << 4) + (ushort) binData.Type);
            }
        }

        public BinDataProperty Property { get; }

        private readonly string _linkFileAbsolutePath;
        public string LinkFileAbsolutePath
        {
            get
            {
                if (Property.Type != TypeProperty.Link)
                {
                    throw new HwpUnsupportedProperty();
                }
                return _linkFileAbsolutePath;
            }
        }

        private readonly string _linkFileRelativePath;
        public string LinkFileRelativePath
        {
            get
            {
                if (Property.Type != TypeProperty.Link)
                {
                    throw new HwpUnsupportedProperty();
                }
                return _linkFileRelativePath;
            }
        }

        private readonly UInt16 _binaryDataId;
        public UInt16 BinaryDataId
        {
            get
            {
                if (Property.Type != TypeProperty.Enbedding && Property.Type != TypeProperty.Storage)
                {
                    throw new HwpUnsupportedProperty();
                }
                return _binaryDataId;
            }
        }

        private readonly string _binaryDataExtension;
        public string BinaryDataExtension
        {
            get
            {
                if (Property.Type != TypeProperty.Enbedding)
                {
                    throw new HwpUnsupportedProperty();
                }
                return _binaryDataExtension;
            }
        }

        public BinData(uint level, byte[] bytes, DocumentInformation _ = null)
            : base(BinDataTagId, level, (uint) bytes.Length, bytes)
        {
            var pos = 0;
            Property = new BinDataProperty(bytes.ToUInt16());
            pos += 2;

            if (Property.Type == TypeProperty.Link)
            {
                var absolutePathLenth = bytes.ToWord(pos);
                pos += 2;

                _linkFileAbsolutePath = Encoding.Unicode.GetString(bytes, pos, 2*absolutePathLenth);
                pos += 2*absolutePathLenth;

                var relativePathLength = bytes.ToWord(pos);
                pos += 2;

                _linkFileRelativePath = Encoding.Unicode.GetString(bytes, pos, 2*relativePathLength);
                pos += 2*relativePathLength;
            }

            if (Property.Type == TypeProperty.Enbedding || Property.Type == TypeProperty.Storage)
            {
                _binaryDataId = bytes.ToUInt16(pos);
                pos += 2;
            }

            if (Property.Type == TypeProperty.Enbedding)
            {
                var extensionLength = bytes.ToWord(pos);
                pos += 2;

                _binaryDataExtension = Encoding.Unicode.GetString(bytes, pos, 2*extensionLength);
                pos += 2*extensionLength;
            }
        }
    }
}
