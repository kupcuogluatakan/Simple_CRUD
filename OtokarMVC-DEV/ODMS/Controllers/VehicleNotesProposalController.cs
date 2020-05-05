using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.VehicleNoteProposal;
using System;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class VehicleNotesProposalController : ControllerBase
    {
        #region VehicleNotesProposal Index

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleNotes.VehicleNotesIndex)]
        [HttpGet]
        public ActionResult VehicleNotesProposalIndex(int id = 0)
        {
            VehicleNotesProposalModel Vml = new VehicleNotesProposalModel();

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
        public ActionResult ListVehicleNoteProposal([DataSourceRequest] DataSourceRequest request, VehicleNotesProposalListModel vehicleNotesProposalModel)
        {
            VehicleNotesProposalBL vehicleNBL = new VehicleNotesProposalBL();
            VehicleNotesProposalListModel model_vehicleN = new VehicleNotesProposalListModel(request);
            int totalCount = 0;

            model_vehicleN.VehicleNotesId = vehicleNotesProposalModel.VehicleNotesId;
            model_vehicleN.DealerName = vehicleNotesProposalModel.DealerName;
            model_vehicleN.Note = vehicleNotesProposalModel.Note;
            model_vehicleN.IsActiveName = vehicleNotesProposalModel.IsActiveName;
            model_vehicleN.VehicleId = vehicleNotesProposalModel.VehicleId;

            var rValue = vehicleNBL.ListVehicleNotesProposal(UserManager.UserInfo, model_vehicleN, out totalCount).Data;
            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        #endregion VehicleNotesProposal Index

        #region VehicleNotesProposal Create

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleNotes.VehicleNotesIndex, CommonValues.PermissionCodes.VehicleNotes.VehicleNotesCreate)]
        [HttpGet]
        public ActionResult VehicleNotesProposalCreate(int id = 0)
        {
            VehicleNotesProposalModel v = new VehicleNotesProposalModel();
            v.VehicleId = id;
            v.IsActive = true;
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleNotes.VehicleNotesIndex, CommonValues.PermissionCodes.VehicleNotes.VehicleNotesCreate)]
        [HttpPost]
        public ActionResult VehicleNotesProposalCreate(VehicleNotesProposalModel viewModel)
        {
            var vehicleNotesBo = new VehicleNotesProposalBL();
            if (ModelState.IsValid)
            {
                viewModel.DealerId = UserManager.UserInfo.GetUserDealerId();
                viewModel.CommandType = CommonValues.DMLType.Insert;

                if (UserManager.UserInfo.GetUserDealerId() == 0)
                {
                    viewModel.ApproveDate = DateTime.Now;
                    viewModel.ApproveUser = UserManager.UserInfo.UserId;

                    vehicleNotesBo.DMLVehicleNotesProposal(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);
                }
                else
                {
                    vehicleNotesBo.DMLVehicleNotesProposal(UserManager.UserInfo, viewModel);
                    SetMessage(MessageResource.VehicleNote_Display_SuccessMessage, CommonValues.MessageSeverity.Success);
                }

                ModelState.Clear();

                return View(new VehicleNotesProposalModel() { VehicleId = viewModel.VehicleId });
            }
            return View(viewModel);
        }

        #endregion VehicleNotesProposal Create

        #region VehicleNotesProposal Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.VehicleNotes.VehicleNotesIndex, CommonValues.PermissionCodes.VehicleNotes.VehicleNotesDelete)]
        public ActionResult DeleteVehicle(int id)
        {
            VehicleNotesProposalModel viewModel = new VehicleNotesProposalModel() { VehicleNotesId = id };
            var vehicleNotesBo = new VehicleNotesProposalBL();
            viewModel.CommandType = CommonValues.DMLType.Delete;
            vehicleNotesBo.DMLVehicleNotesProposal(UserManager.UserInfo, viewModel);
          
            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }

        #endregion VehicleNotesProposal Delete
    }
}