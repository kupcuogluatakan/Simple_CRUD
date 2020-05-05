using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.CampaignRequest
{
    public class CampaignRequestDetailListModel : BaseListWithPagingModel
    {
        public CampaignRequestDetailListModel([DataSourceRequest] DataSourceRequest request)
    : base(request)
        {
           }

        public CampaignRequestDetailListModel()
        {
        }

        public int CampaignRequestId { get; set; }

        public int PartId { get; set; }
        public string PartName { get; set; }

        public decimal Quantity { get; set; }

        public int SupplyTypeId { get; set; }
        public string SupplyTypeName { get; set; }

        public decimal MstQty { get; set; }
        public decimal DetQty { get; set; }
        public decimal PackageQty { get; set; }

    }
}
