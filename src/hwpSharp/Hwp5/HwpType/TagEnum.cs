namespace hwpSharp.Hwp5.HwpType
{
    /// <summary>
    /// Specifies a tag ID of a hwp 5 data record.
    /// </summary>
    public enum TagEnum
    {
        Unknown = -1,

        #region DocInfo

        /// <summary>
        /// Document property.
        /// </summary>
        DocumentProperties = DataRecord.HwpTagBegin,
        IdMappings = DataRecord.HwpTagBegin + 1,
        BinDate = DataRecord.HwpTagBegin + 2,

        #endregion

        #region BodyText

        ParagraphHeader = DataRecord.HwpTagBegin + 50,
        ParagraphText = DataRecord.HwpTagBegin + 51,
        Table = DataRecord.HwpTagBegin + 61

        #endregion
    }
}
