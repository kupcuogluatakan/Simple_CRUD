using System.Collections.Generic;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using Kendo.Mvc.UI;
using ODMSCommon.Security;
using ODMSModel.VehicleNoteProposalApprove;
using ODMSCommon.Resources;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class VehicleNoteProposalApproveController : ControllerBase
    {
        //
        // GET: /VehicleNoteApprove/
        #region VehicleNoteApprove Index
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveIndex)]
        [HttpGet]
        public ActionResult VehicleNoteProposalApproveIndex(int id = 0)
        {
            SetComboBox();
            VehicleNoteProposalApproveModel model = new VehicleNoteProposalApproveModel();

            if (id <= 0)
            {
                ViewBag.HideElement = true;
                return View();
            }
            model.VehicleId = id;
            ViewBag.HideElement = false;

            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveIndex)]
        [HttpPost]
        public ActionResult ListVehicleNoteProposalApprove([DataSourceRequest] DataSourceRequest request, VehicleNoteProposalApproveListModel model)
        {
            if (!UserManager.UserInfo.IsDealer)
            {
                VehicleNoteProposalApproveBL vehicleNABL = new VehicleNoteProposalApproveBL();
                VehicleNoteProposalApproveListModel model_vehicleNA = new VehicleNoteProposalApproveListModel(request) { CreateDate = model.CreateDate };
                int totalCount = 0;

                model_vehicleNA.DealerId = model.DealerId;
                model_vehicleNA.Note = model.Note;
                model_vehicleNA.CreateDate = model.CreateDate;
                model_vehicleNA.VinId = model.VinId;

                var rValue = vehicleNABL.ListVehicleNoteProposalApprove(UserManager.UserInfo, model_vehicleNA, out totalCount).Data;

                return Json(new
                {
                    Data = rValue,
                    Total = totalCount
                });
            }
            else
                return Json(new { Data = new List<VehicleNoteProposalApproveListModel>(), Total = 0 });

        }
        #endregion

        #region VehicleNoteProposalApprove Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveDelete)]
        public ActionResult DeleteVehicleNoteProposalApprove(VehicleNoteProposalApproveModel model)
        {
            VehicleNoteProposalApproveModel vnaModel = new VehicleNoteProposalApproveModel();
            var vnaBl = new VehicleNoteProposalApproveBL();
            vnaModel.VehicleNotesId = model.VehicleNotesId;

            vnaModel.CommandType = vnaModel.VehicleNotesId > 0 ? ODMSCommon.CommonValues.DMLType.Delete : "";

            vnaBl.DeleteVehicleNoteProposalApprove(vnaModel);

            if (vnaModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);

            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, vnaModel.ErrorMessage);
        }
        #endregion

        #region VehicleNoteProposalApprove Approve

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveApprove)]
        [HttpGet]
        public ActionResult VehicleNoteProposalApproveApprove(int id)
        {

            VehicleNoteProposalApproveModel vnaModel = new VehicleNoteProposalApproveModel();
            VehicleNoteProposalApproveBL vnaBL = new VehicleNoteProposalApproveBL();
            vnaModel.VehicleNotesId = id;
            vnaBL.GetVehicleNoteProposalApprove(UserManager.UserInfo, vnaModel);

           
            SetComboBox();
            return View(vnaModel);
        }
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveApprove)]
        [HttpPost]
        public ActionResult VehicleNoteProposalApproveApprove(VehicleNoteProposalApproveModel viewModel)
        {
            var vnaBo = new VehicleNoteProposalApproveBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = ODMSCommon.CommonValues.DMLType.Update;

                vnaBo.ApproveVehicleNoteProposal(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            SetComboBox();

            if (viewModel.ErrorNo == 0)
            {
                ModelState.Clear();
                return View();
            }
            else
            {
                return View(viewModel);
            }
        }
        #endregion

        private void SetComboBox()
        {

            List<SelectListItem> list_SelectList = CommonBL.ListStatus().Data;
            List<SelectListItem> list_DealerN = DealerBL.ListDealerAsSelectListItem().Data;
            List<SelectListItem> NoteList = VehicleNoteProposalApproveBL.ListVehicleNoteProposalApproveAsSelected(UserManager.UserInfo).Data;

            ViewBag.IASelectList = list_SelectList;
            ViewBag.DNSelectList = list_DealerN;
            ViewBag.NSelectList = NoteList;
        }


    }
}
