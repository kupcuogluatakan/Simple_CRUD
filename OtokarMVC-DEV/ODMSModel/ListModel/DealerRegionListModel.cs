using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;

namespace ODMSModel.ListModel
{
    public class DealerRegionListModel : BaseListWithPagingModel
    {
        public DealerRegionListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"DealerRegionId", "ID_DEALER_REGION"},
                    {"DealerRegionName", "DEALER_REGION_NAME"}
                };
            SetMapper(dMapper);
        }

        public DealerRegionListModel()
        {
        }

        public int DealerRegionId { get; set; }

        [Display(Name = "DealerRegion_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerRegionName { get; set; }
    }
}
