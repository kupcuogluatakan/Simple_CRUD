using System;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.VatRatio;
using Permission = ODMSCommon.CommonValues.PermissionCodes.VatRatio;
using PermissionExp = ODMSCommon.CommonValues.PermissionCodes.VatRatioExp;
namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class VatRatioController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(Permission.VatRatioIndex)]
        public ActionResult VatRatioIndex()
        {
            return View();
        }

        [HttpPost]
        [AuthorizationFilter(Permission.VatRatioIndex)]
        public ActionResult ListVatRatios([DataSourceRequest] DataSourceRequest request, string IsActive)
        {
            var bus = new VatRatioBL();
            var model = new VatRatioListModel(request);
            var totalCount = 0;

            if (!string.IsNullOrEmpty(IsActive))
            {
                model.SearchIsActive = Convert.ToBoolean(Convert.ToInt16(IsActive));
            }
            else
            {
                model.SearchIsActive = null;
            }


            var list = bus.ListVatRatios(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = list,
                Total = totalCount
            });
        }


        [HttpGet]
        [AuthorizationFilter(Permission.VatRatioIndex, Permission.VatRatioCreate)]
        public ActionResult VatRatioCreate()
        {
            var model = new VatRatioModel { IsActive = true };
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.VatRatioIndex, Permission.VatRatioCreate)]
        public ActionResult VatRatioCreate(VatRatioModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                model.IsActive = true;
                new VatRatioBL().DMLVatRatio(UserManager.UserInfo, model);

                CheckErrorForMessage(model, true);

                if (model.ErrorNo != 1)
                    ModelState.Clear();
                return View();
            }
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(Permission.VatRatioIndex, Permission.VatRatioCreate)]
        public ActionResult VatRatioUpdate(decimal VatRatio, bool IsActive, string InvoiceLabel)
        {
            var model = new VatRatioModel { VatRatio = VatRatio, IsActive = IsActive, InvoiceLabel = InvoiceLabel };
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.VatRatioIndex, Permission.VatRatioCreate)]
        public ActionResult VatRatioUpdate(VatRatioModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                // model.IsActive = true;//Setting it true at create
                new VatRatioBL().DMLVatRatio(UserManager.UserInfo, model);

                CheckErrorForMessage(model, true);

                ModelState.Clear();
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.VatRatioIndex, Permission.VatRatioDelete)]
        public ActionResult VatRatioDelete(decimal vatRatio)
        {
            var bus = new VatRatioBL();
            var model = new VatRatioModel { CommandType = CommonValues.DMLType.Delete, VatRatio = vatRatio };
            bus.DMLVatRatio(UserManager.UserInfo, model);
            // CheckErrorForMessage(model, true);
            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        #region Vat Ratio Expl

        [HttpGet]
        [AuthorizationFilter(Permission.VatRatioIndex)]
        public ActionResult VatRatioExp(decimal vatRatio)
        {

            return PartialView("_VatRatioExp", new VatRatioModel { VatRatio = vatRatio });
        }

        [HttpPost]
        [AuthorizationFilter(Permission.VatRatioIndex)]
        public ActionResult ListVatRatioExps([DataSourceRequest] DataSourceRequest request, decimal vatRatio)
        {
            var bus = new VatRatioBL();
            var model = new VatRatioExpListModel(request);
            var totalCount = 0;
            model.VatRatio = vatRatio;
            var list = bus.ListVatRatioExps(UserManager.UserInfo, model, out totalCount);

            return Json(new
            {
                Data = list.Data,
                Total = list.Total
            });
        }


        #region VatRatioExp Create
        [HttpGet]
        [AuthorizationFilter(PermissionExp.VatRatioExpIndex, PermissionExp.VatRatioExpCreate)]
        public ActionResult VatRatioExpCreate(decimal? vatRatio)
        {
            var model = new VatRatioExpModel { VatRatio = vatRatio.GetValueOrDefault() };
            FillComboBoxes();
            return View("VatRatioExpCreate", model);
        }
        [HttpPost]
        [AuthorizationFilter(PermissionExp.VatRatioExpIndex, PermissionExp.VatRatioExpCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult VatRatioExpCreate(VatRatioExpModel model)
        {
            FillComboBoxes();
            if (ModelState.IsValid == false)
                return View(model);
            var bus = new VatRatioBL();
            model.CommandType = CommonValues.DMLType.Insert;
            bus.DMLVatRatioExp(UserManager.UserInfo, model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return View(new VatRatioExpModel() { VatRatio = model.VatRatio });
        }
        #endregion

        #region Maintenance Labour Update
        [HttpGet]
        [AuthorizationFilter(PermissionExp.VatRatioExpIndex, PermissionExp.VatRatioExpUpdate)]
        public ActionResult VatRatioExpUpdate(decimal? vatRatio, int? countryId)
        {
            FillComboBoxes();
            if (!(vatRatio.HasValue && vatRatio > 0) || !(countryId.HasValue && countryId > 0))
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View();
            }
            return View(new VatRatioBL().GetVatRatioExp(UserManager.UserInfo, vatRatio.GetValueOrDefault(), countryId.GetValueOrDefault()).Model);
        }
        [HttpPost]
        [AuthorizationFilter(PermissionExp.VatRatioExpIndex, PermissionExp.VatRatioExpUpdate)]
        [ValidateAntiForgeryToken]
        public ActionResult VatRatioExpUpdate(VatRatioExpModel model)
        {
            FillComboBoxes();
            if (ModelState.IsValid == false)
                return View(model);
            var bus = new VatRatioBL();
            model.CommandType = CommonValues.DMLType.Update;
            bus.DMLVatRatioExp(UserManager.UserInfo,model);
            CheckErrorForMessage(model, true);
            ModelState.Clear();
            return View(model);
        }
        #endregion

        #region Maintenance Labour Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(PermissionExp.VatRatioExpIndex, PermissionExp.VatRatioExpDelete)]
        public ActionResult VatRatioExpDelete(decimal? vatRatio, int? countryId)
        {
            if (!(vatRatio.HasValue && vatRatio > 0) || !(countryId.HasValue && countryId > 0))
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_NoRecordFound);
            }
            var bus = new VatRatioBL();
            var model = new VatRatioExpModel
                {
                    VatRatio = vatRatio.GetValueOrDefault(),
                    CountryId = countryId.GetValueOrDefault(),
                    CommandType = CommonValues.DMLType.Delete
                };
            bus.DMLVatRatioExp(UserManager.UserInfo,model);
            ModelState.Clear();
            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                model.ErrorMessage);
        }
        #endregion

        #region Maintenance Labour Details
        [HttpGet]
        [AuthorizationFilter(PermissionExp.VatRatioExpIndex, PermissionExp.VatRatioExpDetails)]
        public ActionResult VatRatioExpDetails(decimal? vatRatio, int? countryId)
        {
            if (!(vatRatio.HasValue && vatRatio > 0) || !(countryId.HasValue && countryId > 0))
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View();
            }
            return View(new VatRatioBL().GetVatRatioExp(UserManager.UserInfo, vatRatio.GetValueOrDefault(), countryId.GetValueOrDefault()).Model);
        }
        #endregion

        #region Private Methods

        private void FillComboBoxes()
        {
            ViewBag.CountryList = CommonBL.ListCountries(UserManager.UserInfo).Data;
        }

        #endregion



        #endregion
    }
}
