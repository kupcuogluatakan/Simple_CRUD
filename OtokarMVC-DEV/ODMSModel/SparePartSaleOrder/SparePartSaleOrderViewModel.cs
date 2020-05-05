using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using FluentValidation.Attributes;

namespace ODMSModel.SparePartSaleOrder
{
    [Validator(typeof(SparePartSaleOrderViewModelValidator))]
    public class SparePartSaleOrderViewModel : ModelBase
    {
        public int SparePartSaleId { get; set; }
        [Display(Name = "SparePartSaleOrder_Display_FirmType", ResourceType = typeof(MessageResource))]
        public int? FirmTypeId { get; set; }
        [Display(Name = "SparePartSaleOrder_Display_SoNumber", ResourceType = typeof(MessageResource))]
        public string SoNumber { get; set; }

        [Display(Name = "SparePartSaleOrder_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public int? CustomerId { get; set; }
        [Display(Name = "SparePartSaleOrder_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }

        [Display(Name = "SparePartSaleOrder_Display_DealerName", ResourceType = typeof(MessageResource))]
        public int? DealerId { get; set; }
        [Display(Name = "SparePartSaleOrder_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "SparePartSaleOrder_Display_Status", ResourceType = typeof(MessageResource))]
        public int? StatusId { get; set; }
        [Display(Name = "SparePartSaleOrder_Display_Status", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

        [Display(Name = "SparePartSaleOrder_Display_SoType", ResourceType = typeof(MessageResource))]
        public int? SoTypeId { get; set; }
        [Display(Name = "SparePartSaleOrder_Display_SoType", ResourceType = typeof(MessageResource))]
        public string SoTypeName { get; set; }

        [Display(Name = "SparePartSaleOrder_Display_StockType", ResourceType = typeof(MessageResource))]
        public int? StockTypeId { get; set; }
        [Display(Name = "SparePartSaleOrder_Display_StockType", ResourceType = typeof(MessageResource))]
        public string StockTypeName { get; set; }

        [Display(Name = "SparePartSaleOrder_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SparePartSaleOrder_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "SparePartSaleOrder_Display_StartDate", ResourceType = typeof(MessageResource))]
        public DateTime? StartDate { get; set; }
        [Display(Name = "SparePartSaleOrder_Display_EndDate", ResourceType = typeof(MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "SparePartSaleOrder_Display_OrderDate", ResourceType = typeof(MessageResource))]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "SparePartSaleOrder_Display_IsProposal", ResourceType = typeof(MessageResource))]
        public bool IsProposal { get; set; }
        [Display(Name = "SparePartSaleOrder_Display_IsProposal", ResourceType = typeof(MessageResource))]
        public string IsProposalName { get; set; }

        [Display(Name = "SparePartSaleOrder_Display_IsFixedPrice", ResourceType = typeof(MessageResource))]
        public bool IsFixedPrice { get; set; }
        [Display(Name = "SparePartSaleOrder_Display_IsFixedPrice", ResourceType = typeof(MessageResource))]
        public string IsFixedPriceName { get; set; }
        public bool? IsPartOriginal { get; set; }
    }
}
