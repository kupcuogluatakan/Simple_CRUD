using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.StockCardTransaction;

namespace ODMS.Controllers
{
    public class StockCardTransactionController : ControllerBase
    {
        private void SetDefaultValues()
        {
            ViewBag.StockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.TransactionTypeList = CommonBL.ListLookup(UserManager.UserInfo,CommonValues.LookupKeys.TransactionType).Data;
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.StockCardTransaction.StockCardTransactionIndex)]
        public ActionResult StockCardTransactionIndex(int partId, int dealerId)
        {
            SetDefaultValues();
            return PartialView(new StockCardTransactionListModel() {PartId = partId, DealerId = dealerId});
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockCardTransaction.StockCardTransactionIndex)]
        public ActionResult ListStockCardTransaction([DataSourceRequest]DataSourceRequest request,StockCardTransactionListModel hModel)
        {
            SetDefaultValues();
            var bl = new StockCardTransactionBL();
            var model = new StockCardTransactionListModel(request);
            int totalCount = 0;

            model.PartId = hModel.PartId;
            model.DealerId = hModel.DealerId;
            model.TransactionType = hModel.TransactionType;
            model.StockTypeId = hModel.StockTypeId;
            model.StartDate = hModel.StartDate;
            model.EndDate = hModel.EndDate;

            var rValue = bl.ListStockCardTransaction(UserManager.UserInfo,model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

    }
}
