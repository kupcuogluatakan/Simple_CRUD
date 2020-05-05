using System.Linq;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.DealerSaleSparepart;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ODMSModel.SparePart;
using System.IO;
using ODMSModel.DownloadFileActionResult;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DealerSaleSparepartController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.IsActiveList = CommonBL.ListStatus().Data;
        }

        [HttpGet]
        public JsonResult GetSparepartListPrice(Int64? idPart)
        {
            if (idPart != null)
            {
                return Json(new DealerSaleSparepartBL().GetSparepartListPrice(UserManager.UserInfo, idPart).Model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new List<DealerSaleSparepartIndexViewModel>(), JsonRequestBehavior.AllowGet);
            }
        }

        #region DealerSaleSparepart Index

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartIndex)]
        [HttpGet]
        public ActionResult DealerSaleSparepartIndex()
        {
            SetDefaults();

            DealerSaleSparepartListModel model = new DealerSaleSparepartListModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartIndex)]
        [HttpPost]
        public ActionResult UploadListFromExcel(HttpPostedFileBase uploadFile)
        {

            ListDealerSaleSparepartIndexViewModel model = new ListDealerSaleSparepartIndexViewModel()
            {
                ListModel = new List<DealerSaleSparepartIndexViewModel>()
            };

            if (uploadFile != null)
            {
                var bo = new DealerSaleSparepartBL();
                Stream s = uploadFile.InputStream;
                List<ListDealerSaleSparepartIndexViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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

                    return RedirectToAction("DealerSaleSparepartIndex");
                }
                else
                {
                    foreach (var item in modelList[0].ListModel)
                    {
                        SparePartIndexViewModel temp = new SparePartIndexViewModel() { PartCode = item.PartCode };
                        new SparePartBL().GetSparePart(UserManager.UserInfo, temp);
                        //

                        item.ListPrice = bo.GetSparepartListPrice(UserManager.UserInfo, temp.PartId).Model.ListPrice;
                        item.SalePrice = item.ListPrice - (item.DiscountRatio * item.ListPrice / 100);
                        item.ShipQty = temp.ShipQuantity.GetValue<decimal>();
                        item.IsActive = true;
                        item.IdPart = temp.PartId;

                        if (UserManager.UserInfo.GetUserDealerId() != 0)
                            item.IdDealer = UserManager.UserInfo.GetUserDealerId();
                    }
                    var _model = new ListDealerSaleSparepartIndexViewModel() { ListModel = modelList[0].ListModel };
                    var returnList = new DealerSaleSparepartBL().DMLDealerSaleSparepartList(UserManager.UserInfo, _model).Model;


                    var ms = bo.SetExcelReportForReturnStatus(returnList, "İşlem Raporu");

                    var fileViewModel = new DownloadFileViewModel
                    {
                        FileName = CommonValues.ProcessReport + DateTime.Now + CommonValues.ExcelExtOld,
                        ContentType = CommonValues.ExcelContentType,
                        MStream = ms,
                        Id = Guid.NewGuid()
                    };

                    Session[CommonValues.DownloadFileFormatCookieKey] = fileViewModel.Add(fileViewModel);
                    TempData[CommonValues.DownloadFileIdCookieKey] = fileViewModel.Id;

                    SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Fail);
                }
            }
            else
            {
                SetMessage(MessageResource.Global_Warning_ExcelNotSelected, CommonValues.MessageSeverity.Fail);
            }
            return RedirectToAction("DealerSaleSparepartIndex");
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartIndex, CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartDetails)]
        public ActionResult ListDealerSaleSparepart([DataSourceRequest] DataSourceRequest request, DealerSaleSparepartListModel model)
        {
            var dealerSaleSparepartBo = new DealerSaleSparepartBL();

            var v = new DealerSaleSparepartListModel(request);
            v.IdDealer = model.IdDealer;
            v.DiscountPrice = model.DiscountPrice;
            v.DiscountRatio = model.DiscountRatio;
            v.StockQuantity = model.StockQuantity;
            v.ListPrice = model.ListPrice;
            v.PartName = model.PartName;
            v.PartCode = model.PartCode;
            v.CreateDate = model.CreateDate;
            v.IsActive = model.IsActive;

            var totalCnt = 0;
            var returnValue = dealerSaleSparepartBo.ListDealerSaleSparepart(UserManager.UserInfo, v, out totalCnt).Data;

            if (model.DoNotReturnStockQuantityZero)
            {
                returnValue = (from e in returnValue.AsEnumerable()
                               where e.StockQuantity != 0
                               select e).ToList<DealerSaleSparepartListModel>();
            }

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region DealerSaleSparepart Create

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartIndex, CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartCreate)]
        public ActionResult DealerSaleSparepartCreate()
        {
            SetDefaults();

            var model = new DealerSaleSparepartIndexViewModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            model.IsActive = true;
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartCreate, CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartCreate)]
        [HttpPost]
        public ActionResult DealerSaleSparepartCreate(DealerSaleSparepartIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();

            //Bu ekranda merkez kullanıcısının create ve update yetkisi bulunmaması gerekir.
            //DealerID = 0 olduğu durumlarda insert ve update FK hatası ortaya çıkar.
            if (UserManager.UserInfo.GetUserDealerId() == 0)
            {
                SetMessage(MessageResource.DealerSaleSparepart_Error_NoCenterUser, CommonValues.MessageSeverity.Fail);

                return View();
            }

            var dealerSaleSparepartBo = new DealerSaleSparepartBL();

            DealerSaleSparepartIndexViewModel viewControlModel = new DealerSaleSparepartIndexViewModel();

            if (UserManager.UserInfo.GetUserDealerId() != 0)
                viewControlModel.IdDealer = UserManager.UserInfo.GetUserDealerId();
            viewControlModel.IdPart = viewModel.IdPart;

            dealerSaleSparepartBo.GetDealerSaleSparepart(UserManager.UserInfo, viewControlModel);

            if (viewControlModel.DiscountRatio == null)
            {
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;

                    if (UserManager.UserInfo.GetUserDealerId() != 0)
                        viewModel.IdDealer = UserManager.UserInfo.GetUserDealerId();

                    dealerSaleSparepartBo.DMLDealerSaleSparepart(UserManager.UserInfo, viewModel);

                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                    return View();
                }

                return View(viewModel);
            }
            else
            {
                SetMessage(MessageResource.CustomerDiscount_Error_DataHave, CommonValues.MessageSeverity.Fail);

                ModelState.Clear();
                return View();
            }
        }

        #endregion

        #region DealerSaleSparepart Update
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartIndex, CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartUpdate)]
        [HttpGet]
        public ActionResult DealerSaleSparepartUpdate(Int64 id, int dealerId)
        {
            SetDefaults();
            var v = new DealerSaleSparepartIndexViewModel();
            if (id != 0)//if (!string.IsNullOrEmpty(id))
            {
                var dealerSaleSparepartBo = new DealerSaleSparepartBL();
                v.IdPart = id;
                v.IdDealer = UserManager.UserInfo.GetUserDealerId() == 0 ? dealerId : UserManager.UserInfo.GetUserDealerId();


                dealerSaleSparepartBo.GetDealerSaleSparepart(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartIndex, CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartUpdate)]
        [HttpPost]
        public ActionResult DealerSaleSparepartUpdate(DealerSaleSparepartIndexViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var dealerSaleSparepartBo = new DealerSaleSparepartBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;

                dealerSaleSparepartBo.DMLDealerSaleSparepart(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }

            dealerSaleSparepartBo.GetDealerSaleSparepart(UserManager.UserInfo, viewModel);
            return View(viewModel);
        }

        #endregion

        #region DealerSaleSparepart Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartIndex, CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartDelete)]
        public ActionResult DeleteDealerSaleSparepart(Int64 idPart, int idDealer)
        {
            DealerSaleSparepartIndexViewModel viewModel = new DealerSaleSparepartIndexViewModel
            {
                IdPart = idPart,
                IdDealer = idDealer
            };

            var dealerSaleSparepartBo = new DealerSaleSparepartBL();
            dealerSaleSparepartBo.GetDealerSaleSparepart(UserManager.UserInfo, viewModel);

            viewModel.CommandType = CommonValues.DMLType.Delete;
            dealerSaleSparepartBo.DMLDealerSaleSparepart(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }

        #endregion

        #region DealerSaleSparepart Details
        [AuthorizationFilter(CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartIndex, CommonValues.PermissionCodes.DealerSaleSparepart.DealerSaleSparepartDetails)]
        [HttpGet]
        public ActionResult DealerSaleSparepartDetails(Int64? id, int? idDealer)
        {
            var v = new DealerSaleSparepartIndexViewModel();
            var DealerSaleSparepartBo = new DealerSaleSparepartBL();

            v.IdPart = id;
            v.IdDealer = UserManager.UserInfo.GetUserDealerId();
            SetDefaults();
            DealerSaleSparepartBo.GetDealerSaleSparepart(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion

        #region Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetShipQtyAndUnitByPartId(int partId)
        {
            var sparePartService = new SparePartBL();

            SparePartIndexViewModel model = new SparePartIndexViewModel() { PartId = partId };

            sparePartService.GetSparePart(UserManager.UserInfo, model);


            return Json(new
            {
                ShipQty = model.ShipQuantity,
                Unit = model.UnitName
            });
        }

        #endregion

        #region Excel Upload
        public ActionResult UploadExcelSample()
        {
            var bo = new DealerSaleSparepartBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.DealerSaleSparepart_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        #endregion
    }
}