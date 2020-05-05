using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.CycleCount;
using ODMSModel.CycleCountPlan;
using ODMSModel.CycleCountResult;
using ODMSModel.ListModel;
using ODMSModel.Rack;
using ODMSModel.Warehouse;
using ODMSModel.SparePart;
using ODMSModel.StockCard;
using ODMSData;
using ODMS.Core;
using static ODMSCommon.CommonValues;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CycleCountController : ControllerBase
    {
        private void SetDefaults()
        {
            // status
            List<SelectListItem> statusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.StatusLookup).Data;
            ViewBag.StatusList = statusList;

            //DisplayCurrentAmount
            List<SelectListItem> yesNoList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.YesNo).Data;
            ViewBag.YesNoList = yesNoList;

            //StockTypeLists
            List<SelectListItem> stockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            var bedelli = stockTypeList.FirstOrDefault(x => x.Value == "1");
            ViewBag.BedelliText = bedelli.Text;


            if (!ODMSHelpers.UserHasPermission(PermissionCodes.CycleCount.CycleCountWithoutChargeCampaign))
            {
                stockTypeList.Remove(stockTypeList.FirstOrDefault(d => d.Value == ((int)StockType.Bedelsiz).ToString()));
                stockTypeList.Remove(stockTypeList.FirstOrDefault(d => d.Value == ((int)StockType.Kampanya).ToString()));
            }

            ViewBag.StockTypeList = stockTypeList;
        }

        #region CycleCount Index

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCount.CycleCountIndex)]
        [HttpGet]
        public ActionResult CycleCountIndex(string cycleCountId, bool? isClosedApprovedTab)
        {
            CycleCountViewModel model = new CycleCountViewModel();

            if (!string.IsNullOrEmpty(cycleCountId))
            {
                string[] cryptedValue = CommonUtility.DecryptSymmetric(cycleCountId).Split('-');

                if (Convert.ToInt32(cryptedValue.LastOrDefault()) == UserManager.UserInfo.GetUserDealerId())
                {
                    model.CycleCountId = cryptedValue.FirstOrDefault();
                    CycleCountBL cyclecountBl = new CycleCountBL();
                    cyclecountBl.GetCycleCount(UserManager.UserInfo, model);

                    if (model.CycleCountStatus == (int)CommonValues.CycleCountStatus.Finished || model.CycleCountStatus == (int)CommonValues.CycleCountStatus.Approved)
                        model.IsClosedApproveTab = isClosedApprovedTab.HasValue && isClosedApprovedTab.Value;
                }
            }
            else
            {
                if (UserManager.UserInfo.GetUserDealerId() > 0)
                {
                    model.DealerId = UserManager.UserInfo.GetUserDealerId();
                }
                DealerBL dealerBL = new DealerBL();
                model.DealerName = dealerBL.GetDealer(UserManager.UserInfo, model.DealerId.GetValue<int>()).Model.Name;
                model.CycleCountStatus = (int)CommonValues.CycleCountStatus.Planning;
                model.DisplayCurrentAmount = true;
            }
            SetDefaults();

            model.StockTypeId = 1; // sadece bedelli say - ibrahim @22.5.2019 TFS[183443]

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCount.CycleCountIndex)]
        [HttpPost]
        public ActionResult CycleCountIndex(CycleCountViewModel viewModel)
        {
            string[] decrypedValue = CommonUtility.DecryptSymmetric(viewModel.CycleCountId).Split('-');
            viewModel.CycleCountId = decrypedValue != null ? decrypedValue.FirstOrDefault() : string.Empty;
            viewModel.StockTypeId = 1; // sadece bedelli say - ibrahim @22.5.2019 TFS[183443]

            SetDefaults();
            var cycleCountBo = new CycleCountBL();
            CycleCountPlanBL cycleCountPlanBL = new CycleCountPlanBL();

            if (ModelState.IsValid)
            {
                #region Create 
                if (Request.Params["action:CycleCountCreate"] != null)
                {
                    int cnt = 0;
                    bool isExists = cycleCountBo.ListCycleCount(UserManager.UserInfo, new CycleCountListModel(), out cnt).Data.Any(p => p.CycleCountStatus.Value != (int)CommonValues.CycleCountStatus.Approved &&
                p.CycleCountStatus.Value != (int)CommonValues.CycleCountStatus.Cancelled);

                    if (!isExists)
                    {
                        viewModel.CommandType = CommonValues.DMLType.Insert;
                        cycleCountBo.DMLCycleCount(UserManager.UserInfo, viewModel);
                        CheckErrorForMessage(viewModel, true);
                    }
                    else
                    {
                        SetMessage(MessageResource.CycleCount_Error_Message_Exists, CommonValues.MessageSeverity.Fail);
                        return View(viewModel);
                    }

                }
                #endregion

                #region Update
                if (Request.Params["action:CycleCountUpdate"] != null)
                {
                    viewModel.CommandType = CommonValues.DMLType.Update;
                    cycleCountBo.DMLCycleCount(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);
                }
                #endregion

                #region Start
                if (Request.Params["action:CycleCountStart"] != null)
                {
                    int totalPlanCount = 0;
                    CycleCountPlanListModel planListModel = new CycleCountPlanListModel
                    {
                        CycleCountId = viewModel.CycleCountId.GetValue<int>()
                    };

                    cycleCountPlanBL.ListCycleCountPlans(UserManager.UserInfo, planListModel, out totalPlanCount);

                    if (totalPlanCount == 0)
                    {
                        SetMessage(MessageResource.CycleCount_Warning_NoDetail, CommonValues.MessageSeverity.Fail);
                    }
                    else
                    {
                        cycleCountBo.StartCycleCount(UserManager.UserInfo, viewModel);
                        CheckErrorForMessage(viewModel, true);

                        if (viewModel.ErrorNo == 0)
                        {
                            CycleCountPlanViewModel cycleCountPlanViewModel = new CycleCountPlanViewModel() { CycleCountId = Convert.ToInt32(viewModel.CycleCountId) };
                            var result = cycleCountPlanBL.ListById(UserManager.UserInfo, cycleCountPlanViewModel).Data;
                            cycleCountBo.LockRack(result, LockType.Start);

                            viewModel.CycleCountStatus = (int)CommonValues.CycleCountStatus.Started;
                            viewModel.StartDate = DateTime.Now;
                            viewModel.CommandType = CommonValues.DMLType.Update;
                            cycleCountBo.DMLCycleCount(UserManager.UserInfo, viewModel);
                            CheckErrorForMessage(viewModel, true);
                        }

                        if (viewModel.ErrorNo == 2)
                        {
                            SetMessage(MessageResource.CycleCount_Error_Message_NoRecord, CommonValues.MessageSeverity.Fail);
                        }
                    }
                }
                #endregion

                #region Cancel
                if (Request.Params["action:CycleCountCancel"] != null)
                {
                    CycleCountPlanViewModel cycleCountPlanViewModel = new CycleCountPlanViewModel() { CycleCountId = Convert.ToInt32(viewModel.CycleCountId) };
                    var result = cycleCountPlanBL.ListById(UserManager.UserInfo, cycleCountPlanViewModel).Data;
                    cycleCountBo.LockRack(result, LockType.Approved);

                    viewModel.CycleCountStatus = (int)CommonValues.CycleCountStatus.Cancelled;
                    viewModel.CommandType = CommonValues.DMLType.Update;
                    cycleCountBo.DMLCycleCount(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);
                }
                #endregion

                #region Get Report

                if (Request.Params["action:CycleCountReport"] != null)
                {

                    MemoryStream ms = null;

                    try
                    {
                        ms = new MemoryStream(new CycleCountBL().GetCycleCountReport(Convert.ToInt32(viewModel.CycleCountId)));
                        return File(ms, "application/pdf", string.Format(MessageResource.CycleCount_ReportName, viewModel.CycleCountId));
                    }
                    catch
                    {
                        SetMessage(MessageResource.ErrorCycleCountReport, CommonValues.MessageSeverity.Fail);
                    }
                }

                #endregion 

                ModelState.Clear();
            }
            cycleCountBo.GetCycleCount(UserManager.UserInfo, viewModel);

            string encryptedId = CommonUtility.EncryptSymmetric(viewModel.CycleCountId + CommonValues.Minus + viewModel.DealerId);
            return RedirectToAction("CycleCountIndex", "CycleCount", new { cycleCountId = encryptedId });
        }
        #endregion


        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountResult.CycleCountResultSave)]
        [HttpPost]
        public ActionResult CycleCountResultImportFromExcel(CycleCountResultListModel model, HttpPostedFileBase excelFile)
        {
            var returnModel = new CycleCountViewModel();
            string encrytedId = CommonUtility.EncryptSymmetric(model.CycleCountId + CommonValues.Minus + UserManager.UserInfo.GetUserDealerId());

            returnModel.CycleCountId = model.CycleCountId.ToString();
            CycleCountBL cyclecountBl = new CycleCountBL();
            cyclecountBl.GetCycleCount(UserManager.UserInfo, returnModel);

            //Cycle Counts whic is not started or finished cannot be edited.
            if (returnModel.CycleCountStatus != (int)CommonValues.CycleCountStatus.Started)
            {
                return RedirectToAction("CycleCountIndex", "CycleCount", new { cycleCountId = encrytedId, isClosedApprovedTab = true });
            }

            if (excelFile == null)
            {
                SetMessage(MessageResource.CycleCount_Error_ImportExcel_InvalidExcelFile, CommonValues.MessageSeverity.Fail);
                return RedirectToAction("CycleCountIndex", "CycleCount", new { cycleCountId = encrytedId, isClosedApprovedTab = true });
            }

            CycleCountResultBL cycleCountResultBo = new CycleCountResultBL();
            //Parse Excel
            Stream excelFileStream = excelFile.InputStream;
            var cycleCountResultViewModel = new CycleCountResultViewModel();
            var excelList = cycleCountResultBo.ParseExcel(UserManager.UserInfo, cycleCountResultViewModel, excelFileStream).Model.ExcelList;

            // send to db
            string errorMessage = cycleCountResultBo.DMLCycleCountResultBulk(UserManager.UserInfo, model.CycleCountId, excelList).Message;

            if (errorMessage != string.Empty)
                SetMessage(errorMessage, CommonValues.MessageSeverity.Fail);
            else
                SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);

            return RedirectToAction("CycleCountIndex", "CycleCount", new { cycleCountId = encrytedId, isClosedApprovedTab = true });
        }


    }


}