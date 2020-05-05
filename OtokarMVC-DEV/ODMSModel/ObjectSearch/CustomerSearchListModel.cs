using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;


namespace ODMSModel.ObjectSearch
{
    public class CustomerSearchListModel : ODMSModel.ListModel.BaseListWithPagingModel
    {
        public CustomerSearchListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CustomerFullName", "CUSTOMER_FULL_NAME"},
                    {"TcIdentityNo", "TC_IDENTITY_NO"},
                    {"TaxNo", "TAX_NO"},
                    {"PassportNo", "PASSPORT_NO"},
                    {"TaxOffice", "TAX_OFFICE"},
                    {"CustomerTypeName", "CUSTOMER_TYPE_NAME"},
                    {"GovernmentTypeName", "GOVERNMENT_TYPE_NAME"},
                    {"CompanyTypeName", "COMPANY_TYPE_NAME"},
                    {"SAPCustomerSSID", "SAP_CUSTOMER_SSID"},
                    {"BOSCustomerSSID", "BOS_CUSTOMER_SSID"},
                    {"CountryName", "COUNTRY_NAME"}
                };
            SetMapper(dMapper);
        }

        public CustomerSearchListModel()
        {
        }
        //WitholdingStatus
        public int? WitholdingStatus { get; set; }
        [Display(Name = "Customer_Display_WitholdingStatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WitholdingStatusName { get; set; }
        //CustomerType
        public int? CustomerTypeId;
        [Display(Name = "Customer_Display_CustomerTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerTypeName { get; set; }
        //Country
        public int? CountryId;
        [Display(Name = "Customer_Display_CountryName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CountryName { get; set; }
        //PassportNo
        [Display(Name = "Customer_Display_MobileNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MobileNo { get; set; }
        public int CustomerId { get; set; }
        [Display(Name = "Customer_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerFullName { get; set; }
        [Display(Name = "Customer_Display_TCIdentityNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TCIdentityNo { get; set; }
        [Display(Name = "Customer_Display_TaxNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TaxNo { get; set; }
        [Display(Name = "Customer_Display_PassportNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PassportNo { get; set; }
        [Display(Name = "Customer_Display_TaxOffice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TaxOffice { get; set; }
        public int? OrgTypeId { get; set; }
        [Display(Name = "Customer_Display_GovernmentTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GovernmentTypeName { get; set; }
        [Display(Name = "Customer_Display_CompanyTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CompanyTypeName { get; set; }
        [Display(Name = "Customer_Display_SSID", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SAPCustomerSSID { get; set; }
        [Display(Name = "Customer_Display_SSID", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BOSCustomerSSID { get; set; }
    }
}
