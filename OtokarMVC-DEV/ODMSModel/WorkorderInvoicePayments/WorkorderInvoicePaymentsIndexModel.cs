using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ODMSCommon.Resources;
using ODMSModel.PaymentType;

namespace ODMSModel.WorkorderInvoicePayments
{
    public class WorkorderInvoicePaymentsIndexModel
    {
        public int WorkorderInvoiceId { get; set; }
        public int WorkorderId { get; set; }

        [Display(Name = "WorkorderInvoicePayments_Display_PaymentTypeList", ResourceType = typeof(MessageResource))]
        public List<PaymentTypeListModel> PaymentTypeList { get; set; }

        [Display(Name = "WorkorderInvoicePayments_Display_BankList", ResourceType = typeof(MessageResource))]
        public List<SelectListItem> BankList { get; set; }

        [Display(Name = "WorkorderInvoicePayments_Display_PayAmount", ResourceType = typeof(MessageResource))]
        public double? PayAmount { get; set; }

        [Display(Name = "WorkorderInvoicePayments_Display_InstallmentNumber", ResourceType = typeof(MessageResource))]
        public int? InstalmentNumber { get; set; }
    }
}
