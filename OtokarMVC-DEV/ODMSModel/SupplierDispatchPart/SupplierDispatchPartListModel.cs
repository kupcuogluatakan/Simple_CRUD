using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.SupplierDispatchPart
{
    public class SupplierDispatchPartListModel : BaseListWithPagingModel
    {
        public SupplierDispatchPartListModel()
        {

        }

        public SupplierDispatchPartListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"PartCode","PART_CODE"},
                     {"Qty", "DD.RECEIVED_QUANT"},
                     {"InvoiceP", "INVOICE_PRICE"},
                     {"PartName","PART_NAME"},
                     {"InvoicePriceString","INVOICE_PRICE"},
                     {"PONumber","PO_NUMBER"},
                     {"DesirePartCode","DESIRE_PART_CODE"}
                 };
            SetMapper(dMapper);
        }

        public long DeliverySeqNo { get; set; }

        public long DeliveryId { get; set; }

        public string WayBillNo { get; set; }

        public string WayBillDate { get; set; }

        public int SupplierId { get; set; }

        [Display(Name = "SupplierDispatchPart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        public long PartId { get; set; }

        [UIHint("DecimalNumericTextbox")]
        [Display(Name = "SupplierDispatchPart_Display_Qty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Qty { get; set; }

        //[UIHint("DecimalNumericTextbox")]

        //[NumericTextBoxUIHint("NumericTextBox","InvoicePrice",2,0,10)]
        [UIHint("DecimalNumericTextbox")]
        [Display(Name = "SupplierDispatchPart_Display_InvoicePrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal InvoiceP { get; set; }

        public string CommandType { get; set; }

        public int ErrorNo { get; set; }

        public string ErrorMessage { get; set; }

        public int StatusId { get; set; }
        [Display(Name = "SupplierDispatchPart_Display_InvoicePrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoicePriceString { get; set; }
        [Display(Name = "SupplierDispatchPart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "PurchaseOrder_Display_PoNumber", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long? PONumber { get; set; }
        [Display(Name = "SupplierDispatchPart_Display_ShipPartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DesirePartCode { get; set; }
    }
}
