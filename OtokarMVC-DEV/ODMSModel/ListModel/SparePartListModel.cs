using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;

namespace ODMSModel.ListModel
{
    public class SparePartListModel : BaseListWithPagingModel
    {
        public SparePartListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PartId", "PART_ID"},
                    {"DealerId", "DEALER_ID"},
                    {"PartCode","PART_CODE"},
                    {"PartTypeCode","PART_TYPE_CODE"},
                    {"PartName","PART_NAME"},
                    {"CompatibleGuaranteeUsage","COMPATIBLE_GUARANTEE_USAGE"},
                    {"VatRatio","VAT_RATIO"},
                    {"Unit","UNIT"},
                    {"Brand","BRAND"},
                    {"Weight","WEIGHT"},
                    {"Volume","VOLUME"},
                    {"ShipQuantity","SHIP_QUANTITY"},
                    {"NSN","NSN"},
                    {"AdminDesc","ADMIN_DESC"},
                    {"PartSection","PART_SECTION"},
                    {"IsActiveName","IS_ACTIVE"},
                    {"IsOriginalName","ORIGINAL_PART_ID"},
                    {"GuaranteeAuthorityNeedName","GUARANTEE_AUTHORITY_NEED"}
                };
            SetMapper(dMapper);
        }

        public SparePartListModel()
        {
        }
        public int? PartId { get; set; }

        public int? IsOriginal { get; set; }
        [Display(Name = "SparePart_Display_IsOriginal", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsOriginalName { get; set; }

        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        public int? DealerId { get; set; }
        [Display(Name = "SparePart_Display_DealerId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        public int? OriginalPartId { get; set; }
        [Display(Name = "SparePart_Display_OriginalPartId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string OriginalPartName { get; set; }

        [Display(Name = "SparePart_Display_PartTypeCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartTypeCode { get; set; }

        [Display(Name = "SparePart_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "SparePart_Display_PartSection", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartSection { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        [Display(Name = "SparePart_Display_GuaranteeAuthorityNeed", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool? GuaranteeAuthorityNeed { get; set; }

         [Display(Name = "SparePart_Display_GuaranteeAuthorityNeed", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GuaranteeAuthorityNeedName { get; set; }

        public string Unit { get; set; }

        //Brand
        [Display(Name = "SparePart_Display_Brand", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Brand { get; set; }

        //ShipQuantity
        [Display(Name = "SparePart_Display_ShipQuantity", ResourceType = typeof(MessageResource))]
        public decimal? ShipQuantity { get; set; }
    }
}
