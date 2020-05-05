using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.CampaignRequest
{
    public class CampaignRequestListModel : BaseListWithPagingModel
    {
        public CampaignRequestListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CampaignCode", "CAMPAIGN_CODE"},
                    {"IdCampaignRequest", "ID_CAMPAIGN_REQUEST"},
                    {"DealerName", "DEALER_NAME"},
                    {"VerihcleModelCode", "MODEL_KOD"},
                    {"RequestNote", "REQUEST_NOTE"},
                    {"CampaignName", "CAMPAIGN_NAME"},
                    {"Quantity", "QUANTITY"},
                    {"RequestStatusName", "REQUEST_STATUS_NAME"},
                    {"PreferredOrderDate", "PREFERRED_ORDER_DATE"},
                    {"PoNumber","PO_NUMBER"},
                    {"WorkOrderId","ID_WORK_ORDER"}
                };
            SetMapper(dMapper);
        }

        public CampaignRequestListModel()
        {
        }

        public string CampaignCode { get; set; }

        [Display(Name = "CampaignRequest_Display_IdCampaignRequest", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? IdCampaignRequest { get; set; }

        [Display(Name = "CampaignRequest_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdDealer { get; set; }
        [Display(Name = "CampaignRequest_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "CampaignRequest_Display_VerihcleModelCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VerihcleModelCode { get; set; }

        [Display(Name = "CampaignRequest_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }

        [Display(Name = "CampaignRequest_Display_ListCampaignName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignName { get; set; }

        [Display(Name = "CampaignRequest_Display_RequestStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? RequestStatus { get; set; }

        [Display(Name = "CampaignRequest_Display_RequestStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestStatusName { get; set; }

        [Display(Name = "CampaignRequest_Display_PreferredOrderDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? PreferredOrderDate { get; set; }

        [Display(Name = "LabourTechnician_Display_WorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64 WorkOrderId { get; set; }

        [Display(Name = "CampaignRequest_Display_PoNumber", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? PoNumber { get; set; }

    }
}
