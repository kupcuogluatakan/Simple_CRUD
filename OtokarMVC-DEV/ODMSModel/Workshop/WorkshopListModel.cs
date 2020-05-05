using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.Workshop
{
    public class WorkshopListModel : BaseListWithPagingModel
    {
        public int Id { get; set; }

        [Display(Name = "Workshop_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }
        
        public int DealerId { get; set; }

        [Display(Name = "Dealer_Display_Name", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool IsActive { get; set; }
        
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public new string IsActiveString
        {
            get
            {
                if (IsActive)
                    return MessageResource.Global_Display_Active;
                return MessageResource.Global_Display_Passive;
            }
        }

        public WorkshopListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"Id", "ID_WORKSHOP"},
                     {"Name", "WORKSHOP_NAME"},
                     {"DealerId", "ID_DEALER"},
                     {"DealerName", "DEALER_NAME"},
                     {"IsActiveString", "IS_ACTIVE"}
                 };
            SetMapper(dMapper);
        }

        public WorkshopListModel() { }
    }
}
