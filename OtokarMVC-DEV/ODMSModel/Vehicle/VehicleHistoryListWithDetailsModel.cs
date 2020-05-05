using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;

namespace ODMSModel.ListModel
{
    public class VehicleHistoryListWithDetailsModel : BaseListWithPagingModel
    {
        public VehicleHistoryListWithDetailsModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            //var dMapper = new Dictionary<string, string>
            //    {
            //        {"ProcessType", "PROCESS_TYPE_NAME"},
            //        {"IndicatorType", "INDICATOR_TYPE_NAME"},
            //        {"DealerName", "DEALER_NAME"},
            //        {"IndicatorDate","ISNULL(WOD.UPDATE_DATE, WOD.CREATE_DATE)"},
            //        {"VehicleKM", "VEHICLE_KM"},
            //        {"WorkOrderDate","WO.WO_DATE"}
            //    };
            //SetMapper(dMapper);

        }
        
        public VehicleHistoryListWithDetailsModel()
        {

        }

        public int VehicleId { get; set; }
        public int VehicleHistoryId { get; set; }
        public int VehicleHistoryDetailId { get; set; }
        public int WorkOrderDetailId { get; set; }
        public int DealerId { get; set; }
        public bool IsPart { get; set; }
        public DateTime CreateDate { get; set; }
        public string AppIndicName { get; set; }

        public int PartId { get; set; }
        public int LabourId { get; set; }

        [Display(Name = "VehicleHistoryDetail_Display_WarrantyRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarratyRatio { get; set; }

        [Display(Name = "VehicleHistoryDetail_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "VehicleHistoryDetail_Display_LabourName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LabourName { get; set; }

        [Display(Name = "Global_Display_IndicatorType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IndicatorType  {get;set;}

        [Display(Name = "Global_Display_ProcessType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string   ProcessType             {get;set;}

        [Display(Name = "GRADGif_Display_WorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64 WorkOrderId             {get;set;}

        [Display(Name = "VehicleHistory_Display_AppIndicName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string   AppIndicCode            {get;set;}

        [Display(Name = "VehicleHistory_Display_CampaignNameCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string   CampaignNameCode        {get;set;}

        [Display(Name = "VehicleHistory_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string   DealerName              {get;set;}

        [Display(Name = "VehicleHistory_Display_IndicatorDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime IndicatorDate           {get;set;}

        [Display(Name = "VehicleHistory_Display_VehicleKM", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public long VehicleKM               {get;set;}

        [Display(Name = "VehicleHistory_Display_WorkOrderDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime WorkOrderDate           {get;set;}

        [Display(Name = "VehicleHistoryDetail_Display_DetailCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string   DetailCode              {get;set;}

        [Display(Name = "VehicleHistoryDetail_Display_DetailDescription", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string   DetailDescription       {get;set;}

        [Display(Name = "VehicleHistoryDetail_Display_ListPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string   ListPrice               {get;set;}

        [Display(Name = "VehicleHistoryDetail_Display_WarrantyPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string   WarrantyPrice           {get;set;}

        [Display(Name = "VehicleHistoryDetail_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string   Quantity                 {get;set;}


      

    }
}
