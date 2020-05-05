using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.Warehouse
{
    [Validator(typeof(WarehouseDetailModelValidator))]
    public class WarehouseDetailModel : ModelBase
    {
        public int Id { get; set; }

        [Display(Name = "Warehouse_Display_Code", ResourceType = typeof(MessageResource))]
        public string Code { get; set; }

        [Display(Name = "Warehouse_Display_Name", ResourceType = typeof(MessageResource))]
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

        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(MessageResource))]
        public int? DealerId { get; set; }

        public bool HideFormElements { get; set; }

        [Display(Name = "Warehouse_Display_StorageType", ResourceType = typeof(MessageResource))]
        public int? StorageType { get; set; }

        [Display(Name = "Warehouse_Display_StorageType", ResourceType = typeof(MessageResource))]
        public string StorageTypeName { get; set; }
    }
}
