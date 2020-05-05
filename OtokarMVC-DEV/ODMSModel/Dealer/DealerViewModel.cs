using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSModel.Dealer
{
    /// <summary>
    /// ViewModel for dealer
    /// </summary>
    [Validator(typeof(DealerViewModelValidator))]
    public class DealerViewModel : ModelBase
    {
        /// <summary>
        /// Id for the dealer
        /// </summary>
        public int DealerId { get; set; }

        /// <summary>
        /// SSSID for the dealer
        /// </summary>
        [Display(Name = "Dealer_Display_SSID", ResourceType = typeof(MessageResource))]
        public string SSID { get; set; }

        /// <summary>
        /// Brach SSID for the dealer: from SAP
        /// </summary>
        [Display(Name = "Dealer_Display_BranchSSID", ResourceType = typeof(MessageResource))]
        public string BranchSSID { get; set; }

        /// <summary>
        /// Short name for the dealer
        /// </summary>
        [Display(Name = "Dealer_Display_ShortName", ResourceType = typeof(MessageResource))]
        public string ShortName { get; set; }

        /// <summary>
        /// Name for the dealer
        /// </summary>
        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }

        /// <summary>
        /// Region Id for the dealer
        /// </summary>
        [Display(Name = "Dealer_Display_DealerRegion", ResourceType = typeof(MessageResource))]
        public int? DealerRegionId { get; set; }

        /// <summary>
        /// Country for the dealer
        /// </summary>
        [Display(Name = "Global_Display_Country", ResourceType = typeof(MessageResource))]
        public int? Country { get; set; }

        /// <summary>
        /// City for the dealer
        /// </summary>
        [Display(Name = "Global_Display_City", ResourceType = typeof(MessageResource))]
        public int? City { get; set; }

        /// <summary>
        /// City for the dealer
        /// </summary>
        [Display(Name = "Global_Display_City", ResourceType = typeof(MessageResource))]
        public string ForeignCity { get; set; }
        [Display(Name = "Dealer_Display_AcceptOrderProposal", ResourceType = typeof(MessageResource))]
        public bool AcceptOrderProposal { get; set; }
        public bool? AcceptOrderProposalSearch { get; set; }
        /// <summary>
        /// Town for the dealer
        /// </summary>
        [Display(Name = "CustomerAddress_Display_TownName", ResourceType = typeof(MessageResource))]
        public string Town { get; set; }

        /// <summary>
        /// Town for the dealer
        /// </summary>
        [Display(Name = "CustomerAddress_Display_TownName", ResourceType = typeof(MessageResource))]
        public string ForeignTown { get; set; }


        /// <summary>
        /// Address1 for the dealer
        /// </summary>
        [Display(Name = "Dealer_Display_Address1", ResourceType = typeof(MessageResource))]
        public string Address1 { get; set; }

        /// <summary>
        /// Address2 for the dealer
        /// </summary>
        [Display(Name = "Dealer_Display_Address2", ResourceType = typeof(MessageResource))]
        public string Address2 { get; set; }

        /// <summary>
        /// Tax office for the dealer
        /// </summary>
        [Display(Name = "Dealer_Display_TaxOffice", ResourceType = typeof(MessageResource))]
        public string TaxOffice { get; set; }

        /// <summary>
        /// Tax no for the dealer
        /// </summary>
        [Display(Name = "Dealer_Display_TaxNo", ResourceType = typeof(MessageResource))]
        public string TaxNo { get; set; }

        /// <summary>
        /// Phone for the dealer
        /// </summary>
        [Display(Name = "Dealer_Display_Phone", ResourceType = typeof(MessageResource))]
        public string Phone { get; set; }

        [Display(Name = "Dealer_Display_MobilePhone", ResourceType = typeof(MessageResource))]
        public string MobilePhone { get; set; }


        /// <summary>
        /// Fax for the dealer
        /// </summary>
        [Display(Name = "Dealer_Display_Fax", ResourceType = typeof(MessageResource))]
        public string Fax { get; set; }

        /// <summary>
        /// Contact Name and Surname for the dealer
        /// </summary>
        [Display(Name = "Dealer_Display_ContactFullName", ResourceType = typeof(MessageResource))]
        public string ContactNameSurname { get; set; }

        /// <summary>
        /// DealerClassName for the dealer
        /// </summary>
        public string DealerClassName { get; set; }

        /// <summary>
        /// DealerClassLookCode for the dealer
        /// </summary>
        [Display(Name = "Dealer_Display_DealerClass", ResourceType = typeof(MessageResource))]
        public string DealerClassCode { get; set; }

        /// <summary>
        /// Ts12047Certificate check for the dealer
        /// </summary>
        [Display(Name = "Dealer_Display_HasTs12047Certificate", ResourceType = typeof(MessageResource))]
        public bool HasTs12047Certificate { get; set; }

        /// <summary>
        /// Ts12047Certificate validity date
        /// </summary>
        [DataType(DataType.Date, ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Dealer_Display_Ts12047CertificateDate", ResourceType = typeof(MessageResource))]
        public DateTime? Ts12047CertificateDate { get; set; }

        /// <summary>
        /// contact email
        /// </summary>
        [Display(Name = "Dealer_Display_ContactEmail", ResourceType = typeof(MessageResource))]
        public string ContactEmail { get; set; }

        /// <summary>
        /// Service Responsibility Insurance check
        /// </summary>
        [Display(Name = "Dealer_Display_HasServiceResponsibilityInsurance", ResourceType = typeof(MessageResource))]
        public bool HasServiceResponsibilityInsurance { get; set; }

        public int CustomerGroupLookKey { get; set; }

        [Display(Name = "Dealer_Display_CustomerGroup", ResourceType = typeof(MessageResource))]
        public string CustomerGroupLookVal { get; set; }

        [Display(Name = "Dealer_Display_SaleChannel", ResourceType = typeof(MessageResource))]
        public string SaleChannelCode { get; set; }

        [Display(Name = "Dealer_Display_SaleChannel", ResourceType = typeof(MessageResource))]
        public string SaleChannelName { get; set; }

        [Display(Name = "Dealer_Display_WorkShopPlanType", ResourceType = typeof(MessageResource))]
        public WorkshopPlan WorkshopPlanType { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }

        [Display(Name = "Dealer_Display_ClaimRatio", ResourceType = typeof(MessageResource))]
        public decimal ClaimRatio { get; set; }

        [Display(Name = "Dealer_Display_Auto_Mrp", ResourceType = typeof(MessageResource))]
        public bool AutoMrp { get; set; }

        [Display(Name = "Dealer_Display_Last_Mrp_Date", ResourceType = typeof(MessageResource))]
        public string LastMrpDate { get; set; }

        public enum WorkshopPlan
        {
            [StringValue("Dealer_WorkshopPlanType_Basic")]
            Basic,
            [StringValue("Dealer_WorkshopPlanType_Advanced")]
            Advanced
        }
        #region Constants

        public static readonly string DealerClassLookUpCode = "DEALER_CLASS";
        public static readonly string CustomerGroupLookUpCode = "CUST_GROUP";

        #endregion
        #region Extra Show Values

        public string RegionName { get; set; }

        public string CountryName { get; set; }

        public string CityName { get; set; }

        public string TownName { get; set; }

        public string DealerClass { get; set; }

        public string CustomerGroup { get; set; }

        public string RegionResponsibleEmail { get; set; }
        public int RegionResponsibleUserId { get; set; }

        #endregion
        [Display(Name = "Currency_Display_CurrencyCode", ResourceType = typeof(MessageResource))]
        public string CurrencyCode { get; set; }
        [Display(Name = "Currency_Display_CurrencyName", ResourceType = typeof(MessageResource))]
        public string CurrencyName { get; set; }

        [Display(Name = "PurchaseOrderGroup_Display_GroupName", ResourceType = typeof(MessageResource))]
        public string PurchaseOrderGroupId { get; set; }

        [Display(Name = "PurchaseOrderGroup_Display_GroupName", ResourceType = typeof(MessageResource))]
        public string PurchaseOrderGroupName { get; set; }
        [Display(Name = "Dealer_Display_IsElectronicInvoiceEnabled", ResourceType = typeof(MessageResource))]
        public bool IsElectronicInvoiceEnabled { get; set; }

        [Display(Name = "Dealer_Display_Latitude", ResourceType = typeof(MessageResource))]
        public string Latitude { get; set; }

        [Display(Name = "Dealer_Display_Longitute", ResourceType = typeof(MessageResource))]
        public string Longitude { get; set; }

        [Display(Name = "Dealer_Display_IsSaleDealer", ResourceType = typeof(MessageResource))]
        public bool IsSaleDealer { get; set; }

        public int IdPoGroup { get; set; }

    }
}

