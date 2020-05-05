using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Business;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.FavoriteScreen;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class FavoriteScreenController : ControllerBase
    {

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.FavoriteScreen.FavoriteScreenIndex)]
        public ActionResult FavoriteScreenIndex()
        {
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.FavoriteScreen.FavoriteScreenIndex)]
        public ActionResult ListAllScreen()
        {
            return Json(new { Data = new FavoriteScreenBL().ListAllScreen(UserManager.UserInfo).Data });
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.FavoriteScreen.FavoriteScreenIndex)]
        public ActionResult ListFavoriteScreen()
        {
            return Json(new { Data = new FavoriteScreenBL().ListFavoriteScreen(UserManager.UserInfo).Data });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.FavoriteScreen.FavoriteScreenIndex)]
        public JsonResult Save(SaveModel model)
        {
            if (model.ScreenIdList == null)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                MessageResource.FavoriteScreen_RequiredFavorite);
            }

            var bo = new FavoriteScreenBL();
            bo.Save(UserManager.UserInfo, model);
            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }


    }
}