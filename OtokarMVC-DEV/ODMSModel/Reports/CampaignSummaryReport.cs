using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ODMSModel.Reports
{
    public class CampaignSummaryReport
    {
        [Display(Name = "CampaignSummaryReport_Display_GroupName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GroupCode { get; set; }

        [Display(Name = "CampaignSummaryReport_Display_GroupName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GroupName { get; set; }

        [Display(Name = "CampaignSummaryReport_Display_CompaignCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CampaignCode { get; set; }

        [Display(Name = "CampaignSummaryReport_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }

        [Display(Name = "CampaignSummaryReport_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }
        [Display(Name = "CampaignSummaryReport_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }
        [Display(Name = "CampaignSummaryReport_Display_TotalCampaignInternalVehicle", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int TotalCampaignInternalVehicle { get; set; }
        [Display(Name = "CampaignSummaryReport_Display_TotalCampaignUseVehicle", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int TotalCampaignUseVehicle { get; set; }

        [Display(Name = "CampaignSummaryReport_Display_CampaignUseVehicle", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int CampaignUseVehicle { get; set; }

        [Display(Name = "CampaignSummaryReport_Display_Application", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public float Application { get; set; }

        [Display(Name = "CampaignSummaryReport_Display_WorkerAmount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public double WorkerAmount { get; set; }

        [Display(Name = "CampaignSummaryReport_Display_BitMount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public double BitMount { get; set; }
        
       
        [Display(Name = "CampaignSummaryReport_Display_Currency", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Currency { get; set; }

        public int GroupType { get; set; }

        [Display(Name = "CampaignSummaryReport_Display_Orderqty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int Orderqty { get; set; }

        [Display(Name = "CampaignSummaryReport_Display_ReturnedQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int ReturnedQty { get; set; }

        [Display(Name = "PartExchangeReport_Display_AvgPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal AvgPrice { get; set; }
    }
}
