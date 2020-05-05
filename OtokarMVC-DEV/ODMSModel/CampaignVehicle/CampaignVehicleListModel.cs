using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;

namespace ODMSModel.CampaignVehicle
{
    public class CampaignVehicleListModel : BaseListWithPagingModel
    {
        public CampaignVehicleListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CampaignCode", "CAMPAIGN_CODE"},
                    {"VehicleId", "ID_LABOUR"},
                    {"VinNo", "VIN_NO"},
                    {"IsActive", "IS_ACTIVE"},
                    {"IsActiveName", "IS_ACTIVE_NAME"},
                    {"IsUtilized", "IS_UTILIZED"},
                    {"IsUtilizedName", "IS_UTILIZED_NAME"},
                    {"DealerName", "DEALER_SHRT_NAME"},
                    {"WorkOrderID", "ID_WORK_ORDER"},
                    {"WorkOrderVehicleLeaveDate", "VEHICLE_LEAVE_DATE"},
                    {"WorkOrderKM", "VEHICLE_KM"},
                    {"IsCanceledName", "IS_CANCELED_NAME"},
                    {"DenyReason", "CAMPAIGN_DENY_REASON"},
                    {"CancelReason", "CANCEL_REASON"},
                    {"Customer", "CUSTOMER"},
                    {"LastWorkOrderId", "LAST_WORK_ORDER_ID"},
                    {"IsCompleteString", "IS_COMPLETE"}
                };
            SetMapper(dMapper);
        }

        public CampaignVehicleListModel()
        {
        }

        public string CampaignCode { get; set; }

        public int VehicleId { get; set; }

        [Display(Name = "CampaignVehicle_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }

        public int IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        public int IsUtilized { get; set; }
        [Display(Name = "CampaignVehicle_Display_IsUtilized", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsUtilizedName { get; set; }





        [Display(Name = "CampaignVehicle_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "CampaignVehicle_Display_WorkOrderID", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkOrderID { get; set; }
        [Display(Name = "CampaignVehicle_Display_WorkOrderVehicleLeaveDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? WorkOrderVehicleLeaveDate { get; set; }
        [Display(Name = "CampaignVehicle_Display_WorkOrderKM", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int WorkOrderKM { get; set; }
        [Display(Name = "CampaignVehicle_Display_DealerRejectDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerRejectDesc { get; set; }
        [Display(Name = "CampaignVehicle_Display_CustomerRejectDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerRejectDesc { get; set; }


        public bool IsCanceled { get; set; }
        [Display(Name = "CampaignVehicle_Display_IsCanceledName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsCanceledName { get; set; }
        [Display(Name = "CampaignVehicle_Display_DenyReason", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DenyReason { get; set; }
        [Display(Name = "CampaignVehicle_Display_CancelReason", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CancelReason { get; set; }

        [Display(Name = "Appointment_Display_Customer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Customer { get; set; }

        [Display(Name = "CampaignVehicle_Display_LastWorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LastWorkOrderId { get; set; }

        [Display(Name = "CampaignVehicle_Display_IsComplete", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int IsComplete { get; set; }
        [Display(Name = "CampaignVehicle_Display_IsComplete", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsCompleteString { get; set; }
        [Display(Name = "ChargePerCarReport_VehicleKm", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]        
        public int VehicleKM { get; set; }

    }
}
