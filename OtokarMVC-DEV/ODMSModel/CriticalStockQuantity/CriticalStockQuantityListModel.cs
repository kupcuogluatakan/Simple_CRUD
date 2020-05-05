using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.CriticalStockQuantity
{
    public class CriticalStockQuantityListModel : BaseListWithPagingModel
    {
        public CriticalStockQuantityListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PartId", "ID_PART"},
                    {"PartCode","PART_CODE"},
                    {"PartName","PART_NAME"},
                    {"DealerId","ID_DEALER"},
                    {"DealerName", "DEALER_NAME"},
                    {"CriticalStockLevel","CRITICAL_STOCK_QUANTITY"},
                    {"StockQuantity","SUM(STD.QUANTITY)"}
                };
            SetMapper(dMapper);
        }

        public CriticalStockQuantityListModel()
        {
        }

        public int? PartId { get; set; }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        public int? DealerId { get; set; }
        [Display(Name = "CriticalStockQuantity_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "CriticalStockQuantity_Display_CriticalStockLevel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal CriticalStockLevel { get; set; }

        [Display(Name = "CriticalStockQuantity_Display_StockQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal StockQuantity { get; set; }

        public int StockTypeId { get; set; }
    }
}
