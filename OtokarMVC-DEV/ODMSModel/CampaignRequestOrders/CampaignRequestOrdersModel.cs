using System;
using System.ComponentModel.DataAnnotations;
using ODMSModel.Validation;
namespace ODMSModel.CampaignRequestOrders
{
    public class CampaignRequestOrdersModel : ModelBase
    {
        //public int DealerId { get; set; }
        //public int SuppliDealer { get; set; }
        
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

        public DateTime? RequestDate { get; set; }
    }
}
