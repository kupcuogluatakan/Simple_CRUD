using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class GuaranteePartsListModel : BaseListWithPagingModel
    {
        public GuaranteePartsListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"Id", "APPOINTMENT_INDICATOR_CONTENT_ID"},
                     {"AppointIndicId", "APPOINTMENT_INDICATOR_ID"},
                     {"PartName", "PART_NAME"},
                     {"Quantity", "QUANTITY"},
                     {"ListPrice", "LIST_PRICE"}
                 };
            SetMapper(dMapper);

        }

        public GuaranteePartsListModel()
        {
        }

        public Int64 Id { get; set; }

        public Int64 GuaranteeId { get; set; }

        public int GuaranteeSeq { get; set; }

        public int PartId { get; set; }

        [Display(Name = "GuaranteeParts_Display_PartNameCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCodeName { get; set; }

        [Display(Name = "GuaranteeParts_Display_PartSerial", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SerialNo { get; set; }
        /// <summary>
        /// Dismantle Part Id
        /// </summary>
        [UIHint("RemovalPartListEditor")]
        [Display(Name = "GuaranteeParts_Display_DisPartNameCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Value { get; set; }
        /// <summary>
        /// Dismantle PartName / PartCode
        /// </summary>
        [UIHint("RemovalPartListEditor")]
        [Display(Name = "GuaranteeParts_Display_DisPartNameCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Text { get; set; }

        [Display(Name = "Global_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }

        [Display(Name = "GuaranteeParts_Display_DisPartSerial", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DisSerialNo { get; set; }

        [Display(Name = "GuaranteeParts_Display_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockType { get; set; }

        [Display(Name = "GuaranteeParts_Display_Ratio",ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Ratio { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_WarrantyUnitPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal WarrantyPrice { get; set; }

        [Display(Name = "GuaranteeRequestApprove_Display_WarrantyTotal", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal WarrantyTotal { get; set; }
        [Display(Name = "GuaranteeRequestApprove_Display_PartCount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int PartCount { get; set; }
    }
}
