using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.SparePartAssemble;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ODMSModel.SparePartSplitting;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class SparePartAssembleController : ControllerBase
    {
        #region Member Varables

        private void SetDefaults()
        {
            ViewBag.StatusList = CommonBL.ListStatusBool().Data;
        }

        #endregion

        #region SparePartAssemble Index

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleIndex)]
        [HttpGet]
        public ActionResult SparePartAssembleIndex()
        {
            SetDefaults();
 
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleIndex, CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleIndex)]//Details olabilir
        public ActionResult ListSparePartAssemble([DataSourceRequest] DataSourceRequest request, SparePartAssembleListModel model)
        {
            var sparePartAssembleBo = new SparePartAssembleBL();

            var v = new SparePartAssembleListModel(request)
                {
                    IdDealer = model.IdDealer,
                    IdPart = model.IdPart,
                    IdPartAssemble = model.IdPartAssemble,
                    IsActive = model.IsActive
                };

            var totalCnt = 0;
            var returnValue = sparePartAssembleBo.ListSparePartAssemble(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleIndex, CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleIndex)]
        public ActionResult ListSparePartSplitting([DataSourceRequest] DataSourceRequest request, SparePartSplittingListModel hModel)
        {
            var bl = new SparePartAssembleBL();
            var totalCnt = 0;

            var v = new SparePartSplittingListModel(request)
            {
                PartCode = hModel.PartCode,
                PartName = hModel.PartName
            };

            var returnValue = bl.ListSparePartSplitting(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region SparePartAssemble Create

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleIndex, CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleCreate)]
        public ActionResult SparePartAssembleCreate()
        {
            SetDefaults();

            var model = new SparePartAssembleIndexViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleIndex, CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleCreate)]
        [HttpPost]
        public ActionResult SparePartAssembleCreate(SparePartAssembleIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var sparePartAssembleBo = new SparePartAssembleBL();
            if (UserManager.UserInfo.GetUserDealerId() > 0)
            {
                viewModel.IdDealer = UserManager.UserInfo.GetUserDealerId();
            }
            var viewControlModel = new SparePartAssembleIndexViewModel
                {
                    IdPart = viewModel.IdPart,
                    IdPartAssemble = viewModel.IdPartAssemble
                };
            sparePartAssembleBo.GetSparePartAssemble(UserManager.UserInfo, viewControlModel);

            SetDefaults();

            if ((viewControlModel.Quantity == null) && (viewControlModel.IdPart != viewControlModel.IdPartAssemble))
            {
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;
                    sparePartAssembleBo.DMLSparePartAssemble(UserManager.UserInfo, viewModel);

                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                }

                return View();
            }
            else
            {
                SetMessage(MessageResource.Error_DB_RecordExists, CommonValues.MessageSeverity.Fail);

                return View(viewModel);
            }
        }

        #endregion

        #region SparePartAssemble Update
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleIndex, CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleUpdate)]
        [HttpGet]
        public ActionResult SparePartAssembleUpdate(long? idPart, long? idPartAssemble)
        {
            SetDefaults();
            var v = new SparePartAssembleIndexViewModel();
            if ((idPart > 0) && (idPartAssemble > 0))
            {
                var sparePartAssembleBo = new SparePartAssembleBL();
                if (UserManager.UserInfo.GetUserDealerId() > 0)
                {
                    v.IdDealer = UserManager.UserInfo.GetUserDealerId();
                }
                v.IdPart = idPart;
                v.IdPartAssemble = idPartAssemble;
                sparePartAssembleBo.GetSparePartAssemble(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleIndex, CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleUpdate)]
        [HttpPost]
        public ActionResult SparePartAssembleUpdate(SparePartAssembleIndexViewModel viewModel) //, IEnumerable<HttpPostedFileBase> attachments)
        {
            var sparePartAssembleBo = new SparePartAssembleBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;

                sparePartAssembleBo.DMLSparePartAssemble(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }

            SetDefaults();
            return View(viewModel);
        }

        #endregion

        #region SparePartAssemble Delete
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleIndex, ODMSCommon.CommonValues.PermissionCodes.SparePartAssemble.SparePartAssembleDelete)]
        public ActionResult DeleteSparePartAssemble(Int64 idPart, Int64 idPartAssemble)
        {
            SparePartAssembleIndexViewModel viewModel = new SparePartAssembleIndexViewModel
                {
                    IdPart = idPart,
                    IdPartAssemble = idPartAssemble
                };

            var sparePartAssembleBo = new SparePartAssembleBL();
            sparePartAssembleBo.GetSparePartAssemble(UserManager.UserInfo, viewModel);

            viewModel.CommandType = CommonValues.DMLType.Delete;

            sparePartAssembleBo.DMLSparePartAssemble(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion

        #region Split Part Usage

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartAssemble.ChangeSplitPartUsage)]
        [HttpPost]
        public ActionResult ChangeSplitPartUsage(long partId, bool usable)
        {
            new SparePartAssembleBL().ChangeSplitPartUsage(UserManager.UserInfo,partId, usable);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
        }

        #endregion

    }
}