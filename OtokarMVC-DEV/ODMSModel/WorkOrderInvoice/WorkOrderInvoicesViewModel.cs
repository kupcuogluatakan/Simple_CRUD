using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using FluentValidation.Attributes;

namespace ODMSModel.WorkOrderInvoice
{
    [Validator(typeof(WorkOrderInvoicesViewModelValidator))]
    public class WorkOrderInvoicesViewModel : ModelBase
    {
        public bool HideElements { get; set; }

        public long WorkOrderId { get; set; }

        public long WorkOrderInvoiceId { get; set; }

        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public int CustomerId { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "User_Display_Address", ResourceType = typeof(MessageResource))]
        public int? AddressId { get; set; }
        [Display(Name = "User_Display_Address", ResourceType = typeof(MessageResource))]
        public string Address { get; set; }
        [Display(Name = "LabourType_Display_VatRatio", ResourceType = typeof(MessageResource))]
        public decimal VatRatio { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvSerialNo", ResourceType = typeof(MessageResource))]
        public string InvoiceSerialNo { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvNo", ResourceType = typeof(MessageResource))]
        public string InvoiceNo { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvDate", ResourceType = typeof(MessageResource))]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "WorkOrderInvoice_Display_InvoiceRatio", ResourceType = typeof(MessageResource))]
        public Decimal? InvoiceRatio { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_InvAmount", ResourceType = typeof(MessageResource))]
        public decimal InvoiceAmount { get; set; }
        [Display(Name = "WorkOrderInvoices_Display_InvoiceVatAmount", ResourceType = typeof(MessageResource))]
        public decimal? InvoiceVatAmount { get; set; }
        [Display(Name = "WorkorderListInvoices_Display_DueDuration", ResourceType = typeof(MessageResource))]
        public int? DueDuration { get; set; }
        [Display(Name = "Currency_Display_CurrencyName", ResourceType = typeof(MessageResource))]
        public string Currrency { get; set; }
        [Display(Name = "Customer_Display_WitholdingStatusName", ResourceType = typeof(MessageResource))]
        public bool HasWitholding { get; set; }
        [Display(Name = "Customer_Display_WitholdingRate", ResourceType = typeof(MessageResource))]
        public string WitholdId { get; set; }
        public int WitholdingId { get; set; }
        public bool IsFromProposalWitholding { get; set; }
        public decimal WitholdAmount { get; set; }
        public int WitholdDivident { get; set; }
        public int WitholdDivisor { get; set; }

        [Display(Name = "WorkOrderInvoice_Display_InvoiceType", ResourceType = typeof(MessageResource))]
        public int InvoiceTypeId { get; set; }
        [Display(Name = "WorkOrderInvoice_Display_SpecialInvoiceAmount", ResourceType = typeof(MessageResource))]
        public decimal SpecialInvoiceAmount { get; set; }
        [Display(Name = "WorkOrderInvoice_Display_SpecialInvoiceVatAmount", ResourceType = typeof(MessageResource))]
        public decimal SpecialInvoiceVatAmount { get; set; }
        [Display(Name = "WorkOrderInvoice_Display_SpecialInvoiceDescription", ResourceType = typeof(MessageResource))]
        public string SpecialInvoiceDescription { get; set; }

        public static String InvoiceTypeLookKey = "INVOICE_TYPE";

        public string WorkOrderIds { get; set; }
        [Display(Name = "WorkOrderInvoice_Display_InvoiceType", ResourceType = typeof(MessageResource))]
        public InvoiceTypes InvoiceType { get; set; }

        [Flags]
        public enum InvoiceTypes : byte
        {
            Printed = 0x00,
            Transcript = 0x01
        }
    }
}
