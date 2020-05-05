using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.Dealer
{
    public class DealerListModel : BaseListWithPagingModel
    {
        public DealerListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"DealerId", "ID_DEALER"},
                     {"SSID", "DEALER_SSID"},
                     {"ShortName", "DEALER_SHRT_NAME"},
                     {"Name", "DEALER_NAME"},
                     {"Country", "CL.COUNTRY_NAME"},
                     {"City", "CI.CITY_NAME"},
                     {"RegionName", "DEALER_REGION_NAME"},
                     {"ContactFullName","CNTCT_NAME_SURNAME"},
                     {"IsActiveString","IS_ACTIVE"},
                     {"BranchSSID","DEALER_BRANCH_SSID"},
                     {"IsSaleDealerString","IS_SALE_DEALER_STRING"},
                     {"PurchaseOrderGroupName","PURCHASE_ORDER_GROUP_NAME"}
                 };
            SetMapper(dMapper);

        }
        [Display(Name = "Dealer_Display_BranchSSID", ResourceType = typeof(MessageResource))]
        public string BranchSSID { get; set; }
        public DealerListModel() { }
        public int DealerId { get; set; }
        [Display(Name = "Dealer_Display_SSID", ResourceType = typeof(MessageResource))]
        public string SSID { get; set; }
        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }
        [Display(Name = "Dealer_Display_ShortName", ResourceType = typeof(MessageResource))]
        public string ShortName { get; set; }
        [Display(Name = "Global_Display_Country", ResourceType = typeof(MessageResource))]
        public string Country { get; set; }
        [Display(Name = "Global_Display_City", ResourceType = typeof(MessageResource))]
        public string City { get; set; }
        [Display(Name = "Dealer_Display_ContactFullName", ResourceType = typeof(MessageResource))]
        public string ContactFullName { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool IsActive { get; set; }
        [Display(Name = "Dealer_Display_DealerRegion", ResourceType = typeof(MessageResource))]
        public string RegionName { get; set; }

        //below props are for search criteria
        [Display(Name = "Dealer_Display_DealerRegion", ResourceType = typeof(MessageResource))]
        public int DealerRegionId { get; set; }

        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int PoGroupId { get; set; }


        [Display(Name = "Dealer_Display_AcceptOrderProposal", ResourceType = typeof(MessageResource))]
        public bool? AcceptOrderProposal { get; set; }

        [Display(Name = "Dealer_Display_IsElectronicInvoiceEnabled", ResourceType = typeof(MessageResource))]
        public bool? IsElectronicInvoiceEnabled { get; set; }
        public string Address { get; set; }

        public int TownId { get; set; }
        public string Town { get; set; }
        public string GroupName { get; set; }
        public int DealerLastUpdate { get; set; }
        public int CountryLastUpdate { get; set; }
        public int CityLastUpdate { get; set; }
        public int TownLastUpdate { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitute { get; set; }

        [Display(Name = "Dealer_Display_IsSaleDealer", ResourceType = typeof(MessageResource))]
        public bool IsSaleDealer { get; set; }
        [Display(Name = "Dealer_Display_IsSaleDealer", ResourceType = typeof(MessageResource))]
        public string IsSaleDealerString { get; set; }
        [Display(Name = "PurchaseOrderGroup_Display_GroupName", ResourceType = typeof(MessageResource))]
        public string PurchaseOrderGroupId { get; set; }

        [Display(Name = "PurchaseOrderGroup_Display_GroupName", ResourceType = typeof(MessageResource))]
        public string PurchaseOrderGroupName { get; set; }
    }
}

