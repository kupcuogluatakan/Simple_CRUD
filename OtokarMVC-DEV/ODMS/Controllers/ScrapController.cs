using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.ScrapDetail;
using ODMSModel.Scrap;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ScrapController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.StockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
        }

        #region Scrap Index

        [AuthorizationFilter(CommonValues.PermissionCodes.Scrap.ScrapIndex)]
        [HttpGet]
        public ActionResult ScrapIndex(int? scrapId)
        {
            SetDefaults();
            ScrapListModel model = new ScrapListModel();
            if (scrapId != null && scrapId != 0)
            {
                model.ScrapId = scrapId.GetValue<int>();
            }
            if (UserManager.UserInfo.GetUserDealerId() != 0)
            {
                model.ScrapDealerId = UserManager.UserInfo.GetUserDealerId();
            }

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Scrap.ScrapIndex, CommonValues.PermissionCodes.Scrap.ScrapIndex)]
        public ActionResult ListScrap([DataSourceRequest] DataSourceRequest request, ScrapListModel model)
        {
            var scrapBo = new ScrapBL();

            var v = new ScrapListModel(request)
                {
                    ScrapDealerId = model.ScrapDealerId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    PartCode = model.PartCode,
                    PartName = model.PartName,
                    ScrapStockTypeId = (int) CommonValues.StockType.Bedelli
                };

            var totalCnt = 0;
            var returnValue = scrapBo.ListScrap(UserManager.UserInfo,v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion
        
        #region Scrap Delete

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Scrap.ScrapIndex, CommonValues.PermissionCodes.Scrap.ScrapDelete)]
        public ActionResult DeleteScrap(int scrapId)
        {
            ScrapViewModel viewModel = new ScrapViewModel();
            viewModel.ScrapId = scrapId;

            var scrapBo = new ScrapBL();
            // scrap_det kayıtlarından hiçbirinin cancel ve confirm stock transaction id değeri dolu değilse kayıt silinebilir.
            ScrapDetailBL detailBo = new ScrapDetailBL();
            int count = 0;
            int nonTransactionCount = 0;
            ScrapDetailListModel detailModel = new ScrapDetailListModel();
            detailModel.ScrapId = scrapId;
            List<ScrapDetailListModel> listModel = detailBo.ListScrapDetail(UserManager.UserInfo,detailModel, out count).Data;

            if (count != 0)
            {
                var nonTransactionalRows = (from r in listModel.AsEnumerable()
                                            where
                                                r.CancelIdStockTransaction != null ||
                                                r.ConfirmIdStockTransaction != null
                                            select r);
                nonTransactionCount = nonTransactionalRows.Count();
            }
            if (count == 0 || nonTransactionCount == 0)
            {
                scrapBo.GetScrap(UserManager.UserInfo,viewModel);

                viewModel.CommandType = CommonValues.DMLType.Delete;
                scrapBo.DMLScrap(UserManager.UserInfo,viewModel);

                ModelState.Clear();
                if (viewModel.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                        MessageResource.Global_Display_Success);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                    viewModel.ErrorMessage);
            }
            else
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                    MessageResource.Scrap_Error_TransactionDetailExists);
            }
        }

        #endregion
    }
}