using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.WorkorderInvoicePayments
{
    public class WorkorderInvoicePaymentsListModel : BaseListWithPagingModel
    {
        public int Id { get; set; }
        public int WorkorderInvoiceId { get; set; }
        public int WorkorderId { get; set; }

        public int? BankId { get; set; }

        [Display(Name = "Bank_Display_Name", ResourceType = typeof(MessageResource))]
        public string BankName { get; set; }

        public int? PaymentTypeId { get; set; }

        [Display(Name = "WorkorderInvoicePayments_Display_PaymentTypeName", ResourceType = typeof(MessageResource))]
        public string PaymentTypeName { get; set; }

        [Display(Name = "WorkorderInvoicePayments_Display_PaymentAmount", ResourceType = typeof(MessageResource))]
        public double? PayAmount { get; set; }

        [Display(Name = "WorkorderInvoicePayments_Display_InstalmentNumber", ResourceType = typeof(MessageResource))]
        public int? InstalmentNumber { get; set; }

        [Display(Name = "WorkorderInvoicePayments_Display_TransmitNumber", ResourceType = typeof(MessageResource))]
        public string TransmitNumber { get; set; }

        public WorkorderInvoicePaymentsListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"Id", "ID_WORKORDER_INVO_PAYMNT"},
                     {"WorkorderInvoiceId", "ID_WORK_ORDER_INV"},
                     {"WorkorderId", "ID_WORK_ORDER"},
                     {"BankId", "ID_BANK"},
                     {"BankName", "BANK_NAME"},
                     {"PaymentTypeId", "ID_PAYMENT_TYPE"},
                     {"PaymentTypeName", "PAYMENT_TYPE_DESC"},
                     {"PayAmount", "PAY_AMOUNT"},
                     {"InstalmentNumber", "INSTALMENT_NUMBER"},
                     {"TransmitNumber", "TRANSMIT_NO"}
                 };
            SetMapper(dMapper);
        }

        public WorkorderInvoicePaymentsListModel() { }
    }
}
