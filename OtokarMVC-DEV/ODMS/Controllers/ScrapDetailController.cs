using System.IO;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Common;
using ODMSModel.Scrap;
using ODMSModel.ScrapDetail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ScrapDetailController : ControllerBase
    {
        #region General Methods

        private void SetDefaults()
        {
            ViewBag.DealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.ScrapReasonList = CommonBL.ListLookup(UserManager.UserInfo,CommonValues.LookupKeys.ScrapReason).Data;
        }

        [ValidateAntiForgeryToken]
        public JsonResult ScrapReasonIsDescRequired(string scrapReasonId)
        {
            if (!string.IsNullOrEmpty(scrapReasonId))
            {
                int isRequired = CommonBL.GetGeneralParameterValue(CommonValues.GeneralParameters.ScrapReasonIsDescRequired).Model.GetValue<int>();
                return Json(new {IsReasonDescRequired = isRequired.GetValue<bool>()});
            }
            else
            {
                return null;
            }
        }

        public ActionResult DownloadUploadedFile(string docId)
        {
            var bo = new DocumentBL();
            DocumentInfo model = bo.GetDocumentById(docId.GetValue<int>()).Model;

            return File(model.DocBinary, model.DocMimeType, model.DocName);
        }
        #endregion

        #region ScrapDetail Index

        [AuthorizationFilter(CommonValues.PermissionCodes.ScrapDetail.ScrapDetailIndex)]
        [HttpGet]
        public ActionResult ScrapDetailIndex(int? scrapId)
        {
            SetDefaults();
            ScrapDetailViewModel model = new ScrapDetailViewModel();
            if (scrapId != 0 && scrapId != null)
            {
                ScrapViewModel masterModel = new ScrapViewModel {ScrapId = scrapId.GetValue<int>()};
                ScrapBL masterBo = new ScrapBL();
                masterBo.GetScrap(UserManager.UserInfo,masterModel);
                model.ScrapId = masterModel.ScrapId;
                model.ScrapDate = masterModel.ScrapDate;
                model.ScrapReasonDesc = masterModel.ScrapReasonDesc;
                model.DocId = masterModel.DocId;
                model.DocName = masterModel.DocName;
                model.DealerId = masterModel.DealerId;
                model.ScrapReasonId = masterModel.ScrapReasonId;
            }
            else
            {
                if (UserManager.UserInfo.GetUserDealerId() != 0)
                {
                    model.DealerId = UserManager.UserInfo.GetUserDealerId();
                }
            }

            return PartialView(model);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.ScrapDetail.ScrapDetailIndex,
            CommonValues.PermissionCodes.Scrap.ScrapUpdate)]
        public ActionResult SaveFile(HttpPostedFileBase file, int scrapId)
        {
            ScrapBL masterBo = new ScrapBL();
            ScrapViewModel masterModel = new ScrapViewModel();

            if (file != null)
            {
                masterModel.ScrapId = scrapId;
                masterBo.GetScrap(UserManager.UserInfo,masterModel);
                masterModel.DocId = SaveAttachments(masterModel.DocId, file);
                masterModel.DocName = file.FileName;
                masterModel.CommandType = CommonValues.DMLType.Update;
                masterBo.DMLScrap(UserManager.UserInfo,masterModel);
            }
            return Json(masterModel.DocName);
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.ScrapDetail.ScrapDetailIndex,
            CommonValues.PermissionCodes.Scrap.ScrapUpdate)]
        public ActionResult RemoveFile(int scrapId)
        {
            int docId = 0;
            ScrapBL masterBo = new ScrapBL();
            ScrapViewModel masterModel = new ScrapViewModel {ScrapId = scrapId};

            masterBo.GetScrap(UserManager.UserInfo,masterModel);
            docId = masterModel.DocId.GetValue<int>();
            masterModel.DocId = null;
            masterModel.DocName = null;
            masterModel.CommandType = CommonValues.DMLType.Update;
            masterBo.DMLScrap(UserManager.UserInfo,masterModel);

            DocumentInfo docInfo = new DocumentInfo {DocId = docId, CommandType = CommonValues.DMLType.Delete};
            DocumentBL docBo = new DocumentBL();
            docBo.DMLDocument(UserManager.UserInfo,docInfo);

            return Json("");
        }

        [HttpPost]
        [AuthorizationFilter(CommonValues.PermissionCodes.ScrapDetail.ScrapDetailIndex)]
        public JsonResult ScrapDetailIndex(ScrapDetailViewModel model)
        {
            SetDefaults();

            ScrapBL masterBo = new ScrapBL();
            ScrapViewModel masterModel = new ScrapViewModel();

            if ((model.DealerId == null || model.DealerId == 0) && UserManager.UserInfo.IsDealer)
            {
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            }
            if (model.ScrapDate != null)
            {
                if (model.ScrapId != 0)
                {
                    masterModel.CommandType = CommonValues.DMLType.Update;
                    masterModel.ScrapId = model.ScrapId;
                    masterBo.GetScrap(UserManager.UserInfo,masterModel);
                }
                else
                {
                    masterModel.CommandType = CommonValues.DMLType.Insert;
                }
                masterModel.ScrapDate = model.ScrapDate;
                masterModel.DealerId = model.DealerId;
                if (model.ScrapReasonId != 4)
                {
                    List<SelectListItem> reasonList = CommonBL.ListLookup(UserManager.UserInfo,CommonValues.LookupKeys.ScrapReason).Data;
                    string reasonDesc = (from e in reasonList.AsEnumerable()
                                         where e.Value == model.ScrapReasonId.GetValue<string>()
                                         select e.Text).First();
                    model.ScrapReasonDesc = reasonDesc;
                }
                masterModel.ScrapReasonDesc = model.ScrapReasonDesc;
                masterModel.ScrapReasonId = model.ScrapReasonId.GetValue<int>();

                masterBo.DMLScrap(UserManager.UserInfo,masterModel);
                model.ScrapId = masterModel.ScrapId;
                ViewBag.RemovedScrapDetailIdList = null;
            }
            return Json(new {success = true, scrapId = masterModel.ScrapId});
        }

        private int? SaveAttachments(int? docId, HttpPostedFileBase attachments)
        {
            if (attachments != null)
            {
                MemoryStream target = new MemoryStream();
                attachments.InputStream.CopyTo(target);
                byte[] data = target.ToArray();

                DocumentInfo documentInfo = new DocumentInfo()
                    {
                        DocId = docId.GetValue<int>(),
                        DocBinary = data,
                        DocMimeType = attachments.ContentType,
                        DocName = attachments.FileName,
                        CommandType = CommonValues.DMLType.Insert
                    };
                DocumentBL documentBo = new DocumentBL();
                documentBo.DMLDocument(UserManager.UserInfo,documentInfo);
                docId = documentInfo.DocId;
            }
            return docId;
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.ScrapDetail.ScrapDetailIndex,
            CommonValues.PermissionCodes.ScrapDetail.ScrapDetailIndex)]
        public ActionResult ListScrapDetail([DataSourceRequest] DataSourceRequest request, ScrapDetailViewModel model)
        {
            string errorMessage = string.Empty;
            var returnValue = new List<ScrapDetailListModel>();
            var scrapDetailBo = new ScrapDetailBL();

            var v = new ScrapDetailListModel(request) {ScrapId = model.ScrapId, DealerId = model.DealerId};

            if (model.ScrapId != 0)
            {
                ScrapViewModel masterModel = new ScrapViewModel();
                masterModel.ScrapId = model.ScrapId;
                ScrapBL sBo = new ScrapBL();
                sBo.GetScrap(UserManager.UserInfo,masterModel);
                v.DealerId = masterModel.DealerId;

                var totalDetailCnt = 0;
                var returnValueDetail = scrapDetailBo.ListScrapDetail(UserManager.UserInfo,v, out totalDetailCnt).Data;
                returnValue.AddRange(returnValueDetail);
            }

            if (!string.IsNullOrEmpty(model.Barcode))
            {
                if (model.ScrapId != 0)
                {
                    var control = (from s in returnValue.AsEnumerable()
                                   where s.Barcode == model.Barcode
                                   select s);
                    if (control.Any())
                    {
                        errorMessage = MessageResource.ScrapDetail_Warning_SamePartExists;
                    }
                }
                if (errorMessage.Length == 0)
                {
                    v.Barcode = model.Barcode;
                    var totalPartCount = 0;
                    List<ScrapDetailListModel> partList = scrapDetailBo.ListScrapDetailPartByBarcode(UserManager.UserInfo,v,out totalPartCount).Data;
                    if (!partList.Any())
                    {
                        errorMessage = MessageResource.ScrapDetail_Warning_PartNotFound;
                    }
                    else
                    {
                        if (partList.ElementAt(0).StockTypeId == 0)
                        {
                            errorMessage = MessageResource.ScrapDetail_Warning_PartDoesntHaveStockCard;
                        }
                        else
                        {
                            partList = (from pl in partList.AsEnumerable()
                                        where pl.RackId != 0 && pl.Quantity.GetValue<decimal>() != 0
                                        select pl).ToList<ScrapDetailListModel>();
                            if (!partList.Any())
                            {
                                errorMessage = MessageResource.ScrapDetail_Warning_PartNotFound;
                            }
                            else
                            {
                                foreach (var scrapDetailListModel in partList)
                                {
                                    if (scrapDetailListModel.StockQuantity == 0)
                                    {
                                        if (scrapDetailListModel.Quantity.GetValue<decimal>() == 0)
                                        {
                                            returnValue.Remove(scrapDetailListModel);
                                        }
                                        else
                                        {
                                            scrapDetailListModel.StockQuantity =
                                                scrapDetailListModel.Quantity.GetValue<decimal>();
                                        }
                                    }
                                    else
                                    {
                                        if (scrapDetailListModel.Quantity.GetValue<decimal>() != 0)
                                        {
                                            scrapDetailListModel.StockQuantity = (scrapDetailListModel.StockQuantity >
                                                                                  scrapDetailListModel.Quantity.GetValue<decimal>()
                                                                                      ? scrapDetailListModel.Quantity.GetValue<decimal>()
                                                                                      : scrapDetailListModel.StockQuantity);
                                        }
                                    }

                                    scrapDetailListModel.Quantity = "0";
                                }

                                returnValue.AddRange(partList);
                            }
                        }
                    }
                }
            }

            return Json(new
                {
                    Data = returnValue,
                    Total = returnValue.Count(),
                    ErrorMessage = errorMessage
                });
        }


        [AuthorizationFilter(CommonValues.PermissionCodes.ScrapDetail.ScrapDetailCreate,
            CommonValues.PermissionCodes.ScrapDetail.ScrapDetailUpdate)]
        [HttpPost]
        public ActionResult ScrapDetailSave([DataSourceRequest] DataSourceRequest request,
                                            [Bind(Prefix = "models")] IEnumerable<ScrapDetailListModel>
                                                scrapDetailList)
        {
            int scrapId = (scrapDetailList.ElementAt(0) as ScrapDetailListModel).ScrapId;

            if (scrapId != 0)
            {
                var totalDetailCnt = 0;
                var v = new ScrapDetailListModel {ScrapId = scrapId};
                ScrapDetailBL scrapDetailBo = new ScrapDetailBL();

                var duplicateControl = (from c in
                                            (from r in scrapDetailList.AsEnumerable()
                                             group r by
                                                 new {r.ScrapId, r.PartId, r.StockTypeId, r.WarehouseId, r.RackId}
                                             into grp
                                             select new {grp.Key, Count = grp.Count()})
                                        where c.Count > 1
                                        select c);

                if (duplicateControl.Any())
                {
                    return Json(MessageResource.ScrapDetail_Warning_DuplicateFound);
                }


                List<ScrapDetailListModel> existingList = scrapDetailBo.ListScrapDetail(UserManager.UserInfo,v, out totalDetailCnt).Data;
                var existingControl = (from e in existingList.AsEnumerable()
                                       join n in scrapDetailList.AsEnumerable()
                                           on
                                           new {e.PartId, e.StockTypeId, e.WarehouseId, e.RackId, e.ScrapId}
                                           equals
                                           new {n.PartId, n.StockTypeId, n.WarehouseId, n.RackId, n.ScrapId}
                                       where n.ScrapDetailId != e.ScrapDetailId
                                       select e);
                if (existingControl.Any())
                {
                    return Json(MessageResource.ScrapDetail_Warning_SameDataAlreadyExists);
                }

                var quantityControl = (from e in scrapDetailList.AsEnumerable()
                                       where e.Quantity.GetValue<decimal>() != 0
                                       select e);
                var existingQuantityControl =(from e in existingList.AsEnumerable()
                                       where e.Quantity.GetValue<decimal>() != 0
                                       select e);
                if ((!quantityControl.Any() && scrapDetailList.Count() == existingList.Count()) ||
                    (!quantityControl.Any() && !existingQuantityControl.Any()))
                {
                    return Json(MessageResource.ScrapDetail_Warning_ZeroValues);
                }
            }

            string result = SaveResults(scrapDetailList, scrapId);

            if (result.Length != 0)
            {
                return Json(result);
            }

            return Json("");
        }

        private static string SaveResults(IEnumerable<ScrapDetailListModel> scrapDetailList, int scrapId)
        {
            ScrapDetailBL resultBl = new ScrapDetailBL();
            ScrapDetailViewModel resultViewModel = new ScrapDetailViewModel();

            foreach (var result in scrapDetailList)
            {
                resultViewModel.ScrapDetailId = result.ScrapDetailId;
                resultBl.GetScrapDetail(UserManager.UserInfo,resultViewModel);
                resultViewModel.ScrapId = scrapId;
                resultViewModel.PartId = result.PartId.GetValue<int>();
                resultViewModel.StockTypeId = result.StockTypeId;
                resultViewModel.WarehouseId = result.WarehouseId;
                resultViewModel.RackId = result.RackId;
                resultViewModel.Quantity = result.Quantity.GetValue<decimal>();
                resultViewModel.CommandType = result.Quantity.GetValue<decimal>() == 0
                                                  ? CommonValues.DMLType.Delete
                                                  : result.IsNew
                                                        ? CommonValues.DMLType.Insert
                                                        : CommonValues.DMLType.Update;
                resultBl.DMLScrapDetail(UserManager.UserInfo,resultViewModel);
                if (resultViewModel.ErrorNo > 0)
                {
                    return resultViewModel.ErrorMessage;
                }
            }
            return string.Empty;
        }

        #endregion

        #region ScrapDetail Delete


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.ScrapDetail.ScrapDetailIndex, CommonValues.PermissionCodes.ScrapDetail.ScrapDetailDelete)]
        public ActionResult DeleteScrapDetail(int scrapDetailId)
        {
            ScrapDetailViewModel viewModel = new ScrapDetailViewModel {ScrapDetailId = scrapDetailId};
            var scrapDetailBo = new ScrapDetailBL();
            // scrapDetail_det kaydının cancel ve confirm stock transaction id değeri dolu değilse kayıt silinebilir.
            viewModel = scrapDetailBo.GetScrapDetail(UserManager.UserInfo,viewModel).Model;
            if (viewModel.ScrapId != 0)
            {
                if ((viewModel.CancelIdStockTransaction == null && viewModel.ConfirmIdStockTransaction == null) ||
                    (viewModel.CancelIdStockTransaction == 0 && viewModel.ConfirmIdStockTransaction == 0))
                {
                    viewModel.CommandType = CommonValues.DMLType.Delete;
                    scrapDetailBo.DMLScrapDetail(UserManager.UserInfo,viewModel);

                    ModelState.Clear();
                    if (viewModel.ErrorNo == 0)
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                                   MessageResource.Global_Display_Success);
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                                                               viewModel.ErrorMessage);
                }
                else
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                                                               MessageResource.ScrapDetail_Error_TransactionExists);
                }
            }
            
            return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                       MessageResource.Global_Display_Success);
        }

        #endregion

        #region Approve/Cancel
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.ScrapDetail.ScrapDetailUpdate,
            CommonValues.PermissionCodes.ScrapDetail.ScrapDetailIndex)]
        public ActionResult ApproveScrapDetail(string scrapDetailIdList, int scrapId)
        {
            bool isSuccessfull = true;
            string errorMessage = string.Empty;
            ViewBag.HideElements = false;
            List<string> idList = scrapDetailIdList.Split(',').ToList<string>();

            if (!string.IsNullOrEmpty(scrapDetailIdList))
            {
                int totalDetailCount = 0;
                var bo = new ScrapDetailBL();
                var listModel = new ScrapDetailListModel()
                    {
                        ScrapId = scrapId
                    };
                List<ScrapDetailListModel> detailList = bo.ListScrapDetail(UserManager.UserInfo,listModel, out totalDetailCount).Data;
                var control = (from r in detailList.AsEnumerable()
                               where scrapDetailIdList.Contains(r.ScrapDetailId.GetValue<string>())
                                     && r.CancelUserId == 0 && r.ConfirmUserId == 0
                               select r);
                if (control.Count() != idList.Count())
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.ScrapDetail_Error_ApproveStatus);
                }
                else
                {
                    foreach (string idStr in idList)
                    {
                        var model = new ScrapDetailViewModel()
                            {
                                ScrapId = scrapId,
                                ScrapDetailId = idStr.GetValue<int>()
                            };
                        model = bo.GetScrapDetail(UserManager.UserInfo,model).Model;
                        model.CommandType = CommonValues.DMLType.Update;
                        model.ConfirmUserId = UserManager.UserInfo.UserId;
                        bo.DMLScrapDetail(UserManager.UserInfo,model);
                        if (model.ErrorNo > 0)
                        {
                            isSuccessfull = false;
                            errorMessage = model.ErrorMessage;
                            break;
                        }
                    }
                    if (isSuccessfull)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                              MessageResource.Global_Display_Success);
                    }
                    else
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, errorMessage);
                    }
                }
            }
            else
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.ScrapDetail_Error_RowNotSelected);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.ScrapDetail.ScrapDetailUpdate,
            CommonValues.PermissionCodes.ScrapDetail.ScrapDetailIndex)]
        public ActionResult CancelScrapDetail(string scrapDetailIdList, int scrapId)
        {
            bool isSuccessfull = true;
            string errorMessage = string.Empty;
            ViewBag.HideElements = false;
            List<string> idList = scrapDetailIdList.Split(',').ToList<string>();

            if (!string.IsNullOrEmpty(scrapDetailIdList))
            {
                int totalDetailCount = 0;
                var bo = new ScrapDetailBL();
                var listModel = new ScrapDetailListModel()
                {
                    ScrapId = scrapId
                };
                List<ScrapDetailListModel> detailList = bo.ListScrapDetail(UserManager.UserInfo,listModel, out totalDetailCount).Data;
                var control = (from r in detailList.AsEnumerable()
                               where scrapDetailIdList.Contains(r.ScrapDetailId.GetValue<string>())
                                     && r.CancelUserId == 0 && r.ConfirmUserId != 0
                               select r);
                if (control.Count() != idList.Count())
                {
                    return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.ScrapDetail_Error_CancelStatus);
                }
                else
                {
                    foreach (string idStr in idList)
                    {
                        var model = new ScrapDetailViewModel()
                        {
                            ScrapId = scrapId,
                            ScrapDetailId = idStr.GetValue<int>()
                        };
                        model = bo.GetScrapDetail(UserManager.UserInfo, model).Model;
                        model.CommandType = CommonValues.DMLType.Update;
                        model.CancelUserId = UserManager.UserInfo.UserId;
                        bo.DMLScrapDetail(UserManager.UserInfo, model);
                        if (model.ErrorNo > 0)
                        {
                            isSuccessfull = false;
                            errorMessage = model.ErrorMessage;
                            break;
                        }
                    }
                    if (isSuccessfull)
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                                                              MessageResource.Global_Display_Success);
                    }
                    else
                    {
                        return GenerateAsyncOperationResponse(AsynOperationStatus.Error, errorMessage);
                    }
                }
            }
            else
            {
                return GenerateAsyncOperationResponse(AsynOperationStatus.Error, MessageResource.ScrapDetail_Error_RowNotSelected);
            }
        }
        #endregion
    }
}