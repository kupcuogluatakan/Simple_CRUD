using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
namespace ODMSModel.WorkorderListInvoices
{
    
    public class WorkorderListInvoicesListModel : BaseListWithPagingModel
    {
        public WorkorderListInvoicesListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
            {
                {"WorkOrderInvId", "ID_WORK_ORDER_INV"},
                {"Address", "ADDRESS_1"},
                {"InvSerialNo", "INVOICE_SERIAL_NO"},//Fatura seri no
                {"InvNo", "INVOICE_NO"},//fatura no
                {"DueDuration", "DUE_DURATION"},//vade
                {"InvAmount", "INVOICE_AMOUNT"},//tutar
                {"InvVatAmount", "INVOICE_VAT_AMOUNT"},//Fatura KDV Oranı
                {"InvDate", "INVOICE_DATE"}//Tarihi

            };
            SetMapper(dMapper);
        }


        public WorkorderListInvoicesListModel() { }
      
        public int WorkOrderInvId { get; set; }

        public int WorkOrderId { get; set; }

        [Display(Name = "CustomerAddress_Display_Address1", ResourceType = typeof(MessageResource))]
        public string Address { get; set; }

        [Display(Name = "WorkorderListInvoices_Display_InvSerialNo", ResourceType = typeof(MessageResource))]
        public string InvSerialNo { get; set; }

        [Display(Name = "WorkorderListInvoices_Display_InvNo", ResourceType = typeof(MessageResource))]
        public string InvNo { get; set; }

        [Display(Name = "WorkorderListInvoices_Display_DueDuration", ResourceType = typeof(MessageResource))]
        public string DueDuration { get; set; }

        [Display(Name = "WorkorderListInvoices_Display_InvAmount", ResourceType = typeof(MessageResource))]
        public string InvAmount { get; set; }

        [Display(Name = "WorkorderListInvoices_Display_InvVatAmount", ResourceType = typeof(MessageResource))]
        public string InvVatAmount { get; set; }

        [Display(Name = "WorkorderListInvoices_Display_InvDate", ResourceType = typeof(MessageResource))]
        public string InvDate { get; set; }
    }
}
