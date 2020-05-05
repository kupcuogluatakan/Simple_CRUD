using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.SparePartCountryVatRatio;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class SparePartCountryVatRatioController : ControllerBase
    {
        #region Member Varables

        private void SetDefaults()
        {
            ViewBag.CountryList = SparePartCountryVatRatioBL.ListCountryNameAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.StatusList = CommonBL.ListStatus().Data;
            ViewBag.VatRatioList = CommonBL.ListVatRatio().Data;
        }

        #endregion

        #region SparePartCountryVatRatio Index

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartCountryVatRatio.SparePartCountryVatRatioIndex)]
        [HttpGet]
        public ActionResult SparePartCountryVatRatioIndex()
        {
            SetDefaults();
            SparePartCountryVatRatioListModel model = new SparePartCountryVatRatioListModel();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartCountryVatRatio.SparePartCountryVatRatioIndex, CommonValues.PermissionCodes.SparePartCountryVatRatio.SparePartCountryVatRatioIndex)]//Details olabilir
        public ActionResult ListSparePartCountryVatRatio([DataSourceRequest] DataSourceRequest request, SparePartCountryVatRatioListModel model)
        {
            var sparePartCountryVatRatioBo = new SparePartCountryVatRatioBL();

            var v = new SparePartCountryVatRatioListModel(request)
                {
                    IdPart = model.IdPart,
                    IdCountry = model.IdCountry,
                    PartCode = model.PartCode,
                    IsActive = model.IsActive
                };

            var totalCnt = 0;
            var returnValue = sparePartCountryVatRatioBo.ListSparePartCountryVatRatio(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region SparePartCountryVatRatio Create

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartCountryVatRatio.SparePartCountryVatRatioIndex, CommonValues.PermissionCodes.SparePartCountryVatRatio.SparePartCountryVatRatioCreate)]
        public ActionResult SparePartCountryVatRatioCreate()
        {
            SetDefaults();

            var model = new SparePartCountryVatRatioViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartCountryVatRatio.SparePartCountryVatRatioIndex, CommonValues.PermissionCodes.SparePartCountryVatRatio.SparePartCountryVatRatioCreate)]
        [HttpPost]
        public ActionResult SparePartCountryVatRatioCreate(SparePartCountryVatRatioViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            var sparePartCountryVatRatioBo = new SparePartCountryVatRatioBL();

            var viewControlModel = new SparePartCountryVatRatioViewModel
                {
                    IdPart = viewModel.IdPart,
                    IdCountry = viewModel.IdCountry
                };
            sparePartCountryVatRatioBo.GetSparePartCountryVatRatio(UserManager.UserInfo, viewControlModel);

            SetDefaults();

            if ((viewControlModel.VatRatio == null))
            {
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;
                    sparePartCountryVatRatioBo.DMLSparePartCountryVatRatio(UserManager.UserInfo, viewModel);
                }
            }
            else
            {
                viewModel.IdPart = viewControlModel.IdPart;
                viewModel.IdCountry = viewControlModel.IdCountry;
                viewModel.IsActive = true;
                viewModel.CommandType = CommonValues.DMLType.Update;
                sparePartCountryVatRatioBo.DMLSparePartCountryVatRatio(UserManager.UserInfo, viewModel);
            }
            CheckErrorForMessage(viewModel, true);

            ModelState.Clear();
            var model = new SparePartCountryVatRatioViewModel();
            model.IsActive = true;
            return View(model);
        }

        #endregion

        #region SparePartCountryVatRatio Update
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartCountryVatRatio.SparePartCountryVatRatioIndex, CommonValues.PermissionCodes.SparePartCountryVatRatio.SparePartCountryVatRatioUpdate)]
        [HttpGet]
        public ActionResult SparePartCountryVatRatioUpdate(Int64? idPart, int? idCountry)
        {
            SetDefaults();
            var v = new SparePartCountryVatRatioViewModel();
            if ((idPart > 0) && (idCountry > 0))
            {
                var sparePartCountryVatRatioBo = new SparePartCountryVatRatioBL();
                
                v.IdPart = idPart;
                v.IdCountry = idCountry;
                sparePartCountryVatRatioBo.GetSparePartCountryVatRatio(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartCountryVatRatio.SparePartCountryVatRatioIndex, CommonValues.PermissionCodes.SparePartCountryVatRatio.SparePartCountryVatRatioUpdate)]
        [HttpPost]
        public ActionResult SparePartCountryVatRatioUpdate(SparePartCountryVatRatioViewModel viewModel) //, IEnumerable<HttpPostedFileBase> attachments)
        {
            var sparePartCountryVatRatioBo = new SparePartCountryVatRatioBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;

                sparePartCountryVatRatioBo.DMLSparePartCountryVatRatio(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }

            SetDefaults();
            return View(viewModel);
        }

        #endregion

        #region SparePartCountryVatRatio Delete
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.SparePartCountryVatRatio.SparePartCountryVatRatioIndex, CommonValues.PermissionCodes.SparePartCountryVatRatio.SparePartCountryVatRatioDelete)]
        public ActionResult DeleteSparePartCountryVatRatio(Int64 idPart, int idCountry)
        {
            SparePartCountryVatRatioViewModel viewModel = new SparePartCountryVatRatioViewModel
                {
                    IdPart = idPart,
                    IdCountry = idCountry
                };

            var sparePartCountryVatRatioBo = new SparePartCountryVatRatioBL();
            sparePartCountryVatRatioBo.GetSparePartCountryVatRatio(UserManager.UserInfo, viewModel);

            viewModel.CommandType = CommonValues.DMLType.Passive;

            sparePartCountryVatRatioBo.DMLSparePartCountryVatRatio(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion
    }
}