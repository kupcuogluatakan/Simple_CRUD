using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;
using ODMSModel.ViewModel;

namespace ODMSModel.SparePart
{
    [Validator(typeof(SparePartIndexViewModelValidator))]
    public class SparePartIndexViewModel : ModelBase
    {
        public SparePartIndexViewModel()
        {
        }
        public bool HideFormElements { get; set; }
        public string MultiLanguageContentAsText { get; set; }

        public int PartId { get; set; }

        public int? IsOriginal { get; set; }
        [Display(Name = "SparePart_Display_IsOriginal", ResourceType = typeof(MessageResource))]
        public string IsOriginalName { get; set; }

        //Dealer
        public int? DealerId { get; set; }
        [Display(Name = "SparePart_Display_DealerId", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        //OriginalPart
        public int OriginalPartId { get; set; }
        [Display(Name = "SparePart_Display_OriginalPartId", ResourceType = typeof(MessageResource))]
        public string OriginalPartName { get; set; }

        //PartTypeCode
        public string PartTypeCode { get; set; }
        [Display(Name = "SparePart_Display_PartTypeCode", ResourceType = typeof(MessageResource))]
        public string PartTypeName { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartNameInLanguage { get; set; }

        //CompatibleGuaranteeUsage
        public int? CompatibleGuaranteeUsage { get; set; }
        [Display(Name = "SparePart_Display_CompatibleGuaranteeUsage", ResourceType = typeof(MessageResource))]
        public string CompatibleGuaranteeUsageName { get; set; }

        //VatRatio
        [Display(Name = "SparePart_Display_VatRatio", ResourceType = typeof(MessageResource))]
        public string VatRatio { get; set; }

        //Unit
        public int? Unit { get; set; }
        [Display(Name = "SparePart_Display_Unit", ResourceType = typeof(MessageResource))]
        public string UnitName { get; set; }

        //Brand
        [Display(Name = "SparePart_Display_Brand", ResourceType = typeof(MessageResource))]
        public string Brand { get; set; }

        //Weight
        [Display(Name = "SparePart_Display_Weight", ResourceType = typeof(MessageResource))]
        public decimal Weight { get; set; }

        //Volume
        [Display(Name = "SparePart_Display_Volume", ResourceType = typeof(MessageResource))]
        public decimal Volume { get; set; }

        [Display(Name = "SparePart_Display_GuaranteeAuthorityNeed", ResourceType = typeof(MessageResource))]
        public bool? GuaranteeAuthorityNeed { get; set; }

        //ShipQuantity
        [Display(Name = "SparePart_Display_ShipQuantity", ResourceType = typeof(MessageResource))]
        public decimal? ShipQuantity { get; set; }

        //NSN
        [Display(Name = "SparePart_Display_NSN", ResourceType = typeof(MessageResource))]
        public string NSN { get; set; }

        //PartSection
        [Display(Name = "SparePart_Display_PartSection", ResourceType = typeof(MessageResource))]
        public string PartSection { get; set; }

        //PartCode
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }

        //AdminDesc
        [Display(Name = "SparePart_Display_AdminDesc", ResourceType = typeof(MessageResource))]
        public string AdminDesc { get; set; }
        /// <summary>
        /// SPARE_PART_CLASS_CODE => this comes from SAP we just show it on details 
        /// </summary>
        [Display(Name = "SparePart_Display_ClassCode", ResourceType = typeof(MessageResource))]
        public string ClassCode { get; set; }

        /// <summary>
        /// Supply channel discount ratio for the spare part
        /// </summary>
        [Display(Name = "SparePart_Display_SupplyDiscountRatio", ResourceType = typeof(MessageResource))]
        public decimal DiscountRatio { get; set; }

        //IsActive
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public new bool IsActive { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        //PartName
        private MultiLanguageModel _partName;
        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public MultiLanguageModel PartName { get { return _partName ?? new MultiLanguageModel(); } set { _partName = value; } }

        public string GuaranteeAuthorityNeedName { get; set; }

        [Display(Name = "SparePart_Display_LeadTime", ResourceType = typeof(MessageResource))]
        public int? LeadTime { get; set; }

        [Display(Name = "SparePart_Display_IsOrderAllowed", ResourceType = typeof(MessageResource))]
        public bool IsOrderAllowed { get; set; }

        public string Barcode { get; set; }
        [Display(Name = "StockCard_Display_AlternatePart", ResourceType = typeof(MessageResource))]
        public string AlternatePart { get; set; }
    }
}
