using Kendo.Mvc.UI;
using ODMS.Controllers;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Business;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Title;
using ODMSModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class TitleController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.YesNoList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.YesNo).Data;
            ViewBag.StatusList = CommonBL.ListStatusAll(2).Data;
            ViewBag.StatusList2 = CommonBL.ListStatus().Data;
        }

        #region Index
        [AuthorizationFilter(CommonValues.PermissionCodes.Title.TitleIndex)]
        [HttpGet]
        public ActionResult TitleIndex()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Title.TitleIndex, CommonValues.PermissionCodes.Title.TitleList)]
        public ActionResult ListTitle([DataSourceRequest]DataSourceRequest request, TitleListModel model)
        {
            var titleBl = new TitleBL();
            var v = new TitleListModel(request);
            v.AdminDesc = model.AdminDesc;
            v.IsActive = model.IsActive;
            v.TitleName = model.TitleName;
            v.IsTechnician = model.IsTechnician;
            var totalCnt = 0;
            var returnValue = titleBl.ListTitle(UserManager.UserInfo, v, out totalCnt).Data;
            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        #endregion
        #region Create
        [AuthorizationFilter(CommonValues.PermissionCodes.Title.TitleIndex, CommonValues.PermissionCodes.Title.TitleCreate)]
        [HttpGet]
        public ActionResult TitleCreate()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Title.TitleIndex, CommonValues.PermissionCodes.Title.TitleCreate)]
        [HttpPost]
        public ActionResult TitleCreate(TitleIndexViewModel viewModel)
        {
            SetDefaults();
            if (ModelState.IsValid)
            {
                var TitleBo = new TitleBL();
                viewModel.CommandType = viewModel.TitleId > 0 ? CommonValues.DMLType.Update : CommonValues.DMLType.Insert;
                TitleBo.DMLTitle(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                ModelState.Clear();
                return View();

            }

            return View(viewModel);
        }
        #endregion
        #region Update
        [AuthorizationFilter(CommonValues.PermissionCodes.Title.TitleIndex, CommonValues.PermissionCodes.Title.TitleIndex)]
        [HttpGet]
        public ActionResult TitleUpdate(int id = 0)
        {
            SetDefaults();
            var v = new TitleIndexViewModel();
            if (id > 0)
            {
                var TitleBo = new TitleBL();

                v.TitleId = id;
                TitleBo.GetTitle(UserManager.UserInfo, v);
            }
            return View(v);
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.Title.TitleIndex, CommonValues.PermissionCodes.Title.TitleUpdate)]
        [HttpPost]
        public ActionResult TitleUpdate(TitleIndexViewModel viewModel)
        {
            SetDefaults();
            var TitleBo = new TitleBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = viewModel.TitleId > 0 ? CommonValues.DMLType.Update : CommonValues.DMLType.Insert;
                TitleBo.DMLTitle(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                viewModel.TitleName = (MultiLanguageModel)CommonUtility.DeepClone(viewModel.TitleName);
                viewModel.TitleName.MultiLanguageContentAsText = viewModel.MultiLanguageContentAsText;
            }
            return View(viewModel);
        }
        #endregion
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Title.TitleIndex, CommonValues.PermissionCodes.Title.TitleDelete)]
        public ActionResult DeleteTitle(int TitleId)
        {
            TitleBL rpBo = new TitleBL();
            List<TitleListModel> TitlePermissionList = rpBo.ListUserIncludedInTitle(UserManager.UserInfo, TitleId).Data;
            if (TitlePermissionList.Count == 0)
            {
                TitleIndexViewModel viewModel = new TitleIndexViewModel() { TitleId = TitleId };
                var TitleBo = new TitleBL();
                viewModel.CommandType = viewModel.TitleId > 0 ? CommonValues.DMLType.Delete : string.Empty;
                TitleBo.DMLTitle(UserManager.UserInfo, viewModel);

                ModelState.Clear();
                if (viewModel.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                          MessageResource.Global_Display_Success);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                                                      viewModel.ErrorMessage);
            }
            else
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Title_Display_Warning_PersonelExists);
            }
        }
        #endregion
        #region Details
        [AuthorizationFilter(CommonValues.PermissionCodes.Title.TitleIndex, CommonValues.PermissionCodes.Title.TitleList)]
        [HttpGet]
        public ActionResult TitleDetails(int id = 0)
        {
            var v = new TitleIndexViewModel();
            var TitleBo = new TitleBL();

            v.TitleId = id;
            TitleBo.GetTitle(UserManager.UserInfo, v);

            return View(v);
        }
        #endregion
    }
}