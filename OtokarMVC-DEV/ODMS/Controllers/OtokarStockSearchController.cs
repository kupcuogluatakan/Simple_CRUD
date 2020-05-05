using System.Web.Mvc;
using ODMS.Filters;
using ODMSBusiness;
using ODMSModel.OtokarStockSearch;
using System.Web;
using System.IO;
using ODMSModel.StockCard;
using System.Collections.Generic;
using ODMSModel.DownloadFileActionResult;
using System;
using ODMSCommon;
using System.Text;
using ODMSCommon.Resources;
using Kendo.Mvc.UI;
using System.Data;
using System.Web.Configuration;
using ODMS.OtokarService;
using ODMSModel.SparePart;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class OtokarStockSearchController : ControllerBase
    {

        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.OtokarStockSearh.OtokarStockSeachIndex)]
        public ActionResult OtokarStockSearchIndex()
        {
            return View(new OtokarStockSearchViewModel());
        }

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.OtokarStockSearh.OtokarStockSeachIndex)]
        public ActionResult OtokarStockSearchIndex(OtokarStockSearchViewModel model, HttpPostedFileBase excelFile)
        {
            if (excelFile != null)
            {
                StockCardBL bo = new StockCardBL();
                Stream s = excelFile.InputStream;
                StockCardViewModel viewMo = new StockCardViewModel();
                List<StockCardViewModel> listModel = bo.ParseExcelByOtokarStockSearch(UserManager.UserInfo, viewMo, s).Data;

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

        public string GetPartQuantity(string partCode)
        {
            if (General.IsTest)
                return "0";

            string quantity = string.Empty;
            using (var pssc = GetClient())
            {
                string psUser = WebConfigurationManager.AppSettings["PSSCUser"];
                string psPassword = WebConfigurationManager.AppSettings["PSSCPass"];

                DataSet rValue = pssc.ZYP_SD_WEB_MATERIAL_ATP2(psUser, psPassword, partCode);
                if (rValue.Tables[0].Rows.Count > 0)
                {
                    var row = rValue.Tables[0].Rows[0];
                    quantity = $"{row[1]} {row[2]}";
                }
            }
            return quantity;
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.OtokarStockSearh.OtokarStockSeachIndex)]
        public ActionResult ListOtokarStockSearchIndex([DataSourceRequest] DataSourceRequest request, OtokarStockSearchViewModel model)
        {
            List<OtokarStockSearchViewModel> returnModel = new List<OtokarStockSearchViewModel>();
            if (!string.IsNullOrEmpty(model.PartCodeList))
            {
                string[] partCodeList = model.PartCodeList.Split(',');
                foreach (var item in partCodeList)
                {
                    returnModel.Add(new OtokarStockSearchViewModel
                    {
                        PartCode = item,
                        WebServiceResult = GetPartQuantity(item)
                    });
                }
            }

            if (model.PartId != null)
            {
                var v = new SparePartIndexViewModel();
                var sparePartBo = new SparePartBL();

                v.PartId = model.PartId.GetValue<int>();

                sparePartBo.GetSparePart(UserManager.UserInfo, v);

                if (!string.IsNullOrEmpty(v.PartCode))
                {
                    if (v.OriginalPartId != 0)
                    {
                        SetMessage(MessageResource.Exception_Not_Original_Part, CommonValues.MessageSeverity.Fail);
                    }
                    returnModel.Add(new OtokarStockSearchViewModel
                    {
                        PartCode = v.PartCode,
                        WebServiceResult = GetPartQuantity(v.PartCode)
                    });


                    SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                }
                else
                {
                    SetMessage(MessageResource.Exception_PartNotFound, CommonValues.MessageSeverity.Fail);
                }

            }
            return Json(new
            {
                Data = returnModel,
                Total = returnModel.Count
            });
        }
        public ActionResult ExcelSample()
        {
            var bo = new StockCardBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.StockCard_PageTitle_Search + CommonValues.ExcelExtOld);
        }
    }
}