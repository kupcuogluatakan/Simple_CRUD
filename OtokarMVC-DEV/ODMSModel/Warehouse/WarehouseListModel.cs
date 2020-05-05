using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.Warehouse
{
    public class WarehouseListModel : BaseListWithPagingModel
    {
        public int Id { get; set; }

        [Display(Name = "Warehouse_Display_Code", ResourceType = typeof(MessageResource))]
        public string Code { get; set; }

        [Display(Name = "Warehouse_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public new string IsActiveString
        {
            get
            {
                if (IsActive)
                    return MessageResource.Global_Display_Active;
                return MessageResource.Global_Display_Passive;
            }
        }

        
        public int? DealerId { get; set; }

        public WarehouseListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"Id", "ID_WAREHOUSE"},
                     {"Name", "WAREHOUSE_NAME"},
                     {"Code", "WAREHOUSE_CODE"},
                     {"IsActiveString", "IS_ACTIVE"},
                     {"StorageTypeName", "STORAGE_NAME"}
                 };
            SetMapper(dMapper);
        }

        public WarehouseListModel() { }

        [Display(Name = "Warehouse_Display_StorageType", ResourceType = typeof(MessageResource))]
        public string StorageTypeName { get; set; }

        public int? StorageType { get; set; }
    }
}
