using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.StockCardChangePart;

namespace ODMS.Controllers
{
    public class StockCardChangePartController : ControllerBase
    {
        [AuthorizationFilter(CommonValues.PermissionCodes.StockCardChangePart.StockCardChangePartIndex)]
        public ActionResult StockCardChangePartIndex(int partId)
        {
            var bl = new StockCardChangePartBL();
            var model = new StockCardChangePartListModel() { PartId = partId };

            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockCardChangePart.StockCardChangePartIndex)]
        public ActionResult ListStockCardChangePart([DataSourceRequest]DataSourceRequest request, StockCardChangePartListModel hModel)
        {
            var bl = new StockCardChangePartBL();
            var model = new StockCardChangePartListModel(request) {PartId = hModel.PartId};
            var totalCount = 0;

            var rValue = bl.ListStockCardChangePart(model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }
    }
}
