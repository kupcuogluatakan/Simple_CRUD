using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.VehicleNote;
using System;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class VehicleNotesController : ControllerBase
    {
        #region VehicleNotes Index

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleNotes.VehicleNotesIndex)]
        [HttpGet]
        public ActionResult VehicleNotesIndex(int id = 0)
        {
            VehicleNotesModel Vml = new VehicleNotesModel();

            if (id <= 0)
            {
                ViewBag.HideElement = true;
                return RedirectToAction("Index", "ErrorPage");
            }
            Vml.VehicleId = id;
            ViewBag.HideElement = false;
            return PartialView(Vml);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Role.RoleTypeIndex, CommonValues.PermissionCodes.VehicleNotes.VehicleNotesIndex)]
        public ActionResult ListVehicleNote([DataSourceRequest] DataSourceRequest request, VehicleNotesListModel vehicleNotesModel)
        {
            VehicleNotesBL vehicleNBL = new VehicleNotesBL();
            VehicleNotesListModel model_vehicleN = new VehicleNotesListModel(request);
            int totalCount = 0;

            model_vehicleN.VehicleNotesId = vehicleNotesModel.VehicleNotesId;
            model_vehicleN.DealerName = vehicleNotesModel.DealerName;
            model_vehicleN.Note = vehicleNotesModel.Note;
            model_vehicleN.IsActiveName = vehicleNotesModel.IsActiveName;
            model_vehicleN.VehicleId = vehicleNotesModel.VehicleId;

            var rValue = vehicleNBL.ListVehicleNotes(UserManager.UserInfo, model_vehicleN, out totalCount).Data;
            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        #endregion VehicleNotes Index

        #region VehicleNotes Create

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleNotes.VehicleNotesIndex, CommonValues.PermissionCodes.VehicleNotes.VehicleNotesCreate)]
        [HttpGet]
        public ActionResult VehicleNotesCreate(int id = 0)
        {
            VehicleNotesModel v = new VehicleNotesModel();
            v.VehicleId = id;
            v.IsActive = true;
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleNotes.VehicleNotesIndex, CommonValues.PermissionCodes.VehicleNotes.VehicleNotesCreate)]
        [HttpPost]
        public ActionResult VehicleNotesCreate(VehicleNotesModel viewModel)
        {
            var vehicleNotesBo = new VehicleNotesBL();
            if (ModelState.IsValid)
            {
                viewModel.DealerId = UserManager.UserInfo.GetUserDealerId();
                viewModel.CommandType = CommonValues.DMLType.Insert;

                if (UserManager.UserInfo.GetUserDealerId() == 0)
                {
                    viewModel.ApproveDate = DateTime.Now;
                    viewModel.ApproveUser = UserManager.UserInfo.UserId;

                    vehicleNotesBo.DMLVehicleNotes(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);
                }
                else
                {
                    vehicleNotesBo.DMLVehicleNotes(UserManager.UserInfo, viewModel);
                    SetMessage(MessageResource.VehicleNote_Display_SuccessMessage, CommonValues.MessageSeverity.Success);
                }

                ModelState.Clear();

                return View(new VehicleNotesModel() { VehicleId = viewModel.VehicleId });
            }
            return View(viewModel);
        }

        #endregion VehicleNotes Create
        
        #region VehicleNotes Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleNotes.VehicleNotesIndex, CommonValues.PermissionCodes.VehicleNotes.VehicleNotesDelete)]
        public ActionResult DeleteVehicle(int id)
        {
            VehicleNotesModel viewModel = new VehicleNotesModel() { VehicleNotesId = id };
            var vehicleNotesBo = new VehicleNotesBL();
            viewModel.CommandType = CommonValues.DMLType.Delete;
            vehicleNotesBo.DMLVehicleNotes(UserManager.UserInfo, viewModel);
            //var deleteResult = CheckErrorForMessage(viewModel, true);
            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }

        #endregion VehicleNotes Delete
    }
}