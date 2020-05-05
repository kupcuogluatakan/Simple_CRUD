using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class FixAssetInventoryReport
    {
        [Display(Name = "FixAssetInventory_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EquipmentTypeName { get; set; }
        [Display(Name = "FixAssetInventory_Display_Dealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "FixAssetInventory_Display_DealerRegion", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RegionName { get; set; }
        [Display(Name = "FixAssetInventory_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "FixAssetInventory_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        [Display(Name = "FixAssetInventory_Display_FixAssetCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FixAssetCode { get; set; }
        [Display(Name = "FixAssetInventory_Display_FixAssetName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FixAssetName { get; set; }
        [Display(Name = "FixAssetInventory_Display_SerialNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SerialNo { get; set; }
        [Display(Name = "FixAssetInventory_Display_VehicleGroup", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleGroupName { get; set; }
        [Display(Name = "FixAssetInventory_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StatusName { get; set; }
        [Display(Name = "FixAssetInventory_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime CreateDate { get; set; }
    }
}
