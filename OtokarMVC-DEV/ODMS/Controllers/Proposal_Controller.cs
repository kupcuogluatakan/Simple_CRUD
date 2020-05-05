using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Business;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Fleet;
using ODMSModel.Proposal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Permission = ODMSCommon.CommonValues.PermissionCodes.Proposal;


namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class Proposal_Controller : ControllerBase
    {
        #region Index

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalIndex)]
        public ActionResult ProposalIndex()
        {
            FillComboBoxes();
            ProposalListModel model = new ProposalListModel();

            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();

            ViewBag.ProposalStatusList = new ProposalBL().ListProposalStats(UserManager.UserInfo).Data;
            ViewBag.VehicleTypeList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, null).Data;

            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalIndex)]
        public ActionResult ListProposal([DataSourceRequest]DataSourceRequest request, ProposalListModel viewModel)
        {
            var bus = new ProposalBL();
            var model = new ProposalListModel(request)
            {
                SearchIsActive = viewModel.SearchIsActive,
                DealerId = viewModel.DealerId,
                VehiclePlate = viewModel.VehiclePlate,
                CustomerName = viewModel.CustomerName,
                ProposalStatusId = viewModel.ProposalStatusId,
                EndDate = viewModel.EndDate,
                StartDate = viewModel.StartDate,
                VehicleType = viewModel.VehicleType,
                VinNo = viewModel.VinNo,
                ProposalId = viewModel.ProposalId
            };

            var totalCnt = 0;
            var returnValue = bus.ListProposal(UserManager.UserInfo, model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region Create

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalIndex, Permission.ProposalCreate)]
        public ActionResult ProposalCreate()
        {
            FillComboBoxes();

            ProposalViewModel model = new ProposalViewModel();

            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();

            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalIndex, Permission.ProposalCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult ProposalCreate(ProposalViewModel model)
        {
            FillComboBoxes();
            if (ModelState.IsValid == false)
                return View(model);

            var bus = new ProposalBL();
            model.CommandType = CommonValues.DMLType.Insert;

            bus.DMLProposal(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo > 0) return View(model);
            return RedirectToAction("ProposalDetails", new { id = model.ProposalId, seq = 0 });
            //return View(model);
        }
        #endregion

        #region Details

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalIndex, Permission.ProposalDetails)]
        public ActionResult ProposalDetails(long? id, int seq)
        {
            if (!(id.HasValue && id > 0))
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View();
            }
            return View(new ProposalBL().GetProposalViewModel(UserManager.UserInfo, id.GetValueOrDefault(), seq).Model);
        }

        #endregion

        #region Private Methods

        private void FillComboBoxes()
        {
            var bus = new ProposalBL();
            ViewBag.AppointmentTypeList = bus.ListAppointmentTypesForProposal(UserManager.UserInfo).Data;
            ViewBag.StuffList = bus.GetDealerUsers(UserManager.UserInfo.GetUserDealerId()).Data;
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
        }

        #endregion

        public ActionResult GetProposalData(int id, string type)
        {
            switch (type)
            {
                case "Appointment":
                case "Vehicle":
                case "Customer":
                    return Json(new ProposalBL().GetProposalData(id, type).Model);
                default:
                    return Json(0);
            }
        }

        [AuthorizationFilter(Permission.ProposalIndex)]
        public ActionResult GetProposalPartial(long id, int seq)
        {
            return PartialView("_ProposalDisplay", new ProposalBL().GetProposalViewModel(UserManager.UserInfo, id, seq).Model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalIndex)]
        public ActionResult CheckFleet(int customerId, int vehicleId)
        {
            return Json(new { applicableFleetId = new ProposalBL().CheckFleet(UserManager.UserInfo, customerId, vehicleId).Model });
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalIndex, Permission.ProposalCreate)]
        public ActionResult ShowFleetInfo(int id) /*fleetId*/
        {
            if (id > 0)
            {
                var model = new FleetViewModel { IdFleet = id };
                new FleetBL().GetFleet(UserManager.UserInfo, model);
                return PartialView("_FleetInfo", model);
            }
            return new HttpNotFoundResult();
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalIndex, Permission.ProposalUpdate)]
        public ActionResult CancelProposal(long id = 0)
        {
            if (id == 0) return HttpNotFound("id=0");
            var model = new ProposalBL().CancelProposal(UserManager.UserInfo, id).Model;

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.ProposalIndex)]
        public JsonResult GetVehicleCustomerId(int vehicleId)
        {
            return Json(new ProposalBL().GetVehicleCustomerId(vehicleId).Model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.ProposalIndex, Permission.ProposalCreate)]
        public ActionResult OpenCustomerChange(int customerId, int vehicleCustomerId)
        {
            var model = new ProposalBL().GetCustomerChangeData(customerId, vehicleCustomerId).Model;
            return PartialView("_CustomerChangeInfo", model);
        }

        [HttpPost]
        public ActionResult ConvertProposalCard(int ProposalId, int seq)
        {
            string msgSuc = MessageResource.Global_Display_Success;
            using (var ts = new TransactionScope(TransactionScopeOption.Suppress))
            {
                var proposalBo = new ProposalBL();
                var viewModel = new ProposalBL().GetProposalViewModel(UserManager.UserInfo, ProposalId, seq).Model;

                if (viewModel.ErrorNo > 0)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
                }

                if (viewModel.IsConvert)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.DB_Record_Has_Changed);

                var result = proposalBo.ConvertToWorkOrder(UserManager.UserInfo, new ProposalViewModel { ProposalId = ProposalId, ProposalSeq = seq }).Model;

                if (result.ErrorNo > 0)
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, result.ErrorMessage);
                }
                ts.Complete();
            }
            
            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, msgSuc);
        }


    }
}