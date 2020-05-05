using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.VatRatio
{
    public class VatRatioListModel:BaseListWithPagingModel
    {
        public VatRatioListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
             var dMapper = new Dictionary<string, string>
                 {
                     {"VatRatio", "VAT_RATIO"},               
                     {"IsActiveString","STATUS"},
                     {"InvoiceLabel","INVOICE_LABEL"}
                 };
            SetMapper(dMapper);

        }

        public VatRatioListModel()
        {
        }
        [Display(Name = "Vehicle_Display_VatRatio", ResourceType = typeof(MessageResource))]
        public decimal VatRatio { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool IsActive { get; set; }

        [Display(Name = "Vehicle_Display_InvoiceLabel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string InvoiceLabel { get; set; }
    }
}
