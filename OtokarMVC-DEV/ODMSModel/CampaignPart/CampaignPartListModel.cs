using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.CampaignPart
{
    public class CampaignPartListModel : BaseListWithPagingModel
    {
        public CampaignPartListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CampaignCode", "CAMPAIGN_CODE"},
                    {"PartCode", "PART_CODE"},
                    {"PartTypeDesc", "PART_NAME"},
                    {"Quantity", "QUANTITY"},
                    {"SupplyTypeName", "SUPPLY_TYPE"}
                };
            SetMapper(dMapper);
        }

        public CampaignPartListModel()
        {
        }

        public string CampaignCode { get; set; }

        public Int64 PartId { get; set; }

        [Display(Name = "CampaignPart_Display_PartCode", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "CampaignPart_Display_PartTypeDesc",
            ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string PartTypeDesc { get; set; }

        [Display(Name = "CampaignPart_Display_Quantity", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }

        public int SupplyType { get; set; }
        [Display(Name = "CampaignPart_Display_SupplyType", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string SupplyTypeName { get; set; }
    }
}
