using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.SparePartGuaranteeAuthorityNeed
{
    public class SparePartGuaranteeAuthorityNeedListModel : BaseListWithPagingModel
    {
        public SparePartGuaranteeAuthorityNeedListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PartCode", "PART_CODE"},
                    {"PartName", "PART_NAME"},
                    {"GuaranteeAuthorityNeedName","GUARANTEE_AUTHORITY_NEED"}
                };
            SetMapper(dMapper);
        }

        public SparePartGuaranteeAuthorityNeedListModel()
        {
        }

        public int PartId { get; set; }

        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "SparePart_Display_PartName",
            ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }
        
        [Display(Name = "SparePart_Display_GuaranteeAuthorityNeed",
            ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public bool? GuaranteeAuthorityNeed { get; set; }
        [Display(Name = "SparePart_Display_GuaranteeAuthorityNeedName",
            ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GuaranteeAuthorityNeedName { get; set; }

        public int GuaranteeAuthorityNeedSearch { get; set; }
    }
}
