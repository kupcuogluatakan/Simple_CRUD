using System;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.ClaimDismantledParts
{
    public class ClaimDismantledPartsViewModel : ModelBase
    {

        public int ClaimDismantledPartId { get; set; }
        public int ClaimWaybillId { get; set; }

        public int PartId { get; set; }
        [Display(Name = "ClaimDismantledParts_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        public int DealerId { get; set; }
        public int WorkOrderDetailId { get; set; }
        public DateTime FirmActionDate { get; set; }
        public int ClaimRecallPeriodId { get; set; }
        public string SupplierCode { get; set; }
        public int GuaranteeId { get; set; }
        public int GuaranteeSeq { get; set; }
        public DateTime BarcodeFirstPrintDate { get; set; }
        public DateTime DealerScrapDate { get; set; }
        public bool IsApproved { get; set; }

        public int DismantledPartId { get; set; }
        [Display(Name = "ClaimDismantledParts_Display_DismantledPartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DismantledPartName { get; set; }

        [Display(Name = "ClaimDismantledParts_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }

        [Display(Name = "ClaimDismantledParts_Display_DismantledPartSerialNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DismantledPartSerialNo { get; set; }

        [Display(Name = "ClaimDismantledParts_Display_Barcode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Barcode { get; set; }

        [Display(Name = "ClaimDismantledParts_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime CreateDate { get; set; }

        public int FirmActionId { get; set; }
        [Display(Name = "ClaimDismantledParts_Display_FirmActionName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FirmActionName { get; set; }

        [Display(Name = "ClaimDismantledParts_Display_FirmActionExplanation", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FirmActionExplanation { get; set; }

        [Display(Name = "ClaimDismantledParts_Display_RackCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RackCode { get; set; }

        public string IdList { get; set; }
    }
}
