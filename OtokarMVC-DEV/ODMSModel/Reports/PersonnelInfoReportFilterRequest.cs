namespace ODMSModel.Reports
{
    using System.ComponentModel.DataAnnotations;
    using Kendo.Mvc.UI;
    using System;
    public class PersonnelInfoReportFilterRequest : ReportListModelBase
    {
        #region ctors
        public PersonnelInfoReportFilterRequest(DataSourceRequest request) : base(request)
        {
            SetMapper(null);
        }
        public PersonnelInfoReportFilterRequest()
        {
        }
        #endregion

        #region public properties

        [Display(Name = "WorkOrderPerformanceReport_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public string DealerIdList { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_UserCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string UserCodeList { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int IsActive { get; set; }

        [Display(Name = "User_Display_IdentityNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IdentityNoList { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_Title", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TitleList { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_ShowEducationDetails", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool ShowEducationDetails { get; set; }

        #region educational filters
        [Display(Name = "PersonnelInfoReport_Display_EducationCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationCode { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_EducationName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationName { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_VehicleModel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModel { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_EducationType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EducationType { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_EducationBeginDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EducationBeginDate { get; set; }

        [Display(Name = "PersonnelInfoReport_Display_EducationEndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EducationEndDate { get; set; }


        #endregion
        #endregion


    }
}
