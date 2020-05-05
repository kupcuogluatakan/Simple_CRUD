using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.SparePartSale
{
    public class SparePartSaleListModel : BaseListWithPagingModel
    {
        public bool IsSelected { get; set; }

        [Display(Name = "SparePartSale_Display_SparePartSaleId", ResourceType = typeof(MessageResource))]
        public int SparePartSaleId { get; set; }

        [Display(Name = "SparePartSale_Display_SaleDate", ResourceType = typeof(MessageResource))]
        public DateTime? SaleDate { get; set; }
        public int SaleStatusLookKey { get; set; }
        [Display(Name = "SparePartSale_Display_SaleStatus", ResourceType = typeof(MessageResource))]
        public string SaleStatusLookVal { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public int CustomerId { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "WorkorderInvoicePayments_Display_PayAmount", ResourceType = typeof(MessageResource))]
        public string PaymentAmount { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvNo", ResourceType = typeof(MessageResource))]
        public string InvoiceNo { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvSerialNo", ResourceType = typeof(MessageResource))]
        public string InvoiceSerialNo { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvDate", ResourceType = typeof(MessageResource))]
        public DateTime? InvoiceDate { get; set; }
        [Display(Name = "SparePartSale_Display_SaleStatus", ResourceType = typeof(MessageResource))]
        public string SaleStatus { get; set; }
        public int? IsTenderSale { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_IsTenderSaleName", ResourceType = typeof(MessageResource))]
        public string IsTenderSaleName { get; set; }
        [Display(Name = "SparePartSale_Display_StockType", ResourceType = typeof(MessageResource))]
        public int StockTypeId { get; set; }
        [Display(Name = "SparePartSale_Display_StockType", ResourceType = typeof(MessageResource))]
        public string StockTypeName { get; set; }


        [Display(Name = "SparePartSale_Display_SaleType", ResourceType = typeof(MessageResource))]
        public int? SaleTypeId { get; set; }
        [Display(Name = "SparePartSale_Display_SaleType", ResourceType = typeof(MessageResource))]
        public string SaleTypeName { get; set; }


        [Display(Name = "SparePartSale_Display_WayBillNo", ResourceType = typeof(MessageResource))]
        public string WaybillNo { get; set; }
        public string WaybillSerialNo { get; set; }
        [Display(Name = "SparePartSale_Display_WayBillDate", ResourceType = typeof(MessageResource))]
        public DateTime? WaybillDate { get; set; }

        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "SparePartSaleDetail_Display_TotalListPrice", ResourceType = typeof(MessageResource))]
        public decimal? TotalListPrice { get; set; }
        [Display(Name = "WorkOrderProcessTypes_TotalPrice", ResourceType = typeof(MessageResource))]
        public decimal? TotalPrice { get; set; }
        public bool DisplayCollectButton { get; set; }
        public bool DisplayCancelCollectButton { get; set; }
        public bool DisplayInvoiceButton { get; set; }
        public bool DisplayWaybillButton { get; set; }
        public bool DisplayCancelButton { get; set; }
        public int? SparePartSaleWaybillId { get; set; }
        public SparePartSaleListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"SparePartSaleId", "ID_PART_SALE"},
                     {"CustomerName", "CUSTOMER_ID"},
                     {"InvoiceNo", "INVOICE_NO"},
                     {"InvoiceSerialNo", "INVOICE_SERIAL_NO"},
                     {"InvoiceDate", "INVOICE_DATE"},
                     {"PaymentAmount", "PAY_AMOUNT"},
                     {"SaleStatus", "SALE_STATUS_LOOKVAL"},
                     {"StockTYpeName", "STL.MAINT_NAME"},
                 };
            SetMapper(dMapper);
        }

        public SparePartSaleListModel() { }

    }
}
