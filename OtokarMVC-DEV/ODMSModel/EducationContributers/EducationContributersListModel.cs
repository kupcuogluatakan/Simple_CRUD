using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.EducationContributers
{
    public class EducationContributersListModel : BaseListWithPagingModel
    {

        public EducationContributersListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"EducationCode", "EDUCATION_CODE"},
                     {"RowNumber", "ROW_NUMBER"},
                     {"TcIdentity", "TC_IDENTITY_NO"},
                     {"FullName", "NAME_SURNAME"},
                     {"WorkingCompany", "WORKING_COMPANY"},
                     {"DealerName", "DEALER_NAME"},
                     {"Grade","GRADE"},
                     {"EducationDate","ED.EDUCATION_TIME"}
                 };
            SetMapper(dMapper);

        }

        public EducationContributersListModel()
        {
        }

        [Display(Name = "Education_Display_Code", ResourceType = typeof(MessageResource))]
        public string EducationCode { get; set; }

        public int RowNumber { get; set; }

        [Display(Name = "EducationCont_Display_TCId", ResourceType = typeof(MessageResource))]
        public string TcIdentity { get; set; }

        [Display(Name = "EducationCont_Display_FullName", ResourceType = typeof(MessageResource))]
        public string FullName { get; set; }

        [Display(Name = "EducationCont_Display_Company", ResourceType = typeof(MessageResource))]
        public string WorkingCompany { get; set; }

        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "EducationCont_Display_Grade", ResourceType = typeof(MessageResource))]
        public string Grade { get; set; }

        [Display(Name = "EducationDates_Display_Date", ResourceType = typeof(MessageResource))]
        public DateTime EducationDate { get; set; }

        [Display(Name = "EducationDates_Display_Date", ResourceType = typeof(MessageResource))]
        public string EducationDateString { get; set; }
    }
}
