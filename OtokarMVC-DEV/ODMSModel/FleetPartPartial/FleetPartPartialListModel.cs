using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System.Collections.Generic;

namespace ODMSModel.FleetPartPartial
{
    public class FleetPartPartialListModel : BaseListWithPagingModel
    {
        public FleetPartPartialListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"FleetId", "FleetId"},                    
                     {"PartName", "PartName"},
                     {"PartCode", "PartCode"},
                     {"PartId", "PartId"}
                 };
            SetMapper(dMapper);
        }

        public FleetPartPartialListModel()
        {
            
        }

        public int FleetId { get; set; }

        public int PartId { get; set; }

        [Display(Name = "FleetPartViewModel_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "FleetPartViewModel_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
    }
}
