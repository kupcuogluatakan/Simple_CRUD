using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSBusiness.Business;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.HolidayDate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    public class HolidayDateController : ControllerBase
    {
        [PreventDirectFilter]
        private void SetDefaults()
        {
            //Status List
            List<SelectListItem> statusList = CommonBL.ListStatus().Data;
            ViewBag.StatusList = statusList;

            List<SelectListItem> languageList = LanguageBL.ListLanguageAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.LanguageList = languageList;

            ViewBag.CountryList = CommonBL.ListCountries(UserManager.UserInfo).Data;
        }

        #region HolidayDate Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateIndex)]
        [HttpGet]
        public ActionResult HolidayDateIndex(int? idHolidayDate)
        {
            SetDefaults();
            HolidayDateListModel model = new HolidayDateListModel();
            if (idHolidayDate != null)
            {
                model.IdHolidayDate = idHolidayDate;
            }
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateIndex, ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateDetails)]
        public ActionResult ListHolidayDate([DataSourceRequest] DataSourceRequest request, HolidayDateListModel model)
        {
            var HolidayDateBo = new HolidayDateBL();
            var v = new HolidayDateListModel(request);
            var totalCnt = 0;
            v.IdHolidayDate = model.IdHolidayDate;
            v.HolidayDate = model.HolidayDate;
            v.IdCountry = model.IdCountry;
            v.LanguageCode = model.LanguageCode;
            v.Description = model.Description;
            var returnValue = HolidayDateBo.ListHolidayDate(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region HolidayDate Create
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateIndex, ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateCreate)]
        [HttpGet]
        public ActionResult HolidayDateCreate()
        {
            var model = new HolidayDateViewModel();
            model.HolidayDate = DateTime.Now;
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateIndex, ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateCreate)]
        [HttpPost]
        public ActionResult HolidayDateCreate(HolidayDateViewModel viewModel)
        {
            SetDefaults();
            var HolidayDateBo = new HolidayDateBL();

            if (ModelState.IsValid)
            {
                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();
                HolidayDateViewModel model = new HolidayDateViewModel
                {
                    IdHolidayDate = viewModel.IdHolidayDate
                };
                viewModel.CommandType = CommonValues.DMLType.Insert;
                HolidayDateBo.DMLHolidayDate(UserManager.UserInfo, viewModel);
                return View(model);
            }
            CheckErrorForMessage(viewModel, true);
            return View(viewModel);
        }

        #endregion

        #region HolidayDate Update

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateIndex, ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateUpdate)]
        [HttpGet]
        public ActionResult HolidayDateUpdate(int? IdHolidayDate)
        {
            SetDefaults();
            var v = new HolidayDateViewModel();
            if (IdHolidayDate != null && IdHolidayDate > 0)
            {
                var contractBo = new HolidayDateBL();
                v.IdHolidayDate = IdHolidayDate;
                contractBo.GetHolidayDate(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateIndex, ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateUpdate)]
        [HttpPost]
        public ActionResult HolidayDateUpdate(HolidayDateViewModel viewModel)
        {
            SetDefaults();
            var contractBo = new HolidayDateBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                contractBo.DMLHolidayDate(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region HolidayDate Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateIndex, ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateDelete)]
        public ActionResult DeleteHolidayDate(int? idHolidayDate)
        {
            HolidayDateViewModel viewModel = new HolidayDateViewModel() { IdHolidayDate = idHolidayDate };
            var contractBo = new HolidayDateBL();
            viewModel.CommandType = !string.IsNullOrEmpty(viewModel.IdHolidayDate.ToString()) ? CommonValues.DMLType.Delete : string.Empty;
            contractBo.DMLHolidayDate(UserManager.UserInfo, viewModel);

            ModelState.Clear();

            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region HolidayDate Details
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateIndex, ODMSCommon.CommonValues.PermissionCodes.HolidayDate.HolidayDateDetails)]
        [HttpGet]
        public ActionResult HolidayDateDetails(int? idHolidayDate)
        {
            var v = new HolidayDateViewModel();
            var HolidayDateBo = new HolidayDateBL();

            v.IdHolidayDate = idHolidayDate;
            HolidayDateBo.GetHolidayDate(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}
