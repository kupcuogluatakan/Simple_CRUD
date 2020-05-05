using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.CycleCount;
using ODMSModel.CycleCountPlan;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CycleCountPlanController : ControllerBase
    {
        private void SetDefaults()
        {
            // WarehouseList
            List<SelectListItem> warehouseList = WarehouseBL.ListWarehousesOfDealerAsSelectList(UserManager.UserInfo.GetUserDealerId()).Data;
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

        #region CycleCount Address Index

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountPlan.CycleCountPlanIndex)]
        [HttpGet]
        public ActionResult CycleCountPlanIndex(string cycleCountId)
        {
            CycleCountPlanListModel model = new CycleCountPlanListModel();
            if (cycleCountId != null)
            {
                CycleCountViewModel cycleCountModel = new CycleCountViewModel();
                CycleCountBL cycleCountBo = new CycleCountBL();
                cycleCountModel.CycleCountId = cycleCountId;
                cycleCountBo.GetCycleCount(UserManager.UserInfo, cycleCountModel);

                model.CycleCountId = cycleCountId.GetValue<int>();
                ViewBag.CycleCountStatus = cycleCountModel.CycleCountStatus;
            }
            SetDefaults();
            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountPlan.CycleCountPlanIndex)]
        public ActionResult ListCycleCountPlan([DataSourceRequest] DataSourceRequest request, CycleCountPlanListModel model)
        {
            var cycleCountPlanBo = new CycleCountPlanBL();
            var v = new CycleCountPlanListModel(request);
            var totalCnt = 0;
            v.CycleCountId = model.CycleCountId;

            CycleCountViewModel cycleCountModel = new CycleCountViewModel();
            CycleCountBL cycleCountBo = new CycleCountBL();
            cycleCountModel.CycleCountId = model.CycleCountId.GetValue<string>();
            cycleCountBo.GetCycleCount(UserManager.UserInfo, cycleCountModel);
            ViewBag.CycleCountStatus = cycleCountModel.CycleCountStatus;

            var returnValue = cycleCountPlanBo.ListCycleCountPlans(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region CycleCount Address Create
        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountPlan.CycleCountPlanIndex, CommonValues.PermissionCodes.CycleCountPlan.CycleCountPlanCreate)]
        [HttpGet]
        public ActionResult CycleCountPlanCreate(int cycleCountId)
        {
            CycleCountPlanViewModel model = new CycleCountPlanViewModel { CycleCountId = cycleCountId };
            SetDefaults();
            return View(model);
        }

        private bool ValidateCycleCountPlan(CycleCountPlanViewModel viewModel)
        {
            int totalPlanCount = 0;
            // kapsayan kayıt var mı kontrol ediliyor.
            CycleCountPlanListModel planListModel = new CycleCountPlanListModel
            {
                CycleCountId = viewModel.CycleCountId
            };
            CycleCountPlanBL planBL = new CycleCountPlanBL();
            List<CycleCountPlanListModel> planList = planBL.ListCycleCountPlans(UserManager.UserInfo, planListModel, out totalPlanCount).Data;
            if (totalPlanCount != 0)
            {
                // yeni eklenen kayıttan var mı bakılır
                int selectedCount = (from pl in planList.AsEnumerable()
                                     where pl.WarehouseId == viewModel.WarehouseId && pl.RackId == viewModel.RackId
                                           && pl.StockCardId == viewModel.StockCardId
                                     select pl).Count();
                // aynı kayıttan varsa
                if (selectedCount != 0)
                {
                    SetMessage(MessageResource.CycleCountPlan_Warning_SameDataFound, CommonValues.MessageSeverity.Fail);
                    return false;
                }
                // aynı kayıttan yoksa
                else
                {
                    // raf seçmemişse 
                    if (viewModel.RackId == null)
                    {
                        // mevcut kayıtlarda raf seçili var mı bakılır.
                        selectedCount = (from pl in planList.AsEnumerable()
                                         where pl.WarehouseId == viewModel.WarehouseId && pl.RackId != null
                                         select pl).Count();

                        if (selectedCount != 0)
                        {
                            SetMessage(MessageResource.CycleCountPlan_Warning_SameWarehouseNotEmptyRack, CommonValues.MessageSeverity.Fail);
                            return false;
                        }
                        else
                        {
                            // parça seçmemişse
                            if (viewModel.StockCardId == null)
                            {
                                // mevcut kayıtlarda raf boş parça seçili var mı bakılır.
                                selectedCount = (from pl in planList.AsEnumerable()
                                                 where pl.WarehouseId == viewModel.WarehouseId && pl.RackId == null
                                                 && pl.StockCardId != null
                                                 select pl).Count();
                                if (selectedCount != 0)
                                {
                                    SetMessage(MessageResource.CycleCountPlan_Warning_SameWarehouseEmptyRack, CommonValues.MessageSeverity.Fail);
                                    return false;
                                }
                            }
                            // parça seçmişse
                            else
                            {
                                // mevcut kayıtlarda raf ve parça seçilmemiş kayıt var mı bakılır
                                selectedCount = (from pl in planList.AsEnumerable()
                                                 where pl.WarehouseId == viewModel.WarehouseId && pl.RackId == null
                                                 && pl.StockCardId == null
                                                 select pl).Count();
                                if (selectedCount != 0)
                                {
                                    SetMessage(MessageResource.CycleCountPlan_Warning_SameWarehouseEmptyRack, CommonValues.MessageSeverity.Fail);
                                    return false;
                                }
                            }
                        }
                    }
                    // raf seçmişse
                    else
                    {
                        // mevcut kayıtlarda raf seçilmemiş var mı kontrol edilir.
                        selectedCount = (from pl in planList.AsEnumerable()
                                         where pl.WarehouseId == viewModel.WarehouseId && pl.RackId == null
                                         select pl).Count();
                        if (selectedCount != 0)
                        {
                            SetMessage(MessageResource.CycleCountPlan_Warning_SameWarehouseEmptyRack, CommonValues.MessageSeverity.Fail);
                            return false;
                        }

                        // parça seçmemişse
                        if (viewModel.StockCardId == null)
                        {
                            // mevcut kayıtlarda aynı raf için girilmiş parça var mı bakılır.
                            selectedCount = (from pl in planList.AsEnumerable()
                                             where
                                                 pl.WarehouseId == viewModel.WarehouseId &&
                                                 pl.RackId == viewModel.RackId
                                                 && pl.StockCardId != null
                                             select pl).Count();
                            if (selectedCount != 0)
                            {
                                SetMessage(MessageResource.CycleCountPlan_Warning_SameWarehouseSameRackNotEmptyPart, CommonValues.MessageSeverity.Fail);
                                return false;
                            }
                        }
                        // parça seçmişse
                        else
                        {
                            // mevcut kayıtlarda aynı raf için boş girilmiş parça var mı bakılır.
                            selectedCount = (from pl in planList.AsEnumerable()
                                             where
                                                 pl.WarehouseId == viewModel.WarehouseId &&
                                                 pl.RackId == viewModel.RackId
                                                 && pl.StockCardId == null
                                             select pl).Count();
                            if (selectedCount != 0)
                            {
                                SetMessage(MessageResource.CycleCountPlan_Warning_SameWarehouseSameRackEmptyPart, CommonValues.MessageSeverity.Fail);
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountPlan.CycleCountPlanIndex, CommonValues.PermissionCodes.CycleCountPlan.CycleCountPlanCreate)]
        [HttpPost]
        public ActionResult CycleCountPlanCreate(CycleCountPlanViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            if (viewModel.RackId == 0)
                viewModel.RackId = null;
            if (viewModel.StockCardId == 0)
                viewModel.StockCardId = null;

            SetDefaults();
            var cycleCountPlanBo = new CycleCountPlanBL();

            if (ModelState.IsValid)
            {
                if (ValidateCycleCountPlan(viewModel))
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;
                    cycleCountPlanBo.DMLCycleCountPlan(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                    CycleCountPlanViewModel model = new CycleCountPlanViewModel { CycleCountId = viewModel.CycleCountId };


                    return View(model);
                }
            }
            return View(viewModel);
        }

        #endregion

        #region CycleCount Address Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.CycleCountPlan.CycleCountPlanIndex, CommonValues.PermissionCodes.CycleCountPlan.CycleCountPlanDelete)]
        public ActionResult DeleteCycleCountPlan(int cycleCountPlanId)
        {
            CycleCountPlanViewModel viewModel = new CycleCountPlanViewModel() { CycleCountPlanId = cycleCountPlanId };
            var cycleCountPlanBo = new CycleCountPlanBL();
            viewModel.CommandType = viewModel.CycleCountPlanId > 0 ? CommonValues.DMLType.Delete : string.Empty;
            cycleCountPlanBo.DMLCycleCountPlan(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion
    }
}