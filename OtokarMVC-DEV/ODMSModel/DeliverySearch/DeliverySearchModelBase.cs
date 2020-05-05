namespace ODMSModel.DeliverySearch
{
    public class DeliverySearchModelBase
    {
        public int ParentWindowWidth { get; set; }
        public int ParentWindowHeight { get; set; }
        public int PopupWindowWidth { get; set; }
        public int PopupWindowHeight { get; set; }

        public string PopupCloseFunction { get; set; }
        public string PopupOpenFunction { get; set; }

        public SearchType SearchType { get; set; }

        public DeliverySearchModelBase()
        {
            SearchType = SearchType.Main;
        }

    }

    public enum SearchType:byte
    {
        Main=0x00,
        WithPart=0x01
    }

}
