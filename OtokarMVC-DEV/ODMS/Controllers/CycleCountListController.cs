using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.CycleCount;
using ODMSCommon.Security;
using System.Collections.Generic;

namespace ODMS.Controllers
{
    public class CycleCountListController : ControllerBase
    {
        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountList.CycleCountListIndex)]
        public ActionResult CycleCountListIndex()
        {
            ViewBag.StatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.CycleCountLookupStatus).Data;
            
            //StockTypeLists
            List<SelectListItem> stockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.StockTypeList = stockTypeList;

            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountList.CycleCountListIndex)]
        public ActionResult ListCycleCount([DataSourceRequest] DataSourceRequest request, CycleCountListModel model)
        {
            var cycleCountBL = new CycleCountBL();
            var listModel = new CycleCountListModel(request)
            {
                CycleCountName = model.CycleCountName,
                CycleCountStatus = model.CycleCountStatus,
                CycleCountId = model.CycleCountId,
                StockTypeId = model.StockTypeId
            };

            var totalCnt = 0;
            var returnValue = cycleCountBL.ListCycleCount(UserManager.UserInfo, listModel, out totalCnt).Data;

            foreach (var item in returnValue)
            {
                item.encryptedId = HttpUtility.UrlEncode(item.encryptedId);
            }

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
    }
}
