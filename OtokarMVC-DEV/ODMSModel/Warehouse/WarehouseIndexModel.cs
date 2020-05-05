using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ODMSCommon.Resources;

namespace ODMSModel.Warehouse
{
    public class WarehouseIndexModel : IndexModelBase
    {
        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? DealerId { get; set; }

        public List<SelectListItem> DealerList { get; set; }
        
        [Display(Name = "Warehouse_Display_Code", ResourceType = typeof(MessageResource))]
        public string Code { get; set; }

        [Display(Name = "Warehouse_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public bool? IsActive { get; set; }
        
        public bool IsDealerUser
        {
            get { return DealerId != -1; }
        }

        public WarehouseIndexModel()
        {
            //DealerId = -1;
            DealerList = new List<SelectListItem>();
        }

        [Display(Name = "Warehouse_Display_StorageType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? StorageType { get; set; }
    }
}
