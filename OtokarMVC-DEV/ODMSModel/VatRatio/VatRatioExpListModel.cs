using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.VatRatio
{
    public  class VatRatioExpListModel:BaseListWithPagingModel
    {

        public VatRatioExpListModel(Kendo.Mvc.UI.DataSourceRequest request):base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"VatRatio", "VAT_RATIO"},               
                     {"Country","CL.COUNTRY_NAME"},
                     {"Explanation","EXPLANATION"},
                 };
            SetMapper(dMapper);

        }

        public VatRatioExpListModel()
        {
            // TODO: Complete member initialization
        }
        [Display(Name = "Vehicle_Display_VatRatio", ResourceType = typeof(MessageResource))]
        public decimal VatRatio { get; set; }
        [Display(Name = "Global_Display_Country", ResourceType = typeof(MessageResource))]
        public int CountryId { get; set; }
        [Display(Name = "Global_Display_Country", ResourceType = typeof(MessageResource))]
        public string Country { get; set; }
        [Display(Name = "VatRatio_Display_Explaination", ResourceType = typeof(MessageResource))]
        public string Explanation { get; set; }
        [Display(Name = "Global_Display_Warning", ResourceType = typeof(MessageResource))]
        public string Warning { get { return MessageResource.VatRatioExp_WarningMessage; } }
    }
}
