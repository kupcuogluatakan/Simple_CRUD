using ODMSCommon;

namespace ODMSModel.ObjectSearch
{
    public class ObjectSearchModel
    {
        public CommonValues.ObjectSearchType ObjectSearchType { get; set; }
        public string ReferenceObjectId { get; set; }
        public bool Required { get; set; }
        public string WindowTitle { get; set; }
        public string SelectCallBackFunction { get; set; }
        public string ClearCallBackFunction { get; set; }
        public long? ReferenceObjectValue { get; set; }
        public string ReferenceObjectText { get; set; }
        public string ParentWindowId { get; set; }
        public string ReferenceObjectName { get; set; }
        public string FilterId { get; set; }
        public string DataCallbackFunction { get; set; }
        public bool HideTextBox { get; set; }
    }
}
