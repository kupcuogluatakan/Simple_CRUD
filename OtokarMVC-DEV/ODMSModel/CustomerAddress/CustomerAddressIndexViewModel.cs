using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.CustomerAddress
{
    [Validator(typeof(CustomerAddressIndexViewModelValidator))]
    public class CustomerAddressIndexViewModel : ModelBase
    {
        public CustomerAddressIndexViewModel()
        {
        }
        public bool HideFormElements { get; set; }

        //AddressId
        public int AddressId { get; set; }

        //Customer
        public int CustomerId { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }
        
        //AddressType
        public int? AddressTypeId { get; set; }
        [Display(Name = "CustomerAddress_Display_AddressTypeName", ResourceType = typeof(MessageResource))]
        public string AddressTypeName { get; set; }

        //Country
        public int? CountryId { get; set; }
        [Display(Name = "CustomerAddress_Display_CountryName", ResourceType = typeof(MessageResource))]
        public string CountryName { get; set; }
        
        //City
        public int? CityId { get; set; }
        [Display(Name = "CustomerAddress_Display_CityName", ResourceType = typeof(MessageResource))]
        public string CityName { get; set; }
        
        //Town
        public int? TownId { get; set; }
        [Display(Name = "CustomerAddress_Display_TownName", ResourceType = typeof(MessageResource))]
        public string TownName { get; set; }

        //ZipCode
        [Display(Name = "CustomerAddress_Display_ZipCode", ResourceType = typeof(MessageResource))]
        public string ZipCode { get; set; }

        //Address1
        [Display(Name = "CustomerAddress_Display_Address1", ResourceType = typeof(MessageResource))]
        public string Address1 { get; set; }

        //Address2
        [Display(Name = "CustomerAddress_Display_Address2", ResourceType = typeof(MessageResource))]
        public string Address2 { get; set; }

        //Address3
        [Display(Name = "CustomerAddress_Display_Address3", ResourceType = typeof(MessageResource))]
        public string Address3 { get; set; }

        //IsActive
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
    }
}
