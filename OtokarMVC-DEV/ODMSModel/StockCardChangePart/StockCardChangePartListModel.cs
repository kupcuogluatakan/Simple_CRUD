using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.StockCardChangePart
{
    public class StockCardChangePartListModel : BaseListWithPagingModel
    {
        public StockCardChangePartListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"FirstPartCode","SP.PART_CODE"},
                    {"LastPartCode","SPL.PART_CODE"}
                };
            SetMapper(dMapper);
        }

        public StockCardChangePartListModel()
        {
        }

        public int PartId { get; set; }

        [Display(Name = "StockCardChangePart_Display_FirstPart", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FirstPartCode { get; set; }

        [Display(Name = "StockCardChangePart_Display_LastPart", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string LastPartCode { get; set; }
    }
}
