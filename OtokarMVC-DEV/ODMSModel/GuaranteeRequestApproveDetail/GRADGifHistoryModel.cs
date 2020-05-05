using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class GRADGifHistoryModel : BaseListWithPagingModel
    {
        public GRADGifHistoryModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"WayBillNo", "VAYBILL_NO"},
                    {"SupplierName","SUPPLIER_NAME"},
                    {"Status","STATUS_NAME"},
                    {"WayBillDate", "WAYBILL_DATE"},
                    {"TotalPrice", "TOTAL_PRICE"},
                    {"IsPlaced","IS_PLACED"}
                };
            SetMapper(dMapper);
        }

        public GRADGifHistoryModel()
        {

        }
        public Int64 GuaranteeId{ get; set; }
        public int GuaranteeSeq { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_WarrantyStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int WarrantyStatus { get; set; }
        [Display(Name = "GuaranteeRequestApprove_Display_WarrantyStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarrantyStatusName { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "GRADGif_Display_WorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64 WorkOrderId { get; set; }

        [Display(Name = "GRADGif_Display_WorkOrderDetId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64 WorkOrderDetId { get; set; }

        [Display(Name = "VehicleHistory_Display_VehicleKM", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleKm { get; set; }

        [Display(Name = "Global_Display_IndicatorType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IndicatorCode { get; set; }
        [Display(Name = "Global_Display_IndicatorType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IndicatorName { get; set; }

        [Display(Name = "Global_Display_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessCode { get; set; }
        [Display(Name = "Global_Display_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ProcessName { get; set; }

        [Display(Name = "WorkOrderCard_Dislay_VehicleLeaveDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleLeaveDate { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_ApproveUserName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ApprovedUserCode { get; set; }

        public string ApproveduserEmail { get; set; }

        [Display(Name = "Global_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ConfirmDesc { get; set; }

        [Display(Name = "Campaign_Display_CampaignName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string  CampaignName { get; set; }

        

    }
}
