using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.ListModel;
using ODMSModel.SparePartType;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class SparePartTypeController : ControllerBase
    {
        private void SetDefaults()
        {
            List<SelectListItem> statusList = CommonBL.ListStatus().Data;
            ViewBag.StatusList = statusList;
        }

        #region SparePartType Index

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartType.SparePartTypeIndex)]
        [HttpGet]
        public ActionResult SparePartTypeIndex()
        {
            SetDefaults();
            var model = new SparePartTypeListModel();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartType.SparePartTypeIndex, CommonValues.PermissionCodes.SparePartType.SparePartTypeDetails)]
        public ActionResult ListSparePartType([DataSourceRequest] DataSourceRequest request, SparePartTypeListModel model)
        {
            var sparePartTypeBo = new SparePartTypeBL();
            var v = new SparePartTypeListModel(request);
            var totalCnt = 0;
            v.AdminDesc = model.AdminDesc;
            v.PartTypeCode = model.PartTypeCode;
            v.PartTypeName = model.PartTypeName;
            v.IsActive = model.IsActive;
            var returnValue = sparePartTypeBo.ListSparePartTypes(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region SparePartType Create
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartType.SparePartTypeIndex, CommonValues.PermissionCodes.SparePartType.SparePartTypeCreate)]
        [HttpGet]
        public ActionResult SparePartTypeCreate()
        {
            SetDefaults();
            var model = new SparePartTypeIndexViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartType.SparePartTypeIndex, CommonValues.PermissionCodes.SparePartType.SparePartTypeCreate)]
        [HttpPost]
        public ActionResult SparePartTypeCreate(SparePartTypeIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var sparePartTypeBo = new SparePartTypeBL();

            if (ModelState.IsValid)
            {
                viewModel.CommandType = !String.IsNullOrEmpty(viewModel.PartTypeCode)
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;
                sparePartTypeBo.DMLSparePartType(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();
            }
            return View(viewModel);
        }

        #endregion

        #region SparePartType Update
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartType.SparePartTypeIndex, CommonValues.PermissionCodes.SparePartType.SparePartTypeUpdate)]
        [HttpGet]
        public ActionResult SparePartTypeUpdate(string id)
        {
            var v = new SparePartTypeIndexViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                SetDefaults();
                var sparePartTypeBo = new SparePartTypeBL();
                v.PartTypeCode = id;
                sparePartTypeBo.GetSparePartType(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartType.SparePartTypeIndex, CommonValues.PermissionCodes.SparePartType.SparePartTypeUpdate)]
        [HttpPost]
        public ActionResult SparePartTypeUpdate(SparePartTypeIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var sparePartTypeBo = new SparePartTypeBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = !String.IsNullOrEmpty(viewModel.PartTypeCode)
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;
                sparePartTypeBo.DMLSparePartType(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region SparePartType Delete
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartType.SparePartTypeIndex, CommonValues.PermissionCodes.SparePartType.SparePartTypeDelete)]
        public ActionResult DeleteSparePartType(string sparePartTypeCode)
        {
            SparePartTypeIndexViewModel viewModel = new SparePartTypeIndexViewModel() { PartTypeCode = sparePartTypeCode };
            var sparePartTypeBo = new SparePartTypeBL();
            viewModel.CommandType = !string.IsNullOrEmpty(viewModel.PartTypeCode) ? CommonValues.DMLType.Delete : string.Empty;
            sparePartTypeBo.DMLSparePartType(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion

        #region SparePartType Details
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartType.SparePartTypeIndex, CommonValues.PermissionCodes.SparePartType.SparePartTypeDetails)]
        [HttpGet]
        public ActionResult SparePartTypeDetails(string id)
        {
            var v = new SparePartTypeIndexViewModel();
            var sparePartTypeBo = new SparePartTypeBL();

            v.PartTypeCode = id;
            sparePartTypeBo.GetSparePartType(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}