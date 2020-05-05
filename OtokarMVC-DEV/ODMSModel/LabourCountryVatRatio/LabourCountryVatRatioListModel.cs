using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSModel.ListModel;

namespace ODMSModel.LabourCountryVatRatio
{
    public class LabourCountryVatRatioListModel:BaseListWithPagingModel
    {
        public LabourCountryVatRatioListModel(Kendo.Mvc.UI.DataSourceRequest request):base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"CountryName", "COL.COUNTRY_NAME"},
                     {"VatRatio", "LCVT.VAT_RATIO"},
                     {"LabourName", "LL.LABOUR_TYPE_DESC"},
                     {"IsActiveString","LCVT.IS_ACTIVE"}
                 };
            SetMapper(dMapper);
        }

        public LabourCountryVatRatioListModel()
        {
        }

        [Display(Name = "LabourDuration_Display_Labour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int LabourId { get; set; }
        [Display(Name = "LabourDuration_Display_Labour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LabourName { get; set; }
        [Display(Name = "Global_Display_Country", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CountryName { get; set; }
        [Display(Name = "Global_Display_Country", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int CountryId { get; set; }
        [Display(Name = "SparePart_Display_VatRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal VatRatio { get; set; }
    }
}
