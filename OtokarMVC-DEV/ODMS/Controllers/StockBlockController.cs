using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.StockBlock;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class StockBlockController : ControllerBase
    {
        #region Member Varables

        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;

            ViewBag.StockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;

            ViewBag.BalanceList = CommonBL.ListBalanceType().Data;

            ViewBag.StockBlockStatus = CommonBL.ListLookup(UserManager.UserInfo,CommonValues.LookupKeys.StockBlockStatusLookup).Data;
        }

        [HttpGet]
        public JsonResult ListStockTypes(Int64? id, int? idDealer)
        {
            if (id!=null && idDealer != null)
            {
                return Json(new CommonBL().ListStockTypes(UserManager.UserInfo,id, idDealer).Data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new List<StockBlockViewModel>(), JsonRequestBehavior.AllowGet);
            }
  
        }

        [HttpGet]
        public JsonResult GetBlockQuantity(Int64? idPart, int? idStockType, int? idDealer)
        {
            if (idPart != null && idStockType != null && idDealer!=null)
            {
                return Json(new CommonBL().GetBlockQuantity(UserManager.UserInfo,idPart, idStockType, idDealer).Model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new List<StockBlockViewModel>(), JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region StockBlock Index

        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex)]
        [HttpGet]
        public ActionResult StockBlockIndex(Int64? idStockBlock)
        {
            SetDefaults();
            StockBlockListModel model = new StockBlockListModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();

            if (idStockBlock.HasValue)
            {
                model.IdStockBlock = idStockBlock;
                StockBlockBL bo = new StockBlockBL();
                StockBlockViewModel vModel = new StockBlockViewModel();

                vModel.IdStockBlock = idStockBlock;
                bo.GetStockBlock(UserManager.UserInfo,vModel);

                model.BlockReasonDesc = vModel.BlockReasonDesc;
                model.BlockStatusId = vModel.BlockedStatusId;
                model.BlockStatusName = vModel.BlockedStatusName;
            }
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex, CommonValues.PermissionCodes.StockBlock.StockBlockIndex)]
        public ActionResult ListStockBlock([DataSourceRequest] DataSourceRequest request, StockBlockListModel model)
        {
            var stockBlockBo = new StockBlockBL();

            var v = new StockBlockListModel(request)
                {
                    BlockDate = model.BlockDate,
                    IsBalance = model.IsBalance,
                    BlockStatusId = model.BlockStatusId,
                    IdDealer = model.IdDealer
                };

            var totalCnt = 0;
            var returnValue = stockBlockBo.ListStockBlock(UserManager.UserInfo,v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region StockBlock Create

        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex, CommonValues.PermissionCodes.StockBlock.StockBlockCreate)]
        public ActionResult StockBlockCreate()
        {
            SetDefaults();

            var model = new StockBlockViewModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex, CommonValues.PermissionCodes.StockBlock.StockBlockCreate)]
        [HttpPost]
        public ActionResult StockBlockCreate(StockBlockViewModel viewModel)
        {
            var stockBlockBo = new StockBlockBL();

            StockBlockViewModel viewControlModel = new StockBlockViewModel();
            viewModel.IdDealer = viewModel.IdDealer ?? UserManager.UserInfo.GetUserDealerId();
            viewControlModel.IdStockBlock = viewModel.IdStockBlock;
            

            SetDefaults();
            viewModel.BlockedStatusId = CommonValues.StockBlockStatus.PreparedToBlock; //default value

            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Insert;

                stockBlockBo.DMLStockBlock(UserManager.UserInfo,viewModel);

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();
            }

            var model = new StockBlockViewModel();
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                model.IdDealer = UserManager.UserInfo.GetUserDealerId();
            
            return View(model);
        }

        #endregion

        #region StockBlock Update
        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex, CommonValues.PermissionCodes.StockBlock.StockBlockUpdate)]
        [HttpGet]
        public ActionResult StockBlockUpdate(Int64? idStockBlock)//id
        {
            SetDefaults();
            var v = new StockBlockViewModel();
            if (idStockBlock != null)
            {
                var stockBlockBo = new StockBlockBL();
                v.IdStockBlock = idStockBlock;
                stockBlockBo.GetStockBlock(UserManager.UserInfo,v);                
            }
           
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex, CommonValues.PermissionCodes.StockBlock.StockBlockUpdate)]
        [HttpPost]
        public ActionResult StockBlockUpdate(StockBlockViewModel viewModel) // IEnumerable<HttpPostedFileBase> attachments)
        {
            var stockBlockBo = new StockBlockBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;

                stockBlockBo.DMLStockBlock(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }

            SetDefaults();

            return View(viewModel);
        }

        #endregion

        #region StockBlock Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex, CommonValues.PermissionCodes.StockBlock.StockBlockDelete)]
        public ActionResult DeleteStockBlock(int idStockBlock)
        {
            StockBlockViewModel viewModel = new StockBlockViewModel
            {
                IdStockBlock = idStockBlock
            };

            var stockBlockBo = new StockBlockBL();

            viewModel.CommandType = CommonValues.DMLType.Delete;

            stockBlockBo.DMLStockBlock(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion

        #region StockBlockTab Index

        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex)]
        [HttpGet]
        public ActionResult StockBlockTabIndex()
        {
            SetDefaults();
            StockBlockViewModel model = new StockBlockViewModel();

            return PartialView(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockCreate)]
        public ActionResult StockBlockTabIndex(StockBlockViewModel model, HttpPostedFileBase file)
        {
            StockBlockBL masterBo = new StockBlockBL();
            StockBlockViewModel masterModel = new StockBlockViewModel();

            SetDefaults();

            masterBo.DMLStockBlock(UserManager.UserInfo, masterModel);
            
           return Json(model.IdStockBlock, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}