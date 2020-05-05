using System.ComponentModel.DataAnnotations;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ODMSModel.StockCardPriceListModel
{
    public class StockCardPriceListModel : BaseListWithPagingModel
    {
        public int StockCardId { get; set; }

        public int DealerId { get; set; }

        public int PartId { get; set; }

        public int PriceId { get; set; }       

        [Display(Name = "StockCardPriceListModel_Display_CompanyPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal CompanyPrice { get; set; }

        [Display(Name = "StockCardPriceListModel_Display_CostPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal CostPrice { get; set; }

        [Display(Name = "StockCardPriceListModel_Display_ListPrice", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ListPrice { get; set; }

        [Display(Name = "StockCardPriceListModel_Display_PriceList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public List<SelectListItem> PriceList { get; set; }
    }
}
