using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.FixAssetInventory;
using ODMSModel.Rack;
using ODMSModel.SparePart;
using ODMSModel.StockRackDetail;
using ODMSModel.StockTypeDetail;
using System.Linq;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class FixAssetInventoryController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.EquipmentTypeList = FixAssetInventoryBL.ListEquipmentTypeAsSelectList(UserManager.UserInfo).Data;
            ViewBag.VehicleGroupList = VehicleGroupBL.ListVehicleGroupAsSelectList(UserManager.UserInfo).Data;
            ViewBag.StatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.FixAssetStatus).Data;
            ViewBag.YesNoList = CommonBL.ListYesNo().Data;
        }

        [HttpGet]
        public JsonResult ListStockTypes(string partId, string rackId)
        {
            List<SelectListItem> stockTypeSelectList = new List<SelectListItem>();

            if (!string.IsNullOrEmpty(partId) && !string.IsNullOrEmpty(rackId))
            {
                RackDetailModel rackModel = new RackDetailModel { Id = rackId.GetValue<int>() };
                RackBL rackBo = new RackBL();
                rackModel = rackBo.GetRack(rackModel).Model;

                int totalCount = 0;
                StockTypeDetailListModel listModel = new StockTypeDetailListModel();
                if (UserManager.UserInfo.GetUserDealerId() > 0)
                {
                    listModel.IdDealer = UserManager.UserInfo.GetUserDealerId();
                }
                listModel.IdPart = partId.GetValue<int>();
                listModel.IdWarehouse = rackModel.WarehouseId;
                StockTypeDetailBL bo = new StockTypeDetailBL();
                List<StockTypeDetailListModel> stockTypeList = bo.ListStockTypeDetail(UserManager.UserInfo, listModel, out totalCount).Data;

                if (totalCount > 0)
                {
                    foreach (StockTypeDetailListModel stockTypeDetailListModel in stockTypeList)
                    {
                        var control = (from r in stockTypeSelectList.AsEnumerable()
                                       where r.Value == stockTypeDetailListModel.IdStockType.GetValue<string>()
                                       select r);
                        if (!control.Any() && stockTypeDetailListModel.AllowServiceEquipment)
                        {
                            SelectListItem row = new SelectListItem()
                            {
                                Value = stockTypeDetailListModel.IdStockType.GetValue<string>(),
                                Text = stockTypeDetailListModel.AdminDesc
                            };
                            stockTypeSelectList.Add(row);
                        }
                    }
                }
            }

            return Json(stockTypeSelectList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListRacks(string partId)
        {
            List<SelectListItem> rackSelectList = new List<SelectListItem>();

            if (!string.IsNullOrEmpty(partId))
            {
                int totalCount = 0;
                StockRackDetailListModel listModel = new StockRackDetailListModel();
                if (UserManager.UserInfo.GetUserDealerId() > 0)
                {
                    listModel.IdDealer = UserManager.UserInfo.GetUserDealerId();
                }
                listModel.PartId = partId.GetValue<int>();

                StockRackDetailBL bo = new StockRackDetailBL();
                List<StockRackDetailListModel> rackList = bo.ListStockRackDetail(UserManager.UserInfo, listModel, out totalCount).Data;
                if (totalCount > 0)
                {
                    rackSelectList.AddRange(rackList.Select(stockRackDetailListModel => new SelectListItem()
                    {
                        Value = stockRackDetailListModel.RackId.GetValue<string>(),
                        Text =
                                stockRackDetailListModel.WarehouseName + CommonValues.Minus +
                                stockRackDetailListModel.RackName
                    }));
                }
            }
            return Json(rackSelectList, JsonRequestBehavior.AllowGet);
        }

        #region FixAssetInventory Index

        [AuthorizationFilter(CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryIndex)]
        [HttpGet]
        public ActionResult FixAssetInventoryIndex()
        {
            SetDefaults();
            return View();
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryIndex, CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryDetails)]
        public ActionResult ListFixAssetInventory([DataSourceRequest] DataSourceRequest request, FixAssetInventoryListModel model)
        {
            var fixAssetInventoryBo = new FixAssetInventoryBL();
            var v = new FixAssetInventoryListModel(request);
            var totalCnt = 0;
            v.PartCode = model.PartCode;
            v.PartName = model.PartName;
            v.PartId = model.PartId;
            v.EquipmentTypeId = model.EquipmentTypeId;
            v.VehicleGroupId = model.VehicleGroupId;
            v.StatusId = model.StatusId;
            v.Name = model.Name;
            v.SerialNo = model.SerialNo;
            var returnValue = fixAssetInventoryBo.ListFixAssetInventorys(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region FixAssetInventory Create
        [AuthorizationFilter(CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryIndex, CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryCreate)]
        [HttpGet]
        public ActionResult FixAssetInventoryCreate()
        {
            var model = new FixAssetInventoryViewModel
            {
                IsPartOriginal = true,
                StatusId = (int)CommonValues.FixAssetStatus.FixInventory
            };
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryIndex, CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryCreate)]
        [HttpPost]
        public ActionResult FixAssetInventoryCreate(FixAssetInventoryViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var fixAssetInventoryBo = new FixAssetInventoryBL();

            if (viewModel.PartId != 0 && viewModel.PartId != null)
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartId = viewModel.PartId.GetValue<int>();
                SparePartBL spBo = new SparePartBL();
                spBo.GetSparePart(UserManager.UserInfo, spModel);
                viewModel.PartName = spModel.PartNameInLanguage;
            }

            if (ModelState.IsValid)
            {
                int totalCount;
                FixAssetInventoryListModel listModel = new FixAssetInventoryListModel();
                List<FixAssetInventoryListModel> list = fixAssetInventoryBo.ListFixAssetInventorys(UserManager.UserInfo, listModel,
                                                                                                   out totalCount).Data;
                var control = (from r in list.AsEnumerable()
                               where r.Code == viewModel.Code
                               select r);
                if (control.Any())
                {
                    SetMessage(MessageResource.FixAssetInventory_Error_DuplicateCode, CommonValues.MessageSeverity.Fail);
                }
                else
                {
                    viewModel.CommandType = CommonValues.DMLType.Insert;
                    fixAssetInventoryBo.DMLFixAssetInventory(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                    FixAssetInventoryViewModel model = new FixAssetInventoryViewModel
                    {
                        StatusId = (int)CommonValues.FixAssetStatus.FixInventory
                    };
                    return View(model);
                }
            }
            return View(viewModel);
        }

        #endregion

        #region FixAssetInventory Update
        [AuthorizationFilter(CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryIndex, CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryUpdate)]
        [HttpGet]
        public ActionResult FixAssetInventoryUpdate(int fixAssetInventoryId)
        {
            SetDefaults();
            var v = new FixAssetInventoryViewModel();
            if (fixAssetInventoryId > 0)
            {
                var fixAssetInventoryBo = new FixAssetInventoryBL();
                v.FixAssetInventoryId = fixAssetInventoryId;
                fixAssetInventoryBo.GetFixAssetInventory(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryIndex, CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryUpdate)]
        [HttpPost]
        public ActionResult FixAssetInventoryUpdate(FixAssetInventoryViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var fixAssetInventoryBo = new FixAssetInventoryBL();
            if (viewModel.PartId != 0 && viewModel.PartId != null)
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartId = viewModel.PartId.GetValue<int>();
                SparePartBL spBo = new SparePartBL();
                spBo.GetSparePart(UserManager.UserInfo, spModel);
                viewModel.PartName = spModel.PartNameInLanguage;
            }

            if (ModelState.IsValid)
            {
                int totalCount;
                FixAssetInventoryListModel listModel = new FixAssetInventoryListModel();
                List<FixAssetInventoryListModel> list = fixAssetInventoryBo.ListFixAssetInventorys(UserManager.UserInfo, listModel, out totalCount).Data;
                var control = (from r in list.AsEnumerable()
                               where r.Code == viewModel.Code &&
                               r.FixAssetInventoryId != viewModel.FixAssetInventoryId
                               select r);
                if (control.Any())
                {
                    SetMessage(MessageResource.FixAssetInventory_Error_DuplicateCode, CommonValues.MessageSeverity.Fail);
                }
                else
                {
                    viewModel.CommandType = CommonValues.DMLType.Update;
                    fixAssetInventoryBo.DMLFixAssetInventory(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);
                }
            }
            return View(viewModel);
        }

        #endregion

        #region FixAssetInventory Details
        [AuthorizationFilter(CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryIndex, CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryDetails)]
        [HttpGet]
        public ActionResult FixAssetInventoryDetails(int fixAssetInventoryId)
        {
            var v = new FixAssetInventoryViewModel();
            var fixAssetInventoryBo = new FixAssetInventoryBL();

            v.FixAssetInventoryId = fixAssetInventoryId;
            fixAssetInventoryBo.GetFixAssetInventory(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
    }
}