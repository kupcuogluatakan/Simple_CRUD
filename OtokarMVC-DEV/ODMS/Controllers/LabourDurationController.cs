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
using ODMSModel.Labour;
using ODMSModel.LabourDuration;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class LabourDurationController : ControllerBase
    {
        private void SetDefaults()
        {
            var bo = new LabourDurationBL();
            ViewBag.VehicleModelList = bo.GetVehicleModelList(UserManager.UserInfo).Data;
        }

        private void OnlySetVehicleTypeList()
        {
            ViewBag.VehicleTypeIdList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, null).Data;
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex)]
        public ActionResult LabourDurationIndex(int? id)
        {
            var bo = new LabourDurationBL();
            var model = bo.GetLabourDurationIndexModel(UserManager.UserInfo).Model;
            model.LabourId = id;
            ViewBag.VehicleTypeList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, "").Data;
            ViewBag.ModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            return PartialView(model);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex, CommonValues.PermissionCodes.LabourDuration.LabourDurationCreate)]
        public ActionResult LabourDurationCreate(int? id)
        {
            var bo = new LabourDurationBL();
            SetDefaults();
            var labMo = new LabourViewModel { LabourId = id.GetValue<int>() };
            var labBo = new LabourBL();
            labBo.GetLabour(UserManager.UserInfo, labMo);

            var model = new LabourDurationDetailModel
            {
                LabourId = id,
                LabourCode = labMo.LabourCode,
                LabourName = labMo.LabourName.txtSelectedLanguageCode,
                LabourDesc = labMo.AdminDesc,
                IsActive = true
            };
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex,
            CommonValues.PermissionCodes.LabourDuration.LabourDurationCreate)]
        public ActionResult LabourDurationCreate(LabourDurationDetailModel model)
        {
            var bo = new LabourDurationBL();
            SetDefaults();
            if (ModelState.IsValid && model.VehicleEngineTypeIdList != null)
            {
                List<string> codeList = model.VehicleEngineTypeIdList;
                foreach (string code in codeList)
                {
                    model.CommandType = CommonValues.DMLType.Insert;
                    List<string> typeIdEngineType = code.Split('$').ToList<string>();
                    model.VehicleTypeId = typeIdEngineType.ElementAt(0).GetValue<int>();
                    model.EngineType = typeIdEngineType.ElementAt(1);
                    bo.DMLLabourDuration(UserManager.UserInfo, model);
                    CheckErrorForMessage(model, true);
                    ModelState.Clear();
                }

                LabourViewModel labMo = new LabourViewModel();
                labMo.LabourId = model.LabourId.GetValue<int>();
                LabourBL labBo = new LabourBL();
                labBo.GetLabour(UserManager.UserInfo, labMo);

                LabourDurationDetailModel newModel = new LabourDurationDetailModel();
                newModel.LabourId = model.LabourId;
                newModel.LabourCode = labMo.LabourCode;
                newModel.LabourName = labMo.LabourName.txtSelectedLanguageCode;
                newModel.LabourDesc = labMo.AdminDesc;
                return View(newModel);
            }
            return View(model);
        }
        public ActionResult LabourDurationUpdateByDuration()
        {
            var list = ParseModelFromRequestInputStream<List<LabourDurationDetailModel>>();
            foreach (var item in list)
            {
                item.CommandType = CommonValues.DMLType.Update;

                var bo = new LabourDurationBL();
                bo.DMLLabourDuration(UserManager.UserInfo, item);
            }
          return  GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
         
        }
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex, CommonValues.PermissionCodes.LabourDuration.LabourDurationUpdate)]
        public ActionResult LabourDurationUpdate(int labourId = 0, string vehicleModelId = null, int vehicleTypeId = 0, string engineType = null)
        {
            var bo = new LabourDurationBL();

            SetDefaults();
            OnlySetVehicleTypeList();

            var referenceModel = new LabourDurationDetailModel();
            if (labourId > 0 && !string.IsNullOrEmpty(vehicleModelId) && vehicleTypeId > 0)
            {
                LabourViewModel labMo = new LabourViewModel();
                labMo.LabourId = labourId;
                LabourBL labBo = new LabourBL();
                labBo.GetLabour(UserManager.UserInfo, labMo);

                referenceModel.LabourId = labourId;
                referenceModel.LabourCode = labMo.LabourCode;
                referenceModel.LabourName = labMo.LabourName.txtSelectedLanguageCode;
                referenceModel.LabourDesc = labMo.AdminDesc;
                referenceModel.VehicleModelId = vehicleModelId;
                referenceModel.VehicleTypeId = vehicleTypeId;
                referenceModel.EngineType = engineType;
                referenceModel = bo.GetLabourDuration(UserManager.UserInfo, referenceModel).Model;
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex, CommonValues.PermissionCodes.LabourDuration.LabourDurationUpdate)]
        public ActionResult LabourDurationUpdate(LabourDurationDetailModel viewModel)
        {
            var bo = new LabourDurationBL();

            SetDefaults();
            OnlySetVehicleTypeList();

            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                bo.DMLLabourDuration(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }

            LabourViewModel labMo = new LabourViewModel();
            labMo.LabourId = viewModel.LabourId.GetValue<int>();
            LabourBL labBo = new LabourBL();
            labBo.GetLabour(UserManager.UserInfo, labMo);

            viewModel.LabourCode = labMo.LabourCode;
            viewModel.LabourName = labMo.LabourName.txtSelectedLanguageCode;
            viewModel.LabourDesc = labMo.AdminDesc;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex, CommonValues.PermissionCodes.LabourDuration.LabourDurationDelete)]
        public ActionResult LabourDurationDelete(LabourDurationDeleteModel referenceModel)
        {
            var model = new LabourDurationDetailModel
            {
                LabourId = referenceModel.LabourId,
                VehicleModelId = referenceModel.VehicleModelId,
                VehicleTypeId = referenceModel.VehicleTypeId,
                EngineType = referenceModel.EngineType
            };

            if (ModelState.IsValid)
            {
                ViewBag.HideElements = false;
                var bo = new LabourDurationBL();
                model.CommandType = CommonValues.DMLType.Delete;
                bo.DMLLabourDuration(UserManager.UserInfo, model);
                CheckErrorForMessage(model, true);
                ModelState.Clear();

                if (model.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            }
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex, CommonValues.PermissionCodes.LabourDuration.LabourDurationDetails)]
        public ActionResult LabourDurationDetails(int labourId = 0, string vehicleModelId = null, int vehicleTypeId = 0, string engineType = null)
        {
            var referenceModel = new LabourDurationDetailModel
            {
                LabourId = labourId,
                VehicleModelId = vehicleModelId,
                VehicleTypeId = vehicleTypeId,
                EngineType = engineType
            };
            var bo = new LabourDurationBL();

            var model = bo.GetLabourDuration(UserManager.UserInfo, referenceModel).Model;

            LabourViewModel labMo = new LabourViewModel();
            labMo.LabourId = labourId;
            LabourBL labBo = new LabourBL();
            labBo.GetLabour(UserManager.UserInfo, labMo);

            model.LabourCode = labMo.LabourCode;
            model.LabourDesc = labMo.AdminDesc;
            model.LabourName = labMo.LabourName.txtSelectedLanguageCode;

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex, CommonValues.PermissionCodes.LabourDuration.LabourDurationDetails)]
        public ActionResult ListLabourDurations([DataSourceRequest]DataSourceRequest request, LabourDurationListModel model)
        {
            var bo = new LabourDurationBL();
            var referenceModel = new LabourDurationListModel(request)
            {
                LabourId = model.LabourId,
                VehicleModelId = model.VehicleModelId,
                VehicleTypeId = model.VehicleTypeId,
                Duration = model.Duration,
                SearchIsActive = model.SearchIsActive,
                EngineType = (model.EngineType != null ? "'" + model.EngineType.Replace(",", "','") + "'" : null),
                VehicleModelName = (model.VehicleModelName != null ? "'" + model.VehicleModelName.Replace(",", "','") + "'" : null),
                VehicleTypeName = model.VehicleTypeName//(model.VehicleTypeName != null ? "'" + model.VehicleTypeName.Replace(",", "','") + "'" : null)
            };

            int totalCnt;
            var returnValue = bo.ListLabourDurations(UserManager.UserInfo, referenceModel, out totalCnt).Data;
            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex)]
        public ActionResult ListVehicleEngineTypes(string vehicleModelId, string labourId)
        {
            return vehicleModelId == null || labourId == null
                       ? Json(new List<SelectListItem>(), JsonRequestBehavior.AllowGet)
                       : Json(LabourDurationBL.GetVehicleTypeEngineTypeList(UserManager.UserInfo, vehicleModelId, labourId).Data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex)]
        public ActionResult ListVehicleEngineTypesSearch(string labourId)
        {
            return Json(LabourDurationBL.GetVehicleTypeEngineTypeListSearch(UserManager.UserInfo, labourId).Data, JsonRequestBehavior.AllowGet);
        }

        #region Excel Upload

        public ActionResult ExcelSample()
        {
            var bo = new LabourDurationBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.LabourDuration_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.LabourDuration.LabourDurationIndex)]
        public ActionResult LabourDurationIndex(LabourDurationListModel listModel, HttpPostedFileBase labourDurationExcelFile)
        {
            SetDefaults();
            LabourDurationDetailModel model = new LabourDurationDetailModel();

            if (ModelState.IsValid)
            {
                if (labourDurationExcelFile != null)
                {
                    var bo = new LabourDurationBL();
                    Stream s = labourDurationExcelFile.InputStream;
                    List<LabourDurationDetailModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
                    // excel dosyasındaki veriler kontrol edilir.
                    if (model.ErrorNo > 0)
                    {
                        var ms = bo.SetExcelReport(modelList, model.ErrorMessage);

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

                        return RedirectToAction("LabourIndex", "Labour");
                    }
                    else
                    {
                        int totalCount = 0;
                        foreach (var row in modelList)
                        {
                            LabourDurationListModel controlModel = new LabourDurationListModel
                            {
                                LabourId = row.LabourId.GetValue<int>(),
                                //VehicleModelId = row.VehicleModelId,
                                VehicleTypeId = row.VehicleTypeId.GetValue<int>(),
                                //Duration = row.Duration,
                                //SearchIsActive = row.SearchIsActive,
                                EngineType = (row.EngineType != null ? "'" + row.EngineType.Replace(",", "','") + "'" : null),
                                //VehicleModelName = (row.VehicleModelName != null ? "'" + row.VehicleModelName.Replace(",", "','") + "'" : null),
                                //VehicleTypeName = row.VehicleTypeName//(model.VehicleTypeName != null ? "'" + model.VehicleTypeName.Replace(",", "','") + "'" : null)
                            };
                            List<LabourDurationListModel> list = bo.ListLabourDurations(UserManager.UserInfo, controlModel, out totalCount).Data;
                            if (totalCount > 0)
                            {
                                row.CommandType = CommonValues.DMLType.Update;
                            }
                            else
                            {
                                row.CommandType = CommonValues.DMLType.Insert;
                            }
                            bo.DMLLabourDuration(UserManager.UserInfo, row);
                            if (row.ErrorNo > 0)
                            {
                                SetMessage(row.ErrorMessage, CommonValues.MessageSeverity.Fail);
                                return RedirectToAction("LabourIndex", "Labour");
                            }
                        }

                        SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                    }
                }
                else
                {
                    SetMessage(MessageResource.Global_Warning_ExcelNotSelected, CommonValues.MessageSeverity.Fail);
                }
                ModelState.Clear();
            }
            return RedirectToAction("LabourIndex", "Labour");
        }
        #endregion

        public ActionResult SaveLabourDuration(string list)
        {
            return View();
        }
    }
}
