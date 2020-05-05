using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSModel.EducationType;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class EducationTypeController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeIndex)]
        public ActionResult EducationTypeIndex()
        {
            return View();
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeIndex, ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeCreate)]
        public ActionResult EducationTypeCreate()
        {
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeIndex, ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeCreate)]
        public ActionResult EducationTypeCreate(EducationTypeDetailModel model)
        {
            if (ModelState.IsValid)
            {
                var bo = new EducationTypeBL();
                model.CommandType = ODMSCommon.CommonValues.DMLType.Insert;
                bo.DMLEducationType(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();
                return View();
            }
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeIndex, ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeUpdate)]
        public ActionResult EducationTypeUpdate(int id = 0)
        {
            var referenceModel = new EducationTypeDetailModel();
            if (id > 0)
            {
                var bo = new EducationTypeBL();
                referenceModel.Id = id;
                referenceModel = bo.GetEducationType(UserManager.UserInfo, referenceModel).Model;
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeIndex, ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeUpdate)]
        public ActionResult EducationTypeUpdate(EducationTypeDetailModel viewModel)
        {
            var bo = new EducationTypeBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = ODMSCommon.CommonValues.DMLType.Update;
                bo.DMLEducationType(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeIndex, ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeDelete)]
        public ActionResult EducationTypeDelete(int id)
        {
            ViewBag.HideElements = false;

            var bo = new EducationTypeBL();
            var model = new EducationTypeDetailModel {Id = id, CommandType = ODMSCommon.CommonValues.DMLType.Delete};
            bo.DMLEducationType(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeIndex, ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeDetails)]
        public ActionResult EducationTypeDetails(int id)
        {
            var referenceModel = new EducationTypeDetailModel { Id = id };
            var bo = new EducationTypeBL();

            var model = bo.GetEducationType(UserManager.UserInfo, referenceModel).Model;

            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeIndex, ODMSCommon.CommonValues.PermissionCodes.EducationType.EducationTypeDetails)]
        public ActionResult ListEducationTypes([DataSourceRequest]DataSourceRequest request, EducationTypeListModel model)
        {
            var bo = new EducationTypeBL();
            var referenceModel = new EducationTypeListModel(request) { Name = model.Name };
            int totalCnt;
            var returnValue = bo.ListEducationTypes(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }


    }
}
