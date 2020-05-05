using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSModel.Common;
using ODMSModel.PeriodicMaintControlList;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ODMSCommon;
using System.IO;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class PeriodicMaintControlListController : ControllerBase
    {

        private void SetDefaults()
        {
            ViewBag.LanguageList = LanguageBL.ListLanguageAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.VehicleTypeList = VehicleTypeBL.ListVehicleTypeAsSelectList(UserManager.UserInfo, null).Data;
        }

        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.PeriodicMaintControlList.PeriodicMaintControlListIndex)]
        public ActionResult ListVehicleEngineTypes(string vehicleTypeId)
        {
            return string.IsNullOrEmpty(vehicleTypeId)
                       ? Json(new List<SelectListItem>(), JsonRequestBehavior.AllowGet)
                       : Json(VehicleBL.ListVehicleEngineTypesAsSelectListItem(UserManager.UserInfo, vehicleTypeId.GetValue<int?>()).Data, JsonRequestBehavior.AllowGet);
        }

        #region PeriodicMaintControlList Index

        [AuthorizationFilter(CommonValues.PermissionCodes.PeriodicMaintControlList.PeriodicMaintControlListIndex)]
        [HttpGet]
        public ActionResult PeriodicMaintControlListIndex()
        {
            PeriodicMaintControlListListModel model = new PeriodicMaintControlListListModel();
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PeriodicMaintControlList.PeriodicMaintControlListIndex)]
        public ActionResult ListPeriodicMaintControlList([DataSourceRequest] DataSourceRequest request,
                                                   PeriodicMaintControlListListModel model)
        {
            var periodicMaintControlListBo = new PeriodicMaintControlListBL();
            var v = new PeriodicMaintControlListListModel(request);
            var totalCnt = 0;
            v.IdType = model.IdType;
            v.DocId = model.DocId;
            v.LanguageCustom = model.LanguageCustom;
            v.EngineType = model.EngineType;

            var returnValue = periodicMaintControlListBo.ListPeriodicMaintControlList(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region PeriodicMaintControlList Create

        [AuthorizationFilter(CommonValues.PermissionCodes.PeriodicMaintControlList.PeriodicMaintControlListIndex,
            CommonValues.PermissionCodes.PeriodicMaintControlList.PeriodicMaintControlListCreate)]
        [HttpGet]
        public ActionResult PeriodicMaintControlListCreate()
        {
            PeriodicMaintControlListViewModel model = new PeriodicMaintControlListViewModel();

            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(CommonValues.PermissionCodes.PeriodicMaintControlList.PeriodicMaintControlListIndex, CommonValues.PermissionCodes.PeriodicMaintControlList.PeriodicMaintControlListCreate)]
        [HttpPost]
        public ActionResult PeriodicMaintControlListCreate(PeriodicMaintControlListViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            ViewBag.IsSuccessfull = false;
            SetDefaults();

            if (attachments == null)
            {
                SetMessage(MessageResource.PeriodicMaintenance_Warning_Document, CommonValues.MessageSeverity.Fail);
                return View(viewModel);
            }
            var periodicMaintControlListBo = new PeriodicMaintControlListBL();

            if (ModelState.IsValid)
            {
                viewModel.DocId = SaveAttachments(attachments);
                viewModel.CommandType = CommonValues.DMLType.Insert;
                periodicMaintControlListBo.DMLPeriodicMaintControlList(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
                ModelState.Clear();
                if (viewModel.ErrorNo == 0)
                    ViewBag.IsSuccessfull = true;
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

        #region PeriodicMaintControlList Download
        [HttpGet]
        [AuthorizationFilter(CommonValues.PermissionCodes.PeriodicMaintControlList.PeriodicMaintControlListIndex)]
        public ActionResult PeriodicMaintControlListDocumentDownload(int? id)
        {
            DocumentBL docBl = new DocumentBL();
            DocumentInfo docInfo = docBl.GetDocumentById(id.GetValue<int>()).Model;
            return File(docInfo.DocBinary, docInfo.DocMimeType, docInfo.DocName);
        }
        #endregion

        #region PeriodicMaintControlList Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(CommonValues.PermissionCodes.PeriodicMaintControlList.PeriodicMaintControlListIndex, CommonValues.PermissionCodes.PeriodicMaintControlList.PeriodicMaintControlListDelete)]
        public ActionResult DeletePeriodicMaintControlList(int periodicMaintCtrlListId, int idType, string langCode)
        {
            PeriodicMaintControlListViewModel viewModel = new PeriodicMaintControlListViewModel
            {
                PeriodicMaintCtrlListId = periodicMaintCtrlListId,
                IdType = idType,
                LanguageCustom = langCode
            };

            var periodicMaintControlListBo = new PeriodicMaintControlListBL();

            viewModel.CommandType = CommonValues.DMLType.Delete;

            periodicMaintControlListBo.DMLPeriodicMaintControlList(UserManager.UserInfo, viewModel);

            ModelState.Clear();
            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion
    }
}