using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.PurchaseOrder
{
    public class PurchaseOrderListModel : BaseListWithPagingModel
    {
        public PurchaseOrderListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PoNumber", "PO_NUMBER"},
                    {"PoTypeName","PURCHASE_ORDER_TYPE_NAME"},
                    {"VinNo","VIN_NO"},
                    {"DesiredShipDate", "PO_DESIRED_SHIP_DATE"},
                    {"StatusName", "STATUS_LOOKVAL"},
                    {"IsBranchOrder", "IS_BRANCH_ORDER"},
                    {"SapOfferNo","SAP_OFFER_NO"},
                    {"OrderDate","ORDER_DATE"},
                    {"StockType","MAINT_NAME"}
                };
            SetMapper(dMapper);
        }

        public PurchaseOrderListModel()
        {
        }
        [Display(Name = "Delivery_Display_SapOfferNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SapOfferNo { get; set; }
        //PoNumber
        [Display(Name = "PurchaseOrder_Display_PoNumber", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? PoNumber { get; set; }
        [Display(Name = "PurchaseOrder_Display_PoNumber", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string _PoNumber { get; set; }

        //PoType
        [Display(Name = "PurchaseOrder_Display_PoType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int32? PoType { get; set; }

        //PoTypeName
        [Display(Name = "PurchaseOrder_Display_PoType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PoTypeName { get; set; }

        //DesiredShipDate
        [Display(Name = "PurchaseOrder_Display_DesiredShipDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? DesiredShipDate { get; set; }

        //StatusName
        [Display(Name = "PurchaseOrder_Display_StatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StatusName { get; set; }

        //Status
        [Display(Name = "PurchaseOrder_Display_StatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int32? Status { get; set; }

        //IdStockType
        [Display(Name = "StockCard_Display_StockTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int32? IdStockType { get; set; }

        //IdDealer
        public Int32? IdDealer { get; set; }

        [Display(Name = "PurchaseOrder_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "Vehicle_Display_Plate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Plate { get; set; }

        public bool? ManuelPriceAllow { get; set; }

        [Display(Name = "PurchaseOrder_Display_IsBranchOrder", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsBranchOrder { get; set; }

        [Display(Name = "PurchaseOrder_Display_IsBranchOrder", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsBranchOrderName { get; set; }
        [Display(Name = "PurchaseOrderType_Display_IsProposal", ResourceType = typeof(MessageResource))]
        public string IsProposalName { get; set; }
        public string DealerBranchSSID { get; set; }
        public bool IsProposal { get; set; }
        public int SupplyTypeId { get; set; }
        public ODMSCommon.CommonValues.SupplyPort SupplyType { get; set; }

        [Display(Name = "PurchaseOrder_Display_CreditLimit", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CreditLimit { get; set; }

        [Display(Name = "PurchaseOrder_Display_OrderDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? OrderDate { get; set; }

        public bool? IsSASNoSent { get; set; }

        public int? IdSupplier { get; set; }

        /// <summary>
        /// Id_supplier dolu ise lokasyon Tedarikçi / Tedarikçi Adı (supplier tablosunda)Supplier_Id_Dealer dolu ise Bayi / Bayi Kısa Adı (Dealer tablosunda)
        ///(İki alan da boş ise Otokar  olarak lokasyon alanına set edilecek. 
        /// </summary>
        /// <remarks>Since this is a virtual column it will not be sortable </remarks> 
        [Display(Name = "PurchaseOrderInquiryViewModel_Display_Location", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public String Location { get; set; }

        public Int64? SupplierIdDealer { get; set; }

        #region For search criteria
        [Display(Name = "PurchaseOrder_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "PurchaseOrder_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }
        #endregion
        
     
        [Display(Name = "StockCard_Display_StockTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public String StockType { get; set; }
        public int SupplierDealerConfirm { get; set; }

        [Display(Name = "PurchaseOrderInquiryViewModel_Display_OrderLocation", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public OrderLocation OrderLocation { get; set; }

        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "PurchaseOrderInquiryViewModel_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        public decimal TotalReceivedQuantity { get; set; }
        public decimal TotalShipmentQuantity { get; set; }
        [Display(Name = "PurchaseOrderInquiryViewModel_Display_TotalPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal TotalPrice { get; set; }
        public string CampaignCode { get; set; }
    }

    public enum OrderLocation
    {
        Dealer = 1,
        Center = 2,
        Supplier = 3
    }

}
