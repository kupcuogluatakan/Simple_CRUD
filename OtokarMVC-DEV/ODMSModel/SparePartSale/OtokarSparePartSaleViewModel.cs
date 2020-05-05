using FluentValidation.Attributes;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.SparePartSale
{
    [Validator(typeof(OtokarSparePartSaleViewModelValidator))]
    public class OtokarSparePartSaleViewModel : ModelBase
    {
        public int SparePartSaleId { get; set; }
        [Display(Name = "Customer_Display_CustomerTypeName", ResourceType = typeof(MessageResource))]
        public int? CustomerTypeId { get; set; }
        [Display(Name = "Customer_Display_CustomerTypeName", ResourceType = typeof(MessageResource))]
        public string CustomerType { get; set; }
        public string CurrencyCode { get; set; }
        [Display(Name = "User_Display_DealerName", ResourceType = typeof(MessageResource))]
        public int DealerId { get; set; }
        [Display(Name = "User_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "SparePartSale_Display_BillingAddress", ResourceType = typeof(MessageResource))]
        public int? BillingAddressId { get; set; }
        [Display(Name = "SparePartSale_Display_BillingAddress", ResourceType = typeof(MessageResource))]
        public string BillingAddress { get; set; }
        [Display(Name = "SparePartSale_Display_ShippingAddress", ResourceType = typeof(MessageResource))]
        public int? ShippingAddressId { get; set; }
        [Display(Name = "SparePartSale_Display_ShippingAddress", ResourceType = typeof(MessageResource))]
        public string ShippingAddress { get; set; }
        [Display(Name = "SparePartSale_Display_SaleDate", ResourceType = typeof(MessageResource))]
        public DateTime? SaleDate { get; set; }
        [Display(Name = "SparePartSale_Display_SaleResponsible", ResourceType = typeof(MessageResource))]
        public string SaleResponsible { get; set; }
        [Display(Name = "SparePartSale_Display_WayBillSerialNo", ResourceType = typeof(MessageResource))]
        public string WayBillSerialNo { get; set; }
        [Display(Name = "SparePartSale_Display_WayBillDate", ResourceType = typeof(MessageResource))]
        public DateTime? WayBilllDate { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvNo", ResourceType = typeof(MessageResource))]
        public string InvoiceNo { get; set; }
        [Display(Name = "SparePartSale_Display_WayBillNo", ResourceType = typeof(MessageResource))]
        public string WaybillNo { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvDate", ResourceType = typeof(MessageResource))]
        public DateTime? InvoiceDate { get; set; }
        [Display(Name = "SparePartSale_Display_DueDuration", ResourceType = typeof(MessageResource))]
        public int? DueDuration { get; set; }
        [Display(Name = "WorkorderInvoicePayments_Display_PaymentTypeName", ResourceType = typeof(MessageResource))]
        public int? PaymentTypeId { get; set; }
        [Display(Name = "WorkorderInvoicePayments_Display_PaymentTypeName", ResourceType = typeof(MessageResource))]
        public string PaymentType { get; set; }
        [Display(Name = "Bank_Display_Name", ResourceType = typeof(MessageResource))]
        public int? BankId { get; set; }
        [Display(Name = "Bank_Display_Name", ResourceType = typeof(MessageResource))]
        public string Bank { get; set; }
        [Display(Name = "WorkorderInvoicePayments_Display_InstallmentNumber", ResourceType = typeof(MessageResource))]
        public int? InstallmentNumber { get; set; }

        [Display(Name = "WorkorderInvoicePayments_Display_ActualDispatchDate", ResourceType = typeof(MessageResource))]
        public bool IsPrintActualDispatchDate { get; set; }

        //[Display(Name = "WorkorderInvoicePayments_Display_PayAmount", ResourceType = typeof(MessageResource))]
        //public decimal? PaymentAmount { get; set; }
        [Display(Name = "WorkorderInvoicePayments_Display_TransmitNumber", ResourceType = typeof(MessageResource))]
        public string TransmitNo { get; set; }
        public long? SparePartSaleWaybillId { get; set; }
        public int SaleStatusLookKey { get; set; }
        [Display(Name = "SparePartSale_Display_SaleStatus", ResourceType = typeof(MessageResource))]
        public string SaleStatusLookVal { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public int CustomerId { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvSerialNo", ResourceType = typeof(MessageResource))]
        public string InvoiceSerialNo { get; set; }
        [Display(Name = "SparePartSale_Display_WayBillDate", ResourceType = typeof(MessageResource))]
        public DateTime? WaybillDate { get; set; }

        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "SparePartSale_Display_SaleType", ResourceType = typeof(MessageResource))]
        public int? SaleTypeId { get; set; }
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }
        public int? IsTenderSale { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_IsTenderSaleName", ResourceType = typeof(MessageResource))]
        public string IsTenderSaleName { get; set; }
        [Display(Name = "SparePartSale_Display_SaleType", ResourceType = typeof(MessageResource))]
        public string SaleTypeName { get; set; }
        public bool? IsReturn { get; set; }
        [Display(Name = "SparePartSale_Display_IsReturn", ResourceType = typeof(MessageResource))]
        public string IsReturnName { get; set; }

        public int? VehicleId { get; set; }
        [Display(Name = "SparePartSale_Display_Vehicle", ResourceType = typeof(MessageResource))]
        public string VehicleName { get; set; }

        public long? PoNumber { get; set; }

        [Display(Name = "SparePartSale_Display_StockType", ResourceType = typeof(MessageResource))]
        public int? StockTypeId { get; set; }
        [Display(Name = "SparePartSale_Display_StockType", ResourceType = typeof(MessageResource))]
        public string StockTypeName { get; set; }

        public int? PriceListId { get; set; }
        public int? VatExclude { get; set; }
        public bool IsCustomerDealer { get; set; }

        public bool BankRequired { get; set; }
        public bool InstalmentNumberRequired { get; set; }
        public bool DefermentNumberRequired { get; set; }
        public bool? IsPartOriginal { get; set; }
    }
}
