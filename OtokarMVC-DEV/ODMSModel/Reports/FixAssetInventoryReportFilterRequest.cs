using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class FixAssetInventoryReportFilterRequest : ReportListModelBase
    {
        public FixAssetInventoryReportFilterRequest([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {

            SetMapper(null);
        }

        public FixAssetInventoryReportFilterRequest() { }

        [Display(Name = "FixAssetInventory_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerIdList { get; set; }
        [Display(Name = "FixAssetInventory_Display_DealerRegion", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionIdList { get; set; }
        [Display(Name = "FixAssetInventory_Display_EquipmentType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int EquipmentTypeId { get; set; }
        [Display(Name = "FixAssetInventory_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "FixAssetInventory_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "FixAssetInventory_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int StatusId { get; set; }
        [Display(Name = "FixAssetInventory_Display_VehicleGroup", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int VehicleGroupId { get; set; }


    }
}
