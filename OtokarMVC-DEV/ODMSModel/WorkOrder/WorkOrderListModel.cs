using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.WorkOrder
{
    public class WorkOrderListModel:BaseListWithPagingModel
    {
        public  WorkOrderListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
             var dMapper = new Dictionary<string, string>
                 {
                     {"WorkOrderId", "WO.ID_WORK_ORDER"},
                     {"CustomerName", "CUSTOMER_ID"},
                     {"VehiclePlate", "WO.PLATE"},
                     {"WorkOrderDate", "WO_DATE"},
                     {"DealerName", "ID_DEALER"},
                     {"WorkOrderStatus","STATUS_DESC"},
                     {"VehicleCode","V_CODE_KOD"},
                     {"VinNo","VIN_NO"},
                     {"EngineNo","ENGINE_NO"},
                     {"VehicleModel","VEHICLE_MODEL"},
                     {"WarrantyStartDate","WARRANTY_START_DATE"},
                     {"WarrantyEndDate","WARRANTY_END_DATE"},
                     {"WarrantyEndKilometer","WARRANTY_END_KM"},
                     {"VehicleType","VEHICLE_TYPE"}
                 };
            SetMapper(dMapper);

        }

        public WorkOrderListModel(){ }
        [Display(Name = "User_Display_WorkOrderId", ResourceType = typeof(MessageResource))]
        public long WorkOrderId { get; set; }
        [Display(Name = "User_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "CustomerAddress_Display_CustomerName", ResourceType = typeof(MessageResource))]
        public string CustomerName { get; set; }
        [Display(Name = "Appointment_Display_VehiclePlate", ResourceType = typeof(MessageResource))]
        public string VehiclePlate{ get; set; }
        [Display(Name = "WorkOrder_Display_WorkOrderDate", ResourceType = typeof(MessageResource))]
        public DateTime WorkOrderDate { get; set; }
        [Display(Name = "WorkOrder_Display_WorkOrderStatus", ResourceType = typeof(MessageResource))]
        public String WorkOrderStatus { get; set; }
        [Display(Name = "WorkOrder_Display_WorkOrderStatus", ResourceType = typeof(MessageResource))]
        public string WorkOrderStatusId { get; set; }

        public int VehicleId { get; set; }
        [Display(Name = "Vehicle_Display_VehicleCode", ResourceType = typeof(MessageResource))]
        public string VehicleCode { get; set; }
        [Display(Name = "LabourDuration_Display_VehicleModelName", ResourceType = typeof(MessageResource))]
        public string VehicleModel { get; set; }
        [Display(Name = "Appointment_Display_VehicleType", ResourceType = typeof(MessageResource))]
        public string VehicleType { get; set; }
        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "Vehicle_Display_EngineNo", ResourceType = typeof(MessageResource))]
        public string EngineNo { get; set; }
        [Display(Name = "Vehicle_Display_WarrantyStartDate", ResourceType = typeof(MessageResource))]
        public DateTime WarrantyStartDate { get; set; }
        [Display(Name = "Vehicle_Display_WarrantyEndDate", ResourceType = typeof(MessageResource))]
        public DateTime WarrantyEndDate { get; set; }
        [Display(Name = "Vehicle_Display_WarrantyEndKilometer", ResourceType = typeof(MessageResource))]
        public string WarrantyEndKilometer { get; set; }

        [Display(Name = "GuaranteeReport_DealerSSID", ResourceType = typeof(MessageResource))]
        public string DealerSSID { get; set; } 

        public int? DealerId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
