using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.DealerClass
{
    public class DealerClassListModel : BaseListWithPagingModel
    {
       public DealerClassListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"SSIdDealerClass", "SSID_DEALER_CLASS"},
                    {"DealerClassName","DEALER_CLASS_NAME"},
                    {"DealerClassCode","DEALER_CLASS_CODE"},
                    {"IsActive", "IS_ACTIVE"},
                    {"IsActiveName", "IS_ACTIVE_NAME"}
                };
            SetMapper(dMapper);
        }

        public DealerClassListModel()
        {
        }

        [Display(Name = "DealerClass_Display_DealerClassCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerClassCode { get; set; }
        
        //SSIdDealerClass
        [Display(Name = "DealerClass_Display_SSIdDealerClass", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SSIdDealerClass { get; set; }

        //DealerClassName
        [Display(Name = "DealerClass_Display_DealerClassName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerClassName { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool? IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }
    }
}
