using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.CriticalStockCard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ODMSModel.DownloadFileActionResult;
using ODMSModel.SparePart;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CriticalStockCardController : ControllerBase
    {
        #region Member Varables

        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
        }

        #endregion

        #region CriticalStockCard Index

        [AuthorizationFilter(CommonValues.PermissionCodes.CriticalStockCard.CriticalStockCardIndex)]
        [HttpGet]
        public ActionResult CriticalStockCardIndex()
        {
            SetDefaults();
            CriticalStockCardListModel model = new CriticalStockCardListModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CriticalStockCard.CriticalStockCardIndex, CommonValues.PermissionCodes.CriticalStockCard.CriticalStockCardIndex)]
        public ActionResult ListCriticalStockCard([DataSourceRequest] DataSourceRequest request, CriticalStockCardListModel model)
        {
            var criticalStockCardBo = new CriticalStockCardBL();

            var v = new CriticalStockCardListModel(request)
            {
                IdDealer = model.IdDealer,
                IdPart = model.IdPart,
                PartCode = model.PartCode,
                PartName = model.PartName,
                StockCardId = model.StockCardId
            };

            var totalCnt = 0;
            var returnValue = criticalStockCardBo.ListCriticalStockCard(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region CriticalStockCard Create

        [AuthorizationFilter(CommonValues.PermissionCodes.CriticalStockCard.CriticalStockCardIndex, CommonValues.PermissionCodes.CriticalStockCard.CriticalStockCardCreate)]
        public ActionResult CriticalStockCardCreate()
        {
            SetDefaults();

            var model = new CriticalStockCardViewModel();
            model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CriticalStockCard.CriticalStockCardIndex, CommonValues.PermissionCodes.CriticalStockCard.CriticalStockCardCreate)]
        [HttpPost]
        public ActionResult CriticalStockCardCreate(CriticalStockCardViewModel viewModel)
        {
            var criticalStockCardBo = new CriticalStockCardBL();
            viewModel.IdDealer = UserManager.UserInfo.GetUserDealerId();

            CriticalStockCardViewModel viewControlModel = new CriticalStockCardViewModel();
            viewControlModel.IdDealer = viewModel.IdDealer;
            viewControlModel.IdPart = viewModel.IdPart;
            criticalStockCardBo.GetCriticalStockCard(UserManager.UserInfo, viewControlModel);

            SetDefaults();

            if (viewControlModel.CriticalStockQuantity == null)
            {
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;

                    criticalStockCardBo.DMLCriticalStockCard(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                }

            }
            else //Kayıt varsa Quantity güncelleniyor
            {
                if (ModelState.IsValid)
                {
                    viewModel.CommandType = CommonValues.DMLType.Update;

                    criticalStockCardBo.DMLCriticalStockCard(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                }

            }
            var model = new CriticalStockCardViewModel { IdDealer = UserManager.UserInfo.GetUserDealerId() };
            return View(model);
        }

        #endregion

        #region CriticalStockCard Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CriticalStockCard.CriticalStockCardIndex, CommonValues.PermissionCodes.CriticalStockCard.CriticalStockCardDelete)]
        public ActionResult DeleteCriticalStockCard(Int64 idPart, int idDealer)
        {
            CriticalStockCardViewModel viewModel = new CriticalStockCardViewModel { IdPart = idPart, IdDealer = idDealer };

            var criticalStockCardBo = new CriticalStockCardBL();

            viewModel.CommandType = CommonValues.DMLType.Delete;

            criticalStockCardBo.DMLCriticalStockCard(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetShipQtyByPartId(int partId)
        {
            var sparePartService = new SparePartBL();

            SparePartIndexViewModel model = new SparePartIndexViewModel() { PartId = partId };

            sparePartService.GetSparePart(UserManager.UserInfo, model);


            return Json(new
            {
                ShipQty = model.ShipQuantity
            });
        }

        #endregion

        #region Excel Upload

        public ActionResult ExcelSample()
        {
            var bo = new CriticalStockCardBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.CriticalStockCard_PageTitle_Index + CommonValues.ExcelExtOld);
        }
        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.CriticalStockCard.CriticalStockCardIndex, CommonValues.PermissionCodes.CriticalStockCard.CriticalStockCardExcel)]
        public ActionResult CriticalStockCardIndex(CriticalStockCardListModel listModel, HttpPostedFileBase excelFile)
        {

            CriticalStockCardViewModel model = new CriticalStockCardViewModel();

            SetDefaults();

            if (ModelState.IsValid)
            {
                if (excelFile != null)
                {
                    var bo = new CriticalStockCardBL();
                    Stream s = excelFile.InputStream;
                    List<CriticalStockCardViewModel> modelList = bo.ParseExcel(UserManager.UserInfo, model, s).Data;
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
                    }
                    else
                    {
                        foreach (var row in modelList)
                        {
                            CriticalStockCardViewModel inserted = new CriticalStockCardViewModel
                            {
                                IdDealer = row.IdDealer,
                                IdPart = row.IdPart
                            };

                            bo.GetCriticalStockCard(UserManager.UserInfo, inserted);
                            row.CommandType = inserted.CriticalStockQuantity != null ? CommonValues.DMLType.Update : CommonValues.DMLType.Insert;
                            bo.DMLCriticalStockCard(UserManager.UserInfo, row);
                            if (row.ErrorNo > 0)
                            {
                                SetMessage(row.ErrorMessage, CommonValues.MessageSeverity.Fail);
                                return View(listModel);
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
            return View(listModel);
        }

        #endregion
    }
}