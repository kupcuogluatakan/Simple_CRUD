using System.ComponentModel.DataAnnotations;

namespace ODMSModel.TechnicianOperation
{
    public class TechnicianOperationViewModel: ModelBase
    {
        [Display(Name = "TechnicianOperation_Display_Username", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int DmsUserId { get; set; }

        //Password
        [Display(Name = "User_Display_Password", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Password { get; set; }

        public bool IsLogin { get; set; }
        
   }
}
