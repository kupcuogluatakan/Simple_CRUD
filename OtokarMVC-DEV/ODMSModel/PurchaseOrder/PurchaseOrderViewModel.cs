using FluentValidation.Attributes;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSModel.PurchaseOrderDetail;

namespace ODMSModel.PurchaseOrder
{
    [Validator(typeof(PurchaseOrderViewModelValidator))]
    public class PurchaseOrderViewModel : ModelBase
    {
        public PurchaseOrderViewModel()
        {

        }
        public string Location { get; set; }
        //PoNumber
        [Display(Name = "PurchaseOrder_Display_PoNumber", ResourceType = typeof(MessageResource))]
        public Int64? PoNumber { get; set; }

        //PoType
        [Display(Name = "PurchaseOrder_Display_PoType", ResourceType = typeof(MessageResource))]
        public Int32? PoType { get; set; }

        //PoTypeName
        [Display(Name = "PurchaseOrder_Display_PoTypeName", ResourceType = typeof(MessageResource))]
        public string PoTypeName { get; set; }

        //DesiredShipDate
        [DataType(DataType.Date, ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "PurchaseOrder_Display_DesiredShipDate", ResourceType = typeof(MessageResource))]
        public DateTime? DesiredShipDate { get; set; }

        public DateTime? OrderDate { get; set; }

        //StatusName
        [Display(Name = "PurchaseOrder_Display_StatusName", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

        //Status
        [Display(Name = "PurchaseOrder_Display_StatusName", ResourceType = typeof(MessageResource))]
        public new Int32? Status { get; set; }

        //IdStockType
        [Display(Name = "PurchaseOrder_Display_IdStockType", ResourceType = typeof(MessageResource))]
        public Int32? IdStockType { get; set; }
        //
        [Display(Name = "PurchaseOrderType_Display_IsProposal", ResourceType = typeof(MessageResource))]
        public bool IsProposal { get; set; }
        [Display(Name = "PurchaseOrderType_Display_IsProposal", ResourceType = typeof(MessageResource))]
        public string IsProposalName { get; set; }
        //IdDealer
        [Display(Name = "PurchaseOrder_Display_IdDealer", ResourceType = typeof(MessageResource))]
        public Int32? IdDealer { get; set; }
        [Display(Name = "Defect_Display_IdDefect", ResourceType = typeof(MessageResource))]
        public int? IdDefect { get; set; }

        [Display(Name = "PurchaseOrder_Display_IdSupplier", ResourceType = typeof(MessageResource))]
        public Int64? IdSupplier { get; set; }

        public string DealerName { get; set; }

        public string SupplierName { get; set; }
        public bool IsPriceFixed { get; set; }
        public string StockTypeName { get; set; }

        public int? SupplyType { get; set; }

        public string VehiclePlateVinNo { get; set; }

        [Display(Name = "CampaignPart_Display_SupplyType", ResourceType = typeof(MessageResource))]
        public string SupplyTypeName { get; set; }

        [Display(Name = "SparePartSale_Display_Vehicle", ResourceType = typeof(MessageResource))]
        public int? VehicleId { get; set; }

        [Display(Name = "PurchaseOrderType_Display_SalesOrganization", ResourceType = typeof(MessageResource))]
        public string SalesOrganization { get; set; }

        [Display(Name = "PurchaseOrderType_Display_ProposalType", ResourceType = typeof(MessageResource))]
        public string ProposalType { get; set; }

        [Display(Name = "PurchaseOrderType_Display_DeliveryPriority", ResourceType = typeof(MessageResource))]
        public int DeliveryPriority { get; set; }

        [Display(Name = "PurchaseOrderType_Display_OrderReason", ResourceType = typeof(MessageResource))]
        public string OrderReason { get; set; }

        [Display(Name = "PurchaseOrderType_Display_ItemCategory", ResourceType = typeof(MessageResource))]
        public string ItemCategory { get; set; }

        [Display(Name = "PurchaseOrderType_Display_DistrChan", ResourceType = typeof(MessageResource))]
        public string DistrChan { get; set; }

        [Display(Name = "PurchaseOrderType_Display_Division", ResourceType = typeof(MessageResource))]
        public string Division { get; set; }

        public bool? IsVehicleSelectionMust { get; set; }

        public int StatusDetail { get; set; }

        public bool? ManuelPriceAllow { get; set; }

        [Display(Name = "PurchaseOrder_Display_IsBranchOrder", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsBranchOrder { get; set; }

        public string DealerBranchSSID { get; set; }

        public string Description { get; set; }

        public string DealerSSID { get; set; }

        public string BranchSSID { get; set; }

        public string AllDetParts { get; set; }

        public DateTime CreateDate { get; set; }

        public Int64? SupplierIdDealer { get; set; }

        public int OrderNo { get; set; }

        public int SupplierDealerConfirm { get; set; }

        public bool? IsSASNoSent { get; set; }

        public string ModelKod { get; set; }

        public List<PurchaseOrderDetailViewModel> detailList { get; set; }

        [Display(Name = "Currency_Display_CurrencyCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }

        public string VinNo { get; set; }

        public new string UpdateUser { get; set; }

        [Display(Name = "CycleCountPlan_Display_CreateUser", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CreateUser { get; set; }
    }
}

