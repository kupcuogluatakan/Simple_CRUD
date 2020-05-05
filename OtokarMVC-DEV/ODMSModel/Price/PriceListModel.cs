using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.Price
{
    public class PriceListModel : BaseListWithPagingModel
    {
        public PriceListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CampaignCode", "CAMPAIGN_CODE"},
                    {"PartCode", "PART_CODE"},
                    {"PartTypeDesc", "PART_NAME"},
                    {"Quantity", "QUANTITY"},
                    {"SupplyTypeName", "SUPPLY_TYPE"},
                    {"StockQuantity","QUANTITY_SUM" },
                    {"StockReserve","RESERVE_SUM" },
                    {"StockBlock","BLOCK_SUM" },
                    {"PriceList","ID_PRICE_LIST" },
                    {"Price","LIST_PRICE" },
                    {"ValidityStartDate","VALIDITY_START_DATE" },
                    {"ValidityStopDate","VALIDITY_STOP_DATE" },
                };
            SetMapper(dMapper);
        }

        public PriceListModel()
        {
        }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        public int PartId { get; set; }

        [Display(Name = "SparePart_Display_ChangedPartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ChangedPartCode { get; set; }
        [Display(Name = "SparePart_Display_ChangedPartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ChangedPartName { get; set; }
        public int ChangedPartId { get; set; }
        
        [Display(Name = "SparePart_Display_ChangedPartPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ChangedPartPrice { get; set; }


        [Display(Name = "StockCardPriceListModel_Display_PriceList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PriceList { get; set; }
        [Display(Name = "Global_Display_Price", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Price { get; set; }
        [Display(Name = "StockCardPriceListModel_Display_PriceList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int PriceListId { get; set; }

        [Display(Name = "Price_Display_ValidityStartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime ValidityStartDate { get; set; }
        [Display(Name = "Price_Display_ValidityStartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ValidityStartDateString { get; set; }
        [Display(Name = "Price_Display_ValidityStopDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime ValidityStopDate { get; set; }
        [Display(Name = "Price_Display_ValidityStopDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ValidityStopDateString { get; set; }
        public string StockBlock { get; set; }
        [Display(Name = "ChargePerCarReport_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }
        [Display(Name = "ChargePerCarReport_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }
        [Display(Name = "ChargePerCarReport_PartClassList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartClassList { get; set; }
        [Display(Name = "SaleReport_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCodeList { get; set; }
        [Display(Name = "Price_Display_IsValid", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsValid { get; set; }
        [Display(Name = "Price_Display_PartClassCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartClassCode { get; set; }
    }
}
