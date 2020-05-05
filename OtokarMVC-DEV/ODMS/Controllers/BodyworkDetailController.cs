using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.BodyworkDetail;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class BodyworkDetailController : ControllerBase
    {
        //
        // GET: /BodyworkDetail/
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.BodyworkDetail.BodyworkDetailIndex)]
        public ActionResult BodyworkDetailIndex()
        {
            return View();
        }
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.BodyworkDetail.BodyworkDetailIndex)]
        public ActionResult ListBodyworkDetail([DataSourceRequest] DataSourceRequest request,
            BodyworkDetailListModel bModel)
        {
            var bl = new BodyworkDetailBL();
            var model = new BodyworkDetailListModel(request) { BodyworkCode = bModel.BodyworkCode };
            int totalCount = 0;

            var rValue = bl.GetBodyworkDetailList(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.BodyworkDetail.BodyworkDetailIndex, ODMSCommon.CommonValues.PermissionCodes.BodyworkDetail.BodyworkDetailCreate)]
        public ActionResult BodyworkDetailCreate(string id)
        {
            SetComboBox();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.BodyworkDetail.BodyworkDetailIndex, ODMSCommon.CommonValues.PermissionCodes.BodyworkDetail.BodyworkDetailCreate)]
        public ActionResult BodyworkDetailCreate(BodyworkDetailViewModel model)
        {
            SetComboBox();
            if (!ModelState.IsValid)
                return View(model);
            var bl = new BodyworkDetailBL();
            model.CommandType = CommonValues.DMLType.Insert;

            bl.DMLBodyworkDetail(UserManager.UserInfo, model);
            if (CheckErrorForMessage(model, true))
                return View(model);
            ModelState.Clear();

            return View(new BodyworkDetailViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.BodyworkDetail.BodyworkDetailIndex, ODMSCommon.CommonValues.PermissionCodes.BodyworkDetail.BodyworkDetailDelete)]
        public ActionResult BodyworkDetailDelete(string id)
        {
            var bl = new BodyworkDetailBL();
            var model = new BodyworkDetailViewModel();
            model.BodyworkCode = id;
            model.CommandType = CommonValues.DMLType.Delete;

            bl.DMLBodyworkDetail(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.BodyworkDetail.BodyworkDetailIndex, ODMSCommon.CommonValues.PermissionCodes.BodyworkDetail.BodyworkDetailUpdate)]
        public ActionResult BodyworkDetailUpdate(string id)
        {
            var bl = new BodyworkDetailBL();
            var model = new BodyworkDetailViewModel { BodyworkCode = id };

            bl.GetBodyworkDetail(model);

            SetComboBox();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.BodyworkDetail.BodyworkDetailIndex, ODMSCommon.CommonValues.PermissionCodes.BodyworkDetail.BodyworkDetailUpdate)]
        public ActionResult BodyworkDetailUpdate(BodyworkDetailViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var bl = new BodyworkDetailBL();
            model.CommandType = CommonValues.DMLType.Update;

            bl.DMLBodyworkDetail(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);

            return View(model);
        }

        private void SetComboBox()
        {
            ViewBag.SLCountry = CommonBL.ListCountries(UserManager.UserInfo).Data;

        }
        [HttpGet]
        public JsonResult ListCity(int id)
        {
            return Json(CommonBL.ListCities(UserManager.UserInfo, id).Data, JsonRequestBehavior.AllowGet);
        }
    }
}
