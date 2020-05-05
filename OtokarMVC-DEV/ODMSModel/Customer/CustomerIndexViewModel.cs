using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.Customer
{
    [Validator(typeof(CustomerIndexViewModelValidator))]
    public class CustomerIndexViewModel : ModelBase
    {
        public CustomerIndexViewModel()
        {
        }
        public bool HideFormElements { get; set; }

        //CustomerId
        public int CustomerId { get; set; }
        [Display(Name = "Customer_Display_TCIdentityNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TcIdentityNo { get; set; }
        //CustomerSSID
        [Display(Name = "Customer_Display_SSID", ResourceType = typeof(MessageResource))]
        public string SAPCustomerSSID { get; set; }
        [Display(Name = "Customer_Display_SSID", ResourceType = typeof(MessageResource))]
        public string BOSCustomerSSID { get; set; }

        //CustomerName
        [Display(Name = "Customer_Display_Name", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }

        //CustomerLastName
        [Display(Name = "Customer_Display_LastName", ResourceType = typeof(MessageResource))]
        public string CustomerLastName { get; set; }

        //CustomerType
        public int? CustomerTypeId { get; set; }
        [Display(Name = "Customer_Display_CustomerTypeName", ResourceType = typeof(MessageResource))]
        public string CustomerTypeName { get; set; }

        //Dealer
        [Display(Name = "Customer_Display_DealerCustomer", ResourceType = typeof(MessageResource))]
        public bool IsDealerCustomer { get; set; }
        public int? DealerId { get; set; }
        [Display(Name = "Customer_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        //GovernmentType
        public int? GovernmentTypeId { get; set; }
        [Display(Name = "Customer_Display_GovernmentTypeName", ResourceType = typeof(MessageResource))]
        public string GovernmentTypeName { get; set; }

        //CompanyType
        public int? CompanyTypeId { get; set; }
        [Display(Name = "Customer_Display_CompanyTypeName", ResourceType = typeof(MessageResource))]
        public string CompanyTypeName { get; set; }

        //Country
        public int? CountryId { get; set; }
        [Display(Name = "Customer_Display_CountryName", ResourceType = typeof(MessageResource))]
        public string CountryName { get; set; }

        //TaxOffice
        [Display(Name = "Customer_Display_TaxOffice", ResourceType = typeof(MessageResource))]
        public string TaxOffice { get; set; }

        //TaxNo
        [Display(Name = "Customer_Display_TaxNo", ResourceType = typeof(MessageResource))]
        public string TaxNo { get; set; }


        //PassportNo
        [Display(Name = "Customer_Display_PassportNo", ResourceType = typeof(MessageResource))]
        public string PassportNo { get; set; }

        //PassportNo
        [Display(Name = "Customer_Display_MobileNo", ResourceType = typeof(MessageResource))]
        public string MobileNo { get; set; }

        //WitholdingStatus
        public int? WitholdingStatus { get; set; }
        [Display(Name = "Customer_Display_WitholdingStatusName", ResourceType = typeof(MessageResource))]
        public string WitholdingStatusName { get; set; }

        //WitholdingId
        [Display(Name = "Customer_Display_WitholdingRate", ResourceType = typeof(MessageResource))]
        public string WitholdingId { get; set; }
        public string WitholdingName { get; set; }

        //IsActive
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
        [Display(Name = "Dealer_Display_IsElectronicInvoiceEnabled", ResourceType = typeof(MessageResource))]
        public bool IsElectronicInvoiceEnabled { get; set; }
    }
}

