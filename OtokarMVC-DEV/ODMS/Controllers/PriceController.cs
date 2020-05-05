using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSModel.Price;
using ODMSBusiness.Business;
using System.Web;
using System.IO;
using ODMSModel.StockCard;
using ODMSModel.DownloadFileActionResult;
using System.Collections.Generic;
using System;
using System.Text;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    public class PriceController : ControllerBase
    {
        [AuthorizationFilter(CommonValues.PermissionCodes.PriceList.PriceListIndex)]
        public ActionResult PriceIndex()
        {
            var bl = new PriceBL();
            ViewBag.SLPriceList = bl.PriceListCombo(UserManager.UserInfo).Data;
            ViewBag.PartClassList = new PartWithModelPairingBL().ListCodesCombo(UserManager.UserInfo).Data;
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PriceList.PriceListIndex)]
        [HttpPost]
        public ActionResult PriceIndex(PriceListModel model, HttpPostedFileBase excelFile)
        {
            var bl = new PriceBL();
            ViewBag.SLPriceList = bl.PriceListCombo(UserManager.UserInfo).Data;
            ViewBag.PartClassList = new PartWithModelPairingBL().ListCodesCombo(UserManager.UserInfo).Data;

            if (excelFile != null)
            {
                StockCardBL bo = new StockCardBL();
                Stream s = excelFile.InputStream;
                StockCardViewModel viewMo = new StockCardViewModel();
                List<StockCardViewModel> listModel = bo.ParseExcel(UserManager.UserInfo, viewMo, s).Data;

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

                    return View(model);
                }
                else
                {
                    StringBuilder partCodes = new StringBuilder();
                    foreach (StockCardViewModel mo in listModel)
                    {
                        partCodes.Append(mo.PartCode);
                        partCodes.Append(",");
                    }
                    if (partCodes.Length > 0)
                    {
                        partCodes.Remove(partCodes.Length - 1, 1);
                        model.PartCodeList = partCodes.ToString();
                    }
                }
            }
            else
            {
                SetMessage(MessageResource.Global_Warning_ExcelNotSelected, CommonValues.MessageSeverity.Fail);
            }
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PriceList.PriceListIndex)]
        public ActionResult ListPrice([DataSourceRequest]DataSourceRequest reqest, PriceListModel hModel)
        {
            int totalCount = 0;
            var bl = new PriceBL();
            var model = new PriceListModel(reqest)
            {
                PartId = hModel.PartId,
                PriceListId = hModel.PriceListId,
                PartCode = hModel.PartCode,
                PartName = hModel.PartName,
                StartDate = hModel.StartDate,
                EndDate = hModel.EndDate,
                PartClassList = hModel.PartClassList.AddSingleQuote(),
                PartCodeList = hModel.PartCodeList.AddSingleQuote(),
                IsValid = hModel.IsValid
            };

            var rValue = bl.ListPrice(UserManager.UserInfo, model, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

    }
}
