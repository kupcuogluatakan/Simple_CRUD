using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.Supplier
{
    public class SupplierListModel:BaseListWithPagingModel
    {

        public SupplierListModel()
        {}
        public SupplierListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
             var dMapper = new Dictionary<string, string>
                 {
                     {"SupplierId", "ID_SUPPLIER"},
                     {"Ssid", "SUPPLIER_SSID"},
                     {"SupplierName", "SUPPLIER_NAME"},
                     {"TaxOffice", "TAX_OFFICE"},
                     {"TaxNo", "TAX_NO"},
                     {"ContactPerson", "CONTACT_PERSON"},
                     {"Email", "E_MAIL"},
                     {"Phone", "PHONE"},
                     {"MobilePhone", "MOBILE_PHONE"},
                     {"Fax", "FAX"},
                     {"ChamberOfCommerce", "CHAMBER_OF_COMMERCE"},
                     {"TradeRegistryNo", "TRADE_REGISTRY_NO"},
                     {"CountryName", "ID_COUNTRY"},
                     {"CityName", "ID_CITY"},
                     {"TownName", "ID_TOWN"},
                     {"ZipCode", "ZIP_CODE"},
                     {"IsActiveString","IS_ACTIVE"}
                 };
            SetMapper(dMapper);

        }

        public int SupplierId { get; set; }

        public int DealerId { get; set; }

        [Display(Name = "Dealer_Display_SSID", ResourceType = typeof(MessageResource))]
        public string Ssid { get; set; }
        [Display(Name = "Supplier_Display_SupplierName", ResourceType = typeof(MessageResource))]
        public string SupplierName { get; set; }
        [Display(Name = "Dealer_Display_TaxOffice", ResourceType = typeof(MessageResource))]
        public string TaxOffice { get; set; }
        [Display(Name = "Dealer_Display_TaxNo", ResourceType = typeof(MessageResource))]
        public string TaxNo { get; set; }
        [Display(Name = "Global_Display_WebSite", ResourceType = typeof(MessageResource))]
        public string Url { get; set; }
        [Display(Name = "Dealer_Display_ContactFullName", ResourceType = typeof(MessageResource))]
        public string ContactPerson { get; set; }
        [Display(Name = "Dealer_Display_ContactEmail", ResourceType = typeof(MessageResource))]
        public string Email { get; set; }
        [Display(Name = "Dealer_Display_Phone", ResourceType = typeof(MessageResource))]
        public string Phone { get; set; }
        [Display(Name = "Dealer_Display_MobilePhone", ResourceType = typeof(MessageResource))]
        public string MobilePhone { get; set; }
        [Display(Name = "Dealer_Display_Fax", ResourceType = typeof(MessageResource))]
        public string Fax { get; set; }
        [Display(Name = "Supplier_Display_ChamberOfCommerce", ResourceType = typeof(MessageResource))]
        public string ChamberOfCommerce { get; set; }
        [Display(Name = "Supplier_Display_TradeRegistryNo", ResourceType = typeof(MessageResource))]
        public string TradeRegistryNo { get; set; }
        [Display(Name = "Global_Display_Country", ResourceType = typeof(MessageResource))]
        public int CountryId { get; set; }
        [Display(Name = "Global_Display_Country", ResourceType = typeof(MessageResource))]
        public string CountryName { get; set; }
        [Display(Name = "Global_Display_City", ResourceType = typeof(MessageResource))]
        public int CityId { get; set; }
        [Display(Name = "Global_Display_City", ResourceType = typeof(MessageResource))]
        public string CityName { get; set; }
        [Display(Name = "CustomerAddress_Display_TownName", ResourceType = typeof(MessageResource))]
        public int TownId { get; set; }
        [Display(Name = "CustomerAddress_Display_TownName", ResourceType = typeof(MessageResource))]
        public string TownName { get; set; }
        [Display(Name = "CustomerAddress_Display_ZipCode", ResourceType = typeof(MessageResource))]
        public string ZipCode { get; set; }
        [Display(Name = "Appointment_Display_ContactAddress", ResourceType = typeof(MessageResource))]
        public string Address { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public new string IsActiveString { get; set; }
    }
}
