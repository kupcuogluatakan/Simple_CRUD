using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.Fleet;
using ODMSModel.FleetVehicle;
using Perm = ODMSCommon.CommonValues.PermissionCodes.FleetVehicle;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class FleetVehicleController : ControllerBase
    {
        #region Get Actions

        [HttpGet]
        [AuthorizationFilter(Perm.FleetVehicleIndex)]
        public ActionResult FleetVehicleIndex(int id = 0)//Fleet Id
        {
            return PartialView(new FleetVehicleViewModel { HideElements = id == 0, FleetId = id });
        }

        [HttpGet]
        [AuthorizationFilter(Perm.FleetVehicleIndex, Perm.FleetVehicleCreate)]
        public ActionResult FleetVehicleCreate(int fleetId)
        {
            ViewBag.FleetId = fleetId;
            return View();
        }

        [HttpGet]
        [AuthorizationFilter(Perm.FleetVehicleIndex, Perm.FleetVehicleUpdate)]
        public ActionResult FleetVehicleUpdate(int fleetVehicleId)
        {
            var model = new FleetVehicleBL().GetFleetVehicle(fleetVehicleId).Model;
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(Perm.FleetVehicleIndex, Perm.FleetVehicleDetails)]
        public ActionResult FleetVehicleDetails(int fleetVehicleId)
        {
            var model = new FleetVehicleBL().GetFleetVehicle(fleetVehicleId).Model;
            return View(model);
        }

        #endregion

        #region Post Actions

        [HttpPost]
        [AuthorizationFilter(Perm.FleetVehicleIndex, Perm.FleetVehicleCreate)]
        //[ValidateAntiForgeryToken]
        public ActionResult FleetVehicleCreate(FleetVehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Insert;
                new FleetVehicleBL().DMLFleetVehicle(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                return RedirectToAction("FleetVehicleCreate", new { fleetId = model.FleetId });
            }

            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(Perm.FleetVehicleIndex, Perm.FleetVehicleCreate)]
        //[ValidateAntiForgeryToken]
        public ActionResult FleetVehicleUpdate(FleetVehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CommandType = CommonValues.DMLType.Update;
                new FleetVehicleBL().DMLFleetVehicle(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                return RedirectToAction("FleetVehicleUpdate", new { fleetVehicleId = model.FleetVehicleId });
            }

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AuthorizationFilter(Perm.FleetVehicleIndex, Perm.FleetVehicleDelete)]
        public ActionResult FleetVehicleDelete(int? FleetVehicleId)
        {
            if (!(FleetVehicleId.HasValue && FleetVehicleId > 0))
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_NoRecordFound);
            }

            var bus = new FleetVehicleBL();
            var model = new FleetVehicleViewModel
            {
                FleetVehicleId = FleetVehicleId ?? 0,
                CommandType = CommonValues.DMLType.Delete
            };
            bus.DMLFleetVehicle(UserManager.UserInfo, model);

            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpPost]
        [AuthorizationFilter(Perm.FleetVehicleIndex)]
        public ActionResult ListFleetVehicles([DataSourceRequest]DataSourceRequest request, int fleetId)
        {
            var bus = new FleetVehicleBL();
            var model = new FleetVehicleListModel(request) { FleetId = fleetId };
            var totalCnt = 0;
            var returnValue = bus.ListFleetVehicle(model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        [HttpGet]
        public ActionResult FleetVehicleExcelCreate(int fleetId)
        {
            var model = new FleetVehicleViewModel() { FleetId = fleetId };

            return PartialView(model);
        }
        public ActionResult ExcelSample()
        {
            var bo = new FleetVehicleBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.FleetVehicle_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        [HttpPost]
        public ActionResult FleetVehicleExcelCreate(HttpPostedFileBase file, FleetVehicleViewModel model)
        {
            var listModel = new List<FleetVehicleViewModel>();
            var fModel = new FleetViewModel { IdFleet = model.FleetId };
            var fBo = new FleetBL();

            var bl = new FleetVehicleBL();
            fBo.GetFleet(UserManager.UserInfo, fModel);

            if (!(fModel.StartDateValid <= DateTime.Now && fModel.EndDateValid >= DateTime.Now))
            {
                SetMessage(MessageResource.Error_DB_RecordOutOfDate, CommonValues.MessageSeverity.Fail);
                return PartialView(model);
            }
            else
            {
                if (file != null && Path.GetExtension(file.FileName).ToLower().IndexOf(CommonValues.ExcelExtOld) != -1 &&
                    Path.GetExtension(file.FileName).ToLower().IndexOf(CommonValues.ExcelExtNew) != 1)
                {
                    model.CommandType = CommonValues.DMLType.Insert;
                    using (var s = file.InputStream)
                    {
                        listModel = bl.ParseExcel(UserManager.UserInfo, model, s).Data;
                    }
                    if (!listModel.Any())
                    {
                        SetMessage(MessageResource.Error_DB_NoVehicleInList, CommonValues.MessageSeverity.Fail);

                        return PartialView(model);
                    }
                }
                else
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = MessageResource.FleetPartPartial_Invalid_File;
                    return PartialView(model);
                }

                if (listModel.Exists(p => p.ErrorNo >= 1))
                {
                    SetModelIfError(listModel, model);

                    SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);

                    return PartialView(model);
                }
                else
                {
                    bl.DMLFleetVehicleWithList(UserManager.UserInfo, model, listModel);
                    if (listModel.Exists(p => p.ErrorNo >= 1))
                    {
                        SetModelIfError(listModel, model);
                        SetMessage(model.ErrorMessage, CommonValues.MessageSeverity.Fail);

                        return PartialView(model);
                    }
                    CheckErrorForMessage(model, true);
                    return PartialView(model);
                }
            }

        }

        private void SetModelIfError(List<FleetVehicleViewModel> listModel, FleetVehicleViewModel model)
        {
            var bl = new FleetVehicleBL();
            var ms = bl.SetExcelReport(listModel, model.ErrorMessage);

            var fileViewModel = new DownloadFileViewModel
            {
                FileName = CommonValues.ErrorLog + DateTime.Now + CommonValues.ExcelExtOld,
                ContentType = CommonValues.ExcelContentType,
                MStream = ms,
                Id = Guid.NewGuid()
            };

            Session[CommonValues.DownloadFileFormatCookieKey] = fileViewModel.Add(fileViewModel);
            TempData[CommonValues.DownloadFileIdCookieKey] = fileViewModel.Id;
        }
    }
}
