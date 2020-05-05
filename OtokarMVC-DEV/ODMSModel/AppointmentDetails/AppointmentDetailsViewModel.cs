using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using FluentValidation.Attributes;
namespace ODMSModel.AppointmentDetails
{
    [Validator(typeof(AppointmentDetailsViewModelValidator))]
    public class AppointmentDetailsViewModel:ModelBase
    {
        [Display(Name = "AppointmentDetails_Display_Appointment", ResourceType = typeof(MessageResource))]
        public int AppointmentIndicatorId { get; set; }
        [Display(Name = "Global_Display_MainCategory", ResourceType = typeof(MessageResource))]
        public int? MainCategoryId { get; set; }
        [Display(Name = "Global_Display_Category", ResourceType = typeof(MessageResource))]
        public int? CategoryId { get; set; }
        [Display(Name = "Global_Display_SubCategory", ResourceType = typeof(MessageResource))]
        public int? SubCategoryId { get; set; }
        [Display(Name = "Global_Display_SubCategory", ResourceType = typeof(MessageResource))]
        public int? SelectedSubCategoryId { get; set; }
        [Display(Name = "Global_Display_TotalPrice", ResourceType = typeof(MessageResource))]
        public decimal TotalPrice { get; set; }
        [Display(Name = "Global_Display_MainCategory", ResourceType = typeof(MessageResource))]
        public string MainCategoryName { get; set; }
        [Display(Name = "Global_Display_Category", ResourceType = typeof(MessageResource))]
        public string CategoryName { get; set; }
        [Display(Name = "Global_Display_SubCategory", ResourceType = typeof(MessageResource))]
        public string SubCategoryName { get; set; }

        public int VehicleId { get; set; }
        [Display(Name = "AppointmentDetails_Display_IndicType", ResourceType = typeof(MessageResource))]
        public string IndicatorTypeCode { get; set; }
        [Display(Name = "AppointmentDetails_Display_CampCode", ResourceType = typeof(MessageResource))]
        public string CampaignCode { get; set; }
        [Display(Name = "AppointmentDetails_Display_CupMaint", ResourceType = typeof(MessageResource))]
        public int? MaintKId { get; set; }
        [Display(Name = "AppointmentDetails_Display_PerMaint", ResourceType = typeof(MessageResource))]
        public int? MaintPId { get; set; }
        

        [Display(Name = "AppointmentDetails_Display_Appointment", ResourceType = typeof(MessageResource))]
        public int AppointmentId { get; set; }
        public bool HideElements { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Code", ResourceType = typeof(MessageResource))]
        public string FailureCodeId { get; set; }
        [Display(Name = "TechnicianOperationViewModel_Display_ProcessType", ResourceType = typeof(MessageResource))]
        public string ProcessTypeId { get; set; }
        public int ProposalSeq { get; set; }

    }
}
