using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;

namespace ODMSModel.ListModel
{
    public class VehicleHistoryListModel : BaseListWithPagingModel
    {
        public VehicleHistoryListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"ProcessType", "PROCESS_TYPE_NAME"},
                    {"IndicatorType", "INDICATOR_TYPE_NAME"},
                    {"DealerName", "DEALER_NAME"},
                    {"IndicatorDate","ISNULL(WOD.UPDATE_DATE, WOD.CREATE_DATE)"},
                    {"VehicleKM", "VEHICLE_KM"},
                    {"WorkOrderDate","WO.WO_DATE"}
                };
            SetMapper(dMapper);

        }

        public VehicleHistoryListModel()
        {
        }
        [Display(Name = "GRADGif_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "GRADGif_Display_Plate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Plate { get; set; }
        [Display(Name = "GRADGif_Display_CustomerIds", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerIds { get; set; }
        public int VehicleId { get; set; }
        public int VehicleHistoryId { get; set; }
        public int WorkOrderDetailId { get; set; }

        [Display(Name = "GRADGif_Display_WorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64 WorkOrderId { get; set; }

        [Display(Name = "Global_Display_IndicatorType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IndicatorType { get; set; }
        [Display(Name = "Global_Display_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessType { get; set; }
        public int DealerId { get; set; }
        [Display(Name = "VehicleHistory_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "VehicleHistory_Display_IndicatorDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime IndicatorDate { get; set; }

        [Display(Name = "VehicleHistory_Display_VehicleKM", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long VehicleKM { get; set; }

        [Display(Name = "VehicleHistory_Display_WorkOrderDate", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public DateTime WorkOrderDate { get; set; }

        [Display(Name = "VehicleHistory_Display_CampaignNameCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignNameCode { get; set; }

        [Display(Name = "VehicleHistory_Display_AppIndicName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AppIndicCode { get; set; }
        public string AppIndicName { get; set; }
   


    }
}
