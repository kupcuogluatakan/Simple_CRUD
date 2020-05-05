using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.Fleet;
using ODMSModel.FleetPartPartial;
using ODMSCommon;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class FleetPartPartialController : ControllerBase
    {
        #region Fields

        private readonly SparePartBL _sparePartService = new SparePartBL();
        private readonly FleetPartPartialBL _fleetPartPartialService = new FleetPartPartialBL();

        #endregion

        #region Index

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.FleetPartPartial.FleetPartPartialIndex)]
        public ActionResult FleetPartPartialIndex(int id)
        {
            if (id == 0)
                return RedirectToAction("Index", "ErrorPage");

            var model = new FleetPartViewModel { FleetId = id };

            return PartialView(model);
        }

        #endregion

        #region Create

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.FleetPartPartial.FleetPartPartialCreate)]
        public ActionResult FleetPartPartialCreate(int fleetId)
        {
            var model = new FleetPartViewModel { FleetId = fleetId };

            return PartialView(model);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.FleetPartPartial.FleetPartPartialCreate)]
        public ActionResult FleetPartPartialExcelCreate(int fleetId)
        {
            var model = new FleetPartViewModel { FleetId = fleetId };

            return PartialView(model);
        }


        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.FleetPartPartial.FleetPartPartialCreate)]
        public ActionResult FleetPartPartialCreate(FleetPartViewModel model)
        {
            if (_fleetPartPartialService.IsPartConstricted(UserManager.UserInfo, model).Model)
            {
                if (model.PartId.HasValue)
                {
                    model.PartId = model.PartId;
                    model.CommandType = CommonValues.DMLType.Insert;

                    _fleetPartPartialService.Insert(UserManager.UserInfo, model);

                    foreach (var item in ModelState.Keys.Where(item => item != "FleetId"))
                        ModelState[item].Value = new ValueProviderResult(string.Empty, string.Empty, CultureInfo.CurrentCulture);

                }
                else
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.FleetPartPartial_Invalid_Product;
                }
            }
            else
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.FleetPartPartial_Constricted_Part;
            }

            CheckErrorForMessage(model, true);

            return PartialView(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.FleetPartPartial.FleetPartPartialCreate)]
        public ActionResult FleetPartPartialExcelCreate(HttpPostedFileBase file, FleetPartViewModel model)
        {
            var fModel = new FleetViewModel { IdFleet = model.FleetId };
            var fBo = new FleetBL();
            fBo.GetFleet(UserManager.UserInfo, fModel);
            if (!(fModel.StartDateValid <= DateTime.Now && fModel.EndDateValid >= DateTime.Now))
            {
                SetMessage(MessageResource.Error_DB_RecordOutOfDate, CommonValues.MessageSeverity.Fail);
                return PartialView(model);
            }
            else
            {
                if (_fleetPartPartialService.IsPartConstricted(UserManager.UserInfo, model).Model)
                {
                    FleetPartPartialBL fleetePartPartialService = new FleetPartPartialBL();
                    List<FleetPartViewModel> listModels = new List<FleetPartViewModel>();

                    if (file != null &&
                        Path.GetExtension(file.FileName).ToLower().IndexOf(CommonValues.ExcelExtOld) != -1 &&
                        Path.GetExtension(file.FileName).ToLower().IndexOf(CommonValues.ExcelExtNew) != 1)
                    {
                        model.CommandType = CommonValues.DMLType.Insert;
                        using (Stream s = file.InputStream)
                        {
                            listModels = fleetePartPartialService.ParseExcel(UserManager.UserInfo, model, s).Data;
                        }
                    }
                    else
                    {
                        model.ErrorNo = 1;
                        model.ErrorMessage = MessageResource.FleetPartPartial_Invalid_File;
                    }

                    if (listModels.Exists(q => q.ErrorNo >= 1))
                    {
                        var ms = fleetePartPartialService.SetExcelReport(listModels, model.ErrorMessage);

                        var fileViewModel = new DownloadFileViewModel
                        {
                            FileName = CommonValues.ErrorLog + DateTime.Now + CommonValues.ExcelExtOld,
                            ContentType = CommonValues.ExcelContentType,
                            MStream = ms,
                            Id = Guid.NewGuid()
                        };

                        Session[CommonValues.DownloadFileFormatCookieKey] = fileViewModel.Add(fileViewModel);
                        TempData[CommonValues.DownloadFileIdCookieKey] = fileViewModel.Id;

                        SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);

                        return PartialView(model);
                    }
                    else
                    {
                        var fleetPartData = new FleetPartPartialBL();
                        fleetPartData.Insert(UserManager.UserInfo, model, listModels);
                        CheckErrorForMessage(model, true);
                        return PartialView(model);
                    }
                }
                else
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.FleetPartPartial_Constricted_Part;

                    CheckErrorForMessage(model, true);
                    return PartialView(model);
                }
            }
        }

        #endregion

        #region Delete

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.FleetPartPartial.FleetPartPartialDelete)]
        public ActionResult FleetPartPartialDelete(FleetPartViewModel model)
        {
            model.CommandType = ODMSCommon.CommonValues.DMLType.Delete;
            _fleetPartPartialService.Delete(UserManager.UserInfo, model);

            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_Unexpected);
        }

        #endregion

        #region Select

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.FleetPartPartial.FleetPartPartialSelect)]
        public ActionResult ListFleetPartPartial([DataSourceRequest]DataSourceRequest request, FleetPartPartialListModel model)
        {
            var referenceModel = new FleetPartPartialListModel(request) { FleetId = model.FleetId };

            int totalCnt = 0;

            var returnValue = _fleetPartPartialService.List(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        public ActionResult ExcelSample()
        {
            var bo = new FleetPartPartialBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.FleetPartPartial_PageTitle_Index + CommonValues.ExcelExtOld);
        }
    }
}
