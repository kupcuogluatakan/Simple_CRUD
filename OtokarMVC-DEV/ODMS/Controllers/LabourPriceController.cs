using System;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.LabourPrice;
using System.Linq;
using Permission = ODMSCommon.CommonValues.PermissionCodes.LabourPrice;
using System.Web;
using System.IO;
using System.Collections.Generic;
using ODMSModel.DownloadFileActionResult;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class LabourPriceController : ControllerBase
    {

        #region LabourIndex

        [HttpGet]
        [AuthorizationFilter(Permission.LabourPriceIndex)]
        public ActionResult LabourPriceIndex()
        {
            FillComboBoxes();
            return View();
        }


        [HttpPost]
        [AuthorizationFilter(Permission.LabourPriceIndex, Permission.LabourPriceCreate)]
        public ActionResult LabourPriceIndex(HttpPostedFileBase excelFile)
        {
            FillComboBoxes();
            LabourPriceViewModel model = new LabourPriceViewModel();
            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new LabourPriceBL();
                    Stream s = excelFile.InputStream;
                    List<LabourPriceViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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

                        return View();
                    }
                    else
                    {
                        int totalCount = 0;
                        foreach (var row in modelList)
                        {
                            if (row.HasTSLabourPriceId > 0 && row.HasNoTSLabourPriceId > 0)
                                row.CommandType = CommonValues.DMLType.Update;
                            else
                                row.CommandType = CommonValues.DMLType.Insert;


                            LabourPriceDMLBackEnd(row);
                            if (row.ErrorNo > 0)
                            {
                                SetMessage(row.ErrorMessage, CommonValues.MessageSeverity.Fail);
                                return View();
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
            return View();
        }

        public ActionResult ExcelSample()
        {
            var bo = new LabourPriceBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.LabourPrice_PageTitle_Index + CommonValues.ExcelExtOld);
        }

        [HttpPost]
        [AuthorizationFilter(Permission.LabourPriceIndex)]
        public ActionResult ListLabourPrices([DataSourceRequest]DataSourceRequest request, LabourPriceListModel viewModel)
        {
            var bus = new LabourPriceBL();
            var model = new LabourPriceListModel(request)
            {
                DealerRegionId = viewModel.DealerRegionId,
                LabourPriceTypeId = viewModel.LabourPriceTypeId,
                VehicleGroupId = viewModel.VehicleGroupId,
                ModelKod = viewModel.ModelKod,
                SearchHasTsPaper = viewModel.SearchHasTsPaper,
                ValidEndDate = viewModel.ValidEndDate,
                ValidFromDate = viewModel.ValidFromDate,
                ValidDate = viewModel.ValidDate,
                CurrencyCode = viewModel.CurrencyCode,
                DealerClass = viewModel.DealerClass
            };

            if (!string.IsNullOrEmpty(viewModel.IsActiveString))
            {
                model.SearchIsActive = Convert.ToBoolean(Convert.ToInt16(viewModel.IsActiveString));
            }
            else
            {
                model.SearchIsActive = null;
            }

            var totalCnt = 0;
            var returnValue = bus.ListLabourPrices(UserManager.UserInfo, model, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });

        }

        #endregion

        #region Labour Create
        [HttpGet]
        [AuthorizationFilter(Permission.LabourPriceIndex, Permission.LabourPriceCreate)]
        public ActionResult LabourPriceCreate()
        {
            FillComboBoxes();
            LabourPriceViewModel model = new LabourPriceViewModel();
            model.IsActive = true;
            return View(model);
        }


        private void LabourPriceDMLBackEnd(LabourPriceViewModel model)
        {
            if (model.HasTSLabourPriceId > 0 && model.HasNoTSLabourPriceId > 0)
            {
                //Update
                LabourPriceDMLBackEndUpdate(model);
            }
            else
            {
                //Insert
                LabourPriceDMLBackEndCreate(model);
            }
        }

        private void LabourPriceDMLBackEndCreate(LabourPriceViewModel model)
        {
            var bus = new LabourPriceBL();
            var hasTSLabourPriceModel = new LabourPriceViewModel
                {
                    CurrencyCode = model.CurrencyCode,
                    DealerClass = model.DealerClass,
                    DealerRegionId = model.DealerRegionId,
                    HasTsPaper = true,
                    IsActive = model.IsActive,
                    LabourPriceTypeId = model.LabourPriceTypeId,
                    ModelCode = model.ModelCode,
                    UnitPrice = model.HasTSUnitPrice,
                    ValidEndDate = model.ValidEndDate,
                    ValidFromDate = model.ValidFromDate,
                    VehicleGroupId = model.VehicleGroupId,
                    CommandType = CommonValues.DMLType.Insert
                };

                bus.DMLLabourPrice(UserManager.UserInfo, hasTSLabourPriceModel);

                CheckErrorForMessage(hasTSLabourPriceModel, true);

                var hasNoTSLabourPriceModel = new LabourPriceViewModel
                {
                    CurrencyCode = model.CurrencyCode,
                    DealerClass = model.DealerClass,
                    DealerRegionId = model.DealerRegionId,
                    HasTsPaper = false,
                    IsActive = model.IsActive,
                    LabourPriceTypeId = model.LabourPriceTypeId,
                    ModelCode = model.ModelCode,
                    UnitPrice = model.HasNoTSUnitPrice,
                    ValidEndDate = model.ValidEndDate,
                    ValidFromDate = model.ValidFromDate,
                    VehicleGroupId = model.VehicleGroupId,
                    CommandType = CommonValues.DMLType.Insert
                };

                bus.DMLLabourPrice(UserManager.UserInfo, hasNoTSLabourPriceModel);

                CheckErrorForMessage(hasNoTSLabourPriceModel, true);
            
        }

        private void LabourPriceDMLBackEndUpdate(LabourPriceViewModel model)
        {
            var bus = new LabourPriceBL();
            
            var hasTSLabourPriceModel = new LabourPriceViewModel
            {
                LabourPriceId = model.HasTSLabourPriceId,
                CurrencyCode = model.CurrencyCode,
                DealerClass = model.DealerClass,
                DealerRegionId = model.DealerRegionId,
                HasTsPaper = true,
                IsActive = model.IsActive,
                LabourPriceTypeId = model.LabourPriceTypeId,
                ModelCode = model.ModelCode,
                UnitPrice = model.HasTSUnitPrice,
                ValidEndDate = model.ValidEndDate,
                ValidFromDate = model.ValidFromDate,
                VehicleGroupId = model.VehicleGroupId,
                CommandType = CommonValues.DMLType.Update
            };

            bus.DMLLabourPrice(UserManager.UserInfo, hasTSLabourPriceModel);

            CheckErrorForMessage(hasTSLabourPriceModel, true);

            var hasNoTSLabourPriceModel = new LabourPriceViewModel
            {
                LabourPriceId = model.HasNoTSLabourPriceId,
                CurrencyCode = model.CurrencyCode,
                DealerClass = model.DealerClass,
                DealerRegionId = model.DealerRegionId,
                HasTsPaper = false,
                IsActive = model.IsActive,
                LabourPriceTypeId = model.LabourPriceTypeId,
                ModelCode = model.ModelCode,
                UnitPrice = model.HasNoTSUnitPrice,
                ValidEndDate = model.ValidEndDate,
                ValidFromDate = model.ValidFromDate,
                VehicleGroupId = model.VehicleGroupId,
                CommandType = CommonValues.DMLType.Update
            };

            bus.DMLLabourPrice(UserManager.UserInfo, hasNoTSLabourPriceModel);

            CheckErrorForMessage(hasNoTSLabourPriceModel, true);

        }
        [HttpPost]
        [AuthorizationFilter(Permission.LabourPriceIndex, Permission.LabourPriceCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult LabourPriceCreate(LabourPriceViewModel model)
        {
            string errorMessage = string.Empty;
            FillComboBoxes();
            if (ModelState.IsValid == false)
                return View(model);
            bool isValid = true;
            var bus = new LabourPriceBL();
            var listModel = new LabourPriceListModel();
            var totalCnt = 0;
            listModel.ModelKod = model.ModelCode;
            listModel.LabourPriceTypeId = model.LabourPriceTypeId;
            listModel.DealerRegionId = model.DealerRegionId;
            listModel.CurrencyCode = model.CurrencyCode;
            listModel.DealerClass = model.DealerClass;
            listModel.ValidEndDate = model.ValidEndDate;
            listModel.ValidFromDate = model.ValidFromDate;

            var returnValue = bus.ListLabourPrices(UserManager.UserInfo, listModel, out totalCnt).Data;
            if (totalCnt != 0)
            {
                var control = (from r in returnValue.AsEnumerable()
                               where r.ValidFromDate == model.ValidFromDate
                                     && r.HasTsPaper == model.HasTsPaper
                               select r);
                var control2 = (from r in returnValue.AsEnumerable()
                                where (((r.ValidFromDate <= model.ValidFromDate) &&
                                        (r.ValidEndDate <= model.ValidEndDate))
                                       ||
                                       ((r.ValidFromDate <= model.ValidFromDate) &&
                                        (r.ValidEndDate >= model.ValidEndDate && r.ValidFromDate <= model.ValidEndDate))
                                       ||
                                       ((r.ValidFromDate >= model.ValidFromDate) &&
                                        (r.ValidEndDate <= model.ValidEndDate))
                                       ||
                                       ((r.ValidFromDate >= model.ValidFromDate) &&
                                        (r.ValidEndDate >= model.ValidEndDate && r.ValidEndDate >= model.ValidFromDate)))
                                      && r.IsActive
                                select r);
                if (control.Any())
                {
                    isValid = false;
                    errorMessage = MessageResource.LabourPrice_Warning_SameValuesExists;
                }
                if (control2.Any())
                {
                    isValid = false;
                    errorMessage = MessageResource.LabourPrice_Warning_SamePeriodExists;
                }
            }
            if (isValid)
            {
                var hasTSLabourPriceModel = new LabourPriceViewModel
                {
                    CurrencyCode = model.CurrencyCode,
                    DealerClass = model.DealerClass,
                    DealerRegionId = model.DealerRegionId,
                    HasTsPaper = true,
                    IsActive = model.IsActive,
                    LabourPriceTypeId = model.LabourPriceTypeId,
                    ModelCode = model.ModelCode,
                    UnitPrice = model.HasTSUnitPrice,
                    ValidEndDate = model.ValidEndDate,
                    ValidFromDate = model.ValidFromDate,
                    VehicleGroupId = model.VehicleGroupId,
                    CommandType = CommonValues.DMLType.Insert
                };

                bus.DMLLabourPrice(UserManager.UserInfo, hasTSLabourPriceModel);

                CheckErrorForMessage(hasTSLabourPriceModel, true);

                var hasNoTSLabourPriceModel = new LabourPriceViewModel
                {
                    CurrencyCode = model.CurrencyCode,
                    DealerClass = model.DealerClass,
                    DealerRegionId = model.DealerRegionId,
                    HasTsPaper = false,
                    IsActive = model.IsActive,
                    LabourPriceTypeId = model.LabourPriceTypeId,
                    ModelCode = model.ModelCode,
                    UnitPrice = model.HasNoTSUnitPrice,
                    ValidEndDate = model.ValidEndDate,
                    ValidFromDate = model.ValidFromDate,
                    VehicleGroupId = model.VehicleGroupId,
                    CommandType = CommonValues.DMLType.Insert
                };

                bus.DMLLabourPrice(UserManager.UserInfo, hasNoTSLabourPriceModel);

                CheckErrorForMessage(hasNoTSLabourPriceModel, true);

                ModelState.Clear();
                return View();
            }
            else
            {
                SetMessage(errorMessage, CommonValues.MessageSeverity.Fail);
                return View(model);
            }
        }
        #endregion

        #region Labour Price Update
        [HttpGet]
        [AuthorizationFilter(Permission.LabourPriceIndex, Permission.LabourPriceUpdate)]
        public ActionResult LabourPriceUpdate(int? hasTSId, int? hasNoTSId)
        {
            FillComboBoxes();

            if (!(hasTSId.HasValue && hasTSId > 0) && !(hasNoTSId.HasValue && hasNoTSId > 0))
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View();
            }

            var hasTSModel = new LabourPriceBL().GetLabourPrice(UserManager.UserInfo, hasTSId.GetValueOrDefault()).Model;
            var hasNoTSModel = new LabourPriceBL().GetLabourPrice(UserManager.UserInfo, hasNoTSId.GetValueOrDefault()).Model;

            var model = new LabourPriceViewModel
            {
                CurrencyCode = hasTSModel.CurrencyCode,
                CurrencyName = hasTSModel.CurrencyName,
                DealerClass = hasTSModel.DealerClass,
                DealerClassName = hasTSModel.DealerClassName,
                DealerRegionId = hasTSModel.DealerRegionId,
                DealerRegionName = hasTSModel.DealerRegionName,
                HasNoTSLabourPriceId = hasNoTSModel.LabourPriceId,
                HasNoTSUnitPrice = hasNoTSModel.UnitPrice,
                HasTSLabourPriceId = hasTSModel.LabourPriceId,
                HasTSUnitPrice = hasTSModel.UnitPrice,
                IsActive = hasTSModel.IsActive,
                IsActiveString = hasTSModel.IsActiveString,
                LabourPriceType = hasTSModel.LabourPriceType,
                LabourPriceTypeId = hasTSModel.LabourPriceTypeId,
                ModelCode = hasTSModel.ModelCode,
                ModelName = hasTSModel.ModelName,
                ValidEndDate = hasTSModel.ValidEndDate,
                ValidFromDate = hasTSModel.ValidFromDate,
                VehicleGroup = hasTSModel.VehicleGroup,
                VehicleGroupId = hasTSModel.VehicleGroupId
            };

            return View(model);
        }
        [HttpPost]
        [AuthorizationFilter(Permission.LabourPriceIndex, Permission.LabourPriceUpdate)]
        [ValidateAntiForgeryToken]
        public ActionResult LabourPriceUpdate(LabourPriceViewModel model)
        {
            string errorMessage = string.Empty;
            FillComboBoxes();

            if (ModelState.IsValid == false)
                return View(model);

            var bus = new LabourPriceBL();
            bool isValid = true;
            var listModel = new LabourPriceListModel();
            var totalCnt = 0;
            listModel.ModelKod = model.ModelCode;
            listModel.LabourPriceTypeId = model.LabourPriceTypeId;
            listModel.DealerRegionId = model.DealerRegionId;
            listModel.CurrencyCode = model.CurrencyCode;
            listModel.DealerClass = model.DealerClass;
            listModel.ValidEndDate = model.ValidEndDate;
            listModel.ValidFromDate = model.ValidFromDate;
            var returnValue = bus.ListLabourPrices(UserManager.UserInfo, listModel, out totalCnt).Data.Where(c => c.LabourPriceId != string.Format("{0}${1}", model.HasTSLabourPriceId, model.HasNoTSLabourPriceId));
            if (returnValue.Any())
            {
                var control = (from r in returnValue.AsEnumerable()
                               where r.ValidFromDate == model.ValidFromDate
                                     && r.HasTsPaper == model.HasTsPaper
                                     &&
                                     (r.HasNoTSLabourPriceId != model.HasNoTSLabourPriceId &&
                                      r.HasTSLabourPriceId != model.HasTSLabourPriceId)
                               select r);
                var control2 = (from r in returnValue.AsEnumerable()
                                where (((r.ValidFromDate <= model.ValidFromDate) &&
                                        (r.ValidEndDate <= model.ValidEndDate))
                                       ||
                                       ((r.ValidFromDate <= model.ValidFromDate) &&
                                        (r.ValidEndDate >= model.ValidEndDate))
                                       ||
                                       ((r.ValidFromDate >= model.ValidFromDate) &&
                                        (r.ValidEndDate <= model.ValidEndDate))
                                       ||
                                       ((r.ValidFromDate >= model.ValidFromDate) &&
                                        (r.ValidEndDate >= model.ValidEndDate)))
                                      && r.IsActive
                                      && (r.HasNoTSLabourPriceId != model.HasNoTSLabourPriceId &&
                                       r.HasTSLabourPriceId != model.HasTSLabourPriceId)
                                select r);
                if (control.Any())
                {
                    isValid = false;
                    errorMessage = MessageResource.LabourPrice_Warning_SameValuesExists;
                }
                if (control2.Any())
                {
                    isValid = false;
                    errorMessage = MessageResource.LabourPrice_Warning_SamePeriodExists;
                }
            }

            if (isValid)
            {
                var hasTSLabourPriceModel = new LabourPriceViewModel
                {
                    LabourPriceId = model.HasTSLabourPriceId,
                    CurrencyCode = model.CurrencyCode,
                    DealerClass = model.DealerClass,
                    DealerRegionId = model.DealerRegionId,
                    HasTsPaper = true,
                    IsActive = model.IsActive,
                    LabourPriceTypeId = model.LabourPriceTypeId,
                    ModelCode = model.ModelCode,
                    UnitPrice = model.HasTSUnitPrice,
                    ValidEndDate = model.ValidEndDate,
                    ValidFromDate = model.ValidFromDate,
                    VehicleGroupId = model.VehicleGroupId,
                    CommandType = CommonValues.DMLType.Update
                };

                bus.DMLLabourPrice(UserManager.UserInfo, hasTSLabourPriceModel);

                CheckErrorForMessage(hasTSLabourPriceModel, true);

                var hasNoTSLabourPriceModel = new LabourPriceViewModel
                {
                    LabourPriceId = model.HasNoTSLabourPriceId,
                    CurrencyCode = model.CurrencyCode,
                    DealerClass = model.DealerClass,
                    DealerRegionId = model.DealerRegionId,
                    HasTsPaper = false,
                    IsActive = model.IsActive,
                    LabourPriceTypeId = model.LabourPriceTypeId,
                    ModelCode = model.ModelCode,
                    UnitPrice = model.HasNoTSUnitPrice,
                    ValidEndDate = model.ValidEndDate,
                    ValidFromDate = model.ValidFromDate,
                    VehicleGroupId = model.VehicleGroupId,
                    CommandType = CommonValues.DMLType.Update
                };

                bus.DMLLabourPrice(UserManager.UserInfo, hasNoTSLabourPriceModel);

                CheckErrorForMessage(hasNoTSLabourPriceModel, true);

                ModelState.Clear();
            }
            else
            {
                SetMessage(errorMessage, CommonValues.MessageSeverity.Fail);
            }
            return View(model);
        }

        #endregion

        #region Labour Price Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(Permission.LabourPriceIndex, Permission.LabourPriceDelete)]
        public ActionResult LabourPriceDelete(int? hasTSId, int? hasNoTSId)
        {
            if (!(hasTSId.HasValue && hasTSId > 0) && !(hasNoTSId.HasValue && hasNoTSId > 0))
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Error_DB_NoRecordFound);
            }

            var bus = new LabourPriceBL();

            var hasTSmodel = new LabourPriceViewModel { LabourPriceId = hasTSId ?? 0, CommandType = CommonValues.DMLType.Delete };
            bus.DMLLabourPrice(UserManager.UserInfo, hasTSmodel);

            var hasNoTSmodel = new LabourPriceViewModel { LabourPriceId = hasNoTSId ?? 0, CommandType = CommonValues.DMLType.Delete };
            bus.DMLLabourPrice(UserManager.UserInfo, hasNoTSmodel);

            ModelState.Clear();

            if (hasTSmodel.ErrorNo == 0 && hasNoTSmodel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, hasTSmodel.ErrorMessage + " " + hasNoTSmodel.ErrorMessage);
        }
        
        #endregion

        #region Labour Price Details

        [HttpGet]
        [AuthorizationFilter(Permission.LabourPriceIndex, Permission.LabourPriceDetails)]
        public ActionResult LabourPriceDetails(int? hasTSId, int? hasNoTSId)
        {
            if (!(hasTSId.HasValue && hasTSId > 0) && !(hasNoTSId.HasValue && hasNoTSId > 0))
            {
                SetMessage(MessageResource.Error_DB_NoRecordFound, CommonValues.MessageSeverity.Fail);
                return View();
            }
            var hasTSModel = new LabourPriceBL().GetLabourPrice(UserManager.UserInfo, hasTSId.GetValueOrDefault()).Model;
            var hasNoTSModel = new LabourPriceBL().GetLabourPrice(UserManager.UserInfo, hasNoTSId.GetValueOrDefault()).Model;

            var model = new LabourPriceViewModel
            {
                CurrencyCode = hasTSModel.CurrencyCode,
                CurrencyName = hasTSModel.CurrencyName,
                DealerClass = hasTSModel.DealerClass,
                DealerClassName = hasTSModel.DealerClassName,
                DealerRegionId = hasTSModel.DealerRegionId,
                DealerRegionName = hasTSModel.DealerRegionName,
                HasNoTSLabourPriceId = hasNoTSModel.LabourPriceId,
                HasNoTSUnitPrice = hasNoTSModel.UnitPrice,
                HasTSLabourPriceId = hasTSModel.LabourPriceId,
                HasTSUnitPrice = hasTSModel.UnitPrice,
                IsActive = hasTSModel.IsActive,
                IsActiveString = hasTSModel.IsActiveString,
                LabourPriceType = hasTSModel.LabourPriceType,
                LabourPriceTypeId = hasTSModel.LabourPriceTypeId,
                ModelCode = hasTSModel.ModelCode,
                ModelName = hasTSModel.ModelName,
                ValidEndDate = hasTSModel.ValidEndDate,
                ValidFromDate = hasTSModel.ValidFromDate,
                VehicleGroup = hasTSModel.VehicleGroup,
                VehicleGroupId = hasTSModel.VehicleGroupId
            };
            return View(model);
        }
        #endregion

        #region Private Methods

        private void FillComboBoxes()
        {
            var bus = new LabourPriceBL();
            ViewBag.VehicleModelList = bus.ListVehicleModels(UserManager.UserInfo).Data;
            ViewBag.DealerClassList = bus.ListDealerClasses().Data;
            ViewBag.DealerRegionList = bus.ListDealerRegions().Data;
            ViewBag.LabourTypeList = bus.ListLabourTypes(UserManager.UserInfo).Data;
            ViewBag.CurrencyCodeList = bus.ListCurrencyCodes(UserManager.UserInfo).Data;
        }

        #endregion

    }
}
