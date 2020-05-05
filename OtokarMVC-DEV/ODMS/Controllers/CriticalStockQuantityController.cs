using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.CriticalStockQuantity;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CriticalStockQuantityController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
        }

        #region CriticalStockQuantity Index

        [AuthorizationFilter(CommonValues.PermissionCodes.CriticalStockQuantity.CriticalStockQuantityIndex)]
        [HttpGet]
        public ActionResult CriticalStockQuantityIndex()
        {
            SetDefaults();
            CriticalStockQuantityListModel model = new CriticalStockQuantityListModel();
            if (UserManager.UserInfo.GetUserDealerId() > 0)
            {
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            }
            model.StockTypeId =
                CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.StockType).Model
                        .GetValue<int>();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CriticalStockQuantity.CriticalStockQuantityIndex, CommonValues.PermissionCodes.CriticalStockQuantity.CriticalStockQuantityIndex)]
        public ActionResult ListCriticalStockQuantity([DataSourceRequest] DataSourceRequest request, CriticalStockQuantityListModel model)
        {
            var stockTypeDetailBo = new CriticalStockQuantityBL();

            var v = new CriticalStockQuantityListModel(request)
                {
                    PartName = model.PartName,
                    PartCode = model.PartCode,
                    DealerId = model.DealerId,
                    StockTypeId = model.StockTypeId
                };

            var totalCnt = 0;
            var returnValue = stockTypeDetailBo.ListCriticalStockQuantity(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion
    }
}