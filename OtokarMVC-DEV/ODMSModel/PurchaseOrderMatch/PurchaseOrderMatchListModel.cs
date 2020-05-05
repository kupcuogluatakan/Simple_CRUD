using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.PurchaseOrderMatch
{
    public class PurchaseOrderMatchListModel : BaseListWithPagingModel
    {
        public PurchaseOrderMatchListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"PurhcaseOrderGroupName", "GROUP_NAME"},
                    {"PurhcaseOrderTypeName", "PURCHASE_ORDER_TYPE_NAME"},
                    {"SalesOrganization", "SALES_ORGANIZATION"},
                    {"DistrChan", "DISTR_CHAN"},
                    {"Division", "DIVISION"},
                    {"StateName", "IS_ACTIVE"}

                };
            SetMapper(dMapper);
        }

        public PurchaseOrderMatchListModel()
        {
        }

        [Display(Name = "PurchaseOrderMatch_Display_Purhcase_Order_Group_Name", ResourceType = typeof(MessageResource))]
        public int PurhcaseOrderGroupId { get; set; }

        [Display(Name = "PurchaseOrderMatch_Display_Purhcase_Order_Type_Name", ResourceType = typeof(MessageResource))]
        public int PurhcaseOrderTypeId { get; set; }

        [Display(Name = "PurchaseOrderMatch_Display_Purhcase_Order_Group_Name", ResourceType = typeof(MessageResource))]
        public string PurhcaseOrderGroupName { get; set; }

        [Display(Name = "PurchaseOrderMatch_Display_Purhcase_Order_Type_Name", ResourceType = typeof(MessageResource))]
        public string PurhcaseOrderTypeName { get; set; }

        [Display(Name = "PurchaseOrderMatch_Display_Sales_Organization", ResourceType = typeof(MessageResource))]
        public string SalesOrganization { get; set; }

        [Display(Name = "PurchaseOrderMatch_Display_DistrChan", ResourceType = typeof(MessageResource))]
        public string DistrChan { get; set; }

        [Display(Name = "PurchaseOrderMatch_Display_Division", ResourceType = typeof(MessageResource))]
        public string Division { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string StateName { get; set; }

    }
}
