using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.WorkorderInvoicePayments
{
    [Validator(typeof(WorkorderInvoicePaymentsDetailModelValidator))]
    public class WorkorderInvoicePaymentsDetailModel : ModelBase
    {
        public int Id { get; set; }
        public int WorkorderInvoiceId { get; set; }
        public int WorkorderId { get; set; }

        public int? PaymentTypeId { get; set; }
        public int? BankId { get; set; }

        public bool BankRequired { get; set; }
        public bool InstalmentNumberRequired { get; set; }
        public bool TransmitNumberRequired { get; set; }

        [Display(Name = "WorkorderInvoicePayments_Display_PaymentTypeName", ResourceType = typeof(MessageResource))]
        public string PaymentTypeName { get; set; }

        [Display(Name = "Bank_Display_Name", ResourceType = typeof(MessageResource))]
        public string BankName { get; set; }

        [Display(Name = "WorkorderInvoicePayments_Display_InstalmentNumber", ResourceType = typeof(MessageResource))]
        public int? InstalmentNumber { get; set; }

        [Display(Name = "WorkorderInvoicePayments_Display_PaymentAmount", ResourceType = typeof(MessageResource))]
        public double PayAmount { get; set; }

        [Display(Name = "WorkorderInvoicePayments_Display_TransmitNumber", ResourceType = typeof(MessageResource))]
        public string TransmitNumber { get; set; }

        [Display(Name = "WorkorderInvoicePayments_Display_PaymentDate", ResourceType = typeof(MessageResource))]
        public DateTime PaymentDate { get; set; }

        public WorkorderInvoicePaymentsDetailModel()
        {
            BankRequired = true;
            InstalmentNumberRequired = true;
            TransmitNumberRequired = true;
        }

    }
}
