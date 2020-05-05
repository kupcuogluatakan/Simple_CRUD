using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.HolidayDate
{
    public class HolidayDateListModel : BaseListWithPagingModel
    {
        public HolidayDateListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"IdHolidayDate","ID_HOLIDAY_DATE" },
                    {"HolidayDate", "HOLIDAY_DATE"},
                    {"Description","DESCRIPTION"},
                    {"IdCountry","ID_COUNTRY"},
                    {"LanguageCode", "LANGUAGE_CODE"}
                };
            SetMapper(dMapper);
        }

        public HolidayDateListModel()
        {
        }

        public int? IdHolidayDate { get; set; }
        [Display(Name = "HolidayDate_Display_HolidayDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? HolidayDate { get; set; }
        [Display(Name = "CampaignLabour_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }
        [Display(Name = "CustomerAddress_Display_CountryName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdCountry { get; set; }
        [Display(Name = "CampaignDocument_Display_LanguageCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LanguageCode { get; set; }
    }
}
