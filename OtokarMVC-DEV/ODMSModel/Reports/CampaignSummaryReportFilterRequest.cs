using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.Reports
{
    public class CampaignSummaryReportFilterRequest : ReportListModelBase
    {
        public CampaignSummaryReportFilterRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }

        public CampaignSummaryReportFilterRequest() { }

        [Display(Name = "VehicleServiceDurationReport_Message_QuestioningBy", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int GroupType { get; set; }

        [Display(Name = "Campaign_Display_StartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Campaign_Display_EndDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_DealerRegionIdList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionIdList { get; set; }

        [Display(Name = "WorkOrderDetailReport_Display_ModelKodList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleModelList { get; set; }

        [Display(Name = "AppointmentDetails_Display_CampCode", ResourceType = typeof(MessageResource))]
        public string CampaignCode { get; set; }

        [Display(Name = "PartStockReport_Currency", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Currency { get; set; }

        [Display(Name = "FleetRequestVehicle_Display_IsWarranty", ResourceType = typeof(MessageResource))]
        public int IsWarranty { get; set; }

        [Display(Name = "VehicleServiceDurationReport_Message_CampaignStatus", ResourceType = typeof(MessageResource))]
        public int CampaignStatus { get; set; }

        [Display(Name = "VehicleServiceDurationReport_Message_IsMust", ResourceType = typeof(MessageResource))]
        public int IsMust { get; set; }

        [Display(Name = "VehicleServiceDurationReport_GuaranteeConfirmStartDate", ResourceType = typeof(MessageResource))]
        public DateTime? GuaranteeConfirmStartDate { get; set; }

        [Display(Name = "VehicleServiceDurationReport_GuaranteeConfirmEndDate", ResourceType = typeof(MessageResource))]
        public DateTime? GuaranteeConfirmEndDate { get; set; }
        [Display(Name = "VehicleServiceDurationReport_Quantity", ResourceType = typeof(MessageResource))]
        public int Quantity { get; set; }


    }
}
