using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.Warehouse;
using ODMSModel.Rack;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class WarehouseController : ControllerBase
    {
        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
        }

        #region Warehouse Index
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Warehouse.WarehouseIndex)]
        public ActionResult WarehouseIndex()
        {
            SetDefaults();
            var model = new WarehouseIndexModel();

            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();

            model.DealerId = UserManager.UserInfo.GetUserDealerId() != 0 ?
                             UserManager.UserInfo.GetUserDealerId() : 0;

            return View(model);

        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Warehouse.WarehouseIndex, CommonValues.PermissionCodes.Warehouse.WarehouseDetails)]
        public ActionResult ListWarehouses([DataSourceRequest]DataSourceRequest request, WarehouseListModel model)
        {
            var bo = new WarehouseBL();
            var referenceModel = new WarehouseListModel(request) { Code = model.Code, Name = model.Name, SearchIsActive = model.SearchIsActive, DealerId = model.DealerId };
            int totalCnt;
            var returnValue = bo.ListWarehouses(UserManager.UserInfo, referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Warehouse Create
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Warehouse.WarehouseIndex, CommonValues.PermissionCodes.Warehouse.WarehouseCreate)]
        public ActionResult WarehouseCreate()
        {
            SetDefaults();

            var model = new WarehouseDetailModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            model.IsActive = true;
            return View(model);

        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Warehouse.WarehouseIndex, CommonValues.PermissionCodes.Warehouse.WarehouseCreate)]
        public ActionResult WarehouseCreate(WarehouseDetailModel model)
        {
            SetDefaults();
            if (ModelState.IsValid)
            {
                var bo = new WarehouseBL();
                int totalCount = 0;
                WarehouseListModel listModel = new WarehouseListModel();
                listModel.Code = model.Code;
                listModel.DealerId = model.DealerId;
                List<WarehouseListModel> list = bo.ListWarehouses(UserManager.UserInfo, listModel, out totalCount).Data;
                if (list.Any())
                {
                    SetMessage(MessageResource.Warehouse_Warning_SameCodeExists, CommonValues.MessageSeverity.Fail);
                }
                else
                {
                    model.Name = model.Code;
                    model.CommandType = model.Id > 0
                                            ? CommonValues.DMLType.Update
                                            : CommonValues.DMLType.Insert;
                    bo.DMLWarehouse(UserManager.UserInfo, model);
                    CheckErrorForMessage(model, true);
                    ModelState.Clear();

                    var newModel = new WarehouseDetailModel();
                    if (UserManager.UserInfo.GetUserDealerId() != 0)
                        newModel.DealerId = UserManager.UserInfo.GetUserDealerId();
                    newModel.IsActive = true;
                    return View(newModel);
                }
            }
            return View(model);
        }

        #endregion

        #region Warehouse Update
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Warehouse.WarehouseIndex, CommonValues.PermissionCodes.Warehouse.WarehouseUpdate)]
        public ActionResult WarehouseUpdate(int id = 0)
        {
            SetDefaults();
            var referenceModel = new WarehouseDetailModel();
            if (id > 0)
            {
                var bo = new WarehouseBL();
                referenceModel.Id = id;
                referenceModel = bo.GetWarehouse(UserManager.UserInfo, referenceModel).Model;
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Warehouse.WarehouseIndex, CommonValues.PermissionCodes.Warehouse.WarehouseUpdate)]
        public ActionResult WarehouseUpdate(WarehouseDetailModel viewModel)
        {
            SetDefaults();
            var bo = new WarehouseBL();
            if (ModelState.IsValid)
            {
                int totalCount = 0;
                WarehouseListModel listModel = new WarehouseListModel();
                listModel.Code = viewModel.Code;
                listModel.DealerId = viewModel.DealerId;
                List<WarehouseListModel> list = bo.ListWarehouses(UserManager.UserInfo, listModel, out totalCount).Data;
                var control = (from e in list.AsEnumerable()
                               where e.Id != viewModel.Id
                               select e);
                if (control.Any())
                {
                    SetMessage(MessageResource.Warehouse_Warning_SameCodeExists, CommonValues.MessageSeverity.Fail);
                }
                else
                {
                    viewModel.Name = viewModel.Code;
                    viewModel.CommandType = viewModel.Id > 0 ? CommonValues.DMLType.Update : CommonValues.DMLType.Insert;
                    bo.DMLWarehouse(UserManager.UserInfo, viewModel);
                    CheckErrorForMessage(viewModel, true);
                }
            }
            return View(viewModel);
        }

        #endregion

        #region Warehouse Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Warehouse.WarehouseIndex, CommonValues.PermissionCodes.Warehouse.WarehouseDelete)]
        public ActionResult WarehouseDelete(WarehouseDetailModel model)
        {
            int totalCount = 0;
            RackListModel rackModel = new RackListModel();
            rackModel.WarehouseId = model.Id;
            rackModel.IsActive = true;
            RackBL rackBo = new RackBL();
            List<RackListModel> rackList = rackBo.ListRacks(rackModel, out totalCount).Data;
            if (rackList.Any())
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Warehouse_Warning_ActiveRackExists);
            }
            else
            {
                ViewBag.HideElements = false;

                var bo = new WarehouseBL();
                model.CommandType = model.Id > 0 ? CommonValues.DMLType.Delete : string.Empty;
                bo.DMLWarehouse(UserManager.UserInfo, model);
                ModelState.Clear();

                if (model.ErrorNo == 0)
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
            }
        }

        #endregion

        #region Warehouse Details
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Warehouse.WarehouseIndex, CommonValues.PermissionCodes.Warehouse.WarehouseDetails)]
        public ActionResult WarehouseDetails(int id = 0)
        {
            var referenceModel = new WarehouseDetailModel { Id = id };
            var bo = new WarehouseBL();

            var model = bo.GetWarehouse(UserManager.UserInfo, referenceModel).Model;

            return View(model);
        }

        #endregion

    }
}
