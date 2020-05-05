using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.DealerRegionResponsible
{
    public class DealerRegionResponsibleListModel : BaseListWithPagingModel
    {
        public int? DealerRegionId { get; set; }

        public int UserId { get; set; }

        [Display(Name = "DealerRegion_Display_Name", ResourceType = typeof(MessageResource))]
        public string DealerRegionName { get; set; }

        [Display(Name = "DealerRegionResponsible_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }

        [Display(Name = "DealerRegionResponsible_Display_Surname", ResourceType = typeof(MessageResource))]
        public string Surname { get; set; }

        [Display(Name = "User_Display_Phone", ResourceType = typeof(MessageResource))]
        public string Phone { get; set; }

        [Display(Name = "User_Display_EMail", ResourceType = typeof(MessageResource))]
        public string Email { get; set; }

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


        public DealerRegionResponsibleListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"DealerRegionId", "ID_DEALER_REGION"},
                     {"UserId", "ID_DMS_USER"},
                     {"DealerRegionName", "DEALER_REGION_NAME"},
                     {"Name", "DU.CUSTOMER_NAME"},
                     {"Surname", "USER_LAST_NAME"},
                     {"Phone", "PHONE"},
                     {"Email", "EMAIL"},
                     {"IsActive", "IS_ACTIVE"}
                 };
            SetMapper(dMapper);
        }

        public DealerRegionResponsibleListModel() { }
    }
}
