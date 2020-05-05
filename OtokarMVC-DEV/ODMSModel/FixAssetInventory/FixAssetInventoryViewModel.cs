using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.FixAssetInventory
{
    [Validator(typeof(FixAssetInventoryViewModelValidator))]
    public class FixAssetInventoryViewModel : ModelBase
    {
        public FixAssetInventoryViewModel()
        {
        }

        public int FixAssetInventoryId { get; set; }

        [Display(Name = "FixAssetInventory_Display_IsPartOriginal", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool? IsPartOriginal { get; set; }

        public int? EquipmentTypeId { get; set; }
        [Display(Name = "FixAssetInventory_Display_EquipmentType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EquipmentTypeName { get; set; }
        
        public int? PartId { get; set; }
        [Display(Name = "FixAssetInventory_Display_Part", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        public int? StockTypeId { get; set; }
        [Display(Name = "FixAssetInventory_Display_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeName { get; set; }
        
        public int? RackId { get; set; }
        [Display(Name = "FixAssetInventory_Display_Rack", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RackName { get; set; }

        public int? WarehouseId { get; set; }
        [Display(Name = "FixAssetInventory_Display_Warehouse", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarehouseName { get; set; }

        [Display(Name = "FixAssetInventory_Display_RackWarehouse", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RackWarehouse { get; set; }
        
        public int? VehicleGroupId { get; set; }
        [Display(Name = "FixAssetInventory_Display_VehicleGroup", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleGroupName { get; set; }

        [Display(Name = "FixAssetInventory_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Code { get; set; }

        [Display(Name = "FixAssetInventory_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Name { get; set; }

        [Display(Name = "FixAssetInventory_Display_SerialNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SerialNo { get; set; }

        [Display(Name = "FixAssetInventory_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }

        [Display(Name = "FixAssetInventory_Display_Unit", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Unit { get; set; }

        [Display(Name = "FixAssetInventory_Display_RestockReason", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RestockReason { get; set; }

        public new int? StatusId { get; set; }
        [Display(Name = "FixAssetInventory_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StatusName { get; set; }
    }
}
