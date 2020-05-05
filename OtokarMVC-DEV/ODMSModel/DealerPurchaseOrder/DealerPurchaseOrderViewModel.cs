using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
namespace ODMSModel.DealerPurchaseOrder
{
    [Validator(typeof(DealerPurchaseOrderValidator))]
    public class DealerPurchaseOrderViewModel  : ModelBase
    {
        //public DealerPurchaseOrderViewModel(bool isWithoutPart)
        //{
        //    if (isWithoutPart)
        //    {
                
        //    }
        //}

        public int PurchaseOrderId { get; set; }

        public int PoDetSeqNo { get; set; }

        [Display(Name = "DealerSaleSparepart_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int DealerId{ get; set; }

        [Display(Name = "DealerSaleSparepart_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? SupplierId { get; set; }

        [Display(Name = "DealerPurchaseOrder_Display_IsViaCenter", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsViaCenter { get; set; }

        [Display(Name = "DealerPurchaseOrder_Display_Vehicle", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? VehicleId { get; set; }
        [Display(Name = "VehicleCode_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleName { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? PartId { get; set; }

        [Display(Name = "DealerPurchaseOrder_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int PurchaseStatus { get; set; }

        [Display(Name = "DealerPurchaseOrder_Display_Date", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? ShipDate { get; set; }

        [Display(Name = "PurchaseOrder_Display_PoType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? PurchaseOrderType { get; set; }

        [Display(Name = "DealerPurchaseOrder_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? Quantity { get; set; }

        [Display(Name = "DealerPurchaseOrder_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? ShipQuantity { get; set; }

        [Display(Name = "DealerSaleSparepart_Display_DiscountRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? DiscountRatio { get; set; }

        [Display(Name = "DealerSaleSparepart_Display_DiscountPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? DiscountPrice { get; set; }

        [Display(Name = "DealerSaleSparepart_Display_ListPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? ListPrice { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        public DateTime? OrderDate { get; set; }
    }
}
