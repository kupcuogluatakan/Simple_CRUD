using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSCommon.Resources;

namespace ODMSModel.PurchaseOrderDetail
{
    public class PurchaseOrderSparePartDetailsModel
    {
        [Display(Name = "PurchaseOrderDetail_Display_SequenceNo", ResourceType = typeof(MessageResource))]
        public long PoDetSeqNo { get; set; }
        public long DeliveryId { get; set; }
        [Display(Name = "DeliveryList_Display_WaybillDate", ResourceType = typeof(MessageResource))]
        public DateTime WayBillDate { get; set; }
        [Display(Name = "DeliveryList_Display_WaybillNo", ResourceType = typeof(MessageResource))]
        public string WayBillNo { get; set; }
        [Display(Name = "DeliveryDetail_Display_DesiredPart", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "Delivery_Display_ShipQuantity", ResourceType = typeof(MessageResource))]
        public int ShipQuantity { get; set; }

        public long? SupplierId { get; set; }
        public int DealerId { get; set; }
        public int? SenderDealerId { get; set; }

        public string DesiredPartCode { get; set; }
        [Display(Name = "DeliveryDetail_Display_SentPart", ResourceType = typeof(MessageResource))]
        public string DesiredPartName { get; set; }


    }
}
