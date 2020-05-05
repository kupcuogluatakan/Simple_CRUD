using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.DealerGuaranteeRatio
{
    public class DealerGuaranteeRatioListModel : BaseListWithPagingModel
    {
        public DealerGuaranteeRatioListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
             var dMapper = new Dictionary<string, string>
                 {
                     {"IdDealer", "ID_DEALER"},
                     {"DealerName", "DEALER_SHRT_NAME"},
                     {"DealerSSID","DEALER_SSID"},
                     {"GuaranteeRatio", "GUARANTEE_RATIO"}
                 };
            SetMapper(dMapper);

        }

        public DealerGuaranteeRatioListModel()
        {
        }

        [Display(Name = "DealerGuaranteeRatio_Display_IdDealer", ResourceType = typeof(MessageResource))]
        public int? IdDealer { get; set; }

        [Display(Name = "DealerGuaranteeRatio_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "DealerGuaranteeRatio_Display_GuaranteeRatio", ResourceType = typeof(MessageResource))]
        public decimal? GuaranteeRatio { get; set; }

        [Display(Name = "DealerGuaranteeRatio_Display_DealerSSID", ResourceType = typeof(MessageResource))]
        public string DealerSSID { get; set; }
    }
}
