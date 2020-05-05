using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.OtokarStockSearch
{
    public class OtokarStockSearchViewModel : BaseListWithPagingModel
    {
        public OtokarStockSearchViewModel([DataSourceRequest] DataSourceRequest request):base(request)
        {

        }
        public OtokarStockSearchViewModel()
        {

        }


        [Display(Name = "PurchaseOrderDetail_Display_PartName",
               ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "PurchaseOrderDetail_Display_PartName",
            ResourceType = typeof(MessageResource))]
        public long? PartId { get; set; }
        [Display(Name = "Otokar_Stock_Search_WebServiceResult",
           ResourceType = typeof(MessageResource))]
        public string WebServiceResult { get; set; }
        [Display(Name = "StockCard_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCodeList { get; set; }

    }
}
