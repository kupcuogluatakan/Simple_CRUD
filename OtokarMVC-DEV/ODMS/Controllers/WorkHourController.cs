using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using System.Linq;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.WorkHour;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class WorkHourController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.StatusOfWorkList = CommonBL.ListStatusOfWork().Data;
            ViewBag.StatusList = CommonBL.ListStatus().Data;
        }

        #region WorkHour Index

        [AuthorizationFilter(CommonValues.PermissionCodes.WorkHour.WorkHourIndex)]
        [HttpGet]
        public ActionResult WorkHourIndex()
        {
            SetDefaults();
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            WorkHourViewModel model = new WorkHourViewModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.WorkHour.WorkHourIndex, CommonValues.PermissionCodes.WorkHour.WorkHourDetails)]
        public ActionResult ListWorkHour([DataSourceRequest] DataSourceRequest request, WorkHourListModel model)
        {
            var WorkHourBo = new WorkHourBL();
            var v = new WorkHourListModel(request)
                {
                    DealerId = model.DealerId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Priority = model.Priority,
                    MeetingInterval = model.MeetingInterval,
                    StatusOfWork = model.StatusOfWork,
                    IsActive = model.IsActive
                };

            var totalCnt = 0;
            var returnValue = WorkHourBo.ListWorkHours(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region WorkHour Create
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkHour.WorkHourIndex, CommonValues.PermissionCodes.WorkHour.WorkHourCreate)]
        public ActionResult WorkHourCreate()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            var model = new WorkHourViewModel();
            for (int i = 0; i < 7; i++)
            {
               model.WeekDays.Add(false); 
            }
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            model.MeetingInterval = CommonBL.GetGeneralParameterValue(CommonValues.LookupKeys.AppointmentInterval).Model.GetValue<int>();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.WorkHour.WorkHourIndex, CommonValues.PermissionCodes.WorkHour.WorkHourCreate)]
        [HttpPost]
        public ActionResult WorkHourCreate(WorkHourViewModel viewModel/*, ICollection<bool> WeekDays, ICollection<TeaBreakModel> TeaBreaks*/)
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            var WorkHourBo = new WorkHourBL();

            if (ModelState.IsValid)
            {
                var v = new WorkHourListModel()
                {
                    DealerId = viewModel.DealerId,
                    StartDate = viewModel.StartDate,
                    EndDate = viewModel.EndDate,
                    Priority = viewModel.Priority,
                    StatusOfWork = viewModel.StatusOfWork.GetValue<int>(),
                    IsActive = viewModel.IsActive.GetValue<int>()
                };

                var totalCnt = 0;
                var returnValue = WorkHourBo.ListWorkHours(UserManager.UserInfo, v, out totalCnt).Data;
                if(totalCnt != 0 
                    && returnValue.Any(e=>e.WorkStartHour == viewModel.WorkStartHour 
                    && e.WorkEndHour == viewModel.WorkEndHour))
                {
                    SetMessage(MessageResource.WorkHour_Error_SamePriorityStartDateEndDate, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }

                viewModel.CommandType = CommonValues.DMLType.Insert;
                WorkHourBo.DMLWorkHour(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();
                var model = new WorkHourViewModel();
                for (int i = 0; i < 7; i++)
                {
                    model.WeekDays.Add(false);
                }
                if (UserManager.UserInfo.GetUserDealerId() != 0)
                    model.DealerId = UserManager.UserInfo.GetUserDealerId();
                model.MeetingInterval = CommonBL.GetGeneralParameterValue(CommonValues.LookupKeys.AppointmentInterval).Model.GetValue<int>();
                model.IsActive = true;
                return View(model);
            }
            return View(viewModel);
        }

        #endregion

        #region WorkHour Update
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkHour.WorkHourIndex, CommonValues.PermissionCodes.WorkHour.WorkHourUpdate)]
        [HttpGet]
        public ActionResult WorkHourUpdate(int id=0)
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
           var WorkHourBo = new WorkHourBL();
           return View( WorkHourBo.GetWorkHour(UserManager.UserInfo, id).Model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.WorkHour.WorkHourIndex, CommonValues.PermissionCodes.WorkHour.WorkHourUpdate)]
        [HttpPost]
        public ActionResult WorkHourUpdate(WorkHourViewModel viewModel)
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            var WorkHourBo = new WorkHourBL();
            if (ModelState.IsValid)
            {
                var v = new WorkHourListModel()
                {
                    DealerId = viewModel.DealerId,
                    StartDate = viewModel.StartDate,
                    EndDate = viewModel.EndDate,
                    Priority = viewModel.Priority,
                    StatusOfWork = viewModel.StatusOfWork.GetValue<int>(),
                    IsActive = viewModel.IsActive.GetValue<int>()
                };

                var totalCnt = 0;
                var returnValue = WorkHourBo.ListWorkHours(UserManager.UserInfo, v, out totalCnt).Data;
                if (totalCnt != 0 
                    && returnValue.Any(e=>e.WorkHourId != viewModel.WorkHourId 
                    && e.WorkStartHour == viewModel.WorkStartHour 
                    && e.WorkEndHour == viewModel.WorkEndHour))
                {
                    SetMessage(MessageResource.WorkHour_Error_SamePriorityStartDateEndDate, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }

                viewModel.CommandType =CommonValues.DMLType.Update;
                WorkHourBo.DMLWorkHour(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region WorkHour Delete
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkHour.WorkHourIndex, CommonValues.PermissionCodes.WorkHour.WorkHourDelete)]
        public ActionResult DeleteWorkHour(int id)
        {
            WorkHourViewModel viewModel = new WorkHourViewModel() { WorkHourId = id};
            var WorkHourBo = new WorkHourBL();
            viewModel.CommandType =  CommonValues.DMLType.Delete;
            WorkHourBo.DMLWorkHour(UserManager.UserInfo, viewModel);
            //var deleteResult = CheckErrorForMessage(viewModel, true);
            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion

        #region WorkHour Details
        [AuthorizationFilter(CommonValues.PermissionCodes.WorkHour.WorkHourIndex, CommonValues.PermissionCodes.WorkHour.WorkHourDetails)]
        [HttpGet]
        public ActionResult WorkHourDetails(int id=0)
        {
            return View(new WorkHourBL().GetWorkHour(UserManager.UserInfo, id).Model);
        }

        #endregion

        [HttpPost]
        public ActionResult TeaBreakList(int workHourId=0)
        {
            return workHourId == 0
                ? Json(new {Data = new List<SelectListItem>()})
                : Json(new {Data = new WorkHourBL().GetTeaBreakList(workHourId).Data});
        }
    }
}
