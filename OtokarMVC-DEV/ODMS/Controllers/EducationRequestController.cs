using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.EducationRequest;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class EducationRequestController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.EducationRequest.EducationRequestIndex)]
        public ActionResult EducationRequestIndex()
        {
            var bo = new EducationRequestBL();
            var model = bo.GetEducationRequestIndexModel(UserManager.UserInfo).Model;
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.EducationRequest.EducationRequestIndex, CommonValues.PermissionCodes.EducationRequest.EducationRequestCreate)]
        public ActionResult EducationRequestCreate()
        {
            var bo = new EducationRequestBL();
            ViewBag.EducationList = bo.GetEducationList(UserManager.UserInfo).Data;
            ViewBag.WorkerList = bo.GetWorkerList(UserManager.UserInfo).Data;
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.EducationRequest.EducationRequestIndex,
            CommonValues.PermissionCodes.EducationRequest.EducationRequestCreate)]
        public ActionResult EducationRequestCreate(EducationRequestDetailModel model)
        {
            var bo = new EducationRequestBL();
            ViewBag.EducationList = bo.GetEducationList(UserManager.UserInfo).Data;
            ViewBag.WorkerList = bo.GetWorkerList(UserManager.UserInfo).Data;

            if (string.IsNullOrEmpty(model.EducationCode))
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Education_Warning_EducationCode);
            }
            if (string.IsNullOrEmpty(model.SerializedWorkerIds))
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Education_Warning_Worker);
            }

            model.CommandType = CommonValues.DMLType.Insert;
            bo.SaveEducationRequest(UserManager.UserInfo, model);

            CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.EducationRequest.EducationRequestIndex, CommonValues.PermissionCodes.EducationRequest.EducationRequestDelete)]
        public ActionResult EducationRequestDelete(long id)
        {
            var model = new EducationRequestDetailModel { Id = id };
            if (ModelState.IsValid)
            {
                ViewBag.HideElements = false;
                var bo = new EducationRequestBL();
                model.CommandType = CommonValues.DMLType.Delete;
                bo.DeleteWorkerFromEducationRequest(model);
                //CheckErrorForMessage(model, true);
                ModelState.Clear();

                if (model.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            }
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.EducationRequest.EducationRequestIndex, CommonValues.PermissionCodes.EducationRequest.EducationRequestDetails)]
        public ActionResult EducationRequestDetails(long id)
        {
            var referenceModel = new EducationRequestDetailModel { Id = id };
            var bo = new EducationRequestBL();

            var model = bo.GetEducationRequest(UserManager.UserInfo, referenceModel).Model;

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.EducationRequest.EducationRequestIndex, CommonValues.PermissionCodes.EducationRequest.EducationRequestDetails)]
        public ActionResult ListEducationRequests([DataSourceRequest]DataSourceRequest request, EducationRequestListModel model)
        {
            var bo = new EducationRequestBL();
            var referenceModel = new EducationRequestListModel(request) { EducationCode = model.EducationCode };

            int totalCnt;
            var returnValue = bo.ListEducationRequests(UserManager.UserInfo, referenceModel, out totalCnt).Data;
            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

    }
}
