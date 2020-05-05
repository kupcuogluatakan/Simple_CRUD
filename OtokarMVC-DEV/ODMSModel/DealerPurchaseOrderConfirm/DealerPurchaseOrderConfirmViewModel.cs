using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.DealerPurchaseOrderConfirm
{
    public class DealerPurchaseOrderConfirmViewModel : ModelBase
    {
        public DealerPurchaseOrderConfirmViewModel()
        { 
        }

        //PoNumber
        [Display(Name = "PurchaseOrder_Display_PoNumber", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? PoNumber { get; set; }

        //DesiredShipDate
        [DataType(DataType.Date, ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "PurchaseOrder_Display_DesiredShipDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? DesiredShipDate { get; set; }

        //StatusName
        [Display(Name = "PurchaseOrder_Display_StatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StatusName { get; set; }

        //Status
        [Display(Name = "PurchaseOrder_Display_StatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public new Int32? StatusId { get; set; }

        //IdDealer
        [Display(Name = "PurchaseOrder_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int32? IdDealer { get; set; }

        public int SupplierDealerConfirm { get; set; }
    }
}
