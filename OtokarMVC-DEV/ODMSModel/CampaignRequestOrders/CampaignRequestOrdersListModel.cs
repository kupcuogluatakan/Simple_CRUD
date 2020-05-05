using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using ODMSModel.Validation;
namespace ODMSModel.CampaignRequestOrders
{
    public class CampaignRequestOrdersListModel : BaseListWithPagingModel
    {
        public CampaignRequestOrdersListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
            {
                {"CampaignRId", "ID_CAMPAIGN_REQUEST"},
                {"ModelName", "MODEL_NAME"},
                {"CampaignName", "CAMPAIGN_NAME"},
                {"CampaignCode", "CAMPAIGN_CODE"},
                {"Quantity", "QUANTITY"},
                {"RequestNote", "REQUEST_NOTE"},
                {"StatusString", "REQUEST_STATUS"},
                {"RequestDate", "PREFERRED_ORDER_DATE"}
            };
            SetMapper(dMapper);
        }
        public CampaignRequestOrdersListModel()
        { }
        [RequiredIfButtonClicked("action:InsertUpdate")]
        [Display(Name = "CampaignRequestOrders_Display_CampaignRequestId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int CampaignRId { get; set; }
        [Display(Name = "CampaignRequestOrders_Display_CampaignCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignCode { get; set; }
        [Display(Name = "CampaignRequestOrders_Display_VehicleModel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelName { get; set; }
        [Display(Name = "CampaignRequestOrders_Display_CampaignName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public string CampaignName { get; set; }
        [Display(Name = "CampaignRequestOrders_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public int Quantity { get; set; }
        [Display(Name = "CampaignRequestOrders_Display_ReqNote", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public string RequestNote { get; set; }
        [Display(Name = "CampaignRequestOrders_Display_RequestStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public bool status { get; set; }
        [Display(Name = "CampaignRequestOrders_Display_RequestDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]

        public DateTime RequestDate { get; set; }
         [Display(Name = "CampaignRequestOrders_Display_RequestStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string  StatusString { get; set; }

    }
}
