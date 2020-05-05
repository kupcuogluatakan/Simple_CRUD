using Kendo.Mvc.UI;
using ODMS.Filters;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.ListModel;
using ODMSModel.Vehicle;
using System.Linq;
using System.IO;
using ODMSModel.StockCard;
using ODMSModel.DownloadFileActionResult;
using System.Text;
using System;
using ODMSModel;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class VehicleController : ControllerBase
    {
        [ValidateAntiForgeryToken]
        public JsonResult CheckPlate(string vehicleId, string plate)
        {
            bool isPlateExists = false;
            if (!string.IsNullOrEmpty(plate))
            {
                int totalCount = 0;
                VehicleListModel listModel = new VehicleListModel();
                listModel.Plate = plate;
                VehicleBL vBo = new VehicleBL();
                List<VehicleListModel> vehicleList = vBo.ListVehicles(UserManager.UserInfo, listModel, out totalCount).Data;
                var control = (from v in vehicleList.AsEnumerable()
                               where v.VehicleId != vehicleId.GetValue<int>()
                               select v);
                if (control.Any())
                {
                    isPlateExists = true;
                }
            }

            return Json(new { IsPlateExists = isPlateExists });
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleIndex)]
        public JsonResult ListVehicleTypes(string vehicleModel)
        {
            return !string.IsNullOrEmpty(vehicleModel) ?
                Json(VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, vehicleModel).Data, JsonRequestBehavior.AllowGet) :
                Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleIndex)]
        public JsonResult ListVehicleCodes(string vehicleType)
        {
            return !string.IsNullOrEmpty(vehicleType) ?
                Json(VehicleBL.ListVehicleCodeAsSelectListItem(UserManager.UserInfo, vehicleType).Data, JsonRequestBehavior.AllowGet) :
                Json(null, JsonRequestBehavior.AllowGet);
        }
        private void SetDefaults()
        {
            List<SelectListItem> vehicleCodeList = VehicleBL.ListVehicleCodeAsSelectListItem(UserManager.UserInfo, null).Data;

            List<SelectListItem> customerList = CustomerBL.ListCustomerAsSelectListItem(UserManager.UserInfo).Data;
            List<SelectListItem> vehicleModelList = VehicleModelBL.ListVehicleModelAsSelectList(UserManager.UserInfo).Data;
            List<SelectListItem> vatExcludeTypeList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.YesNo).Data;
            List<SelectListItem> warrantyStatusList = CommonBL.ListWarrantyStatus().Data;
            List<SelectListItem> statusList = CommonBL.ListStatus().Data;
            List<SelectListItem> priceList = SparePartBL.ListSSIDPriceListAsSelectListItem().Data;

            ViewBag.VehicleCodeList = vehicleCodeList;
            ViewBag.CustomerList = customerList;
            ViewBag.VehicleModelList = vehicleModelList;
            ViewBag.VatExcludeTypeList = vatExcludeTypeList;
            ViewBag.WarrantyStatusList = warrantyStatusList;
            ViewBag.StatusList = statusList;
            ViewBag.PriceList = priceList;
        }

        #region Vehicle Index
        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleIndex)]
        [HttpGet]
        public ActionResult VehicleIndex()
        {
            SetDefaults();
            var asd = UserManager.UserInfo;
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleIndex)]
        [HttpPost]
        public ActionResult VehicleIndex(VehicleListModel model, HttpPostedFileBase excelFile)
        {
            VehicleIndexViewModel newModel = new VehicleIndexViewModel();
            SetDefaults();

            //
            if (excelFile != null)
            {
                //StockCardBL bo = new StockCardBL();
                VehicleBL bo = new VehicleBL();
                Stream s = excelFile.InputStream;
                VehicleIndexViewModel viewMo = new VehicleIndexViewModel();

                List<VehicleIndexViewModel> listModel = bo.ParseExcel(UserManager.UserInfo, viewMo, s).Data;

                if (viewMo.ErrorNo > 0)
                {
                    var ms = bo.SetExcelReport(listModel, viewMo.ErrorMessage);

                    var fileViewModel = new DownloadFileViewModel
                    {
                        FileName = CommonValues.ErrorLog + DateTime.Now + CommonValues.ExcelExtOld,
                        ContentType = CommonValues.ExcelContentType,
                        MStream = ms,
                        Id = Guid.NewGuid()
                    };

                    Session[CommonValues.DownloadFileFormatCookieKey] = fileViewModel.Add(fileViewModel);
                    TempData[CommonValues.DownloadFileIdCookieKey] = fileViewModel.Id;

                    SetMessage(viewMo.ErrorMessage, CommonValues.MessageSeverity.Fail);

                    return View(newModel);
                }
                else
                {
                    StringBuilder VinCodes = new StringBuilder();
                    foreach (VehicleIndexViewModel mo in listModel)
                    {
                        VinCodes.Append(mo.VinNo);
                        VinCodes.Append(",");
                    }
                    if (VinCodes.Length > 0)
                    {
                        VinCodes.Remove(VinCodes.Length - 1, 1);
                        newModel.VinCodeList = VinCodes.ToString();
                    }
                }
            }
            else
            {
                SetMessage(MessageResource.Global_Warning_ExcelNotSelected, CommonValues.MessageSeverity.Fail);
            }
            return View(newModel);
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleIndex, CommonValues.PermissionCodes.Vehicle.VehicleDetails)]
        public ActionResult ListVehicle([DataSourceRequest] DataSourceRequest request, VehicleListModel model)
        {
            var vehicleBo = new VehicleBL();
            var v = new VehicleListModel(request);
            var totalCnt = 0;
            v.IsActive = model.IsActive;
            v.CustomerName = model.CustomerName;
            v.Plate = model.Plate;
            v.VehicleModel = model.VehicleModel;
            v.VehicleCode = model.VehicleCode;
            v.VehicleType = model.VehicleType;
            v.WarrantyEndDate = model.WarrantyEndDate;
            v.ModelYear = model.ModelYear;
            v.WarrantyStartDate = model.WarrantyStartDate;
            v.EngineNo = model.EngineNo;
            v.VinNo = model.VinNo;
            v.VinCodeList = model.VinCodeList.AddSingleQuote();

            var returnValue = vehicleBo.ListVehicles(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Vehicle Create
        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleIndex, CommonValues.PermissionCodes.Vehicle.VehicleCreate)]
        [HttpGet]
        public ActionResult VehicleCreate()
        {
            SetDefaults();
            VehicleIndexViewModel model = new VehicleIndexViewModel();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleIndex, CommonValues.PermissionCodes.Vehicle.VehicleCreate)]
        [HttpPost]
        public ActionResult VehicleCreate(VehicleIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            ViewBag.IsSuccessfullyAdded = false;
            var vehicleBo = new VehicleBL();
            SetDefaults();

            if (ModelState.IsValid)


            {
                int totalCount = 0;
                //VehicleListModel listModel = new VehicleListModel();        
                VehicleBL vBo = new VehicleBL();

                if (vBo.CheckVehicleByVinNoOrPlate(viewModel.VinNo, null).Model)
                {
                    SetMessage(ODMSCommon.Resources.MessageResource.Error_DB_Already_Exists, CommonValues.MessageSeverity.Fail);
                    return View(viewModel);
                }

                //listModel.VinNo = viewModel.VinNo;
                //List<VehicleListModel> list2 = vBo.ListVehicles(listModel, out totalCount);
                //if (totalCount > 0)
                //{
                //    SetMessage(ODMSCommon.Resources.MessageResource.Error_DB_Already_Exists, CommonValues.MessageSeverity.Fail);
                //    return View(viewModel);
                //}

                // TFS NO : 27733 OYA 26.12.2014
                if (viewModel.PlateWillBeUpdated && !string.IsNullOrEmpty(viewModel.Plate))
                {
                    VehicleListModel listModel = new VehicleListModel();
                    listModel.Plate = viewModel.Plate;
                    List<VehicleListModel> list = vBo.ListVehicles(UserManager.UserInfo, listModel, out totalCount).Data;

                    if (totalCount > 0)
                    {
                        foreach (VehicleListModel updated in list)
                        {
                            VehicleIndexViewModel vModel = new VehicleIndexViewModel();
                            vModel.VehicleId = updated.VehicleId;
                            vBo.GetVehicle(UserManager.UserInfo, vModel);
                            vModel.CommandType = CommonValues.DMLType.Update;
                            vModel.Plate = null;
                            vBo.DMLVehicle(UserManager.UserInfo, vModel);
                            if (vModel.ErrorNo > 0)
                            {
                                SetMessage(vModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                                return View(viewModel);
                            }
                        }
                    }
                }

                viewModel.CommandType = viewModel.VehicleId > 0
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;
                vehicleBo.DMLVehicle(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();
                ViewBag.IsSuccessfullyAdded = true;
                VehicleIndexViewModel model = new VehicleIndexViewModel();
                model.IsActive = true;
                return View(model);
            }
            return View(viewModel);
        }

        #endregion

        #region Vehicle Update
        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleIndex, CommonValues.PermissionCodes.Vehicle.VehicleUpdate)]
        [HttpGet]
        public ActionResult VehicleUpdate(int id = 0)
        {
            var v = new VehicleIndexViewModel();
            if (id > 0)
            {
                var vehicleBo = new VehicleBL();
                SetDefaults();
                v.VehicleId = id;
                vehicleBo.GetVehicle(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleIndex, CommonValues.PermissionCodes.Vehicle.VehicleUpdate)]
        [HttpPost]
        public ActionResult VehicleUpdate(VehicleIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var vehicleBo = new VehicleBL();
            if (ModelState.IsValid)
            {
                // km bilgisi mevcutsa silinmesi veya 0 yapılması engelleniyor
                VehicleIndexViewModel existedModel = new VehicleIndexViewModel();
                existedModel.VehicleId = viewModel.VehicleId;
                vehicleBo.GetVehicle(UserManager.UserInfo, existedModel);
                if (existedModel.VehicleKilometer != null && viewModel.VehicleKilometer == null)
                {
                    viewModel.VehicleKilometer = existedModel.VehicleKilometer;
                    SetMessage(MessageResource.Vehicle_Warning_KM, CommonValues.MessageSeverity.Fail);
                }
                else
                {

                    viewModel.CommandType = viewModel.VehicleId > 0
                                                ? CommonValues.DMLType.Update
                                                : CommonValues.DMLType.Insert;
                    vehicleBo.DMLVehicle(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);
                }
            }
            return View(viewModel);
        }

        #endregion

        #region Vehicle Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleIndex, CommonValues.PermissionCodes.Vehicle.VehicleDelete)]
        public ActionResult DeleteVehicle(int vehicleId)
        {
            VehicleIndexViewModel viewModel = new VehicleIndexViewModel() { VehicleId = vehicleId };
            var vehicleBo = new VehicleBL();
            viewModel.CommandType = viewModel.VehicleId > 0 ? CommonValues.DMLType.Delete : string.Empty;
            vehicleBo.DMLVehicle(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion

        #region Vehicle Details
        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleIndex, CommonValues.PermissionCodes.Vehicle.VehicleDetails)]
        [HttpGet]
        public ActionResult VehicleDetails(int id = 0)
        {
            var v = new VehicleIndexViewModel();
            var vehicleBo = new VehicleBL();

            v.VehicleId = id;
            vehicleBo.GetVehicle(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion

        #region Vehicle History Index
        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleHistoryIndex)]
        [HttpGet]
        public ActionResult VehicleHistoryIndex(int? vehicleId)
        {
            var listModel = new VehicleHistoryListModel
            {
                VehicleId = vehicleId.GetValue<int>(),
                DealerId = UserManager.UserInfo.DealerID //UserManager.UserInfo.GetUserDealerId()
            };

            SetDefaults();
            return View(listModel);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleHistoryIndex)]
        public ActionResult ListVehicleHistory([DataSourceRequest] DataSourceRequest request, VehicleHistoryListModel model)
        {
            var vehicleBo = new VehicleBL();
            var v = new VehicleHistoryListModel(request);
            var totalCnt = 0;
            v.VehicleId = model.VehicleId;
            var returnValue = vehicleBo.ListVehicleHistory(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleHistoryIndex)]
        public ActionResult ListVehicleHistoryDetail([DataSourceRequest] DataSourceRequest request, VehicleHistoryDetailListModel model)
        {
            var vehicleBo = new VehicleBL();
            var v = new VehicleHistoryDetailListModel(request);
            var totalCnt = 0;
            v.VehicleHistoryId = model.VehicleHistoryId;
            var returnValue = vehicleBo.ListVehicleHistoryDetails(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleHistoryIndex)]
        public ActionResult ListVehicleHistoryDetailForVehicleHistoryScreen([DataSourceRequest] DataSourceRequest request, int VehicleHistoryId)
        {
            var vehicleBo = new VehicleBL();
            var v = new VehicleHistoryDetailListModel(request);
            var totalCnt = 0;
            v.VehicleHistoryId = VehicleHistoryId;
            var returnValue = vehicleBo.ListVehicleHistoryDetails(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }
        public ActionResult VehicleHistoryTotalPriceIndex(int id)
        {
            var model = new VehicleHistoryTotalPriceListModel() { VehicleId = id };

            var bl = new VehicleBL();
            int totalCount = 0;
            var rValue = bl.ListVehicleHistoryTotalPrice(UserManager.UserInfo, model, out totalCount).Data;
            model.TotalOtokarPrice = rValue.Sum(e => e.OtokarPrice.GetValue<decimal>());
            model.TotalCustomerPrice = rValue.Sum(e => e.CustomerPrice.GetValue<decimal>());

            return View(model);
        }

        public ActionResult ListVehicleHistoryTotalPrice([DataSourceRequest]DataSourceRequest request, VehicleHistoryTotalPriceListModel hModel)
        {
            var bl = new VehicleBL();
            var model = new VehicleHistoryTotalPriceListModel(request) { VehicleId = hModel.VehicleId };
            int totalCount = 0;

            var rValue = bl.ListVehicleHistoryTotalPrice(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });

        }

        #endregion

        #region Vehicle Contact Info

        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleIndex, CommonValues.PermissionCodes.Vehicle.VehicleUpdate)]
        [HttpGet]
        public ActionResult VehicleContactEdit(int id) /*vehicleId*/
        {
            return PartialView("~/Views/WorkOrderCard/_VehicleContactInfo.cshtml",
                new VehicleBL().GetVehicleContactInfo(id).Model);
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleIndex, CommonValues.PermissionCodes.Vehicle.VehicleUpdate)]
        [HttpPost]
        public ActionResult GetVehicleContactAsJson(int id) /*vehicleId*/
        {
            return Json(new VehicleBL().GetVehicleContactInfo(id).Model);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Vehicle.VehicleIndex, CommonValues.PermissionCodes.Vehicle.VehicleUpdate)]
        public ActionResult UpdateContactInfo(VehicleContactInfoModel model)
        {
            if (ModelState.IsValid)
            {
                new VehicleBL().UpdateVehicleContactInfo(UserManager.UserInfo, model);
            }
            return
                GenerateAsyncOperationResponse(
                    model.ErrorNo > 0 ? AsynOperationStatus.Error : AsynOperationStatus.Success,
                    model.ErrorNo > 0 ? model.ErrorDesc : MessageResource.Global_Display_Success);
        }

        #endregion
        public ActionResult VehicleNotesUpdateByExcel(HttpPostedFileBase uexcelFile)
        {
            VehicleIndexViewModel newModel = new VehicleIndexViewModel();
            SetDefaults();

            //
            if (uexcelFile != null)
            {
                //StockCardBL bo = new StockCardBL();
                VehicleBL bo = new VehicleBL();
                Stream s = uexcelFile.InputStream;
                VehicleIndexViewModel viewMo = new VehicleIndexViewModel();

                List<VehicleIndexViewModel> listModel = bo.ParseExcel_UpdateNotesForVehicle(UserManager.UserInfo, viewMo, s).Data;

                if (viewMo.ErrorNo > 0)
                {
                    var ms = bo.SetExcelReportByVehicleNotesUpdate(listModel, viewMo.ErrorMessage);

                    var fileViewModel = new DownloadFileViewModel
                    {
                        FileName = CommonValues.ErrorLog + DateTime.Now + CommonValues.ExcelExtOld,
                        ContentType = CommonValues.ExcelContentType,
                        MStream = ms,
                        Id = Guid.NewGuid()
                    };

                    Session[CommonValues.DownloadFileFormatCookieKey] = fileViewModel.Add(fileViewModel);
                    TempData[CommonValues.DownloadFileIdCookieKey] = fileViewModel.Id;

                    SetMessage(viewMo.ErrorMessage, CommonValues.MessageSeverity.Fail);

                    return View(newModel);
                }
                else
                {
                    //StringBuilder VinCodes = new StringBuilder();
                    //foreach (VehicleIndexViewModel mo in listModel)
                    //{
                    //    VinCodes.Append(mo.VinNo);
                    //    VinCodes.Append(",");
                    //}
                    //if (VinCodes.Length > 0)
                    //{
                    //    VinCodes.Remove(VinCodes.Length - 1, 1);
                    //    newModel.VinCodeList = VinCodes.ToString();
                    //}
                    ModelBase rmodel = bo.UpdateNotesForVehicle(UserManager.UserInfo, listModel).Model;
                    if (rmodel.ErrorNo == 0)
                    {
                        SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                    }
                    else
                    {
                        SetMessage(MessageResource.Global_Display_Error, CommonValues.MessageSeverity.Fail);
                    }
                }
            }
            else
            {
                SetMessage(MessageResource.Global_Warning_ExcelNotSelected, CommonValues.MessageSeverity.Fail);
            }
            return View("VehicleIndex");
        }
        public ActionResult ExcelSample()
        {
            var bo = new VehicleBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.Vehicle_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        public ActionResult VehicleNotesUpdateBySampleExcel()
        {
            var bo = new VehicleBL();
            var ms = bo.SetExcelReportByVehicleNotesUpdate(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.Vehicle_UpdateNotes_Display + CommonValues.ExcelExtOld);
        }
    }
}
