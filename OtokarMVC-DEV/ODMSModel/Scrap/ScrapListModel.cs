using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.Scrap
{
    public class ScrapListModel : BaseListWithPagingModel
    {
        [Display(Name = "Scrap_Display_ScrapId", ResourceType = typeof(MessageResource))]
        public int ScrapId { get; set; }
        
        [Display(Name = "Scrap_Display_StartDate", ResourceType = typeof(MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Scrap_Display_EndDate", ResourceType = typeof(MessageResource))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Scrap_Display_PartName", ResourceType = typeof(MessageResource))]
        public int? ScrapPartId { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "Scrap_Display_StockTypeName", ResourceType = typeof(MessageResource))]
        public int? ScrapStockTypeId { get; set; }

        public int? ScrapDealerId { get; set; }
        [Display(Name = "Scrap_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string ScrapDealerName { get; set; }

        [Display(Name = "Scrap_Display_ScrapDate", ResourceType = typeof(MessageResource))]
        public DateTime ScrapDate { get; set; }

        public int? DocId { get; set; }
        [Display(Name = "Scrap_Display_DocName", ResourceType = typeof(MessageResource))]
        public string DocName { get; set; }

        [Display(Name = "Scrap_Display_ScrapReasonDesc", ResourceType = typeof(MessageResource))]
        public string ScrapReasonDesc { get; set; }

        public int ScrapReasonId { get; set; }
        [Display(Name = "Scrap_Display_ScrapReasonName", ResourceType = typeof(MessageResource))]
        public string ScrapReasonName { get; set; }

        public ScrapListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"ScrapId", "SM.ID_SCRAP"},
                     {"ScrapDealerName", "D.DEALER_NAME"},
                     {"ScrapDate", "SM.SCRAP_DATE"},
                     {"DocName", "DO.DOC_NAME"},
                     {"ScrapReasonDesc", "SM.SCRAP_REASON_DESC"}
                 };
            SetMapper(dMapper);
        }

        public ScrapListModel() { }
    }
}
