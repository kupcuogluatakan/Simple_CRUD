using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.DamagedItemDispose
{
    [Validator(typeof(DamagedItemDisposeViewModelValidator))]
    public class DamagedItemDisposeViewModel : ModelBase
    {
        public DamagedItemDisposeViewModel()
        {
        }
        public bool HideFormElements { get; set; }

        public int DamageDisposeId { get; set; }

        public int? WarehouseId { get; set; }
        [Display(Name = "DamagedItemDispose_Display_WarehouseName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarehouseName { get; set; }

        public int? DealerId { get; set; }
        [Display(Name = "DamagedItemDispose_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        public int? PartId { get; set; }
        [Display(Name = "DamagedItemDispose_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        public int? RackId { get; set; }
        [Display(Name = "DamagedItemDispose_Display_RackName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RackName { get; set; }

        public int? StockTypeId { get; set; }
        [Display(Name = "DamagedItemDispose_Display_StockTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeName { get; set; }
        
        public int? DocId { get; set; }
        [Display(Name = "DamagedItemDispose_Display_DocumentName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DocName { get; set; }

        [Display(Name = "DamagedItemDispose_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }

        [Display(Name = "DamagedItemDispose_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }

        public bool? IsOriginal { get; set; }
        [Display(Name = "DamagedItemDispose_Display_IsOriginalName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsOriginalName { get; set; }

        [Display(Name = "DamagedItemDispose_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? CreateDate { get; set; }
    }
}
