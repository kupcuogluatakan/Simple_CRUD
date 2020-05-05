using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.StockLocation;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class StockLocationController : ControllerBase
    {
        #region Member Varables

        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;          

            ViewBag.WarehouseList = WarehouseBL.ListWarehousesOfDealerAsSelectList(null).Data;
        }

        #endregion

        #region StockLocation Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.StockLocation.StockLocationIndex)]
        [HttpGet]
        public ActionResult StockLocationIndex()
        {
            SetDefaults();
            StockLocationListModel model = new StockLocationListModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();

            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.StockLocation.StockLocationIndex, ODMSCommon.CommonValues.PermissionCodes.StockLocation.StockLocationIndex)]
        public ActionResult ListStockLocation([DataSourceRequest] DataSourceRequest request, StockLocationListModel model)
        {
            var stockLocationBo = new StockLocationBL();

            var v = new StockLocationListModel(request)
                {
                    IdDealer = model.IdDealer,
                    IdPart = model.IdPart,
                    PartCode = model.PartCode,
                    PartName = model.PartName,
                    IdWarehouse = model.IdWarehouse
                };

            var totalCnt = 0;
            var returnValue = stockLocationBo.ListStockLocation(UserManager.UserInfo,v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion
    }
}