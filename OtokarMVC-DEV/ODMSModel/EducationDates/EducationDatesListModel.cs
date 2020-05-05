using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.EducationDates
{
    public class EducationDatesListModel : BaseListWithPagingModel
    {
        public EducationDatesListModel ([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"EducationCode", "EDUCATION_CODE"},
                    {"RowNumber", "ROW_NUMBER"},
                    {"EducationDate","EDUCATION_TIME"},
                    {"EducationPlace", "EDUCATION_PLACE"},
                    {"Instructor", "INSTRUCTOR"},
                    {"Note", "NOTES"},
                    {"MaximumAtt", "MAX_ATTENDEE"},
                    {"MinimumAtt", "MIN_ATTENDEE"}
                };
            SetMapper(dMapper);

        }

        public EducationDatesListModel()
        {
        }

        [Display(Name = "Education_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationCode { get; set; }

        public int RowNumber { get; set; }

        [Display(Name = "EducationDates_Display_Date", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationDate { get; set; }

        [Display(Name = "EducationDates_Display_Date", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime EducationDateTime { get; set; }

        [Display(Name = "EducationDates_Display_Hour", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationHour { get; set; }

        [Display(Name = "EducationDates_Display_Place", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationPlace { get; set; }

        [Display(Name = "EducationDates_Display_Instructor", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Instructor { get; set; }

        [Display(Name = "EducationDates_Display_Note", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Note { get; set; }

        [Display(Name = "EducationDates_Display_Max", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MaximumAtt { get; set; }

        [Display(Name = "EducationDates_Display_Min", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MinimumAtt { get; set; }

    }
}
