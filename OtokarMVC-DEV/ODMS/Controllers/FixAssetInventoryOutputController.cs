using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.FixAssetInventoryOutput;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class FixAssetInventoryOutputController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.StockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;

            ViewBag.StatusList = CommonBL.ListLookup(UserManager.UserInfo, CommonValues.LookupKeys.FixAssetStatus).Data;

            ViewBag.StatusExitList = ((List<SelectListItem>)ViewBag.StatusList).Where(x=>x.Value!="1").ToList<SelectListItem>();
        }

        [HttpGet]
        public JsonResult ListWarehouse(int? idDealer)
        {
            if (idDealer.HasValue)
            {
                List<SelectListItem> warehouseList = WarehouseBL.ListWarehousesOfDealerAsSelectList(idDealer.Value).Data;
                warehouseList.Insert(0, new SelectListItem() { Text = MessageResource.Global_Display_Choose, Value = "0" });

                return Json(warehouseList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
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

        #region FixAssetInventoryOutput Index

        [AuthorizationFilter(CommonValues.PermissionCodes.FixAssetInventoryOutput.FixAssetInventoryOutputIndex)]
        [HttpGet]
        public ActionResult FixAssetInventoryOutputIndex()
        {
            FixAssetInventoryOutputListModel model = new FixAssetInventoryOutputListModel();
            model.FixAssetStatus = (int) CommonValues.FixAssetStatus.FixInventory;
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.FixAssetInventoryOutput.FixAssetInventoryOutputIndex, CommonValues.PermissionCodes.FixAssetInventoryOutput.FixAssetInventoryOutputIndex)]
        public ActionResult ListFixAssetInventoryOutput([DataSourceRequest] DataSourceRequest request, FixAssetInventoryOutputListModel model)
        {
            var fixAssetInventoryOutputBo = new FixAssetInventoryOutputBL();
            var v = new FixAssetInventoryOutputListModel(request);
            var totalCnt = 0;

            v.FixAssetName = model.FixAssetName;
            v.StockType = model.StockType;
            v.FixAssetStatus = model.FixAssetStatus;

            var returnValue = fixAssetInventoryOutputBo.ListFixAssetInventoryOutput(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region FixAssetInventoryOutput Update

        [AuthorizationFilter(CommonValues.PermissionCodes.FixAssetInventoryOutput.FixAssetInventoryOutputIndex, CommonValues.PermissionCodes.FixAssetInventoryOutput.FixAssetInventoryOutputUpdate)]
        [HttpGet]
        public ActionResult FixAssetInventoryOutputUpdate(int? idFixAssetInventory)
        {
            SetDefaults();
            var v = new FixAssetInventoryOutputViewModel();
            if (idFixAssetInventory != null)
            {
                var fixAssetInventoryOutputBo = new FixAssetInventoryOutputBL();
                v.IdFixAssetInventory = idFixAssetInventory;
                fixAssetInventoryOutputBo.GetFixAssetInventoryOutput(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryIndex, CommonValues.PermissionCodes.FixAssetInventory.FixAssetInventoryUpdate)]
        [HttpPost]
        public ActionResult FixAssetInventoryOutputUpdate(FixAssetInventoryOutputViewModel viewModel)
        {
            SetDefaults();

            var fixAssetInventoryOutputBo = new FixAssetInventoryOutputBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;
                fixAssetInventoryOutputBo.DMLFixAssetInventoryOutput(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();

                viewModel.IdPart = null;
                viewModel.RestockReason = null;//Form hide koşulu için

                viewModel.SubmitFinished = true;
            }
        
            return View(viewModel);
        }

        #endregion

    }
}