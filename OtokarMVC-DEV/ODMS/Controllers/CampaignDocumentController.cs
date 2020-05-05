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
using ODMSModel.CampaignDocument;
using ODMSModel.Common;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class CampaignDocumentController : ControllerBase
    {
        private void SetDefaults()
        {
            List<SelectListItem> languageList = LanguageBL.ListLanguageAsSelectListItem(UserManager.UserInfo).Data;
            ViewBag.LanguageList = languageList;
        }

        #region Campaign Document Index

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentIndex)]
        [HttpGet]
        public ActionResult CampaignDocumentIndex(string campaignCode)
        {
            CampaignDocumentListModel model = new CampaignDocumentListModel { CampaignCode = campaignCode };
            SetDefaults();
            return PartialView(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentIndex, ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentDetails)]
        public ActionResult ListCampaignDocument([DataSourceRequest] DataSourceRequest request, CampaignDocumentListModel model)
        {
            var campaignDocumentBo = new CampaignDocumentBL();
            var v = new CampaignDocumentListModel(request);
            var totalCnt = 0;
            v.CampaignCode = model.CampaignCode;
            var returnValue = campaignDocumentBo.ListCampaignDocuments(UserManager.UserInfo, v, out totalCnt).Data;

            return Json(new
            {
                Data = returnValue,
                Total = totalCnt
            });
        }

        #endregion

        #region Campaign Document Create
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentIndex, ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentCreate)]
        [HttpGet]
        public ActionResult CampaignDocumentCreate(string campaignCode)
        {
            CampaignDocumentViewModel model = new CampaignDocumentViewModel();
            model.CampaignCode = campaignCode;
            SetDefaults();
            return View(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentIndex, ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentCreate)]
        [HttpPost]
        public ActionResult CampaignDocumentCreate(CampaignDocumentViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var campaignDocumentBo = new CampaignDocumentBL();

            if (ModelState.IsValid)
            {
                if (attachments != null)
                {
                    viewModel.DocId = SaveAttachments(viewModel.DocId, attachments);
                    viewModel.DocName = attachments.ElementAt(0).FileName;
                    viewModel.CommandType = CommonValues.DMLType.Insert;
                    campaignDocumentBo.DMLCampaignDocument(UserManager.UserInfo, viewModel);

                    CheckErrorForMessage(viewModel, true);

                    ModelState.Clear();
                    CampaignDocumentViewModel model = new CampaignDocumentViewModel
                    {
                        CampaignCode = viewModel.CampaignCode
                    };
                    return View(model);
                }
                else
                {
                    SetMessage(MessageResource.Global_Warning_ExcelNotSelected, CommonValues.MessageSeverity.Fail);
                }
            }
            return View(viewModel);
        }

        private int SaveAttachments(int photoDocId, IEnumerable<HttpPostedFileBase> attachments)
        {
            if (attachments != null)
            {
                MemoryStream target = new MemoryStream();
                attachments.ElementAt(0).InputStream.CopyTo(target);
                byte[] data = target.ToArray();

                DocumentInfo documentInfo = new DocumentInfo()
                {
                    DocId = photoDocId,
                    DocBinary = data,
                    DocMimeType = attachments.ElementAt(0).ContentType,
                    DocName = attachments.ElementAt(0).FileName,
                    CommandType = CommonValues.DMLType.Insert
                };
                DocumentBL documentBo = new DocumentBL();
                documentBo.DMLDocument(UserManager.UserInfo, documentInfo);
                photoDocId = documentInfo.DocId;
            }
            return photoDocId;
        }

        #endregion

        #region Campaign Document Update

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentIndex, ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentUpdate)]
        [HttpGet]
        public ActionResult CampaignDocumentUpdate(string campaignCode, int documentId, string languageCode)
        {
            SetDefaults();
            var v = new CampaignDocumentViewModel();
            if (documentId > 0)
            {
                var campaignDocumentBo = new CampaignDocumentBL();
                v.CampaignCode = campaignCode;
                v.DocId = documentId;
                v.LanguageCode = languageCode;
                campaignDocumentBo.GetCampaignDocument(UserManager.UserInfo, v);
            }
            return View(v);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentIndex, ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentUpdate)]
        [HttpPost]
        public ActionResult CampaignDocumentUpdate(CampaignDocumentViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            SetDefaults();
            var campaignDocumentBo = new CampaignDocumentBL();
            if (ModelState.IsValid)
            {
                if (attachments != null)
                {
                    viewModel.DocId = SaveAttachments(viewModel.DocId, attachments);
                    viewModel.DocName = attachments.ElementAt(0).FileName;
                }
                viewModel.CommandType = CommonValues.DMLType.Update;
                campaignDocumentBo.DMLCampaignDocument(UserManager.UserInfo, viewModel);
                CheckErrorForMessage(viewModel, true);
            }
            return View(viewModel);
        }

        #endregion

        #region Campaign Document Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentIndex, ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentDelete)]
        public ActionResult DeleteCampaignDocument(string campaignCode, int documentId, string languageCode)
        {
            CampaignDocumentViewModel viewModel = new CampaignDocumentViewModel() { CampaignCode = campaignCode, DocId = documentId, LanguageCode = languageCode };
            var campaignDocumentBo = new CampaignDocumentBL();
            viewModel.CommandType = !string.IsNullOrEmpty(viewModel.CampaignCode) ? CommonValues.DMLType.Delete : string.Empty;
            campaignDocumentBo.DMLCampaignDocument(UserManager.UserInfo, viewModel);

            ModelState.Clear();

            if (viewModel.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, viewModel.ErrorMessage);
        }
        #endregion

        #region Campaign Document Details
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentIndex, ODMSCommon.CommonValues.PermissionCodes.CampaignDocument.CampaignDocumentDetails)]
        [HttpGet]
        public ActionResult CampaignDocumentDetails(string campaignCode, int documentId, string languageCode)
        {
            var v = new CampaignDocumentViewModel();
            var campaignDocumentBo = new CampaignDocumentBL();

            v.CampaignCode = campaignCode;
            v.DocId = documentId;
            v.LanguageCode = languageCode;
            campaignDocumentBo.GetCampaignDocument(UserManager.UserInfo, v);

            return View(v);
        }

        #endregion
        [HttpGet]
        public ActionResult CampaignImage(int id)
        {
            DocumentBL docBl = new DocumentBL();
            DocumentInfo docInfo = docBl.GetDocumentById(id.GetValue<int>()).Model;
            return File(docInfo.DocBinary, docInfo.DocMimeType, docInfo.DocName);
        }
    }
}