using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.SupplierDispatchPart
{
    [Validator(typeof(SupplierDispatchPartViewModelValidator))]
    public class SupplierDispatchPartViewModel : ModelBase
    {
        public long DeliverySeqNo { get; set; }

        public long DeliveryId { get; set; }

        [Display(Name = "SupplierDispatchPart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int PartId { get; set; }

        [Display(Name = "SupplierDispatchPart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "SupplierDispatchPart_Display_Qty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? Qty { get; set; }

        [Display(Name = "SupplierDispatchPart_Display_InvoicePrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? InvoicePrice { get; set; }

        public new int StatusId { get; set; }

    }
}
