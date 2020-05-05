using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.StockCardPurchaseOrder;

namespace ODMS.Controllers
{
    public class StockCardPurchaseOrderController : ControllerBase
    {
        //
        // GET: /StockCardPurchaseOrder/
        [AuthorizationFilter(CommonValues.PermissionCodes.StockCardPurchaseOrder.StockCardPurchaseOrderIndex)]
        public ActionResult StockCardPurchaseOrderIndex(int dealerId,int partId)
        {
            var model = new StockCardPurchaseOrderListModel(){DealerId = dealerId, PartId = partId};
            SetComboBox();
            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockCardPurchaseOrder.StockCardPurchaseOrderIndex)]
        public ActionResult ListStockCardPurchaseOrder([DataSourceRequest]DataSourceRequest request, StockCardPurchaseOrderListModel hModel)
        {
            var bl = new StockCardPurchaseOrderBL();
            var model = new StockCardPurchaseOrderListModel(request)
            {
                DealerId  = hModel.DealerId,
                PartId    = hModel.PartId,
                PoNumber  = hModel.PoNumber,
                StartDate = hModel.StartDate,
                EndDate   = hModel.EndDate,
                StatusId  = hModel.StatusId
            };
            string errorDesc = string.Empty;
            int totalCount = 0;
            int errorCode = 0;

            var rValue = bl.ListStockCardPurchaseOrder(UserManager.UserInfo,model, out totalCount, out errorCode, out errorDesc).Data;
            
            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        private void SetComboBox()
        {
            ViewBag.SLStatus = CommonBL.ListLookup(UserManager.UserInfo,"PO_STATUS").Data;
        }
    }
}
