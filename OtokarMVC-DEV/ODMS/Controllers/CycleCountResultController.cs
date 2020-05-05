using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.CycleCount;
using ODMSModel.CycleCountResult;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CycleCountResultController : ControllerBase
    {
        private void SetDefaults()
        {
            // WarehouseList
            List<SelectListItem> warehouseList =
                WarehouseBL.ListWarehousesOfDealerAsSelectList(UserManager.UserInfo.GetUserDealerId()).Data;
            ViewBag.WarehouseList = warehouseList;
        }
        [HttpGet]
        public JsonResult ListRacks(int? id)
        {
            if (id.HasValue)
            {
                List<SelectListItem> rackList = CommonBL.ListRacks(UserManager.UserInfo, id.Value).Data;
                rackList.Insert(0, new SelectListItem() { Text = MessageResource.Global_Display_Choose, Value = "0" });

                return Json(rackList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        #region CycleCount Result Index

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountResult.CycleCountResultIndex)]
        [HttpGet]
        public ActionResult CycleCountResultIndex(string cycleCountId)
        {
            CycleCountResultListModel model = new CycleCountResultListModel();
            if (cycleCountId != null)
            {
                CycleCountViewModel cycleCountModel = new CycleCountViewModel();
                CycleCountBL cycleCountBo = new CycleCountBL();
                cycleCountModel.CycleCountId = cycleCountId;
                cycleCountBo.GetCycleCount(UserManager.UserInfo, cycleCountModel);

                model.CycleCountId = cycleCountId.GetValue<int>();

                ViewBag.CycleCountStatus = cycleCountModel.CycleCountStatus;
                ViewBag.DisplayCurrentAmount = cycleCountModel.DisplayCurrentAmount;
            }

            SetDefaults();
            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountResult.CycleCountResultSave)]
        [HttpPost]
        public ActionResult CycleCountResultSave([DataSourceRequest] DataSourceRequest request,
                                                 [Bind(Prefix = "models")] IEnumerable<CycleCountResultListModel>
                                                     cyclecountResultList)
        {
            int cycleCountId = (cyclecountResultList.ElementAt(0) as CycleCountResultListModel).CycleCountId;
            SaveResults(cyclecountResultList, cycleCountId);

            CycleCountResultListModel model = new CycleCountResultListModel();
            model.CycleCountId = cycleCountId;

            CycleCountViewModel cycleCountModel = new CycleCountViewModel();
            CycleCountBL cycleCountBo = new CycleCountBL();
            cycleCountModel.CycleCountId = cycleCountId.GetValue<string>();
            cycleCountBo.GetCycleCount(UserManager.UserInfo, cycleCountModel);
            ViewBag.CycleCountStatus = cycleCountModel.CycleCountStatus;

            return ListCycleCountResult(request, model);
        }

        private static void SaveResults(IEnumerable<CycleCountResultListModel> cyclecountResultList, int cycleCountId)
        {
            CycleCountResultBL resultBl = new CycleCountResultBL();
            CycleCountResultViewModel resultViewModel = new CycleCountResultViewModel();

            foreach (var result in cyclecountResultList.Where(e=>e.AfterCountQuantity != e.BeforeCountQuantity 
                                                                || e.ApprovedCountQuantity != e.AfterCountQuantity
                                                                || (e.AfterCountQuantity == e.BeforeCountQuantity && e.AfterCountQuantity == 0)
                                                                ))
            {
                resultViewModel.CycleCountId = cycleCountId;
                resultViewModel.CycleCountResultId = result.CycleCountResultId;
                resultBl.GetCycleCountResult(UserManager.UserInfo, resultViewModel);
                resultViewModel.AfterCountQuantity = result.AfterCountQuantity.Value;
                resultViewModel.ApprovedCountQuantity = result.ApprovedCountQuantity;
                resultViewModel.CommandType = CommonValues.DMLType.Update;
                resultBl.DMLCycleCountResult(UserManager.UserInfo, resultViewModel);
            }
        }



        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountResult.CycleCountResultSave)]
        [HttpPost]
        public ActionResult CycleCountResultSendApproval(int cycleCountId)
        {
            CycleCountViewModel model = new CycleCountViewModel { CycleCountId = cycleCountId.GetValue<string>() };
            CycleCountBL cycleCountBl = new CycleCountBL();
            cycleCountBl.GetCycleCount(UserManager.UserInfo, model);
            
            model.CommandType = CommonValues.DMLType.Update;
            model.CycleCountStatus = (int)CommonValues.CycleCountStatus.Finished;
            model.EndDate = DateTime.Now;
            cycleCountBl.DMLCycleCount(UserManager.UserInfo, model);


            if (model.ErrorNo == 0)
            {
                SetMessage(MessageResource.CycleCountResult_Approve_Message, CommonValues.MessageSeverity.Success);
                TempData["Message"] = MessageResource.CycleCountResult_Approve_Message_Link;
            }


            return Json(string.Empty);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountResult.CycleCountResultIndex)]
        public ActionResult ListCycleCountResult([DataSourceRequest] DataSourceRequest request, CycleCountResultListModel model)
        {
            var cycleCountResultBo = new CycleCountResultBL();
            var v = new CycleCountResultListModel(request);
            var totalCnt = 0;
            v.CycleCountId = model.CycleCountId;

            if (model.PageFlag)
                v.PageSize = 0;

            CycleCountViewModel cycleCountModel = new CycleCountViewModel();
            CycleCountBL cycleCountBo = new CycleCountBL();
            cycleCountModel.CycleCountId = model.CycleCountId.GetValue<string>();
            cycleCountBo.GetCycleCount(UserManager.UserInfo, cycleCountModel);
            ViewBag.CycleCountStatus = cycleCountModel.CycleCountStatus;
            ViewBag.DisplayCurrentAmount = cycleCountModel.DisplayCurrentAmount;

            var returnList = cycleCountResultBo.ListCycleCountResults(UserManager.UserInfo, v, out totalCnt).Data;
           
            /*StockTypeDetailBL stockTypeDetailBL = new StockTypeDetailBL();
            List<CycleCountResultAuditViewModel> listModel = new List<CycleCountResultAuditViewModel>();
            foreach (var item in returnList)
            {
                item.CycleCountStatus = cycleCountModel.CycleCountStatus;

                //if (item.QtyState == StockState.Green)
                //{
                    CycleCountResultAuditViewModel auditModel = new CycleCountResultAuditViewModel
                    {
                        WarehouseId = item.WarehouseId,
                        RackId = item.RackId,
                        PartId = item.StockCardId.Value,
                        StockCardId = item.RealStockCardId,
                        CycleCountId = item.CycleCountId
                    };
                    auditModel.CycleCountAuditList = stockTypeDetailBL.ListStokTypeAudit(UserManager.UserInfo, auditModel).Data;
                    
                    listModel.Add(auditModel);
                //}
            }*/

            Session["DefaultInsertStockDiff"] = returnList;

            return Json(new
            {
                Data = returnList.OrderByDescending(e=>e.StockDiffQty),
                Total = totalCnt
            });
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountResult.CycleCountResultIndex, CommonValues.PermissionCodes.CycleCountResult.CycleCountResultCreate)]
        [HttpGet]
        public ActionResult CycleCountResultAudit(int cycleCountId, int stockCardId, int warehouseId, string warehouseName, int rackId, string RackName, string partName, int partId, decimal expQty, decimal bfrQty, int status)
        {
            CycleCountStockDiffBL cycleCountStockBL = new CycleCountStockDiffBL();

            StockTypeDetailBL stockTypeDetailBL = new StockTypeDetailBL();

            CycleCountResultAuditViewModel model = new CycleCountResultAuditViewModel()
            {
                CycleCountId = cycleCountId,
                StockCardId = stockCardId,
                WarehouseId = warehouseId,
                WarehouseName = warehouseName,
                RackId = rackId,
                RackName = RackName,
                PartName = partName,
                PartId = partId,
                ExpectedQty = expQty,
                BfrQty = bfrQty,
                Status = status
            };

            Dictionary<bool, decimal> result = cycleCountStockBL.Exists(model.CycleCountId, model.StockCardId, warehouseId).Model;

            model.CycleCountAuditList = result.FirstOrDefault().Key ? cycleCountStockBL.ListStokTypeAudit(UserManager.UserInfo, model).Data : stockTypeDetailBL.ListStokTypeAudit(UserManager.UserInfo, model).Data;

            model.CycleCountAuditList = model.CycleCountAuditList.OrderBy(p => p.AfterQty).ToList();

            var htmlOutput = new StringBuilder();
            foreach (var item in model.CycleCountAuditList)
            {
                if (item.CycleCountDiffAllow)
                {
                    htmlOutput.Append(CommonValues.RowStart
                                      + @CommonValues.ColumnStart + item.StockName +
                                      CommonValues.ColumnEnd + CommonValues.ColumnStart +
                                      "<input readonly='true' type='text' value='" + item.BeforeQty + "'/>" +
                                      CommonValues.ColumnEnd +
                                      CommonValues.ColumnStart +
                                      "<input class='newValues' type='text' maxlength='6' value='" + item.AfterQty +
                                      "' />"
                                      + CommonValues.ColumnEnd
                                      + CommonValues.RowEnd);
                }
                else
                {
                    htmlOutput.Append(CommonValues.RowStart
                                      + @CommonValues.ColumnStart + item.StockName + CommonValues.ColumnEnd +
                                      CommonValues.ColumnStart + "<input readonly='true' type='text' value='" +
                                      item.BeforeQty + "'/>" +
                                      CommonValues.ColumnEnd +
                                      CommonValues.ColumnStart +
                                      "<input readonly='true' class='newValues' type='text' maxlength='6' value='" +
                                      item.AfterQty + "' />" + CommonValues.ColumnEnd
                                      + CommonValues.RowEnd);
                }

            }

            ViewBag.HtmlOutput = htmlOutput.ToString();

            Session["CycleCountAuditList"] = model.CycleCountAuditList;

            return PartialView(model);
        }

        #endregion

        #region CycleCount Result Create
        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountResult.CycleCountResultIndex, CommonValues.PermissionCodes.CycleCountResult.CycleCountResultCreate)]
        [HttpGet]
        public ActionResult CycleCountResultCreate(int cycleCountId)
        {
            CycleCountResultViewModel model = new CycleCountResultViewModel { CycleCountId = cycleCountId };
            SetDefaults();
            return View(model);
        }

        private bool ValidateCycleCountResult(CycleCountResultViewModel viewModel)
        {
            int totalResultCount = 0;
            // kapsayan kayıt var mı kontrol ediliyor.
            CycleCountResultListModel resultListModel = new CycleCountResultListModel
            {
                CycleCountId = viewModel.CycleCountId
            };
            CycleCountResultBL resultBL = new CycleCountResultBL();
            List<CycleCountResultListModel> resultList = resultBL.ListCycleCountResults(UserManager.UserInfo, resultListModel, out totalResultCount).Data;
            if (totalResultCount != 0)
            {
                // yeni eklenen kayıdın depo bilgisi doğru mu kontrol edilir.

                //selectedCount = (from pl in resultList.AsEnumerable() where pl.WarehouseId == viewModel.WarehouseId select pl).Count();
                int selectedCount = new CycleCountPlanBL().Exists(viewModel.CycleCountId, viewModel.WarehouseId.Value).Model;

                // aynı depo ile kayıt yoksa depo seçimi yanlıştır.
                if (selectedCount == 0)
                {
                    SetMessage(MessageResource.CycleCountResult_Warning_DetailNotFound, CommonValues.MessageSeverity.Fail);
                    return false;
                }

                // yeni eklenen kayıttan var mı bakılır
                selectedCount = (from pl in resultList.AsEnumerable()
                                 where pl.WarehouseId == viewModel.WarehouseId && pl.RackId == viewModel.RackId
                                       && pl.StockCardId == viewModel.StockCardId
                                 select pl).Count();
                // aynı kayıttan varsa
                if (selectedCount != 0)
                {
                    SetMessage(MessageResource.CycleCountPlan_Warning_SameDataFound, CommonValues.MessageSeverity.Fail);
                    return false;
                }
            }
            return true;
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountResult.CycleCountResultIndex, CommonValues.PermissionCodes.CycleCountResult.CycleCountResultCreate)]
        [HttpPost]
        public ActionResult CycleCountResultCreate(CycleCountResultViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var cycleCountResultBo = new CycleCountResultBL();

            if (ModelState.IsValid)
            {
                if (ValidateCycleCountResult(viewModel))
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;
                    cycleCountResultBo.DMLCycleCountResult(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                    CycleCountResultViewModel model = new CycleCountResultViewModel
                    {
                        CycleCountId = viewModel.CycleCountId
                    };
                    return View(model);
                }
            }
            return View(viewModel);
        }
        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountResult.CycleCountResultIndex, CommonValues.PermissionCodes.CycleCountResult.CycleCountResultDelete)]
        [HttpPost]
        public ActionResult DeleteCycleResult(int cycleResultId, int cycleCountId)
        {
            SetDefaults();
            var cycleCountResultBo = new CycleCountResultBL();
            CycleCountResultViewModel model = new CycleCountResultViewModel
            {
                CycleCountResultId = cycleResultId
            };
            model.CommandType = CommonValues.DMLType.Delete;
            cycleCountResultBo.DMLCycleCountResult(UserManager.UserInfo, model);

            //return View(new CycleCountResultViewModel() { CycleCountId = cycleCountId });
            return CycleCountResultIndex(cycleCountId.ToString());
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountResult.CycleCountResultIndex, CommonValues.PermissionCodes.CycleCountResult.CycleCountResultCreate)]
        [HttpPost]
        public ActionResult CycleCountResultAudit(CycleCountResultAuditViewModel viewModel)
        {
            viewModel.CycleCountAuditList = Session["CycleCountAuditList"] != null ? (List<CycleCountResultPrototypeViewModel>)Session["CycleCountAuditList"] : null;

            var htmlOutput = new StringBuilder();
            if (viewModel.CycleCountAuditList != null)
            {
                var values = viewModel.NewQtyValues.Split(',');

                for (int i = 0; i < viewModel.CycleCountAuditList.Count; i++)
                {
                    var convertDecimal = Decimal.Parse(values[i], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                    viewModel.CycleCountAuditList[i].AfterQty = convertDecimal;

                    htmlOutput.Append(CommonValues.RowStart
                                      + @CommonValues.ColumnStart + viewModel.CycleCountAuditList[i].StockName +
                                      CommonValues.ColumnEnd +
                                      CommonValues.ColumnStart +
                                      "<input readonly='true' type='text'" +
                                      " value='" + viewModel.CycleCountAuditList[i].BeforeQty + "'/>" +
                                      CommonValues.ColumnEnd + CommonValues.ColumnStart +
                                      "<input class='newValues' type='text' " +
                                      "maxlength='6' value='" + viewModel.CycleCountAuditList[i].AfterQty + "' />" +
                                      CommonValues.ColumnEnd
                                      + CommonValues.RowEnd);
                }
            }

            ViewBag.HtmlOutput = htmlOutput.ToString();


            var currentQty = Decimal.Parse(viewModel.CurrentQty, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

            if (currentQty == viewModel.ExpectedQty)
            {
                var cycleCountStockDiffBL = new CycleCountStockDiffBL();

                Dictionary<bool, decimal> result = cycleCountStockDiffBL.Exists(viewModel.CycleCountId, viewModel.StockCardId, viewModel.WarehouseId).Model;
                viewModel.CommandType = result.FirstOrDefault().Key ? CommonValues.DMLType.Update : CommonValues.DMLType.Insert;

                cycleCountStockDiffBL.DMLCycleCountStockDiff(UserManager.UserInfo, viewModel);

            }
            else
            {
                viewModel.ErrorNo = 1;
                viewModel.ErrorMessage = MessageResource.CycleCountResultAudit_Display_ErrorMessage;
            }


            CheckErrorForMessage(viewModel, true);

            return View(viewModel);
        }

        #endregion

        public ActionResult ExcelSample()
        {
            var bo = new CycleCountResultBL();
            var ms = bo.SetExcelReport(null, string.Empty);
            return File(ms, CommonValues.ExcelContentType, MessageResource.CycleCount_ReportName + CommonValues.ExcelExtNew);
        }
    }
}