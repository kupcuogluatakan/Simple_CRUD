using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.CountryVatRatio
{
    public class CountryVatRatioViewModel:ModelBase
    {
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
