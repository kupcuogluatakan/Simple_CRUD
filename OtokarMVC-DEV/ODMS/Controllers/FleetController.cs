using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Fleet;
using System;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class FleetController : ControllerBase
    {
        #region Member Varables

        private void SetDefaults()
        {
            ViewBag.YesNoList = CommonBL.ListYesNoValueInt().Data;
        }

        #endregion

        #region Fleet Index

        [AuthorizationFilter(CommonValues.PermissionCodes.Fleet.FleetIndex)]
        [HttpGet]
        public ActionResult FleetIndex()
        {
            SetDefaults();

            FleetListModel model = new FleetListModel() { IsActive = true };

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Fleet.FleetIndex, CommonValues.PermissionCodes.Fleet.FleetIndex)]
        public ActionResult ListFleet([DataSourceRequest] DataSourceRequest request, FleetListModel model)
        {
            var fleetBo = new FleetBL();

            var v = new FleetListModel(request)
            {
                FleetName = model.FleetName,
                FleetCode = model.FleetCode,
                IsActive = model.IsActive,
                IsConstricted = model.IsConstricted
            };

            var totalCnt = 0;
            var returnValue = fleetBo.ListFleet(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Fleet Create

        [AuthorizationFilter(CommonValues.PermissionCodes.Fleet.FleetIndex, CommonValues.PermissionCodes.Fleet.FleetCreate)]
        public ActionResult FleetCreate()
        {
            SetDefaults();
            var model = new FleetViewModel() { IsVinControl = true, IsActive = true };

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Fleet.FleetIndex, CommonValues.PermissionCodes.Fleet.FleetCreate)]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult FleetCreate(FleetViewModel viewModel)
        {
            var fleetBo = new FleetBL();

            FleetViewModel viewControlModel = new FleetViewModel();
            viewControlModel.IdFleet = viewModel.IdFleet;

            fleetBo.GetFleet(UserManager.UserInfo, viewControlModel);

            SetDefaults();

            if (viewControlModel.OtokarPartDiscount == null)
            {
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;

                    fleetBo.DMLFleet(UserManager.UserInfo, viewModel);

                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                }

                return View();
            }
            else
            {
                SetMessage(MessageResource.CustomerDiscount_Error_DataHave, CommonValues.MessageSeverity.Fail);

                return View(viewModel);
            }
        }

        #endregion

        #region Fleet Update
        [AuthorizationFilter(CommonValues.PermissionCodes.Fleet.FleetIndex, CommonValues.PermissionCodes.Fleet.FleetUpdate)]
        [HttpGet]
        public ActionResult FleetUpdate(Int32? idFleet)
        {
            SetDefaults();
            var v = new FleetViewModel();
            if (idFleet != 0)
            {
                var fleetBo = new FleetBL();
                v.IdFleet = idFleet;
                fleetBo.GetFleet(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Fleet.FleetIndex, CommonValues.PermissionCodes.Fleet.FleetUpdate)]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult FleetUpdate(FleetViewModel viewModel)
        {
            var fleetBo = new FleetBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;

                fleetBo.DMLFleet(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }

            SetDefaults();
            return View(viewModel);
        }

        #endregion

        #region Fleet Delete

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Fleet.FleetIndex, CommonValues.PermissionCodes.Fleet.FleetDelete)]
        public ActionResult DeleteFleet(int idFleet)
        {
            FleetViewModel viewModel = new FleetViewModel { IdFleet = idFleet };

            var fleetBo = new FleetBL();

            viewModel.CommandType = CommonValues.DMLType.Delete;

            fleetBo.DMLFleet(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }

        #endregion
    }
}