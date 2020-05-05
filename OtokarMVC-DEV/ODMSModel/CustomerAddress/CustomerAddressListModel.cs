using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.CustomerAddress
{
    public class CustomerAddressListModel : BaseListWithPagingModel
    {
        public CustomerAddressListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CustomerId", "CUSTOMER_ID"},
                    {"AddressTypeName", "ADDRESS_TYPE_LOOKVAL"},
                    {"CountryName", "COUNTRY_NAME"},
                    {"CityName", "CITY_NAME"},
                    {"TownName", "TOWN_NAME"},
                    {"ZipCode", "ZIP_CODE"},
                    {"IsActiveName", "IS_ACTIVE"}
                };
            SetMapper(dMapper);
        }

        public CustomerAddressListModel()
        {
        }

        public int AddressId { get; set; }

        //CustomerId
        public int CustomerId { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }

        //AddressType
        public int AddressTypeId { get; set; }
        [Display(Name = "CustomerAddress_Display_AddressTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AddressTypeName { get; set; }

        //Country
        [Display(Name = "CustomerAddress_Display_CountryName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CountryName { get; set; }

        //City
        [Display(Name = "CustomerAddress_Display_CityName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CityName { get; set; }

        //Town
        [Display(Name = "CustomerAddress_Display_TownName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TownName { get; set; }

        //ZipCode
        [Display(Name = "CustomerAddress_Display_ZipCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ZipCode { get; set; }
        
        //IsActive
        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }
    }
}
