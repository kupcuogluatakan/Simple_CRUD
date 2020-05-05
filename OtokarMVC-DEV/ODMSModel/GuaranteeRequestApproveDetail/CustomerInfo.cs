using System.ComponentModel.DataAnnotations;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class CustomerInfo
    {
        [Display(Name = "Customer_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Name { get; set; }

        [Display(Name = "CustomerAddress_Display_Address1", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Adress { get; set; }

        [Display(Name = "CustomerAddress_Display_CityName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string City { get; set; }

        [Display(Name = "User_Display_Phone", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Phone { get; set; }

        [Display(Name = "Dealer_Display_Fax", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Fax { get; set; }
    }
}
