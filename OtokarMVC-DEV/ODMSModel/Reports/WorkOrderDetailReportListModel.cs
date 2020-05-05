using System.Collections.Generic;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.Reports
{
    public class WorkOrderDetailReportListModel : BaseListWithPagingModel
    {
        public WorkOrderDetailReportListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"WorkOrderNo", "WO.ID_WORK_ORDER"},
                    {"StatusName", "STATUS_DESC"},
                    {"Plate", "PLATE"},
                    {"CustomerNameLastName", "4"},
                    {"VehicleKM", "VEHICLE_KM"},
                    {"DealerName", "DEALER_NAME"},
                    {"WorkOrderDate","WO_DATE"},
                    {"ClosedDate","ClosedDate"},
                    {"DayTime","ClosedDate"},
                    {"VinNo","VIN_NO"},
                    {"WarrantyStartDate","WARRANTY_START_DATE"},
                    {"WarrantyEndDate","WARRANTY_END_DATE"},
                    {"CustomerPartPrice","CUSTOMER_PART_PRICE"},
                    {"CustomerLabourPrice","CUSTOMER_LABOUR_PRICE"},
                    {"WarrantyPartPrice","WARRANTY_PART_PRICE"},
                    {"WarrantyLabourPrice","WARRANTY_LABOUR_PRICE"},
                    {"CurrencyCode","CURRENCY_CODE"},
                    {"ModelKod","MODEL_KOD"},
                    {"DealerRegionName","DEALER_REGION_NAME"},
                    {"VehicleLeaveDate","VEHICLE_LEAVE_DATE"},
                    {"FleetCode","FLEET_CODE"},
                    {"VehicleType","TYPE_NAME"}
                };
            SetMapper(dMapper);
        }

        public WorkOrderDetailReportListModel()
        {
        }

        #region Search Parameters
        [Display(Name = "WorkOrderDetailReport_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerIdList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerRegionIdList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerRegionIdList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_ModelKodList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelKodList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_StatusIdList",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StatusIdList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_IsDayTime",
     ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IsDayTime { get; set; }

        public string WorkOrderIdList { get; set; }

        #endregion

        #region List Display Columns
        [Display(Name = "WorkOrderDetailReport_Display_WorkOrderNo",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkOrderNo { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_StatusName",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StatusName { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_Plate",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Plate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_CustomerNameLastName",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerNameLastName { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_VehicleKM",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleKM { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerName",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_WorkOrderDate",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? WorkOrderDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_ClosedDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? ClosedDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_TotalDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DayTime { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_WoDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WoTime { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_VinNo",ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "Vehicle_Display_WarrantyEndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? WarrantyEndDate { get; set; }
        [Display(Name = "Vehicle_Display_WarrantyStartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? WarrantyStartDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_CustomerPartPrice",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal CustomerPartPrice { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_CustomerLabourPrice",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal CustomerLabourPrice { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_WarrantyPartPrice",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal WarrantyPartPrice { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_WarrantyLabourPrice",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal WarrantyLabourPrice { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_CurrencyCode",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_ModelKod",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelKod { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerRegionName",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerRegionName { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_VehicleLeaveDate",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? VehicleLeaveDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_FleetCode",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FleetCode { get; set; }
        #endregion

        [Display(Name = "WorkOrderDetailReport_Display_DealerSAP", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SAPCode { get; set; }


        public int VehicleId { get; set; }
        [Display(Name = "WorkOrderDetailReport_Display_MaintDayCount",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int MaintDayCount { get; set; }
        [Display(Name = "LabourDuration_Display_VehicleTypeName",
        ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleType { get; set; }

        [Display(Name = "PurchaseOrderInquiryViewModel_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "CampaignLabour_Display_LabourCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LabourCode { get; set; }

    }
}
