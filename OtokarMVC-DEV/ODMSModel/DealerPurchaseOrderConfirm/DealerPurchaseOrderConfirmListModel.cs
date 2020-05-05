using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.DealerPurchaseOrderConfirm
{
    public class DealerPurchaseOrderConfirmListModel : BaseListWithPagingModel
    {
        public DealerPurchaseOrderConfirmListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
              var dMapper = new Dictionary<string, string>
                {
                    {"PoNumber","PO_NUMBER"},
                    {"DealerName", "DEALER_NAME"},
                    {"DesiredShipDate", "PO_DESIRED_SHIP_DATE"},
                    {"StatusName", "STATUS_NAME"}
                };
            SetMapper(dMapper);
        }

        public DealerPurchaseOrderConfirmListModel()
        {
        }

        //IdDealer
        [Display(Name = "DealerGuaranteeRatio_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int32? IdDealer { get; set; }
        
        //IdDealer
        [Display(Name = "PurchaseOrder_Display_IdSupplier", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int32? IdSupplier { get; set; }

        //DealerName
        [Display(Name = "DealerGuaranteeRatio_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        //DesiredShipDate
        [Display(Name = "PurchaseOrder_Display_DesiredShipDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? DesiredShipDate { get; set; }

        //StatusName
        [Display(Name = "PurchaseOrder_Display_StatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StatusName { get; set; }

        [Display(Name = "PurchaseOrder_Display_StatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? StatusId { get; set; }

        [Display(Name = "PurchaseOrder_Display_PoNumber", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? PoNumber { get; set; }

        public int SupplierDealerConfirm { get; set; }
    }
}
