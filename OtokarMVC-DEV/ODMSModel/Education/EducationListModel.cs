using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;
namespace ODMSModel.Education
{
    public class EducationListModel : BaseListWithPagingModel
    {
        public EducationListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"EducationCode", "EDUCATION_CODE"},
                    {"EducationName", "EDUCATION_NAME"},
                    {"AdminDesc", "ADMIN_DESC"},
                    {"EducationDurationDay","DURATION_DAY"},
                    {"EducationDurationHour","DURATION_HOUR"},
                    {"VehicleModelName", "MODEL_NAME"},
                    {"EducationTypeName", "EDUCATION_TYPE_NAME"},
                    {"IsMandatory", "IS_MANDATORY"},
                    {"IsActiveS", "IS_ACTIVE_STRING"}
                };
            SetMapper(dMapper);

        }

        public EducationListModel()
        {
            
        }
        [Display(Name = "Education_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationCode { get; set; }
        [Display(Name = "Education_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationName { get; set; }

        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "Education_Display_DurationDay", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationDurationDay { get; set; }

        [Display(Name = "Education_Display_DurationHour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationDurationHour { get; set; }

        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModelKod { get; set; }
        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModelName { get; set; }

        [Display(Name = "Education_Display_Type", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? EducationTypeId { get; set; }
        [Display(Name = "Education_Display_Type", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationTypeName { get; set; }

        [Display(Name = "Education_Display_Must", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsMandatory { get; set; }
        [Display(Name = "Education_Display_Must", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool? IsMandatorySearch { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveS { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IsActiveSearch { get; set; }
    }
}
