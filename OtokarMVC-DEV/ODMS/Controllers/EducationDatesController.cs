using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSModel.EducationDates;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class EducationDatesController : ControllerBase
    {
        //Index
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesIndex)]
        [HttpGet]
        public ActionResult EducationDatesIndex(string id)
        {
            EducationDatesViewModel eduDatesViewModel = new EducationDatesViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                eduDatesViewModel.EducationCode = id;
                eduDatesViewModel.IsRequestRoot = true;
            }
            else
            {
                eduDatesViewModel.IsRequestRoot = false;
            }
            return PartialView(eduDatesViewModel);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesIndex, ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesDetails)]
        public JsonResult ListEducationDates([DataSourceRequest] DataSourceRequest request, EducationDatesListModel eduDatesListModel)
        {
            EducationDatesBL eduDatesBL = new EducationDatesBL();
            EducationDatesListModel eduDatesmodel = new EducationDatesListModel(request);
            int totalCount = 0;

            eduDatesmodel.EducationCode = eduDatesListModel.EducationCode;

            var rValue = eduDatesBL.GetEducationDatesList(UserManager.UserInfo, eduDatesmodel, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }


        //Details
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesIndex, ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesDetails)]
        [HttpGet]
        public ActionResult EducationDatesDetails(string id, int rowId)
        {
            EducationDatesBL eduDatesBL = new EducationDatesBL();
            EducationDatesViewModel eduDatesModel = new EducationDatesViewModel();
            eduDatesModel.EducationCode = id;
            eduDatesModel.RowNumber = rowId;

            eduDatesBL.GetEducationDate(UserManager.UserInfo, eduDatesModel);

            return View(eduDatesModel);
        }


        //Update
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesUpdate, ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesDetails)]
        [HttpGet]
        public ActionResult EducationDatesUpdate(string id, int rowId)
        {
            EducationDatesBL eduDatesBL = new EducationDatesBL();
            EducationDatesViewModel eduDatesModel = new EducationDatesViewModel();
            eduDatesModel.EducationCode = id;
            eduDatesModel.RowNumber = rowId;

            eduDatesBL.GetEducationDate(UserManager.UserInfo, eduDatesModel);

            return View(eduDatesModel);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesUpdate, ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesDetails)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EducationDatesUpdate(EducationDatesViewModel eduDatesModel)
        {
            if (ModelState.IsValid)
            {
                EducationDatesBL eduDatesBL = new EducationDatesBL();
                eduDatesModel.CommandType = ODMSCommon.CommonValues.DMLType.Update;

                eduDatesBL.DMLEducationDates(UserManager.UserInfo, eduDatesModel);

                CheckErrorForMessage(eduDatesModel, true);

            }
            return View(eduDatesModel);
        }


        //Create
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesCreate, ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesDetails)]
        [HttpGet]
        public ActionResult EducationDatesCreate(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                EducationDatesViewModel eduDatesViewModel = new EducationDatesViewModel { EducationCode = id };

                return View(eduDatesViewModel);
            }
            return View();

        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesCreate, ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesDetails)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EducationDatesCreate(EducationDatesViewModel eduDatesViewModel)
        {

            if (ModelState.IsValid)
            {
                EducationDatesBL eduDatesBL = new EducationDatesBL();
                string eduCode = eduDatesViewModel.EducationCode;
                eduDatesViewModel.CommandType = ODMSCommon.CommonValues.DMLType.Insert;
                eduDatesBL.DMLEducationDates(UserManager.UserInfo, eduDatesViewModel);

                CheckErrorForMessage(eduDatesViewModel, true);
                ModelState.Clear();

                eduDatesViewModel = new EducationDatesViewModel() { EducationCode = eduCode };
                return View(eduDatesViewModel);

            }

            return View(eduDatesViewModel);
        }


        //Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesDelete, ODMSCommon.CommonValues.PermissionCodes.EducationDates.EducationDatesDetails)]
        public ActionResult EducationDatesDelete(string eduCode, int rowId)
        {
            EducationDatesBL eduDatesBL = new EducationDatesBL();
            EducationDatesViewModel eduDatesViewModel = new EducationDatesViewModel
            {
                EducationCode = eduCode,
                RowNumber = rowId,
                CommandType = ODMSCommon.CommonValues.DMLType.Delete
            };

            eduDatesBL.DMLEducationDates(UserManager.UserInfo, eduDatesViewModel);

            if (eduDatesViewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, eduDatesViewModel.ErrorMessage);
        }

    }
}
