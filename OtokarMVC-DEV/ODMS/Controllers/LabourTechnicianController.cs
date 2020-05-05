using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.Labour;
using ODMSModel.LabourTechnician;
using ODMSModel.WorkOrderCard;
using System.Collections.Generic;
using ODMSCommon.Resources;
using System.Linq;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class LabourTechnicianController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.StatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.LabourTechnicianStatus).Data;
            ViewBag.TechnicianList = LabourTechnicianBL.ListTechnicianAsSelectList(UserManager.UserInfo.GetUserDealerId()).Data;
        }

        #region Labour Technician Index
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourTechnician.LabourTechnicianIndex)]
        public ActionResult LabourTechnicianIndex(int? workOrderDetailId, int? labourId)
        {
            LabourTechnicianViewModel model = new LabourTechnicianViewModel();
            SetDefaults();

            DealerBL dBo = new DealerBL();
            DealerViewModel dModel = dBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
            model.WorkshopPlanTypeId = dModel.WorkshopPlanType == DealerViewModel.WorkshopPlan.Basic ? 0 : 1;

            if (workOrderDetailId != null && labourId != null)
            {
                model.WorkOrderDetailId = workOrderDetailId.GetValue<int>();
                model.LabourId = labourId.GetValue<int>();
                model.StatusId = (int)CommonValues.LabourTechnicianStatus.Waiting;
                var bo = new LabourTechnicianBL();
                bo.GetLabourTechnician(UserManager.UserInfo, model);

                var lModel = new LabourViewModel { LabourId = labourId.GetValue<int>() };
                var lBo = new LabourBL();
                lBo.GetLabour(UserManager.UserInfo, lModel);
                model.IsDealerDuration = lModel.IsDealerDuration;
                model.UserID = model.UserID == 0 ? null : model.UserID;
                if (model.StatusId == 4 || true)//TODO: Orhan var olan tecnician userların listelenmesi için
                {
                    model.TecnicianUsers = bo.GetLabourTecnicianInfo(model.LabourTechnicianId).Data;
                }

            }
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourTechnician.LabourTechnicianIndex, CommonValues.PermissionCodes.LabourTechnician.LabourTechnicianUpdate)]
        public ActionResult LabourTechnicianIndex(LabourTechnicianViewModel viewModel)
        {
            SetDefaults();
            var bo = new LabourTechnicianBL();

            if (ModelState.IsValid)
            {
                viewModel.StatusId = viewModel.WorkshopPlanTypeId == 0 ? (int)CommonValues.LabourTechnicianStatus.Completed : (int)CommonValues.LabourTechnicianStatus.NotStarted;

                viewModel.CommandType = CommonValues.DMLType.Update;
                decimal WorkTimeReal = 0;
                if (viewModel.TecnicianUsers != null)
                {
                    foreach (var item in viewModel.TecnicianUsers)
                    {
                        WorkTimeReal += item.WorkTime;
                    }
                }
                viewModel.WorkTimeReal = WorkTimeReal;

                if (viewModel.IsDealerDuration)
                {
                    viewModel.WorkTimeEstimate = viewModel.WorkTimeReal;
                }

                bo.DMLLabourTechnician(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                bo.GetLabourTechnician(UserManager.UserInfo, viewModel);

                if (viewModel.IsDealerDuration)
                {
                    WorkOrderQuantityDataModel woModel = new WorkOrderQuantityDataModel();
                    woModel.Quantity = viewModel.WorkTimeReal.GetValue<decimal>();
                    woModel.WorkOrderDetailId = viewModel.WorkOrderDetailId;
                    woModel.WorkOrderId = viewModel.WorkOrderId.GetValue<long>();
                    woModel.ItemId = viewModel.LabourId.GetValue<int>();
                    woModel.Type = "LABOUR";
                    WorkOrderCardBL woBo = new WorkOrderCardBL();
                    woBo.UpdateDuration(UserManager.UserInfo, woModel);
                    if (woModel.ErrorNo > 0)
                    {
                        SetMessage(woModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                    }
                }

                return RedirectToAction("LabourTechnicianIndex", "LabourTechnician",
                                        new { workOrderDetailId = viewModel.WorkOrderDetailId, labourId = viewModel.LabourId });
            }
            else
            {
                ViewBag.ValidationError = string.Join("; ", ModelState.Values
                                         .SelectMany(x => x.Errors)
                                         .Select(x => x.ErrorMessage));

                bo.GetLabourTechnician(UserManager.UserInfo, viewModel);
                viewModel.TecnicianUsers = bo.GetLabourTecnicianInfo(viewModel.LabourTechnicianId).Data;
            }
            return View(viewModel);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.LabourTechnician.LabourTechnicianIndex, CommonValues.PermissionCodes.LabourTechnician.LabourTechnicianDetails)]
        public ActionResult ListLabourTechnicians([DataSourceRequest]DataSourceRequest request, LabourTechnicianListModel model)
        {
            var bo = new LabourTechnicianBL();
            var referenceModel = new LabourTechnicianListModel(request)
            {
                StatusId = model.StatusId,
                LabourId = model.LabourId,
                WorkOrderId = model.WorkOrderId,
                Plate = model.Plate,
                WorkTimeEstimate = model.WorkTimeEstimate,
                WorkTimeReal = model.WorkTimeReal,
                UserID = model.UserID,
                CreateDate = model.CreateDate,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                LabourName = model.LabourName,
                LabourCode = model.LabourCode,
                DealerId = UserManager.UserInfo.GetUserDealerId()
            };
            int totalCnt;
            var returnValue = bo.ListLabourTechnicians(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region Labour Technician Update
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourTechnician.LabourTechnicianIndex, CommonValues.PermissionCodes.LabourTechnician.LabourTechnicianUpdate)]
        public ActionResult LabourTechnicianUpdate(int labourTechnicianId = 0)
        {
            LabourTechnicianViewModel referenceModel = new LabourTechnicianViewModel();

            SetDefaults();
            if (labourTechnicianId > 0)
            {
                DealerBL dBo = new DealerBL();
                DealerViewModel dModel = dBo.GetDealer(UserManager.UserInfo, UserManager.UserInfo.GetUserDealerId()).Model;
                referenceModel.WorkshopPlanTypeId = dModel.WorkshopPlanType == DealerViewModel.WorkshopPlan.Basic
                                                        ? 0
                                                        : 1;

                LabourTechnicianBL bo = new LabourTechnicianBL();
                referenceModel.LabourTechnicianId = labourTechnicianId;
                referenceModel = bo.GetLabourTechnician(UserManager.UserInfo, referenceModel).Model;

                LabourViewModel lModel = new LabourViewModel { LabourId = referenceModel.LabourId.GetValue<int>() };
                LabourBL lBo = new LabourBL();
                lBo.GetLabour(UserManager.UserInfo, lModel);
                referenceModel.IsDealerDuration = lModel.IsDealerDuration;
                if (referenceModel.UserID == 0) referenceModel.UserID = null;

                referenceModel.TecnicianUsers = bo.GetLabourTecnicianInfo(referenceModel.LabourTechnicianId).Data;

            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourTechnician.LabourTechnicianIndex, CommonValues.PermissionCodes.LabourTechnician.LabourTechnicianUpdate)]
        public ActionResult LabourTechnicianUpdate(LabourTechnicianViewModel viewModel)
        {
            SetDefaults();
            var bo = new LabourTechnicianBL();

            if (ModelState.IsValid)
            {
                viewModel.StatusId = viewModel.WorkshopPlanTypeId == 0 ? (int)CommonValues.LabourTechnicianStatus.Completed : (int)CommonValues.LabourTechnicianStatus.NotStarted;

                viewModel.CommandType = CommonValues.DMLType.Update;

                decimal WorkTimeReal = 0;

                if (viewModel.TecnicianUsers == null)
                {
                    WorkTimeReal = 0;
                }
                else
                {
                    foreach (var item in viewModel.TecnicianUsers)
                    {
                        WorkTimeReal += item.WorkTime;
                    }
                }
                viewModel.WorkTimeReal = WorkTimeReal;

                // TFS NO : 28487 OYA 04.03.2015 Dealer Duration ise estimate'e girilen süre real süresine de aktarılıyor
                if (viewModel.IsDealerDuration)
                {
                    viewModel.WorkTimeEstimate = viewModel.WorkTimeReal;
                }

                bo.DMLLabourTechnician(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                bo.GetLabourTechnician(UserManager.UserInfo, viewModel);
            }

            // TFS NO : 28487 OYA 11.02.2015
            if (viewModel.IsDealerDuration)
            {
                WorkOrderQuantityDataModel woModel = new WorkOrderQuantityDataModel();
                woModel.Quantity = viewModel.WorkTimeReal.GetValue<decimal>();
                woModel.WorkOrderDetailId = viewModel.WorkOrderDetailId;
                woModel.WorkOrderId = viewModel.WorkOrderId.GetValue<long>();
                woModel.ItemId = viewModel.LabourId.GetValue<int>();
                woModel.Type = "LABOUR";
                WorkOrderCardBL woBo = new WorkOrderCardBL();
                woBo.UpdateDuration(UserManager.UserInfo, woModel);
                if (woModel.ErrorNo > 0)
                {
                    SetMessage(woModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                }
            }


            return View(viewModel);
        }
        #endregion

        #region Labour Technician Details
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourTechnician.LabourTechnicianIndex, CommonValues.PermissionCodes.LabourTechnician.LabourTechnicianDetails)]
        public ActionResult LabourTechnicianDetails(int labourTechnicianId)
        {
            SetDefaults();
            var referenceModel = new LabourTechnicianViewModel { LabourTechnicianId = labourTechnicianId };
            var bo = new LabourTechnicianBL();

            var model = bo.GetLabourTechnician(UserManager.UserInfo, referenceModel).Model;
            model.TecnicianUsers = bo.GetLabourTecnicianInfo(model.LabourTechnicianId).Data;

            return View(model);
        }
        #endregion

        public ActionResult GetLabourTecnicianInfo(int LabourTechnicianId)
        {
            var returnModel = new LabourTechnicianBL().GetLabourTecnicianInfo(LabourTechnicianId).Data;
            return Json(new { Data = returnModel });
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourTechnician.LabourTechnicianIndex)]
        public ActionResult CancelLabourTecnician(long? workOrderId, long workOrderDetailId, long labourId)
        {
            LabourTechnicianViewModel model = new LabourTechnicianViewModel();
            model.WorkOrderDetailId = workOrderDetailId.GetValue<int>();
            model.LabourId = labourId.GetValue<int>();
            model.StatusId = (int)CommonValues.LabourTechnicianStatus.Completed;

            var bo = new LabourTechnicianBL();
            bo.GetLabourTechnician(UserManager.UserInfo, model);

            bo.CancelLabourTecnician(UserManager.UserInfo, model);

            return
                Json(new
                {
                    Result =
                        model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    Message = model.ErrorNo > 0 ? model.ErrorMessage : MessageResource.Global_Display_Success,
                });
        }

    }
}
