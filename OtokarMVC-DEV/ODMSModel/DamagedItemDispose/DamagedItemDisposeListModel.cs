using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.DamagedItemDispose
{
    public class DamagedItemDisposeListModel : BaseListWithPagingModel
    {
        public DamagedItemDisposeListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"DamageDisposeId", "ID_DAMAGE_DISPOSE"},
                    {"DealerId", "ID_DEALER"},
                    {"PartId", "ID_PART"},
                    {"PartName", "PART_NAME"},
                    {"RackId", "ID_RACK"},
                    {"RackName", "RACK_NAME"},
                    {"StockTypeId", "ID_STOCK_TYPE"},
                    {"StockTypeName", "STOCK_TYPE_NAME"},
                    {"DocId", "ID_DOC"},
                    {"DocName", "DOC_NAME"},
                    {"Description", "DESCRIPTION"},
                    {"Quantity", "QUANTITY"},
                    {"IsOriginal", "IS_ORIGINAL"},
                    {"CreateDate", "CREATE_DATE"}
                };
            SetMapper(dMapper);
        }

        public DamagedItemDisposeListModel()
        {
        }

        public int DamageDisposeId { get; set; }

        public int? DealerId { get; set; }
        [Display(Name = "DamagedItemDispose_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        public int? PartId { get; set; }
        [Display(Name = "DamagedItemDispose_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        
        public bool? IsOriginal { get; set; }
        [Display(Name = "DamagedItemDispose_Display_IsOriginalName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsOriginalName { get; set; }

        public int? WarehouseId { get; set; }
        [Display(Name = "DamagedItemDispose_Display_WarehouseName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarehouseName { get; set; }

        public int? RackId { get; set; }
        [Display(Name = "DamagedItemDispose_Display_RackName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RackName { get; set; }

        public int? StockTypeId { get; set; }
        [Display(Name = "DamagedItemDispose_Display_StockTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeName { get; set; }

        public int? DocId { get; set; }
        [Display(Name = "DamagedItemDispose_Display_DocumentName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DocName { get; set; }

        [Display(Name = "DamagedItemDispose_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }

        [Display(Name = "DamagedItemDispose_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }

        [Display(Name = "DamagedItemDispose_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }
        [Display(Name = "DamagedItemDispose_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }
        [Display(Name = "DamagedItemDispose_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? CreateDate { get; set; }
    }
}
