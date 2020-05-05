using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.Common;
using ODMSModel.DamagedItemDispose;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class DamagedItemDisposeController : ControllerBase
    {
        private void SetDefaults()
        {
            // DealerList
            List<SelectListItem> dealerList = DealerBL.ListDealerAsSelectListItem().Data;
            ViewBag.DealerList = dealerList;
            // WarehouseList
            List<SelectListItem> warehouseList = WarehouseBL.ListWarehousesOfDealerAsSelectList(UserManager.UserInfo.GetUserDealerId()).Data;
            ViewBag.WarehouseList = warehouseList;
            // StockTypeList
            List<SelectListItem> stockTypeList = CommonBL.ListStockTypes(UserManager.UserInfo).Data;
            ViewBag.StockTypeList = stockTypeList;
            // YesNoList
            List<SelectListItem> yesNolist = CommonBL.ListYesNo().Data;
            ViewBag.YesNoList = yesNolist;
        }

        [HttpGet]
        public JsonResult ListRacks(int? id,Int64 partId)
        {
            if (id.HasValue)
            {
                List<SelectListItem> rackList = CommonBL.ListRacksByPartWareHouse(UserManager.UserInfo, id.Value,partId).Data;
                rackList.Insert(0, new SelectListItem() { Text = MessageResource.Global_Display_Choose, Value = "0" });

                return Json(rackList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        #region DamagedItemDispose Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.DamagedItemDispose.DamagedItemDisposeIndex)]
        [HttpGet]
        public ActionResult DamagedItemDisposeIndex()
        {
            DamagedItemDisposeListModel model = new DamagedItemDisposeListModel();
            if (UserManager.UserInfo.GetUserDealerId() > 0)
            {
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            }
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.DamagedItemDispose.DamagedItemDisposeIndex)]
        public ActionResult ListDamagedItemDispose([DataSourceRequest] DataSourceRequest request,
                                                   DamagedItemDisposeListModel model)
        {
            var damagedItemDisposeBo = new DamagedItemDisposeBL();
            var v = new DamagedItemDisposeListModel(request);
            var totalCnt = 0;
            v.DamageDisposeId = model.DamageDisposeId;
            v.DealerId = model.DealerId;
            v.WarehouseId = model.WarehouseId;
            v.RackId = model.RackId;
            v.PartId = model.PartId;
            v.StartDate = model.StartDate;
            v.EndDate = model.EndDate;
            v.IsOriginal = model.IsOriginal;
            v.StockTypeId = model.StockTypeId;

            var returnValue = damagedItemDisposeBo.ListDamagedItemDisposes(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
                {
                    Data = returnValue,
                    Total = totalCnt
                });
        }

        #endregion

        #region DamagedItemDispose Create

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.DamagedItemDispose.DamagedItemDisposeIndex,
            ODMSCommon.CommonValues.PermissionCodes.DamagedItemDispose.DamagedItemDisposeCreate)]
        [HttpGet]
        public ActionResult DamagedItemDisposeCreate()
        {
            DamagedItemDisposeViewModel model = new DamagedItemDisposeViewModel();
            if (UserManager.UserInfo.GetUserDealerId() > 0)
            {
                model.DealerId = UserManager.UserInfo.GetUserDealerId();
            }
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.DamagedItemDispose.DamagedItemDisposeIndex,
            ODMSCommon.CommonValues.PermissionCodes.DamagedItemDispose.DamagedItemDisposeCreate)]
        [HttpPost]
        public ActionResult DamagedItemDisposeCreate(DamagedItemDisposeViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var damagedItemDisposeBo = new DamagedItemDisposeBL();

            if (ModelState.IsValid)
            {
                viewModel.DocId = SaveAttachments(attachments);
                viewModel.CommandType = CommonValues.DMLType.Insert;
                damagedItemDisposeBo.DMLDamagedItemDispose(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                ModelState.Clear();
            }
            return View(viewModel);
        }
        private int SaveAttachments(IEnumerable<HttpPostedFileBase> attachments)
        {
            int docId = 0;
            if (attachments != null)
            {
                MemoryStream target = new MemoryStream();
                attachments.ElementAt(0).InputStream.CopyTo(target);
                byte[] data = target.ToArray();

                DocumentInfo documentInfo = new DocumentInfo()
                {
                    DocBinary = data,
                    DocMimeType = attachments.ElementAt(0).ContentType,
                    DocName = attachments.ElementAt(0).FileName,
                    CommandType = CommonValues.DMLType.Insert
                };
                DocumentBL documentBo = new DocumentBL();
                documentBo.DMLDocument(UserManager.UserInfo, documentInfo);
                docId = documentInfo.DocId;
            }
            return docId;
        }
        #endregion

        #region DamagedItemDispose Details
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.DamagedItemDispose.DamagedItemDisposeDetails)]
        public ActionResult DamagedItemDisposeDocumentDownload(int? id)
        {
            DocumentBL docBl = new DocumentBL();
            DocumentInfo docInfo = docBl.GetDocumentById(id.GetValue<int>()).Model;
            return File(docInfo.DocBinary, docInfo.DocMimeType, docInfo.DocName);
        }
        #endregion

    }
}