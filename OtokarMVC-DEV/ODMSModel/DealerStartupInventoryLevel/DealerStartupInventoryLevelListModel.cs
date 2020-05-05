using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.DealerStartupInventoryLevel
{
    public class DealerStartupInventoryLevelListModel : BaseListWithPagingModel
    {
        public string DealerClassCode { get; set; }
        public int? DealerClassId { get; set; }
        [Display(Name = "DealerStartupInventoryLevel_Display_DealerClassName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerClassName { get; set; }

        public int? PartId { get; set; }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "DealerStartupInventoryLevel_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }

        [Display(Name = "PurchaseOrderDetail_Display_PackageQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? PacketQuantity { get; set; }

        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        public DealerStartupInventoryLevelListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"DealerClassName", "DEALER_CLASS_CODE"},
                     {"PartCode", "PART_CODE"},
                     {"PartName", "PART_NAME"},
                     {"Quantity", "QUANTITY"},
                     {"IsActiveName", "IS_ACTIVE"},
                     {"PacketQuantity", "SHIP_QUANT"}
                 };
            SetMapper(dMapper);
        }

        public DealerStartupInventoryLevelListModel() { }
    }
}
