using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSModel.ListModel;

namespace ODMSModel.CountryVatRatio
{
    public class CountryVatRatioListModel:BaseListWithPagingModel
    {

        public CountryVatRatioListModel(Kendo.Mvc.UI.DataSourceRequest request):base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"CountryName", "COL.COUNTRY_NAME"},
                     {"PartVatRatio", "PART_VAT_RATIO"},
                     {"LabourVatRatio", "LABOUR_VAT_RATIO"}
                 };
            SetMapper(dMapper);
        }

        public CountryVatRatioListModel()
        {
            
        }
        [Display(Name = "Global_Display_Country", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CountryName { get; set; }
        [Display(Name = "Global_Display_Country", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int CountryId { get; set; }
        [Display(Name = "CountryVatRatio_Display_PartVatRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Decimal PartVatRatio { get; set; }
        [Display(Name = "CountryVatRatio_Display_LabourVatRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Decimal LabourVatRatio { get; set; }

    }
}
