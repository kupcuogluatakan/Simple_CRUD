using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using System.Linq;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.DealerTechnicianGroup;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerTechnicianGroupController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.StatusList = CommonBL.ListStatus().Data;
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.VehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            ViewBag.WorkshopTypeList = WorkshopTypeBL.ListWorkshopTypeAsSelectList(UserManager.UserInfo).Data;
        }

        #region Dealer Technician Group Index
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupIndex)]
        public ActionResult DealerTechnicianGroupIndex()
        {
            SetDefaults();
            DealerTechnicianGroupListModel model = new DealerTechnicianGroupListModel();
            if (UserManager.UserInfo.GetUserDealerId() > 0)
            {
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            }
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupIndex, CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupDetails)]
        public ActionResult ListDealerTechnicianGroups([DataSourceRequest]DataSourceRequest request, DealerTechnicianGroupListModel model)
        {
            var bo = new DealerTechnicianGroupBL();
            var referenceModel = new DealerTechnicianGroupListModel(request)
                {
                    DealerId = model.DealerId, 
                    IsActive = model.IsActive,
                    VehicleModelKod = model.VehicleModelKod,
                    TechnicianGroupName = model.TechnicianGroupName,
                    WorkshopTypeId = model.WorkshopTypeId
                };
            int totalCnt;
            var returnValue = bo.ListDealerTechnicianGroups(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion

        #region Dealer Technician Group Create
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupIndex, CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupCreate)]
        public ActionResult DealerTechnicianGroupCreate()
        {
            SetDefaults();
            DealerTechnicianGroupViewModel model = new DealerTechnicianGroupViewModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();

            model.IsActive = true;
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupIndex, CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupCreate)]
        public ActionResult DealerTechnicianGroupCreate(DealerTechnicianGroupViewModel model)
        {
            SetDefaults();
            
            if (ModelState.IsValid)
            {
                var bo = new DealerTechnicianGroupBL();
                //int relationCount = bo.GetDealerVehicleGroupRelationCount(model.DealerId.GetValue<int>(), model.VehicleGroupId.GetValue<int>());
                //if (relationCount > 0)
                //{
                    model.CommandType = CommonValues.DMLType.Insert;
                    bo.DMLDealerTechnicianGroup(UserManager.UserInfo, model);
                    CheckErrorForMessage(model, true);
                    ModelState.Clear();

                    DealerTechnicianGroupViewModel returnModel = new DealerTechnicianGroupViewModel();
                    returnModel.DealerId = model.DealerId;

                    return View(returnModel);
                //return View();
                //}
                //else
                //{
                //    SetMessage(MessageResource.DealerTechnicianGroup_Warning_DealerName, CommonValues.MessageSeverity.Fail);
                //}
            }
            return View(model);
        }
        #endregion

        #region Dealer Technician Group Update
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupIndex, CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupUpdate)]
        public ActionResult DealerTechnicianGroupUpdate(int dealerTechnicianGroupId = 0)
        {
            SetDefaults();
            var referenceModel = new DealerTechnicianGroupViewModel();
            if (dealerTechnicianGroupId > 0)
            {
                var bo = new DealerTechnicianGroupBL();
                referenceModel.DealerTechnicianGroupId = dealerTechnicianGroupId;
                referenceModel = bo.GetDealerTechnicianGroup(UserManager.UserInfo, referenceModel).Model;

                List<SelectListItem> workshopTypeList = WorkshopTypeBL.ListWorkshopTypeAsSelectList(UserManager.UserInfo).Data;
                var control = (from wt in workshopTypeList.AsEnumerable()
                               where wt.Value.GetValue<int>() == referenceModel.WorkshopTypeId
                               select wt);
                if (!control.Any())
                {
                    referenceModel.WorkshopTypeId = null;
                }
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupIndex, CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupUpdate)]
        public ActionResult DealerTechnicianGroupUpdate(DealerTechnicianGroupViewModel viewModel)
        {
            SetDefaults();
            var bo = new DealerTechnicianGroupBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                bo.DMLDealerTechnicianGroup(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                ModelState.Clear();
                //bo.GetDealerTechnicianGroup(viewModel);
            }
            return View(viewModel);
        }
        #endregion

        #region Dealer Technician Group Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupIndex, CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupDelete)]
        public ActionResult DealerTechnicianGroupDelete(int dealerTechnicianGroupId)
        {
            ViewBag.HideElements = false;

            var bo = new DealerTechnicianGroupBL();
            var model = new DealerTechnicianGroupViewModel { DealerTechnicianGroupId = dealerTechnicianGroupId, CommandType = CommonValues.DMLType.Delete };
            bo.DMLDealerTechnicianGroup(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }
        #endregion

        #region Dealer Technician Group Details
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupIndex, CommonValues.PermissionCodes.DealerTechnicianGroup.DealerTechnicianGroupDetails)]
        public ActionResult DealerTechnicianGroupDetails(int dealerTechnicianGroupId)
        {
            SetDefaults();
            var referenceModel = new DealerTechnicianGroupViewModel { DealerTechnicianGroupId = dealerTechnicianGroupId };
            var bo = new DealerTechnicianGroupBL();

            var model = bo.GetDealerTechnicianGroup(UserManager.UserInfo, referenceModel).Model;

            List<SelectListItem> workshopTypeList = WorkshopTypeBL.ListWorkshopTypeAsSelectList(UserManager.UserInfo).Data;
            var control = (from wt in workshopTypeList.AsEnumerable()
                           where wt.Value.GetValue<int>() == model.WorkshopTypeId
                           select wt);
            if (!control.Any())
            {
                model.WorkshopTypeId = null;
            }

            return View(model);
        }
        #endregion
    }
}
