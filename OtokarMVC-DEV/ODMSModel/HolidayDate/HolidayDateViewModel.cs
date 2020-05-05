using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.HolidayDate
{
    [Validator(typeof(HolidayDateViewModelValidator))]
    public class HolidayDateViewModel : ModelBase
    {
        public HolidayDateViewModel()
        {
        }
        public bool HideFormElements { get; set; }

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
