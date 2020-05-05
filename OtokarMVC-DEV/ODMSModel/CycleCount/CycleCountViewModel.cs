using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.CycleCount
{
    [Validator(typeof(CycleCountViewModelValidator))]
    public class CycleCountViewModel : ModelBase
    {
        public CycleCountViewModel()
        {
        }
        public bool HideFormElements { get; set; }

        //CycleCountId
        [Display(Name = "CycleCount_Display_CycleCountId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CycleCountId { get; set; }

        //Dealer
        public int? DealerId { get; set; }
        [Display(Name = "CycleCount_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        //CycleCountName
        [Display(Name = "CycleCount_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CycleCountName { get; set; }

        //DisplayCurrentAmount
        public bool DisplayCurrentAmount { get; set; }
        [Display(Name = "CycleCount_Display_DisplayCurrentAmount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DisplayCurrentAmountName { get; set; }

        //CycleCountType
        public int? CycleCountStatus { get; set; }
        [Display(Name = "CycleCount_Display_CycleCountStatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CycleCountStatusName { get; set; }

        //StartDate
        [Display(Name = "CycleCount_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        //EndDate
        [Display(Name = "CycleCount_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        //ConfirmDate
        [Display(Name = "CycleCount_Display_ConfirmDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? ConfirmDate { get; set; }

        public string encryptedId { get; set; }

        public bool IsClosedApproveTab { get; set; }

        public int? StockTypeId { get; set; }

        [Display(Name = "CycleCountPlan_Display_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeName { get; set; }

        [Display(Name = "CycleCountPlan_Display_CreateUser", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CreateUser { get; set; }
    }

    public enum LockType
    {
        Start = 0,
        Approved = 1
    }
}
