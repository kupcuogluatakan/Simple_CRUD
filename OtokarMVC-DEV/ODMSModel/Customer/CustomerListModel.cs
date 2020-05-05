using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.Customer
{
    public class CustomerListModel : BaseListWithPagingModel
    {
        public CustomerListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CustomerId", "ID_DEALER_REGION"},
                    {"SAPCustomerSSID", "SAP_CUSTOMER_SSID"},
                    {"BOSCustomerSSID", "BOS_CUSTOMER_SSID"},
                    {"CustomerName", "CUSTOMER_NAME"},
                    {"CustomerLastName", "CUSTOMER_LAST_NAME"},
                    {"CustomerTypeName", "CUSTOMER_TYPE_NAME"},
                    {"GovernmentTypeName", "GOVERNMENT_TYPE_NAME"},
                    {"CompanyTypeName", "COMPANY_TYPE_NAME"},
                    {"CountryName", "COUNTRY_NAME"},
                    {"TaxOffice", "TAX_OFFICE"},
                    {"TaxNo", "TAX_NO"},
                    {"TcIdentityNo", "TC_IDENTITY_NO"},
                    {"PassportNo", "PASSPORT_NO"},
                    {"WitholdingStatusName", "WITHOLDING_STATUS"},
                    {"IsActiveName", "IS_ACTIVE"},
                    {"DealerName", "C.ID_DEALER"}
                };
            SetMapper(dMapper);
        }

        public CustomerListModel()
        {
        }

        public int CustomerId { get; set; }

        //CustomerSSID
        [Display(Name = "Customer_Display_SSID", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SAPCustomerSSID { get; set; }
        [Display(Name = "Customer_Display_SSID", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BOSCustomerSSID { get; set; }

        //CustomerName
        [Display(Name = "Customer_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }

        //CustomerLastName
        [Display(Name = "Customer_Display_LastName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerLastName { get; set; }

        //CustomerType
        public int? CustomerTypeId;
        [Display(Name = "Customer_Display_CustomerTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerTypeName { get; set; }

        //Dealer
        public int? DealerId { get; set; }

        //GovernmentType
        public int? GovernmentTypeId;
        [Display(Name = "Customer_Display_GovernmentTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GovernmentTypeName { get; set; }

        //CompanyType
        public int? CompanyTypeId;
        [Display(Name = "Customer_Display_CompanyTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CompanyTypeName { get; set; }

        //Country
        public int? CountryId;
        [Display(Name = "Customer_Display_CountryName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CountryName { get; set; }

        //TaxOffice
        [Display(Name = "Customer_Display_TaxOffice", ResourceType = typeof(MessageResource))]
        public string TaxOffice { get; set; }

        //TaxNo
        [Display(Name = "Customer_Display_TaxNo", ResourceType = typeof(MessageResource))]
        public string TaxNo { get; set; }

        //TCIdentityNo
        [Display(Name = "Customer_Display_TCIdentityNo", ResourceType = typeof(MessageResource))]
        public string TcIdentityNo { get; set; }

        //PassportNo
        [Display(Name = "Customer_Display_PassportNo", ResourceType = typeof(MessageResource))]
        public string PassportNo { get; set; }

        //IsActive
        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        //PassportNo
        [Display(Name = "Customer_Display_MobileNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MobileNo { get; set; }

        //WitholdingStatus
        public int? WitholdingStatus { get; set; }
        [Display(Name = "Customer_Display_WitholdingStatusName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WitholdingStatusName { get; set; }
    }
}

