using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.StockTypeDetail;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class StockTypeDetailController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.StockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
        }

        #region StockTypeDetail Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.StockTypeDetail.StockTypeDetailIndex)]
        [HttpGet]
        public ActionResult StockTypeDetailIndex(int? dealerId, int? id)
        {
            SetDefaults();
            StockTypeDetailListModel model = new StockTypeDetailListModel();
            if (dealerId != null && id != null)
            {
                model.IdDealer = dealerId;
                model.IdPart = id;
            }
            else
            {
                if (UserManager.UserInfo.GetUserDealerId() != 0)
                {
                    model.IdDealer = UserManager.UserInfo.GetUserDealerId();
                }
            }
            return PartialView(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.StockTypeDetail.StockTypeDetailIndex, ODMSCommon.CommonValues.PermissionCodes.StockTypeDetail.StockTypeDetailIndex)]
        public ActionResult ListStockTypeDetail([DataSourceRequest] DataSourceRequest request, StockTypeDetailListModel model)
        {
            var stockTypeDetailBo = new StockTypeDetailBL();

            var v = new StockTypeDetailListModel(request) {IdDealer = model.IdDealer, IdPart = model.IdPart};

            var totalCnt = 0;
            var returnValue = stockTypeDetailBo.ListStockTypeDetail(UserManager.UserInfo,v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion


        #region Dealer StockTypeDetail Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.StockTypeDetail.StockTypeDetailIndex)]
        [HttpGet]
        public ActionResult DealerStockTypeDetailIndex()
        {
            SetDefaults();
            StockTypeDetailListModel model = new StockTypeDetailListModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();

            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.StockTypeDetail.StockTypeDetailIndex, ODMSCommon.CommonValues.PermissionCodes.StockTypeDetail.StockTypeDetailIndex)]
        public ActionResult ListDealerStockTypeDetail([DataSourceRequest] DataSourceRequest request, StockTypeDetailListModel model)
        {
            var stockTypeDetailBo = new StockTypeDetailBL();

            var v = new StockTypeDetailListModel(request);
            v.PartName = model.PartName;
            v.PartCode = model.PartCode;
            v.IdStockType = model.IdStockType;
            v.IdDealer = model.IdDealer;
            v.DealerName = model.DealerName;
            v.PageSize = 30;

            var totalCnt = 0;
            var returnValue = stockTypeDetailBo.ListStockTypeDetail(UserManager.UserInfo,v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion
    }
}