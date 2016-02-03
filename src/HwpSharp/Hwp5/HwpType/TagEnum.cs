namespace HwpSharp.Hwp5.HwpType
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
        FaceName = DataRecord.HwpTagBegin + 3,
        BorderFill = DataRecord.HwpTagBegin + 4,
        CharShape = DataRecord.HwpTagBegin + 5,
        TabDef = DataRecord.HwpTagBegin + 6,
        Numbering = DataRecord.HwpTagBegin + 7,
        Bullet = DataRecord.HwpTagBegin + 8,
        ParaShape = DataRecord.HwpTagBegin + 9,
        Style = DataRecord.HwpTagBegin + 10,
        DocData = DataRecord.HwpTagBegin + 11,
        DistributeDocData = DataRecord.HwpTagBegin + 12,
        CompatibleDocument = DataRecord.HwpTagBegin + 14,
        LayoutCompatibility = DataRecord.HwpTagBegin + 15,
        TrackChange = DataRecord.HwpTagBegin + 16,
        MemoShape = DataRecord.HwpTagBegin + 76,
        ForbiddenChar = DataRecord.HwpTagBegin + 78,
        TrackChangeShape = DataRecord.HwpTagBegin + 80,
        TrackChangeAuthor = DataRecord.HwpTagBegin + 81,
        #endregion

        #region BodyText
        ParagraphHeader = DataRecord.HwpTagBegin + 50,
        ParagraphText = DataRecord.HwpTagBegin + 51,
        ParagraphCharacterShape = DataRecord.HwpTagBegin + 52,
        ParagraphLineSegment = DataRecord.HwpTagBegin + 53,
        ParagraphRangeTag = DataRecord.HwpTagBegin + 54,
        ControlHeader = DataRecord.HwpTagBegin + 55,
        ListHeader = DataRecord.HwpTagBegin + 56,
        PageDefinition = DataRecord.HwpTagBegin + 57,
        FootnoteShape = DataRecord.HwpTagBegin + 58,
        PageBorderFill = DataRecord.HwpTagBegin + 59,
        ShapeComponent = DataRecord.HwpTagBegin + 60,
        Table = DataRecord.HwpTagBegin + 61,
        ShapeComponentLine = DataRecord.HwpTagBegin + 62,
        ShapeComponentRectangle = DataRecord.HwpTagBegin + 63,
        ShapeComponentEllipse = DataRecord.HwpTagBegin + 64,
        ShapeComponentArc = DataRecord.HwpTagBegin + 65,
        ShapeComponentPolygon = DataRecord.HwpTagBegin + 66,
        ShapeComponentCurve = DataRecord.HwpTagBegin + 67,
        ShapeComponentOle = DataRecord.HwpTagBegin + 68,
        ShapeComponentPicture = DataRecord.HwpTagBegin + 69,
        ShapeComponentContainer = DataRecord.HwpTagBegin + 70,
        ControlData = DataRecord.HwpTagBegin + 71,
        Equation = DataRecord.HwpTagBegin + 72,
        ShapeComponentTextArt = DataRecord.HwpTagBegin + 74,
        FormObject = DataRecord.HwpTagBegin + 75,
        MemoList = DataRecord.HwpTagBegin + 77,
        ChartData = DataRecord.HwpTagBegin + 79,
        VideoData = DataRecord.HwpTagBegin + 82,
        ShapeComponentUnknown = DataRecord.HwpTagBegin + 99
        #endregion
    }
}
