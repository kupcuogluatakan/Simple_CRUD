using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.FixAssetInventoryOutput
{
    [Validator(typeof(FixAssetInventoryOutputViewModelValidator))]
    public class FixAssetInventoryOutputViewModel : ModelBase
    {
        public FixAssetInventoryOutputViewModel()
        {
        }

        public int? IdFixAssetInventory { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_FixAssetName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FixAssetName { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_SerialNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SerialNo { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_ExitDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ExitDesc { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? StockType { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeName { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_ExitDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? ExitDate { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_FixAssetStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? FixAssetStatus { get; set; }
        public string FixAssetStatusName { get; set; }

        public string FixAssetCode { get; set; }

        public Int64? IdPart { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_RestockReason", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RestockReason { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_IdWarehouse", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdWarehouse { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_IdRack", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdRack { get; set; }

        public int? IdDealer { get; set; }

        public bool SubmitFinished { get; set; }
    }
}
