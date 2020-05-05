using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.FixAssetInventory
{
    public class FixAssetInventoryListModel : BaseListWithPagingModel
    {
        public FixAssetInventoryListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"EquipmentTypeName", "EQUIPMENT_NAME"},
                    {"PartName", "PART_NAME"},
                    {"PartCode", "PART_CODE"},
                    {"StockTypeName", "MAINT_NAME"},
                    {"RackName","RACK_NAME"},
                    {"WarehouseName","WAREHOUSE_NAME"},
                    {"VehicleGroupName","VHCL_GRP_NAME"},
                    {"Code","FIX_ASSET_CODE"},
                    {"Name","FIX_ASSET_NAME"},
                    {"SerialNo","SERIAL_NO"},
                    {"Description","DESCRIPTION"},
                    {"Unit","UNIT"},
                    {"RestockReason","FIX_ASSET_RESTOCK_REASON"},
                    {"StatusName","FIX_ASSET_STATUS_LOOKVAL"}
                };
            SetMapper(dMapper);
        }

        public FixAssetInventoryListModel()
        {
        }

        public int FixAssetInventoryId { get; set; }

        [Display(Name = "FixAssetInventory_Display_IsPartOriginal", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsPartOriginal { get; set; }

        public int? EquipmentTypeId { get; set; }
        [Display(Name = "FixAssetInventory_Display_EquipmentType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EquipmentTypeName { get; set; }
        
        public int? PartId { get; set; }
        [Display(Name = "FixAssetInventory_Display_Part", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "PartStockReport_Part_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

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

        public int? StatusId { get; set; }
        [Display(Name = "FixAssetInventory_Display_Status", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StatusName { get; set; }
    }
}
