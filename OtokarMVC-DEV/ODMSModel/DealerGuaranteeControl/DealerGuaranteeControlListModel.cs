using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using ODMSModel.ListModel;

namespace ODMSModel.DealerGuaranteeControl
{
    public class DealerGuaranteeControlListModel: BaseListWithPagingModel
    {
        public DealerGuaranteeControlListModel([DataSourceRequest] DataSourceRequest request)
           : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"Dealer", "D.ID_DEALER"},
                    {"WarrantyStatus", "MST.WARRANTY_STATUS"},
                    {"CategoryId", "MST.CATEGORY_LOOKVAL"},
                    {"ProcessType", "WOD.PROCESS_TYPE_CODE"},
                    {"IndicatorType","WOD.INDICATOR_TYPE_CODE"},
                    {"RequestDate", "GSL.REQUEST_DATE"},
                    {"ApproveDate", "GSL.APPROVE_DATE"},
                    {"WorkOrderId", "WO.ID_WORK_ORDER"},
                    {"WorkOrderDetailId", "WOD.ID_WORK_ORDER_DETAIL"},
                    {"VinNo","WO.VIN_NO"},
                    {"IsPermString","13"},
                    {"FailCodeDesc","15"},
                    {"FailCode","AISC.SUB_CODE,AIFC.CODE"}
                };
            SetMapper(dMapper);
        }
        public DealerGuaranteeControlListModel()
        {

        }

        public long GuaranteeId { get; set; }
        public long GuaranteeSeq { get; set; }
        [Display(Name = "Vehicle_Display_WarrantyStatus", ResourceType = typeof(MessageResource))]
        public string WarrantyStatus { get; set; }
        [Display(Name = "FleetRequestApprove_Display_RequestDescription", ResourceType = typeof(MessageResource))]
        public string RequestDescription { get; set; }
        [Display(Name = "Global_Display_Category", ResourceType = typeof(MessageResource))]
        public string Category { get; set; }
        [Display(Name = "Global_Display_ProcessType", ResourceType = typeof(MessageResource))]
        public string ProcessType { get; set; }
        [Display(Name = "Global_Display_IndicatorType", ResourceType = typeof(MessageResource))]
        public string IndicatorType { get; set; }
        [Display(Name = "WorkOrderPartHistoryReport_Display_WorkOrderId", ResourceType = typeof(MessageResource))]
        public long WorkOrderId { get; set; }
        [Display(Name = "WorkOrderBatchInvoice_Display_WorkOrderDetailId", ResourceType = typeof(MessageResource))]
        public long WorkOrderDetailId { get; set; }
        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "GuaranteeRequestApprove_Display_Permission", ResourceType = typeof(MessageResource))]
        public bool IsPerm { get; set; }
        [Display(Name = "GuaranteeRequestApprove_Display_Permission", ResourceType = typeof(MessageResource))]
        public string IsPermString { get; set; }
        [Display(Name = "AppointmentIndicatorFailureCode_Display_Code", ResourceType = typeof(MessageResource))]
        public string FailCode { get; set; }
        public string FailCodeDesc { get; set; }
        [Display(Name = "Global_Display_RequestDate", ResourceType = typeof(MessageResource))]
        public DateTime RequestDate { get; set; }
        [Display(Name = "Global_Display_ApproveDate", ResourceType = typeof(MessageResource))]
        public DateTime? ApproveDate { get; set; }
        [Display(Name = "GRADGif_Display_GifNo", ResourceType = typeof(MessageResource))]
        public long? GifNo { get; set; }
        public int VehicleId { get; set; }
        [Display(Name = "PurchaseOrderReport_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string Dealer { get; set; }
        public string ConfirmDesc { get; set; }




        [Display(Name = "PDIVehicleResult_Display_DealerName",ResourceType = typeof(MessageResource))]
        public int DealerId { get; set; }
        [Display(Name = "Global_Display_Category", ResourceType = typeof(MessageResource))]
        public string CategoryId { get; set; }
        [Display(Name = "Announcement_Display_StartDate", ResourceType = typeof(MessageResource))]
        public DateTime? StartDate { get; set; }
        [Display(Name = "Announcement_Display_EndDate", ResourceType = typeof(MessageResource))]
        public DateTime? EndDate { get; set; }
        [Display(Name = "DealerFuaranteeControl_Display_ApproveStartDate", ResourceType = typeof(MessageResource))]
        public DateTime? ApproveStartDate { get; set; }
        [Display(Name = "DealerFuaranteeControl_Display_ApproveEndDate", ResourceType = typeof(MessageResource))]
        public DateTime? ApproveEndDate { get; set; }
        public string LanguageCode { get; set; }        
        public int ErrorNo { get; set; }
        public string ErrorDesc { get; set; }

        [Display(Name = "LabourDuration_Display_VehicleTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleType { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_ModelKodList",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelKodList { get; set; }

    }
}
