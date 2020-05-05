using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.Rack;
using ODMSModel.StockRackDetail;
using ODMSModel.StockCard;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class RackController : ControllerBase
    {
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Rack.RackIndex)]
        public ActionResult RackIndex()
        {
            var bo = new RackBL();
            var userInfo = UserManager.UserInfo;
            if (userInfo != null)
            {
                var model = bo.GetRackIndexModel(userInfo.GetUserDealerId()).Model;

                if (userInfo.GetUserDealerId() != 0)
                    model.DealerId = userInfo.GetUserDealerId();

                return View(model);
            }
            return View();
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Rack.RackIndex)]
        public JsonResult ListWarehouses([DataSourceRequest]DataSourceRequest request, int dealerId)
        {
            var result = ListWarehousesOfDealer(dealerId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.Rack.RackIndex, CommonValues.PermissionCodes.Rack.RackDetails)]
        public ActionResult ListRacks([DataSourceRequest]DataSourceRequest request, RackListModel model)
        {

            var bo = new RackBL();
            var referenceModel = new RackListModel(request)
            {
                WarehouseId = model.WarehouseId,
                Code = model.Code,
                //Name = model.Name,
                SearchIsActive = model.SearchIsActive,
                DealerId = model.DealerId
            };
            int totalCnt;
            var returnValue = bo.ListRacks(referenceModel, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Rack.RackIndex, CommonValues.PermissionCodes.Rack.RackCreate)]
        public ActionResult RackCreate()
        {
            var user = UserManager.UserInfo;
            ViewBag.DealerId = user.GetUserDealerId();
            ViewBag.WarehouseList = ListWarehousesOfDealer(user.GetUserDealerId());
            RackDetailModel model = new RackDetailModel();
            model.IsActive = true;
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Rack.RackIndex, CommonValues.PermissionCodes.Rack.RackCreate)]
        public ActionResult RackCreate(RackDetailModel model)
        {
            var user = UserManager.UserInfo;
            ViewBag.DealerId = user.GetUserDealerId();
            ViewBag.WarehouseList = ListWarehousesOfDealer(user.GetUserDealerId());

            if (ModelState.IsValid)
            {
                var bo = new RackBL();

                int totalCount = 0;
                RackListModel listModel = new RackListModel();
                listModel.Code = model.Code;
                listModel.WarehouseId = model.WarehouseId.GetValue<int>();
                List<RackListModel> list = bo.ListRacks(listModel, out totalCount).Data;
                if (list.Any())
                {
                    SetMessage(MessageResource.Rack_Warning_CodeExists, CommonValues.MessageSeverity.Fail);
                }
                else
                {
                    model.CommandType = model.Id > 0 ? CommonValues.DMLType.Update : CommonValues.DMLType.Insert;
                    bo.DMLRack(UserManager.UserInfo, model);
                    CheckErrorForMessage(model, true);
                    ModelState.Clear();

                    RackDetailModel newModel = new RackDetailModel();
                    newModel.IsActive = true;
                    return View(newModel);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Rack.RackIndex, CommonValues.PermissionCodes.Rack.RackUpdate)]
        public ActionResult RackUpdate(int id = 0)
        {
            var user = UserManager.UserInfo;
            ViewBag.DealerId = user.GetUserDealerId();
            ViewBag.WarehouseList = ListWarehousesOfDealer(user.GetUserDealerId());

            int totalCount = 0;
            StockRackDetailListModel srdListModel = new StockRackDetailListModel();
            srdListModel.RackId = id;
            srdListModel.IdDealer = UserManager.UserInfo.GetUserDealerId();

            StockRackDetailBL srdBo = new StockRackDetailBL();

            List<StockRackDetailListModel> srdList = srdBo.ListStockRackDetail(UserManager.UserInfo, srdListModel, out totalCount).Data;
            var control = (from e in srdList.AsEnumerable()
                           where e.Quantity > 0
                           select e);

            var referenceModel = new RackDetailModel();
            if (id > 0)
            {
                var bo = new RackBL();
                referenceModel.Id = id;
                referenceModel = bo.GetRack(referenceModel).Model;
                referenceModel.HaveStockRackDetail = control.Any();
            }
            return View(referenceModel);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.Rack.RackIndex, CommonValues.PermissionCodes.Rack.RackUpdate)]
        public ActionResult RackUpdate(RackDetailModel viewModel)
        {
            var user = UserManager.UserInfo;
            ViewBag.DealerId = user.GetUserDealerId();
            ViewBag.WarehouseList = ListWarehousesOfDealer(user.GetUserDealerId());

            var bo = new RackBL();
            if (ModelState.IsValid)
            {
                int totalCount = 0;

                RackListModel listModel = new RackListModel();
                listModel.WarehouseId = viewModel.WarehouseId.GetValue<int>();
                listModel.Code = viewModel.Code;

                List<RackListModel> list = bo.ListRacks(listModel, out totalCount).Data;
                var control = (from e in list.AsEnumerable()
                               where e.Id != viewModel.Id
                               select e);
                if (control.Any())
                {
                    SetMessage(MessageResource.Rack_Warning_CodeExists, CommonValues.MessageSeverity.Fail);
                }
                else
                {
                    viewModel.CommandType = viewModel.Id > 0
                                                ? CommonValues.DMLType.Update
                                                : CommonValues.DMLType.Insert;
                    bo.DMLRack(UserManager.UserInfo, viewModel);
                    if (viewModel.ErrorNo == 0)
                        SetMessage(MessageResource.Global_Display_Success, CommonValues.MessageSeverity.Success);
                    else
                        SetMessage(viewModel.ErrorMessage, CommonValues.MessageSeverity.Fail);
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.Rack.RackIndex, CommonValues.PermissionCodes.Rack.RackDelete)]
        public ActionResult RackDelete(RackDetailModel model)
        {
            ViewBag.HideElements = false;

            /*TFS NO : 28457 OYA 10.02.2015*/
            int totalCount = 0;

            StockRackDetailListModel srdListModel = new StockRackDetailListModel();
            srdListModel.RackId = model.Id;
            srdListModel.IdDealer = UserManager.UserInfo.GetUserDealerId();

            StockRackDetailBL srdBo = new StockRackDetailBL();
            List<StockRackDetailListModel> srdList = srdBo.ListStockRackDetail(UserManager.UserInfo, srdListModel, out totalCount).Data;
            var control = (from e in srdList.AsEnumerable()
                           where e.Quantity > 0
                           select e);

            if (control.Any())
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.Rack_Warning_HaveStockRackDetail);
            }

            var bo = new RackBL();
            model.CommandType = model.Id > 0 ? CommonValues.DMLType.Delete : string.Empty;
            bo.DMLRack(UserManager.UserInfo, model);

            ModelState.Clear();

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.Rack.RackIndex, CommonValues.PermissionCodes.Rack.RackDetails)]
        public ActionResult RackDetails(int id = 0)
        {
            var referenceModel = new RackDetailModel { Id = id };
            var bo = new RackBL();

            var model = bo.GetRack(referenceModel).Model;

            return View(model);
        }

        #region Helper Methods

        private List<SelectListItem> ListWarehousesOfDealer(int dealerId)
        {
            var bo = new RackBL();
            return bo.ListWarehousesOfDealer(dealerId).Data;
        }

        #endregion

    }
}
