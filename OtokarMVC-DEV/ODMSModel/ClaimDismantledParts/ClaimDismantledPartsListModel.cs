using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.ClaimDismantledParts
{
    public class ClaimDismantledPartsListModel : BaseListWithPagingModel
    {
         public ClaimDismantledPartsListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PartName", "SPL.PART_NAME"},
                    {"DismantledPartName", "DSPL.PART_NAME"},
                    {"Quantity", "QUANTITY"},
                    {"DismantledPartSerialNo", "DISMANTLED_PART_SERIAL_NO"},
                    {"Barcode","BARCODE"},
                    {"FirmActionId", "FIRM_ACTION_LOOKVAL"},
                    {"FirmActionDate", "FIRM_ACTION_DATE"},
                    {"FirmActionName", "FIRM_ACTION_LOOKVAL"},
                    {"FirmActionExplanation", "FIRM_EXPLANATION"},
                    {"RackCode","RACK_CODE"}
                };
            SetMapper(dMapper);
        }

         public ClaimDismantledPartsListModel()
        {
        }

        public int ClaimDismantledPartId { get; set; }
        public int ClaimWaybillId { get; set; }

        public int PartId { get; set; }
        [Display(Name = "ClaimDismantledParts_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

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

        [Display(Name = "ClaimDismantledParts_Display_FirmActionName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int FirmActionId { get; set; }
        [Display(Name = "ClaimDismantledParts_Display_FirmActionName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FirmActionName { get; set; }

        [Display(Name = "ClaimDismantledParts_Display_FirmActionExplanation", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FirmActionExplanation { get; set; }

        [Display(Name = "ClaimDismantledParts_Display_RackCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RackCode { get; set; }

        public bool IsSelected { get; set; }
    }
}
