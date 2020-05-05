using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;

namespace ODMSModel.ListModel
{
    public class VehicleHistoryDetailListModel : BaseListWithPagingModel
    {
        public VehicleHistoryDetailListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"LabourName", "LABOUR_NAME"},
                    {"PartName", "PART_NAME"},
                    {"DetailCode","DET_CODE"},
                    {"DetailDescription", "DET_DESC"},
                    {"Quantity","QUANTITY"},
                    {"WarratyRatio","W_RATIO"},
                    {"WarrantyPrice","W_PRICE"},
                    {"ListPrice","PRICE"}
                };
            SetMapper(dMapper);

        }

        public VehicleHistoryDetailListModel()
        {
        }

        public int VehicleHistoryId { get; set; }
        public int VehicleHistoryDetailId { get; set; }

        public string VinNo { get; set; }
        public string Plate { get; set; }

        public string CustomerIds { get; set; }

        public string ProcessType { get; set; }

        public string IndicatorType { get; set; }

        public bool IsPart { get; set; }

        [Display(Name = "GRADGif_Display_WorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public Int64 WorkOrderId { get; set; }

        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        public int LabourId { get; set; }
        [Display(Name = "VehicleHistoryDetail_Display_LabourName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LabourName { get; set; }
        
        public int PartId { get; set; }
        [Display(Name = "VehicleHistoryDetail_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        
        [Display(Name = "VehicleHistoryDetail_Display_DetailCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DetailCode { get; set; }
        [Display(Name = "VehicleHistoryDetail_Display_DetailDescription", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DetailDescription { get; set; }

        [Display(Name = "VehicleHistoryDetail_Display_Quantity", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string Quantity { get; set; }

        [Display(Name = "VehicleHistoryDetail_Display_WarrantyPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarrantyPrice { get; set; }

        [Display(Name = "VehicleHistoryDetail_Display_ListPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ListPrice { get; set; }

        [Display(Name = "VehicleHistoryDetail_Display_WarrantyRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarratyRatio { get; set; }

    }
}
