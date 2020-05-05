using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSModel.Education;
using ODMSModel.ViewModel;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class EducationController : ControllerBase
    {

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Education.EducationIndex)]
        [HttpGet]
        public ActionResult EducationIndex()
        {
            SetComboBox();
            return View();
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Education.EducationIndex, ODMSCommon.CommonValues.PermissionCodes.Education.EducationDetails)]
        public JsonResult ListEducation([DataSourceRequest] DataSourceRequest request, EducationListModel educationListModel)
        {
            EducationBL educationBL = new EducationBL();
            EducationListModel model = new EducationListModel(request);
            int totalCount = 0;

            model.EducationTypeId = educationListModel.EducationTypeId;
            model.IsActiveSearch = educationListModel.IsActiveSearch;
            model.IsMandatorySearch = educationListModel.IsMandatorySearch;
            model.VehicleModelKod = educationListModel.VehicleModelKod;

            var rValue = educationBL.GetEducationList(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        //Details
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Education.EducationDetails, ODMSCommon.CommonValues.PermissionCodes.Education.EducationIndex)]
        [HttpGet]
        public ActionResult EducationDetails(string id)
        {
            EducationBL educBL = new EducationBL();
            EducationViewModel educViewModel = new EducationViewModel();
            educViewModel.EducationCode = id;

            educBL.GetEducation(UserManager.UserInfo, educViewModel);

            return View(educViewModel);
        }

        //Update
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Education.EducationUpdate, ODMSCommon.CommonValues.PermissionCodes.Education.EducationIndex)]
        [HttpGet]
        public ActionResult EducationUpdate(string id)
        {
            EducationBL educBL = new EducationBL();
            EducationViewModel educViewModel = new EducationViewModel();
            educViewModel.EducationCode = id;

            educBL.GetEducation(UserManager.UserInfo, educViewModel);
            SetComboBox();
            return View(educViewModel);

        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Education.EducationUpdate, ODMSCommon.CommonValues.PermissionCodes.Education.EducationIndex)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EducationUpdate(EducationViewModel eduModel)
        {
            if (ModelState.IsValid)
            {
                EducationBL eduBL = new EducationBL();
                eduModel.CommandType = ODMSCommon.CommonValues.DMLType.Update;

                eduBL.DMLEducation(UserManager.UserInfo, eduModel);

                if (!CheckErrorForMessage(eduModel, true))
                {
                    eduModel.EducationName = (MultiLanguageModel)ODMSCommon.CommonUtility.DeepClone(eduModel.EducationName);
                    eduModel.EducationName.MultiLanguageContentAsText = eduModel.MultiLanguageContentAsText;
                }
            }
            SetComboBox();
            return View(eduModel);
        }


        //Create
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Education.EducationCreate, ODMSCommon.CommonValues.PermissionCodes.Education.EducationIndex)]
        [HttpGet]
        public ActionResult EducationCreate()
        {
            SetComboBox();
            EducationViewModel model = new EducationViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Education.EducationCreate, ODMSCommon.CommonValues.PermissionCodes.Education.EducationIndex)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EducationCreate(EducationViewModel educModel)
        {
            SetComboBox();
            if (ModelState.IsValid)
            {
                EducationBL educationBL = new EducationBL();
                educModel.CommandType = ODMSCommon.CommonValues.DMLType.Insert;

                educationBL.DMLEducation(UserManager.UserInfo, educModel);
                if (!CheckErrorForMessage(educModel, true))
                {
                    ModelState.Clear();
                    SetComboBox();
                    return View();
                }
            }
            return View(educModel);
        }


        //Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.Education.EducationDelete, ODMSCommon.CommonValues.PermissionCodes.Education.EducationIndex)]
        public ActionResult EducationDelete(string eduCode)
        {
            EducationBL eduBL = new EducationBL();
            EducationViewModel eduModel = new EducationViewModel
            {
                EducationCode = eduCode,
                CommandType = ODMSCommon.CommonValues.DMLType.Delete
            };

            eduBL.DMLEducation(UserManager.UserInfo, eduModel);

            if (eduModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, eduModel.ErrorMessage);
        }



        private void SetComboBox()
        {
            List<SelectListItem> listVM = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            List<SelectListItem> listET = EducationBL.ListEducationTypeAsSelectList(UserManager.UserInfo).Data;
            List<SelectListItem> listIA = CommonBL.ListStatus().Data;
            List<SelectListItem> listYN = CommonBL.ListYesNo().Data;
            ViewBag.IASelectList = listIA;
            ViewBag.VMSelectList = listVM;
            ViewBag.ETSelectList = listET;
            ViewBag.YNSelectList = listYN;
        }


    }
}
