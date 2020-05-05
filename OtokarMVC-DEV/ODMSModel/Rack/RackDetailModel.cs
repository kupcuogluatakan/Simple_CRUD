using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using FluentValidation.Attributes;

namespace ODMSModel.Rack
{
    [Validator(typeof(RackDetailModelValidator))]
    public class RackDetailModel : ModelBase
    {
        public int Id { get; set; }

        public int? WarehouseId { get; set; }

        [Display(Name = "Warehouse_Display_Code", ResourceType = typeof(MessageResource))]
        public string WarehouseCode { get; set; }

        [Display(Name = "Warehouse_Display_Name", ResourceType = typeof(MessageResource))]
        public string WarehouseName { get; set; }

        [Display(Name = "Rack_Display_Code", ResourceType = typeof(MessageResource))]
        public string Code { get; set; }

        [Display(Name = "Rack_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public string IsActiveString
        {
            get
            {
                if (IsActive)
                    return MessageResource.Global_Display_Active;
                return MessageResource.Global_Display_Passive;
            }
        }

        public bool HideFormElements { get; set; }

        public bool HaveStockRackDetail { get; set; }
    }
}
