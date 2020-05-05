using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.Rack
{
    public class RackListModel : BaseListWithPagingModel
    {
        public int Id { get; set; }

        public int WarehouseId { get; set; }

        [Display(Name = "Warehouse_Display_Name", ResourceType = typeof(MessageResource))]
        public string WarehouseName { get; set; }

        [Display(Name = "Warehouse_Display_Code", ResourceType = typeof(MessageResource))]
        public string WarehouseCode { get; set; }

        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(MessageResource))]
        public string DealerId { get; set; }
        
        [Display(Name = "Rack_Display_Code", ResourceType = typeof(MessageResource))]
        public string Code { get; set; }
        
        [Display(Name = "Rack_Display_Name", ResourceType = typeof(MessageResource))]
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

        public RackListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"Id", "ID_RACK"},
                     {"WarehouseId", "ID_WAREHOUSE"},
                     {"WarehouseCode", "WAREHOUSE_CODE"},
                     {"Code", "RACK_CODE"},
                     {"Name", "RACK_NAME"},
                     {"IsActiveString", "IS_ACTIVE"}
                 };
            SetMapper(dMapper);
        }

        public RackListModel() { }

    }
}
