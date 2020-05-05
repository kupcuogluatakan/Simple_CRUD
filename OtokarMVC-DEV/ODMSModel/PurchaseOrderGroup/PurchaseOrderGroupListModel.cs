using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.PurchaseOrderGroup
{
    public class PurchaseOrderGroupListModel : BaseListWithPagingModel
    {
        public PurchaseOrderGroupListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {                  
                    {"GroupName", "GROUP_NAME"},
                    {"StateName", "IS_ACTIVE"}
                };
            SetMapper(dMapper);
        }

        public PurchaseOrderGroupListModel()
        {
        }
       
        public int PurchaseOrderGroupId { get; set; }

        [Display(Name = "PurchaseOrderGroup_Display_GroupName", ResourceType = typeof(MessageResource))]
        public string GroupName { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string StateName { get; set; }
    }
}
