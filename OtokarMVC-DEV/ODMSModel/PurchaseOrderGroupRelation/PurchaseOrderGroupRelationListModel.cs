using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.PurchaseOrderGroupRelation
{
    public class PurchaseOrderGroupRelationListModel : BaseListWithPagingModel
    {
        public PurchaseOrderGroupRelationListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"DealerId", "ID_DEALER"},
                    {"PurchaseOrderGroupId","ID_GROUP"}
                };
            SetMapper(dMapper);
        }

        public PurchaseOrderGroupRelationListModel()
        {
            
        }

        public int DealerId { get; set; }

        [Display(Name = "PurchaseOrderMatch_Display_Purhcase_Order_Group_Name", ResourceType = typeof(MessageResource))]
        public int PurchaseOrderGroupId { get; set; }

        public string DealerName { get; set; }

        public string GroupName { get; set; }
    }
}
