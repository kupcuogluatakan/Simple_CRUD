using System.Collections.Generic;
using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using Kendo.Mvc.UI;
using ODMSCommon.Security;
using ODMSModel.VehicleNoteApprove;
using ODMSCommon.Resources;
using System.Linq;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class VehicleNoteApproveController : ControllerBase
    {
        //
        // GET: /VehicleNoteApprove/
        #region VehicleNoteApprove Index
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveIndex)]
        [HttpGet]
        public ActionResult VehicleNoteApproveIndex(int id = 0)
        {
            SetComboBox();
            VehicleNoteApproveModel model = new VehicleNoteApproveModel();

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
        public ActionResult ListVehicleNoteApprove([DataSourceRequest] DataSourceRequest request, VehicleNoteApproveListModel model)
        {
            if (!UserManager.UserInfo.IsDealer)
            {
                VehicleNoteApproveBL vehicleNABL = new VehicleNoteApproveBL();
                VehicleNoteApproveListModel model_vehicleNA = new VehicleNoteApproveListModel(request) { CreateDate = model.CreateDate };
                int totalCount = 0;

                model_vehicleNA.DealerId = model.DealerId;
                model_vehicleNA.Note = model.Note;
                model_vehicleNA.CreateDate = model.CreateDate;
                model_vehicleNA.VinId = model.VinId;

                var rValue = vehicleNABL.ListVehicleNoteApprove(UserManager.UserInfo, model_vehicleNA, out totalCount).Data;

                return Json(new
                {
                    Data = rValue,
                    Total = totalCount
                });
            }
            else
                return Json(new { Data = new List<VehicleNoteApproveListModel>(), Total = 0 });

        }

        [HttpGet]
        public ActionResult GetLastVehicleNoteApprove_ForWebTest(VehicleNoteApproveListModel model)
        {
            VehicleNoteApproveBL vehicleNABL = new VehicleNoteApproveBL();
            VehicleNoteApproveListModel model_vehicleNA = new VehicleNoteApproveListModel() { CreateDate = model.CreateDate };
            int totalCount = 0;

            model_vehicleNA.DealerId = model.DealerId;
            model_vehicleNA.Note = model.Note;
            model_vehicleNA.CreateDate = model.CreateDate;
            model_vehicleNA.VinId = model.VinId;

            var rValue = vehicleNABL.ListVehicleNoteApprove(UserManager.UserInfo, model_vehicleNA, out totalCount).Data;

            if (rValue.Count > 0)
                return new JsonResult() { Data = rValue.First().VehicleNotesId, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult() { Data = 0, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #region VehicleNoteApprove Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveDelete)]
        public ActionResult DeleteVehicleNoteApprove(VehicleNoteApproveModel model)
        {
            VehicleNoteApproveModel vnaModel = new VehicleNoteApproveModel();
            var vnaBl = new VehicleNoteApproveBL();
            vnaModel.VehicleNotesId = model.VehicleNotesId;

            vnaModel.CommandType = vnaModel.VehicleNotesId > 0 ? ODMSCommon.CommonValues.DMLType.Delete : "";

            vnaBl.DeleteVehicleNoteApprove(vnaModel);

            if (vnaModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);

            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, vnaModel.ErrorMessage);
        }
        #endregion

        #region VehicleNoteApprove Approve

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveApprove)]
        [HttpGet]
        public ActionResult VehicleNoteApproveApprove(int id)
        {

            VehicleNoteApproveModel vnaModel = new VehicleNoteApproveModel();
            VehicleNoteApproveBL vnaBL = new VehicleNoteApproveBL();
            vnaModel.VehicleNotesId = id;
            vnaBL.GetVehicleNoteApprove(UserManager.UserInfo, vnaModel);

            //if (ModelState.IsValid)
            //{
            //    vnaModel.CommandType = vnaModel.VehicleNotesId > 0 ? ODMSCommon.CommonValues.DMLType.Update : "";
            //    vnaBL.ApproveVehicleNote(vnaModel);
            //}
            //SetComboBox();
            SetComboBox();
            return View(vnaModel);
        }
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveIndex, ODMSCommon.CommonValues.PermissionCodes.VehicleNoteApprove.VehicleNoteApproveApprove)]
        [HttpPost]
        public ActionResult VehicleNoteApproveApprove(VehicleNoteApproveModel viewModel)
        {
            var vnaBo = new VehicleNoteApproveBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = ODMSCommon.CommonValues.DMLType.Update;

                vnaBo.ApproveVehicleNote(UserManager.UserInfo, viewModel);
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
            List<SelectListItem> NoteList = VehicleNoteApproveBL.ListVehicleNoteApproveAsSelected(UserManager.UserInfo).Data;

            ViewBag.IASelectList = list_SelectList;
            ViewBag.DNSelectList = list_DealerN;
            ViewBag.NSelectList = NoteList;
        }


    }
}
