using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.Validation;
using ODMSModel.ViewModel;
using FluentValidation.Attributes;

namespace ODMSModel.Labour
{
    [Validator(typeof(LabourViewModelValidator))]
    public class LabourViewModel : ModelBase
    {
        public LabourViewModel()
        {
            LabourName = new MultiLanguageModel();
        }

        public bool HideFormElements { get; set; }

        public string MultiLanguageContentAsText { get; set; }

        public int LabourId { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Labour_Display_MainGrp", ResourceType = typeof(MessageResource))]
        public string LabourMainGroupName { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Labour_Display_MainGrp", ResourceType = typeof(MessageResource))]
        public int? LabourMainGroupId { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Labour_Display_SubGrp", ResourceType = typeof(MessageResource))]
        public string LabourSubGroupName { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Labour_Display_SubGrp", ResourceType = typeof(MessageResource))]
        public int? LabourSubGroupId { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Labour_Display_RepairCode", ResourceType = typeof(MessageResource))]
        public string RepairCode { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Labour_Display_Code", ResourceType = typeof(MessageResource))]
        public string LabourCode { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Global_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Labour_Display_DealerDuration", ResourceType = typeof(MessageResource))]
        public bool IsDealerDuration { get; set; }
        
        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Labour_Display_SSID", ResourceType = typeof(MessageResource))]
        public string LabourSSID { get; set; }
        
        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Global_Display_IsExternal_Labour", ResourceType = typeof(MessageResource))]
        public bool IsExternal { get; set; }

        private MultiLanguageModel _labourName;
        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "Labour_Display_Name", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel LabourName { get { return _labourName ?? new MultiLanguageModel(); } set { _labourName = value; } }


        [Display(Name = "LabourType_Display_Name", ResourceType = typeof(MessageResource))]
        public int? LabourTypeId { get; set; }

        [Display(Name = "LabourType_Display_Name", ResourceType = typeof(MessageResource))]
        public string LabourType { get; set; }
    }
}
