using Kendo.Mvc.UI;

namespace ODMSModel.ObjectSearch
{
    public class PurchaseOrderSearchListModel:PurchaseOrder.PurchaseOrderListModel
    {
        public PurchaseOrderSearchListModel([DataSourceRequest] DataSourceRequest request):base(request)
        {
        }

        public PurchaseOrderSearchListModel()
        {
            // TODO: Complete member initialization
        }

        public int SupplierId { get; set; }
    }
}
