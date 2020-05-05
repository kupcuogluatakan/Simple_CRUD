using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.ClaimSupplierPart
{
    public class ClaimSupplierPartListModel : BaseListWithPagingModel
    {
        public ClaimSupplierPartListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"SupplierCode", "SUPPLIER_CODE"},
                    {"SupplierName","SUPPLIER_NAME"},
                    {"PartCode", "PART_CODE"},
                    {"PartName", "ADMIN_DESC"},
                    {"IsActiveName","IS_ACTIVE"}
                };
            SetMapper(dMapper);
        }

        public ClaimSupplierPartListModel()
        {
        }

        [Display(Name = "ClaimSupplierdPart_Display_ClaimRecallPeriodId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int ClaimRecallPeriodId { get; set; }

        [Display(Name = "ClaimSupplierPart_Display_SupplierCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SupplierCode { get; set; }

        [Display(Name = "ClaimSupplierPart_Display_SupplierName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SupplierName { get; set; }

        public int? PartId { get; set; }
        [Display(Name = "ClaimSupplierPart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "ClaimSupplierPart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        [Display(Name = "ClaimRecallPeriodPart_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? CreateDate { get; set; }
    }
}
