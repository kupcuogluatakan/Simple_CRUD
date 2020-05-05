using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.Proficiency;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ProficiencyController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Proficiency.ProficiencyIndex)]
        public ActionResult ProficiencyIndex()
        {
            return View();
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Proficiency.ProficiencyIndex, CommonValues.PermissionCodes.Proficiency.ProficiencyCreate)]
        public ActionResult ProficiencyCreate()
        {
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Proficiency.ProficiencyIndex, CommonValues.PermissionCodes.Proficiency.ProficiencyCreate)]
        public ActionResult ProficiencyCreate(ProficiencyDetailModel model)
        {
            if (ModelState.IsValid)
            {
                var bo = new ProficiencyBL();
                model.CommandType = CommonValues.DMLType.Insert;
                bo.DMLProficiency(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();
                return View();
            }
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Proficiency.ProficiencyIndex, CommonValues.PermissionCodes.Proficiency.ProficiencyUpdate)]
        public ActionResult ProficiencyUpdate(string proficiencyCode, string dealerId)
        {
            var referenceModel = new ProficiencyDetailModel();
            if (!string.IsNullOrEmpty(proficiencyCode))
            {
                var bo = new ProficiencyBL();
                referenceModel.ProficiencyCode = proficiencyCode;
                referenceModel = bo.GetProficiency(UserManager.UserInfo, referenceModel).Model;
            }
            referenceModel.DealerId = dealerId.GetValue<int>();
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Proficiency.ProficiencyIndex, CommonValues.PermissionCodes.Proficiency.ProficiencyUpdate)]
        public ActionResult ProficiencyUpdate(ProficiencyDetailModel model)
        {
            var bo = new ProficiencyBL();
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                bo.DMLProficiency(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Proficiency.ProficiencyIndex, CommonValues.PermissionCodes.Proficiency.ProficiencyDelete)]
        public ActionResult ProficiencyDelete(string proficiencyCode)
        {
            ViewBag.HideElements = false;

            var bo = new ProficiencyBL();
            var model = new ProficiencyDetailModel { ProficiencyCode = proficiencyCode, CommandType = CommonValues.DMLType.Delete };
            bo.DMLProficiency(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Proficiency.ProficiencyIndex, CommonValues.PermissionCodes.Proficiency.ProficiencyDetails)]
        public ActionResult ProficiencyDetails(string proficiencyCode)
        {
            var referenceModel = new ProficiencyDetailModel { ProficiencyCode = proficiencyCode };
            var bo = new ProficiencyBL();

            var model = bo.GetProficiency(UserManager.UserInfo, referenceModel).Model;

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Proficiency.ProficiencyIndex, CommonValues.PermissionCodes.Proficiency.ProficiencyDetails)]
        public ActionResult ListProficiencies([DataSourceRequest]DataSourceRequest request, ProficiencyListModel model)
        {
            var bo = new ProficiencyBL();
            var referenceModel = new ProficiencyListModel(request) { ProficiencyCode = model.ProficiencyCode, ProficiencyName = model.ProficiencyName, Description = model.Description };
            int totalCount;
            var returnValue = bo.ListProficiencies(UserManager.UserInfo, referenceModel, out totalCount).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCount
            });
        }

    }
}
