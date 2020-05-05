using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.ClaimSupplier
{
    public class ClaimSupplierListModel : BaseListWithPagingModel
    {
        public ClaimSupplierListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"SupplierCode", "SUPPLIER_CODE"},
                    {"SupplierName","SUPPLIER_NAME"},
                    {"ClaimRackCode", "CLAIM_RACK_CODE"}
                };
            SetMapper(dMapper);
        }

        public ClaimSupplierListModel()
        {
        }

        [Display(Name = "ClaimSupplier_Display_SupplierCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SupplierCode { get; set; }

        [Display(Name = "ClaimSupplier_Display_SupplierName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SupplierName { get; set; }

        [Display(Name = "ClaimSupplier_Display_ClaimRackCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ClaimRackCode { get; set; }
    }
}
