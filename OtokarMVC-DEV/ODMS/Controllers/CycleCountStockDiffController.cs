using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.CycleCount;
using ODMSModel.CycleCountResult;
using ODMSModel.CycleCountStockDiff;
using ODMSModel.StockCard;
using ODMSModel.CycleCountPlan;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CycleCountStockDiffController : ControllerBase
    {
        private void SetDefaults()
        {
            // WarehouseList
            List<SelectListItem> warehouseList =
                WarehouseBL.ListWarehousesOfDealerAsSelectList(UserManager.UserInfo.GetUserDealerId()).Data;
            ViewBag.WarehouseList = warehouseList;
            // StockTypeList
            List<SelectListItem> stockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.StockTypeList = stockTypeList;
            //DealerList
            List<SelectListItem> dealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerList = dealerList;
            //StockCard
            List<SelectListItem> stockCardList = SparePartBL.ListStockCardsAsSelectListItem(UserManager.UserInfo, true).Data;
            ViewBag.StockCardList = stockCardList;
        }

        #region CycleCountStockDiff Search

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountStockDiff.CycleCountStockDiffSearch)]
        [HttpGet]
        public ActionResult CycleCountStockDiffSearch()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountStockDiff.CycleCountStockDiffSearch)]
        public ActionResult SearchCycleCountStockDiff([DataSourceRequest] DataSourceRequest request, CycleCountStockDiffSearchListModel model)
        {
            var bo = new CycleCountStockDiffBL();
            var v = new CycleCountStockDiffSearchListModel(request);
            var totalCnt = 0;
            v.DealerId = model.DealerId;
            v.PartName = model.PartName;
            v.PartCode = model.PartCode;
            v.IsStockChanged = model.IsStockChanged;
            v.StockCardId = model.StockCardId;
            v.StockTypeId = model.StockTypeId;
            v.StartDate = model.StartDate;
            v.EndDate = model.EndDate;
            var returnValue = bo.SearchCycleCountStockDiffs(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region CycleCount StockDiff Index

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountStockDiff.CycleCountStockDiffIndex)]
        [HttpGet]
        public ActionResult CycleCountStockDiffIndex(string cycleCountId)
        {
            CycleCountStockDiffListModel model = new CycleCountStockDiffListModel();
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
            model.ResultListModel = new CycleCountResultListModel() { CycleCountId = cycleCountId.GetValue<int>() };
            model.DiffDetailListModel = new CycleCountDiffDetailListModel()
            {
                CycleCountId = cycleCountId.GetValue<int>()
            };

            SetDefaults();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCount.CycleCountApprove)]
        public ActionResult ApproveCycleCount(string cycleCountId)
        {

            #region Before Save All ApprovedQty of Records

            /*
             * Maker : Bilal İslam
             * Date : 29/04/2014 13:43
             * Description :it's  may be record two times if clicked save before
             */

            var cycleCountResultBo = new CycleCountResultBL();
            var model = new CycleCountResultListModel
            {
                CycleCountId = Convert.ToInt32(cycleCountId)
            };

            var totalCnt = 0;
            var listModel = cycleCountResultBo.ListCycleCountResults(UserManager.UserInfo, model, out totalCnt).Data;


            CycleCountResultBL resultBl = new CycleCountResultBL();
            CycleCountResultViewModel resultViewModel = new CycleCountResultViewModel();

            foreach (var result in listModel.Where(e=>e.ApprovedCountQuantity != e.BeforeCountQuantity))
            {
                resultViewModel.CycleCountId = Convert.ToInt32(cycleCountId);
                resultViewModel.CycleCountResultId = result.CycleCountResultId;

                resultBl.GetCycleCountResult(UserManager.UserInfo, resultViewModel);

                resultViewModel.AfterCountQuantity = result.AfterCountQuantity.Value;
                resultViewModel.ApprovedCountQuantity = result.ApprovedCountQuantity == 0 ? result.AfterCountQuantity.Value : result.ApprovedCountQuantity;
                resultViewModel.CommandType = CommonValues.DMLType.Update;

                resultBl.DMLCycleCountResult(UserManager.UserInfo, resultViewModel);
            }

            #endregion

            #region It will insert true records to Stock_Diff_Table

            CycleCountStockDiffBL stockDiffBL = new CycleCountStockDiffBL();

            if (Session["DefaultInsertStockDiff"] != null)
            {
                var stockDiffList = ((List<CycleCountResultListModel>)Session["DefaultInsertStockDiff"]).Where(e=>e.ApprovedCountQuantity != e.BeforeCountQuantity);

                foreach (var item in stockDiffList)
                {
                    CycleCountStockDiffViewModel diffModel = new CycleCountStockDiffViewModel();
                    diffModel.WarehouseId = item.WarehouseId;
                    diffModel.StockCardId = item.StockCardId;
                    diffModel.CycleCountId = item.CycleCountId;
                    diffModel.AfterCount = item.AfterCountQuantity.GetValue<decimal>();
                    diffModel.BeforeCount = item.BeforeCountQuantity;
                    diffModel.CommandType = CommonValues.DMLType.Insert;
                    stockDiffBL.DMLCycleCountStockDiff(UserManager.UserInfo, diffModel);
                }
            }

            #endregion

            #region Cycle_Count_Stock_Transaction

            LockRacks(LockType.Approved, Convert.ToInt32(cycleCountId));

            CycleCountStockDiffBL cycleCountStockDiffBL = new CycleCountStockDiffBL();
            CycleCountStockDiffViewModel transactionModel = new CycleCountStockDiffViewModel { CycleCountId = Convert.ToInt32(cycleCountId), CommandType = CommonValues.DMLType.Insert };
            cycleCountStockDiffBL.ApproveCycleCountProcess(UserManager.UserInfo, transactionModel);

            if (transactionModel.ErrorNo > 0)
            {
                LockRacks(LockType.Start, Convert.ToInt32(cycleCountId));

                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, transactionModel.ErrorMessage);
            }
            else
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);



            #endregion
        }

        public static List<CycleCountDiffDetailListModel> GetDiffDetail(int cycleCountId)
        {
            var stockDiffTotalCount = 0;
            CycleCountStockDiffListModel stockDiffListModel = new CycleCountStockDiffListModel
            {
                CycleCountId = cycleCountId
            };
            CycleCountStockDiffBL stockDiffBl = new CycleCountStockDiffBL();
            List<CycleCountStockDiffListModel> stockDiffList = stockDiffBl.ListCycleCountStockDiffs(UserManager.UserInfo, stockDiffListModel, out stockDiffTotalCount).Data;

            var totalCnt = 0;
            var cycleCountResultBo = new CycleCountResultBL();
            CycleCountResultListModel resultListModel = new CycleCountResultListModel
            {
                CycleCountId = cycleCountId
            };
            List<CycleCountResultListModel> resultList = cycleCountResultBo.ListCycleCountResults(UserManager.UserInfo, resultListModel, out totalCnt).Data;

            // bu result bilgilerinden depo ve parça bazlı farklar elde edilir.
            var returnValue = (from a in
                                   (from r in resultList.AsEnumerable()
                                    group r by new { r.WarehouseId, r.WarehouseName, r.StockCardId, r.StockCardName }
                                        into grp
                                    select new CycleCountDiffDetailListModel()
                                    {
                                        CycleCountId = cycleCountId,
                                        WarehouseId = grp.Key.WarehouseId,
                                        WarehouseName = grp.Key.WarehouseName,
                                        StockCardId = grp.Key.StockCardId,
                                        StockCardName = grp.Key.StockCardName,
                                        DifferenceCount =
                                                (grp.Sum(r => r.BeforeCountQuantity) -
                                                 grp.Sum(r => r.ApprovedCountQuantity)),
                                        TransferedCount = (from sd in stockDiffList.AsEnumerable()
                                                           where sd.WarehouseId == grp.Key.WarehouseId &&
                                                                 sd.StockCardId == grp.Key.StockCardId
                                                           group sd by new { sd.StockCardId, sd.WarehouseId }
                                                                   into grpsd
                                                           select grpsd.Sum(sd => sd.AfterCount)).FirstOrDefault()
                                    })
                               where a.DifferenceCount != 0
                               select a)
                .ToList<CycleCountDiffDetailListModel>();
            return returnValue;
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountStockDiff.CycleCountStockDiffIndex)]
        public ActionResult ListCycleCountDiffDetail([DataSourceRequest] DataSourceRequest request,
                                                     CycleCountDiffDetailListModel model)
        {
            var v = new CycleCountDiffDetailListModel(request) { CycleCountId = model.CycleCountId };

            CycleCountViewModel cycleCountModel = new CycleCountViewModel();
            CycleCountBL cycleCountBo = new CycleCountBL();
            cycleCountModel.CycleCountId = model.CycleCountId.GetValue<string>();
            cycleCountBo.GetCycleCount(UserManager.UserInfo, cycleCountModel);
            ViewBag.CycleCountStatus = cycleCountModel.CycleCountStatus;
            ViewBag.DisplayCurrentAmount = cycleCountModel.DisplayCurrentAmount;

            List<CycleCountDiffDetailListModel> returnValue = GetDiffDetail(model.CycleCountId);

            return Json(new
            {
                Data = returnValue,
                Total = returnValue.Count
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountStockDiff.CycleCountStockDiffIndex)]
        public ActionResult ListCycleCountStockDiff([DataSourceRequest] DataSourceRequest request, CycleCountStockDiffListModel model)
        {
            var cycleCountStockDiffBo = new CycleCountStockDiffBL();
            var v = new CycleCountStockDiffListModel(request);
            var totalCnt = 0;
            v.CycleCountId = model.CycleCountId;

            CycleCountViewModel cycleCountModel = new CycleCountViewModel();
            CycleCountBL cycleCountBo = new CycleCountBL();
            cycleCountModel.CycleCountId = model.CycleCountId.GetValue<string>();
            cycleCountBo.GetCycleCount(UserManager.UserInfo, cycleCountModel);

            ViewBag.CycleCountStatus = cycleCountModel.CycleCountStatus;
            ViewBag.DisplayCurrentAmount = cycleCountModel.DisplayCurrentAmount;

            var returnValue = cycleCountStockDiffBo.ListCycleCountStockDiffs(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region CycleCount StockDiff Create
        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountStockDiff.CycleCountStockDiffIndex, CommonValues.PermissionCodes.CycleCountStockDiff.CycleCountStockDiffCreate)]
        [HttpGet]
        public ActionResult CycleCountStockDiffCreate(int cycleCountId)
        {
            CycleCountStockDiffViewModel model = new CycleCountStockDiffViewModel { CycleCountId = cycleCountId };
            SetDefaults();
            return View(model);
        }

        private bool ValidateCycleCountStockDiff(CycleCountStockDiffViewModel viewModel)
        {
            // planda olmayan depo seçilirse
            List<CycleCountDiffDetailListModel> diffDetailList = GetDiffDetail(viewModel.CycleCountId);
            var diffList = (from dd in diffDetailList.AsEnumerable()
                            where dd.WarehouseId == viewModel.WarehouseId &&
                                  dd.StockCardId == viewModel.StockCardId
                            select dd.DifferenceCount);
            if (!diffList.Any())
            {
                SetMessage(MessageResource.CyclecountStockDiff_Warning_NoDiffFound, CommonValues.MessageSeverity.Fail);
                return false;
            }
            // daha önceden aynı şekilde kayıt yaratılmışsa
            var stockDiffCount = 0;
            CycleCountStockDiffListModel stockDiffListModel = new CycleCountStockDiffListModel
            {
                CycleCountId = viewModel.CycleCountId
            };
            CycleCountStockDiffBL stockDiffBl = new CycleCountStockDiffBL();
            List<CycleCountStockDiffListModel> stockDiffList = stockDiffBl.ListCycleCountStockDiffs(UserManager.UserInfo, stockDiffListModel, out stockDiffCount).Data;
            if (stockDiffCount != 0)
            {
                var stockDiff = (from sdl in stockDiffList.AsEnumerable()
                                 where
                                     sdl.WarehouseId == viewModel.WarehouseId
                                     && sdl.StockCardId == viewModel.StockCardId
                                 select sdl);
                if (stockDiff.Count() != 0)
                {
                    SetMessage(MessageResource.CycleCountPlan_Warning_SameDataFound, CommonValues.MessageSeverity.Fail);
                    return false;
                }
            }
            // eksi değer girilmişse ve stock_type_detail tablosunda yeterli bakiye yoksa
            if (viewModel.AfterCount < 0)
            {
                decimal quantity = stockDiffBl.GetStockTypeDetailQuantity(UserManager.UserInfo, viewModel).Model;
                if (quantity < viewModel.AfterCount * -1)
                {
                    SetMessage(MessageResource.CycleCountStockDiff_Warning_NoStockFound, CommonValues.MessageSeverity.Fail);
                    return false;
                }
            }

            viewModel.BeforeCount = diffList.First();

            return true;
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountStockDiff.CycleCountStockDiffIndex, CommonValues.PermissionCodes.CycleCountStockDiff.CycleCountStockDiffCreate)]
        [HttpPost]
        public ActionResult CycleCountStockDiffCreate(CycleCountStockDiffViewModel viewModel)
        {
            SetDefaults();
            var cycleCountStockDiffBo = new CycleCountStockDiffBL();

            int count = 0;
            StockCardBL scBo = new StockCardBL();
            StockCardListModel listModel = new StockCardListModel
            {
                PartId = viewModel.StockCardId,
                DealerId = UserManager.UserInfo.GetUserDealerId()
            };
            List<StockCardListModel> list = scBo.ListStockCards(UserManager.UserInfo, listModel, out count).Data;
            if (list.Any())
            {
                viewModel.StockCardName = list.First().PartName;
            }

            if (ModelState.IsValid)
            {
                if (ValidateCycleCountStockDiff(viewModel))
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;
                    cycleCountStockDiffBo.DMLCycleCountStockDiff(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                    CycleCountStockDiffViewModel model = new CycleCountStockDiffViewModel
                    {
                        CycleCountId = viewModel.CycleCountId
                    };
                    SetDefaults();
                    return View(model);
                }
            }
            return View(viewModel);
        }

        #endregion

        #region CycleCount StockDiff Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountStockDiff.CycleCountStockDiffIndex, CommonValues.PermissionCodes.CycleCountStockDiff.CycleCountStockDiffDelete)]
        public ActionResult DeleteCycleCountStockDiff(int cycleCountStockDiffId)
        {
            CycleCountStockDiffViewModel viewModel = new CycleCountStockDiffViewModel() { CycleCountStockDiffId = cycleCountStockDiffId };
            var cycleCountStockDiffBo = new CycleCountStockDiffBL();
            viewModel.CommandType = viewModel.CycleCountStockDiffId > 0 ? CommonValues.DMLType.Delete : string.Empty;
            cycleCountStockDiffBo.DMLCycleCountStockDiff(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion


        private void LockRacks(LockType type, int cycleCountId)
        {
            CycleCountPlanViewModel cycleCountPlanViewModel = new CycleCountPlanViewModel() { CycleCountId = cycleCountId };
            var result = new CycleCountPlanBL().ListById(UserManager.UserInfo, cycleCountPlanViewModel).Data;
            new CycleCountBL().LockRack(result, type);
        }
    }
}