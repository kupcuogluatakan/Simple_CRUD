using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;


namespace ODMSModel.Reports
{
    public class CampaignSummaryInfoModel
    {
        [Display(Name = "Appointment_Display_VehicleModel", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModel { get; set; }
        [Display(Name = "Appointment_Display_VehicleType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleType { get; set; }
        [Display(Name = "CampaignVehicle_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }
        [Display(Name = "ClaimRecallPeriod_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActive { get; set; }
        [Display(Name = "CampaignSummaryReport_Display_IsApply", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsApply { get; set; }
        [Display(Name = "CampaignSummaryReport_Display_Region", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Region { get; set; }
        [Display(Name = "CampaignVehicle_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "CampaignSummaryReport_Display_WorkOrderCardNumber", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkOrderCardNumber { get; set; }
        [Display(Name = "CampaignSummaryReport_Display_WorkOrderStartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? WorkOrderStartDate { get; set; }
        [Display(Name = "CampaignSummaryReport_Display_Kilometer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Kilometer { get; set; }
        [Display(Name = "CampaignSummaryReport_Display_WarrantyStartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? WarrantyStartDate { get; set; }
        [Display(Name = "CampaignSummaryReport_Display_WorkerMount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WorkerMount { get; set; }
        [Display(Name = "CampaignSummaryReport_Display_BitMount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BitMount { get; set; }
       
        public string BitCoast { get; set; }
        [Display(Name = "GuaranteeReport_CampaignCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignCode { get; set; }
        [Display(Name = "KilometerReportInfo_Display_VehicleId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int VehicleId { get; set; }
        [Display(Name = "PDIInvoiceList_Display_VehicleGroupCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int GroupCode { get; set; }
        [Display(Name = "CampaignSummaryReport_Display_GroupType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int GroupType { get; set; }
        [Display(Name = "ChargePerCarReport_Currency", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Currency { get; set; }
        public decimal AvgPrice { get; set; }
    
        public decimal GifCost { get; set; }

    }
}
