using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon;

namespace ODMSModel
{
    [Serializable]
    public class ModelBase
    {
        public ModelBase()
        {

        }
        public int ErrorNo { get; set; }
        public string ErrorMessage { get; set; }
        public int UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateTrx { get; set; }
        public CommonValues.Status StatusId { get; set; }
        public string Status { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool? SearchIsActive { get; set; }
        public string CommandType { get; set; }
        public string OperationTransactionId { get; set; }
        public string ConditionalCssClassForDealerCombo { get { return ODMSCommon.Security.UserManager.UserInfo.IsDealer ? "k-input-disabled" : string.Empty; } }
    }
}
