using System;
using System.Linq;
using hwpSharp.Common;
using OpenMcdf;

namespace hwpSharp.Hwp5
{
    /// <summary>
    /// Represents a hwp 5.0 file header.
    /// </summary>
    public class Hwp5FileHeader : IFileHeader
    {
        private const int SignatureLength = 32;
        private static readonly byte[] SignatureBytes =
        {
            0x48, 0x57, 0x50, 0x20, 0x44, 0x6f, 0x63, 0x75,
            0x6d, 0x65, 0x6e, 0x74, 0x20, 0x46, 0x69, 0x6c,
            0x65, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        }; // "HWP Document File"

        private const int VersionLength = 4;
        private const int AttributeLength = 4;

        /// <summary>
        /// Gets or sets the document file version.
        /// </summary>
        public Version FileVersion { get; set; }
        
        /// <summary>
        /// Gets or sets the compressed attribute.
        /// </summary>
        public bool Compressed { get; set; }

        /// <summary>
        /// Gets or sets the encryption attribute.
        /// </summary>
        public bool PasswordEncrypted { get; set; }

        /// <summary>
        /// Gets or sets the published document attribute.
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets whether the document has scripts.
        /// </summary>
        public bool HasScript { get; set; }

        /// <summary>
        /// Gets or sets the DRM secure attribute.
        /// </summary>
        public bool DrmSecured { get; set; }

        /// <summary>
        /// Gets or sets whether the document has XML template storages.
        /// </summary>
        public bool HasXmlTemplateStorage { get; set; }

        /// <summary>
        /// Gets or sets whether the document history is managed.
        /// </summary>
        public bool HasHistory { get; set; }

        /// <summary>
        /// Gets or sets whether the document has a digital sign.
        /// </summary>
        public bool HasSign { get; set; }

        /// <summary>
        /// Gets or sets the certificate encryption attribute.
        /// </summary>
        public bool CertificateEncrypted { get; set; }

        /// <summary>
        /// Gets or sets whether the additional certificate is stored.
        /// </summary>
        public bool CertificateReserved { get; set; }

        /// <summary>
        /// Gets or sets whether the document is encrypted with certificate and DRM.
        /// </summary>
        public bool CertificateDrmSecured { get; set; }

        /// <summary>
        /// Gets or sets the CCL document attribute.
        /// </summary>
        public bool CclDocumented { get; set; }

        /// <summary>
        /// Creates a <see cref="Hwp5FileHeader"/> instance with a blank setting.
        /// </summary>
        public Hwp5FileHeader()
        {
            FileVersion = new Version(5, 0, 0, 0);
        }

        internal Hwp5FileHeader(CFStream stream)
        {
            SetFileHeader(stream);
        }

        internal void SetFileHeader(CFStream stream)
        {
            ParseSignature(stream);
            ParseFileVersion(stream);
            ParseAttribute(stream);
        }

        private void ParseAttribute(CFStream stream)
        {
            var readCount = AttributeLength;
            var attributes = stream.GetData(36, ref readCount);

            if (readCount != AttributeLength)
            {
                throw new HwpFileFormatException("File attribute field is corrupted. File may be corrupted.");
            }

            Compressed = (attributes[0] & 0x1u) != 0;
            PasswordEncrypted = (attributes[0] & 0x2u) != 0;
            Published = (attributes[0] & 0x4u) != 0;
            HasScript = (attributes[0] & 0x8u) != 0;
            DrmSecured = (attributes[0] & 0x10u) != 0;
            HasXmlTemplateStorage = (attributes[0] & 0x20u) != 0;
            HasHistory = (attributes[0] & 0x40u) != 0;
            HasSign = (attributes[0] & 0x80u) != 0;
            CertificateEncrypted = (attributes[1] & 0x1u) != 0;
            CertificateReserved = (attributes[1] & 0x2u) != 0;
            CertificateDrmSecured = (attributes[1] & 0x4u) != 0;
            CclDocumented = (attributes[1] & 0x8u) != 0;
        }

        private void ParseFileVersion(CFStream stream)
        {
            var readCount = VersionLength;
            var versionBytes = stream.GetData(32, ref readCount);

            if (readCount != VersionLength)
            {
                throw new HwpFileFormatException("File does not have a version field. File may be corrupted.");
            }

            FileVersion = new Version(versionBytes[0], versionBytes[1], versionBytes[2], versionBytes[3]);

            if (FileVersion.Major != 5 || FileVersion.Minor != 0)
            {
                throw new HwpFileFormatException($"File version '{FileVersion}' is not imcompatible.");
            }
        }

        private static void ParseSignature(CFStream stream)
        {
            var readCount = SignatureLength;
            var signature = stream.GetData(0, ref readCount);

            if (readCount != SignatureLength || !SignatureBytes.SequenceEqual(signature))
            {
                throw new HwpFileFormatException("Signature is not matched. File may be corrupted.");
            }
        }
    }
}
