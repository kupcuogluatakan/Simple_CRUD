using ODMSCommon.Resources;
using System.ComponentModel.DataAnnotations;
namespace ODMSModel.ObjectSearch
{
    public class CustomerSearchViewModel : ObjectSearchModel
    {
        [Display(Name = "Customer_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerFullName { get; set; }
        [Display(Name = "Customer_Display_TCIdentityNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TCIdentityNo { get; set; }
        [Display(Name = "Customer_Display_TaxNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TaxNo { get; set; }
        [Display(Name = "Customer_Display_PassportNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PassportNo { get; set; }
        public int? OrgTypeId { get; set; }

        public int? DealerId { get; set; }
        [Display(Name = "Customer_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "Customer_Display_MobileNo", ResourceType = typeof(MessageResource))]
        public string MobileNo { get; set; }

        public int? CountryId { get; set; }
        [Display(Name = "Customer_Display_CountryName", ResourceType = typeof(MessageResource))]
        public string CountryName { get; set; }

        public int? CompanyTypeId { get; set; }
        [Display(Name = "Customer_Display_CompanyTypeName", ResourceType = typeof(MessageResource))]
        public string CompanyTypeName { get; set; }

        public int? CustomerTypeId { get; set; }
        [Display(Name = "Customer_Display_CustomerTypeName", ResourceType = typeof(MessageResource))]
        public string CustomerTypeName { get; set; }

        public int? WitholdingStatus { get; set; }
        [Display(Name = "Customer_Display_WitholdingStatusName", ResourceType = typeof(MessageResource))]
        public string WitholdingStatusName { get; set; }
    }
}
