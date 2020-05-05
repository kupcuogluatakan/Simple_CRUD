using FluentValidation.Attributes;
using ODMSModel.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.StockBlockDetail
{
    [Validator(typeof(StockBlockDetailViewModelValidator))]
    public class StockBlockDetailViewModel : ModelBase
    {
        public StockBlockDetailViewModel()
        {
            PartSearch = new AutocompleteSearchViewModel();
        }

        [Display(Name = "DealerSaleSparepart_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public AutocompleteSearchViewModel PartSearch { get; set; }

        public Int64? IdStockBlock { get; set; }

        [Display(Name = "StockBlock_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdDealer { get; set; }
        [Display(Name = "StockBlock_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "StockBlock_Display_IdPart", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? IdPart { get; set; }

        [Display(Name = "StockBlock_Display_IdStockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdStockType { get; set; }

        [Display(Name = "StockBlock_Display_BlockQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? BlockQty { get; set; }

        [Display(Name = "StockBlock_Display_UnBlockQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? UnBlockQty { get; set; }

        [Display(Name = "StockBlock_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "StockBlock_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "StockBlock_Display_IdStockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeName { get; set; }

        [Display(Name = "StockBlock_Display_RemoveBlockQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? RemoveBlockQty { get; set; }

        [Display(Name = "StockBlock_Display_RemovedBlockQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? RemovedBlockQty { get; set; }

        public Int64? IdStockBlockDet { get; set; }

        public string BlockReasonDesc { get; set; }
    }
}
