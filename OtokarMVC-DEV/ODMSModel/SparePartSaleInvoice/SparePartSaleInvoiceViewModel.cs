using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ODMSCommon.Resources;
using FluentValidation.Attributes;

namespace ODMSModel.SparePartSaleInvoice
{
    [Validator(typeof(SparePartSaleInvoiceViewModelValidator))]
    public class SparePartSaleInvoiceViewModel : ModelBase
    {
        public int SparePartSaleId { get; set; }
        public int SparePartSaleWaybillId { get; set; }
        public int SparePartSaleInvoiceId { get; set; }
        [Display(Name = "SparePartSaleOrder_Display_DealerName", ResourceType = typeof(MessageResource))]
        public int DealerId { get; set; }
        [Display(Name = "SparePartSaleOrder_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public int CustomerId { get; set; }
        [Display(Name = "SaleReport_Display_InvoiceSerialNo", ResourceType = typeof(MessageResource))]
        public string InvoiceSerialNo { get; set; }
        [Display(Name = "SparePartSale_Display_InvoiceNo", ResourceType = typeof(MessageResource))]
        public string InvoiceNo { get; set; }
        [Display(Name = "SparePartSale_Display_InvoiceDate", ResourceType = typeof(MessageResource))]
        public DateTime? InvoiceDate { get; set; }
        [Display(Name = "SparePartSale_Display_DueDuration", ResourceType = typeof(MessageResource))]
        public int? DueDuration { get; set; }
        [Display(Name = "WorkorderInvoicePayments_Display_PaymentTypeName", ResourceType = typeof(MessageResource))]
        public int? PaymentTypeId { get; set; }
        [Display(Name = "Bank_Display_Name", ResourceType = typeof(MessageResource))]
        public int? BankId { get; set; }
        [Display(Name = "WorkorderInvoicePayments_Display_InstallmentNumber", ResourceType = typeof(MessageResource))]
        public short? InstalmentNumber { get; set; }
        public decimal PayAmount { get; set; }
        public string TransmitNo { get; set; }
        [Display(Name = "WorkOrderInvoiceList_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "WorkOrderInvoiceList_Display_CustomerLastName", ResourceType = typeof(MessageResource))]
        public string CustomerLastName { get; set; }
        public string SparePartSaleWaybillIdList { get; set; }
        [Display(Name = "SparePartSale_Display_BillingAddress", ResourceType = typeof(MessageResource))]
        public int? BillingAddressId { get; set; }
        [Display(Name = "CustomerAddress_Display_CountryName", ResourceType = typeof(MessageResource))]
        public string CustomerAddressCountryText { get; set; }
        [Display(Name = "CustomerAddress_Display_CityName", ResourceType = typeof(MessageResource))]
        public string CustomerAddressCityText { get; set; }
        [Display(Name = "CustomerAddress_Display_TownName", ResourceType = typeof(MessageResource))]
        public string CustomerAddressTownText { get; set; }
        [Display(Name = "CustomerAddress_Display_ZipCode", ResourceType = typeof(MessageResource))]
        public string CustomerAddressZipCode { get; set; }
        [Display(Name = "CustomerAddress_Display_Address1", ResourceType = typeof(MessageResource))]
        public string CustomerAddress1 { get; set; }
        [Display(Name = "CustomerAddress_Display_Address2", ResourceType = typeof(MessageResource))]
        public string CustomerAddress2 { get; set; }
        [Display(Name = "CustomerAddress_Display_Address3", ResourceType = typeof(MessageResource))]
        public string CustomerAddress3 { get; set; }
        [Display(Name = "Customer_Display_TaxOffice", ResourceType = typeof(MessageResource))]
        public string CustomerTaxOffice { get; set; }
        [Display(Name = "Customer_Display_TaxNo", ResourceType = typeof(MessageResource))]
        public string CustomeTaxNo { get; set; }
        [Display(Name = "Customer_Display_TCIdentityNo", ResourceType = typeof(MessageResource))]
        public string CustomerTCIdentity { get; set; }
        [Display(Name = "Customer_Display_PassportNo", ResourceType = typeof(MessageResource))]
        public string CustomerPassportNo { get; set; }
        public bool IsActive { get; set; }
        public string IsActiveString { get; set; }

        public bool BankRequired { get; set; }
        public bool InstalmentRequired { get; set; }
        public bool DefermentRequired { get; set; }
        public List<SelectListItem> SparePartSaleWaybillList { get; set; }
    }
}
