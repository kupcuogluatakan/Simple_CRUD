using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.ScrapDetail
{
    [Validator(typeof(ScrapDetailViewModelValidator))]
    public class ScrapDetailViewModel : ModelBase
    {
        [Display(Name = "Scrap_Display_ScrapId", ResourceType = typeof(MessageResource))]
        public int ScrapId { get; set; }

        public int? DealerId { get; set; }
        [Display(Name = "Scrap_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "Scrap_Display_ScrapDate", ResourceType = typeof(MessageResource))]
        public DateTime? ScrapDate { get; set; }

        public int? DocId { get; set; }
        [Display(Name = "Scrap_Display_DocName", ResourceType = typeof(MessageResource))]
        public string DocName { get; set; }

        [Display(Name = "Scrap_Display_ScrapReasonDesc", ResourceType = typeof(MessageResource))]
        public string ScrapReasonDesc { get; set; }

        public int? ScrapReasonId { get; set; }
        [Display(Name = "Scrap_Display_ScrapReasonName", ResourceType = typeof(MessageResource))]
        public string ScrapReasonName { get; set; }

        [Display(Name = "Scrap_Display_Barcode", ResourceType = typeof(MessageResource))]
        public string Barcode { get; set; }

        public int ScrapDetailId { get; set; }

        public int? PartId { get; set; }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "Scrap_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        public int? StockTypeId { get; set; }
        [Display(Name = "Scrap_Display_StockTypeName", ResourceType = typeof(MessageResource))]
        public string StockTypeName { get; set; }

        public int? WarehouseId { get; set; }
        [Display(Name = "Scrap_Display_WarehouseName", ResourceType = typeof(MessageResource))]
        public string WarehouseName { get; set; }

        public int? RackId { get; set; }
        [Display(Name = "Scrap_Display_RackName", ResourceType = typeof(MessageResource))]
        public string RackName { get; set; }

        [Display(Name = "Scrap_Display_StockQuantity", ResourceType = typeof(MessageResource))]
        public decimal StockQuantity { get; set; }

        [Display(Name = "Scrap_Display_Quantity", ResourceType = typeof(MessageResource))]
        public decimal Quantity { get; set; }
        
        public int? ConfirmIdStockTransaction { get; set; }
        
        public int? CancelIdStockTransaction { get; set; }

        public int? ConfirmUserId { get; set; }
        [Display(Name = "Scrap_Display_ConfirmUserName", ResourceType = typeof(MessageResource))]
        public string ConfirmUserName { get; set; }

        public int? CancelUserId { get; set; }
        [Display(Name = "Scrap_Display_CancelUserName", ResourceType = typeof(MessageResource))]
        public string CancelUserName { get; set; }

        public ScrapDetailViewModel()
        {
        }
    }
}
