using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.CycleCountResult
{
    public class CycleCountResultAuditViewModel : ModelBase
    {
        public int CycleCountId { get; set; }
        public int StockCardId { get; set; }
        public int WarehouseId { get; set; }

        [Display(Name = "CycleCountResultAudit_Display_WarehouseName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarehouseName { get; set; }

        [Display(Name = "CycleCountResultAudit_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "CycleCountResultAudit_Display_CurrentQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ExpectedQty { get; set; }//sayım sonrası

        [Display(Name = "CycleCountResultAudit_Display_DistDiff", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CurrentQty { get; set; }

        [Display(Name = "CycleCountResultAudit_Display_DiffQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DiffQty { get; set; }

        [Display(Name = "CycleCountResultAudit_Display_BfrQty", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal BfrQty { get; set; }//sayım öncesi

        public new int Status { get; set; }
        public int? RackId { get; set; }
        public string RackName { get; set; }
        public int PartId { get; set; }
        public string NewQtyValues { get; set; }
        public List<CycleCountResultPrototypeViewModel> CycleCountAuditList { get; set; }
    }

    public class CycleCountResultPrototypeViewModel
    {
        public string StockName { get; set; }
        public decimal BeforeQty { get; set; }
        public decimal AfterQty { get; set; }
        public int StockTypeId { get; set; }
        public bool CycleCountDiffAllow { get; set; }
    }
}
