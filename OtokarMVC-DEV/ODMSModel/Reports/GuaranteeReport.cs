using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class GuaranteeReport
    {
        [Display(Name = "GuaranteeReport_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "GuaranteeReport_RegionName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionName { get; set; }
        [Display(Name = "GuaranteeReport_DealerSSID", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DEALER_SSID { get; set; }
        [Display(Name = "GuaranteeReport_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VEHICLE_VIN_NO { get; set; }
        [Display(Name = "GuaranteeReport_WorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ID_WORK_ORDER { get; set; }
        [Display(Name = "GuaranteeReport_WorkOrderDetailId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ID_WORK_ORDER_DETAIL { get; set; }
        [Display(Name = "GuaranteeReport_SSID_Guarantee", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SSID_GUARANTEE { get; set; }
        public string GuaranteeId { get; set; }
        public string GuaranteeSeqNo { get; set; }
        [Display(Name = "GuaranteeReport_Category", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CATEGORY_LOOKVAL { get; set; }
        [Display(Name = "GuaranteeReport_Vehicle_KM", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VEHICLE_KM { get; set; }
        [Display(Name = "GuaranteeReport_CampaignCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CAMPAIGN_CODE { get; set; }
        public string ID_VEHICLE { get; set; }
        [Display(Name = "GuaranteeReport_VehiclePlate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VEHICLE_PLATE { get; set; }
        [Display(Name = "GuaranteeReport_VehicleNote", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VEHICLE_NOTES { get; set; }
        [Display(Name = "GuaranteeReport_CenterNote", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CENTER_NOTES { get; set; }
        [Display(Name = "GuaranteeReport_SpecialNote", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SPECIAL_NOTES { get; set; }
        [Display(Name = "GuaranteeReport_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }
        //[Display(Name = "GuaranteeReport_ConfirmedUser", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public string ConfirmedUser { get; set; }
        [Display(Name = "GuaranteeReport_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CODE { get; set; }
        [Display(Name = "GuaranteeReport_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CODEDESC { get; set; }
        [Display(Name = "GuaranteeReport_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime CREATE_DATE { get; set; }
        [Display(Name = "GuaranteeReport_LeaveDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime VEHICLE_LEAVE_DATE { get; set; }
        [Display(Name = "GuaranteeReport_PdiGif", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PDI_GIF { get; set; }
        [Display(Name = "GuaranteeReport_CrmCustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CRM_CUSTOMER_CUSTOMER_NAME { get; set; }
        [Display(Name = "GuaranteeReport_CrmCustomerLastName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CRM_CUSTOMER_CUSTOMER_LASTNAME { get; set; }
        [Display(Name = "GuaranteeReport_LabourTotalPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal LABOUR_TOTAL_PRICE { get; set; }
        [Display(Name = "GuaranteeReport_PartsTotalPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal PARTS_TOTAL_PRICE { get; set; }
        [Display(Name = "GuaranteeReport_IndicatorTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string INDICATOR_TYPE_NAME { get; set; }
        [Display(Name = "GuaranteeReport_ProcessTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PROCESS_TYPE_NAME { get; set; }



        [Display(Name = "GuaranteeReport_WebServiceLabourTotalPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal WEB_SERVICE_LABOUR_TOTAL_PRICE { get; set; }
        [Display(Name = "GuaranteeReport_WebServicePartsTotalPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal WEB_SERVICE_PARTS_TOTAL_PRICE { get; set; }
    }
}
