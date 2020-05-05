using System.Linq;
using ODMS.Filters;
using ODMSBusiness;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon.Security;
using Kendo.Mvc.UI;
using ODMSCommon;
using ODMSModel.SparePart;
using ODMSModel.StockBlockDetail;
using ODMSModel.StockBlock;
using ODMSCommon.Resources;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class StockBlockDetailController : ControllerBase
    {
        #region Member Variables

        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;

            ViewBag.StockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;

        }

        [HttpGet]
        public JsonResult ListStockTypes(Int64? id, int idDealer)
        {
            if (id != null && idDealer != 0)
            {
                return Json(new CommonBL().ListStockTypes(UserManager.UserInfo, id, idDealer).Data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new List<StockBlockDetailViewModel>(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetBlockQuantity(Int64? idPart, int? idStockType, int? idDealer)
        {
            if (idPart != null && idStockType != null && idDealer != null)
            {
                return Json(new CommonBL().GetBlockQuantity(UserManager.UserInfo, idPart, idStockType, idDealer).Model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new List<StockBlockViewModel>(), JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region StockBlockDetail Index

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex)]
        public ActionResult StockBlockDetailIndex(long? idStockBlock)
        {
            var model = new StockBlockDetailListModel();

            if (idStockBlock != null)
            {
                StockBlockViewModel masterModel = new StockBlockViewModel();
                masterModel.IdStockBlock = idStockBlock.GetValue<int>();
                StockBlockBL sbBo = new StockBlockBL();
                sbBo.GetStockBlock(UserManager.UserInfo, masterModel);

                if (UserManager.UserInfo.GetUserDealerId() != 0)
                    model.IdDealer = UserManager.UserInfo.GetUserDealerId();
                model.IdStockBlock = idStockBlock;
                model.StockBlockStatusId = masterModel.BlockedStatusId;
                model.BlockReasonDesc = masterModel.BlockReasonDesc;
            }

            return PartialView(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex)]
        [HttpPost]
        public ActionResult StockBlockDetailIndex([DataSourceRequest] DataSourceRequest request, Int64? idStockBlock)
        {
            SetDefaults();
            StockBlockDetailListModel model = new StockBlockDetailListModel();

            var stockBlockDetailBo = new StockBlockDetailBL();

            var v = new StockBlockDetailListModel(request);

            if (idStockBlock != null)
            {
                if (UserManager.UserInfo.GetUserDealerId() != 0)
                    model.IdDealer = UserManager.UserInfo.GetUserDealerId();
                v.IdStockBlock = idStockBlock;
            }

            var totalCnt = 0;
            var returnValue = stockBlockDetailBo.ListStockBlockDetail(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex, CommonValues.PermissionCodes.StockBlock.StockBlockIndex)]
        public ActionResult ListStockBlockDetail([DataSourceRequest] DataSourceRequest request, StockBlockDetailListModel model)
        {
            var stockBlockDetailBo = new StockBlockDetailBL();

            var v = new StockBlockDetailListModel(request);

            var totalCnt = 0;
            List<StockBlockDetailListModel> returnValue = new List<StockBlockDetailListModel>();
            if (model.IdStockBlock!=null)
            {
                v.IdDealer = model.IdDealer;
                v.IdStockBlock = model.IdStockBlock;
                v.StockBlockStatusId = model.StockBlockStatusId;
                v.BlockReasonDesc = model.BlockReasonDesc;

                returnValue = stockBlockDetailBo.ListStockBlockDetail(UserManager.UserInfo, v, out totalCnt).Data;

            }
            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region StockBlockDetail Create

        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex, CommonValues.PermissionCodes.StockBlock.StockBlockCreate)]
        public ActionResult StockBlockDetailCreate(Int64? idStockBlock)
        {
            SetDefaults();

            var model = new StockBlockDetailViewModel();
            var stockBlockDetailBo = new StockBlockDetailBL();

            if (idStockBlock != null)
            {
                model.IdStockBlock = idStockBlock;
                if (UserManager.UserInfo.GetUserDealerId() != 0)
                    model.IdDealer = UserManager.UserInfo.GetUserDealerId();            
            }

            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex, CommonValues.PermissionCodes.StockBlock.StockBlockCreate)]
        [HttpPost]
        public ActionResult StockBlockDetailCreate(StockBlockDetailViewModel viewModel)
        {
            var stockBlockDetailBo = new StockBlockDetailBL();

            StockBlockDetailViewModel viewControlModel = new StockBlockDetailViewModel();
            viewModel.IdDealer = viewModel.IdDealer ?? UserManager.UserInfo.GetUserDealerId();
            viewControlModel.IdStockBlock = viewModel.IdStockBlock;
            viewControlModel.IdStockType = viewModel.IdStockType;
            viewControlModel.IdPart = viewModel.IdPart;

            stockBlockDetailBo.GetStockBlockDetail(UserManager.UserInfo, viewControlModel);
            
            if (viewModel.IdPart.HasValue)
            {
                SparePartIndexViewModel spModel = new SparePartIndexViewModel();
                spModel.PartId = viewModel.IdPart.GetValue<int>();
                SparePartBL spBo = new SparePartBL();
                spBo.GetSparePart(UserManager.UserInfo, spModel);
                viewModel.PartCode = spModel.PartCode;
                viewModel.PartName = spModel.PartCode + CommonValues.Slash + spModel.PartNameInLanguage;
            }

            if (viewControlModel.IdStockBlockDet != null)
            {
                SetMessage(MessageResource.StockBlock_Error_DataHave, CommonValues.MessageSeverity.Fail);

                ModelState.Clear();
                return View(viewModel);
            }

            SetDefaults();

            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Insert;

                stockBlockDetailBo.DMLStockBlockDetail(UserManager.UserInfo, viewModel);

                CheckErrorForMessage(viewModel, true);

                ModelState.Clear();

                var model = new StockBlockDetailViewModel();
                model.IdStockBlock = viewControlModel.IdStockBlock; ;
                if (UserManager.UserInfo.GetUserDealerId() != 0)
                    model.IdDealer = UserManager.UserInfo.GetUserDealerId();
                return View(model);
            }

            return View(viewModel);
        }

        #endregion

        #region StockBlockDetail Update
        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex, CommonValues.PermissionCodes.StockBlock.StockBlockUpdate)]
        [HttpGet]
        public ActionResult StockBlockDetailUpdate(Int64? idStockBlockDet)
        {
            SetDefaults();
            var v = new StockBlockDetailViewModel();
            if (idStockBlockDet != null)
            {
                var stockBlockDetailBo = new StockBlockDetailBL();
                v.IdStockBlockDet = idStockBlockDet;
                stockBlockDetailBo.GetStockBlockDetails(UserManager.UserInfo, v);

            }
            return View(v);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex, CommonValues.PermissionCodes.StockBlock.StockBlockUpdate)]
        [HttpPost]
        public ActionResult StockBlockDetailUpdate([DataSourceRequest] DataSourceRequest request, StockBlockDetailViewModel
                                                     viewModel)
        {
            if (UserManager.UserInfo.GetUserDealerId() != 0)
                viewModel.IdDealer = UserManager.UserInfo.GetUserDealerId();


            var stockBlockDetailBo = new StockBlockDetailBL();
            if (ModelState.IsValid)
            {
                viewModel.CommandType = CommonValues.DMLType.Update;

                stockBlockDetailBo.DMLStockBlockDetail(UserManager.UserInfo, viewModel);
                
                return ReturnCustomErrorMessage(viewModel);
            }

            return View();
        }

        public JsonResult ReturnCustomErrorMessage(ODMSModel.ModelBase targetModel)
        {
            string ErrorMessage = String.Empty;
            bool IsError = false;

            if (targetModel.ErrorNo > 0)
            {
                ErrorMessage = targetModel.ErrorMessage;
                IsError = true;
            }
            else
            {
                ErrorMessage = MessageResource.Global_Display_Success;
            }

            return Json(new { isError = IsError, message = ErrorMessage });

        }

        #endregion

        #region StockBlockDetail Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex, CommonValues.PermissionCodes.StockBlock.StockBlockDelete)]
        public ActionResult DeleteStockBlockDetail(Int64 idStockBlockDet)
        {
            StockBlockDetailViewModel viewModel = new StockBlockDetailViewModel {IdStockBlockDet = idStockBlockDet};

            var StockBlockDetailBo = new StockBlockDetailBL();

            viewModel.CommandType = CommonValues.DMLType.Delete;

            StockBlockDetailBo.DMLStockBlockDetail(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                viewModel.ErrorMessage);
        }
        #endregion

        #region StockBlockDetail Block-UnBlock

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex, CommonValues.PermissionCodes.StockBlock.StockBlockCreate)]
        public ActionResult StockBlockDetailBlocked(Int64? idStockBlock)
        {
            List<StockBlockDetailViewModel> stockBlockListModel = new List<StockBlockDetailViewModel>();
            StockBlockDetailViewModel errorModel = new StockBlockDetailViewModel();
            SetDefaults();

            var model = new StockBlockDetailViewModel();
            var stockBlockDetailBo = new StockBlockDetailBL();

            if (idStockBlock != null)
            {
                model.IdStockBlock = idStockBlock;
                if (UserManager.UserInfo.GetUserDealerId() != 0)
                    model.IdDealer = UserManager.UserInfo.GetUserDealerId();
                stockBlockListModel = stockBlockDetailBo.GetStockBlockDetailList(UserManager.UserInfo, model).Data;             
            }

            errorModel = stockBlockDetailBo.DMLStockBlockDetailList(UserManager.UserInfo,stockBlockListModel, errorModel).Model;

            if (errorModel.ErrorNo == 0)
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            }
            else
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                    errorModel.ErrorMessage);
        }

        //Unblock StockBlockDetail List
        [AuthorizationFilter(CommonValues.PermissionCodes.StockBlock.StockBlockIndex, CommonValues.PermissionCodes.StockBlock.StockBlockUpdate)]
        [HttpPost]
        public ActionResult StockBlockDetailSave([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<StockBlockDetailListModel> modelList)
        {

            StockBlockDetailViewModel errorModel = new StockBlockDetailViewModel();

            SaveResults(modelList, errorModel);
            
            if (errorModel.ErrorNo == 0)
                return Json("");
            return Json(errorModel.ErrorMessage);

        }


        private static void SaveResults(IEnumerable<StockBlockDetailListModel> stockBlockDetailList,
                                        StockBlockDetailViewModel stockBlockDetailViewModel)
        {
            StockBlockDetailBL stockBlockDetailBl = new StockBlockDetailBL();

            if (UserManager.UserInfo.GetUserDealerId() != 0)
                stockBlockDetailViewModel.IdDealer = UserManager.UserInfo.GetUserDealerId();

            if (stockBlockDetailList.Any())
            {
                foreach (var result in stockBlockDetailList)
                {
                    stockBlockDetailViewModel.BlockQty = result.BlockQty;
                    stockBlockDetailViewModel.UnBlockQty = result.UnBlockQty;
                    stockBlockDetailViewModel.RemovedBlockQty = result.RemovedQty;
                    stockBlockDetailViewModel.IdStockBlock = result.IdStockBlock;
                    stockBlockDetailViewModel.IdStockBlockDet = result.IdStockBlockDet;
                    stockBlockDetailViewModel.BlockReasonDesc = result.BlockReasonDesc;
                    stockBlockDetailViewModel.IdPart = result.IdPart;
                    stockBlockDetailViewModel.IdStockType = result.IdStockType;
                    stockBlockDetailViewModel.CommandType = CommonValues.BlockType.RemoveBlock;
                    stockBlockDetailBl.DMLStockBlockDetail(UserManager.UserInfo,stockBlockDetailViewModel);
                }
                int totalCount = 0;
                StockBlockDetailListModel sbdListModel = new StockBlockDetailListModel();
                sbdListModel.IdStockBlock = stockBlockDetailList.ElementAt(0).IdStockBlock;
                List<StockBlockDetailListModel> list = stockBlockDetailBl.ListStockBlockDetail(UserManager.UserInfo,sbdListModel,out totalCount).Data;
                if (list.Any())
                {
                    var control = (from e in list
                                   where e.BlockQty != 0
                                   select e);
                    if (!control.Any())
                    {
                        StockBlockBL sbBo = new StockBlockBL();
                        StockBlockViewModel sbModel = new StockBlockViewModel();
                        sbModel.IdStockBlock = stockBlockDetailList.ElementAt(0).IdStockBlock;
                        sbBo.GetStockBlock(UserManager.UserInfo,sbModel);
                        sbModel.CommandType = CommonValues.DMLType.Update;
                        sbModel.BlockedStatusId = CommonValues.StockBlockStatus.BlockRemoved;
                        sbBo.DMLStockBlock(UserManager.UserInfo, sbModel);
                        if (sbModel.ErrorNo > 0)
                        {
                            stockBlockDetailViewModel.ErrorNo = sbModel.ErrorNo;
                            stockBlockDetailViewModel.ErrorMessage = sbModel.ErrorMessage;
                        }
                    }
                }
            }
        }

        #endregion
    }
}