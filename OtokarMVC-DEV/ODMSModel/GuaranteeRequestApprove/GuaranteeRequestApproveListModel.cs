using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.GuaranteeRequestApprove
{
    public class GuaranteeRequestApproveListModel : BaseListWithPagingModel
    {
        public GuaranteeRequestApproveListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"ProcessType", "PROCESS_TYPE_NAME"},
                    {"WarrantyStatusName","WARRANTY_STATUS_NAME"},
                    {"IndicatorTypeName", "INDICATOR_TYPE_NAME"},
                    {"WorkOrderId", "ID_WORK_ORDER"},
                    {"DealerName", "DEALER_NAME"},
                    {"VehicleVinNo", "VEHICLE_VIN_NO"},
                    {"VehicleCode", "VEHICLE_V_CODE_KOD"},
                    {"IsPerm", "IS_PERM"},
                    {"FailCode", "FAIL_CODE"},
                    {"ApproveUserName", "APPROVE_USER_NAME"},
                    {"RequestDate", "REQUEST_DATE"}
                };
            SetMapper(dMapper);
        }

        public GuaranteeRequestApproveListModel()
        {
        }

        public Int64? IdGuarantee { get; set; }

        public Int16? GuaranteeSeq { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_RequestUserName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestUserName { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_ApproveUserName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ApproveUserName { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_RequestDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RequestDesc { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_WarrantyStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarrantyStatusName { get; set; }

        #region Search Variables

        [Display(Name = "CustomerDiscount_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int32? IdDealer { get; set; }

        [Display(Name = "Global_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Global_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_WarrantyStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarrantyStatus { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_Category", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CategoryId { get; set; }
        [Display(Name = "GuaranteeRequestApprove_Display_Category", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CategoryName { get; set; }
        public Int32 IdUser { get; set; }

        [Display(Name = "DealerRegion_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerRegionId { get; set; }

        [Display(Name = "Global_Display_RequestDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime RequestDate { get; set; }

        [Display(Name = "Global_Display_ApproveDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? ApproveDate { get; set; }

        [Display(Name = "Global_Display_IndicatorType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IndicatorTypeName { get; set; }

        #endregion

        [Display(Name = "LabourTechnician_Display_WorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? WorkOrderId { get; set; }

        [Display(Name = "GRADGif_Display_WorkOrderDetId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64? WorkOrderDetailId { get; set; }

        [Display(Name = "Global_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime CreateDate { get; set; }

        [Display(Name = "CampaignRequestApprove_Display_RequestDealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleVinNo { get; set; }

        [Display(Name = "VehicleCode_Display_Kod", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleCode { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_Permission", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsPerm { get; set; }

        [Display(Name = "Global_Display_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessType { get; set; }

        [Display(Name = "Vehicle_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustName { get; set; }

        [Display(Name = "AppointmentIndicatorFailureCode_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FailCode { get; set; }
        
        public int? IdVehicle { get; set; }

        public int? IsEditable { get; set; }

        public Int64 GifNo { get; set; }

        [Display(Name = "DealerFuaranteeControl_Display_ApproveStartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? ApproveStartDate { get; set; }
        [Display(Name = "DealerFuaranteeControl_Display_ApproveEndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? ApproveEndDate { get; set; }

        [Display(Name = "Global_Display_IndicatorType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IndicatorType { get; set; }
        [Display(Name = "LabourDuration_Display_VehicleTypeName",
    ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleType { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_ModelKodList",
         ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ModelKodList { get; set; }
    }
}
