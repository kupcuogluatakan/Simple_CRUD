using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSModel.ProposalDocuments;
using ODMSBusiness.Business;
using ODMSCommon.Security;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class ProposalDocumentsController : ControllerBase
    {
        //Index
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ProposalDocuments.ProposalDocumentIndex)]
        public ActionResult ProposalDocumentsIndex(int id, int seq)
        {
            ProposalDocumentsViewModel model = new ProposalDocumentsViewModel();

            if (id > 0)
            {
                model.ProposalId = id;
                model.ProposalSeq = seq;
                model.IsRequestRoot = true;
            }
            return PartialView(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ProposalDocuments.ProposalDocumentIndex)]
        public JsonResult ListProposalDocuments([DataSourceRequest]DataSourceRequest request, ProposalDocumentsListModel model)
        {
            ProposalDocumentsBL woDocBL = new ProposalDocumentsBL();
            ProposalDocumentsListModel docModel = new ProposalDocumentsListModel(request);
            int totalCount = 0;
            docModel.ProposalId = model.ProposalId;
            docModel.ProposalSeq = model.ProposalSeq;

            var rValue = woDocBL.ListProposalDocuments(UserManager.UserInfo,docModel, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        //Create
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ProposalDocuments.ProposalDocumentCreate)]
        public ActionResult ProposalDocumentsCreate(int wOrderId, int seq)
        {
            ProposalDocumentsViewModel model = new ProposalDocumentsViewModel();
            if (wOrderId > 0)
            {
                model.ProposalId = wOrderId;
                model.ProposalSeq = seq;
            }
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ProposalDocuments.ProposalDocumentCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult ProposalDocumentsCreate(ProposalDocumentsViewModel model)
        {
            if (model.file == null)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Global_Display_FileValid;
                CheckErrorForMessage(model, true);
                return View();
            }

            if (string.IsNullOrEmpty(model.Description))
            {

                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Proposal_Warning_Description;
                CheckErrorForMessage(model, true);
                return View();
            }

            if (model.ProposalId > 0)
            {

                ProposalDocumentsBL woDocBL = new ProposalDocumentsBL();
                model.CommandType = ODMSCommon.CommonValues.DMLType.Insert;
                model.DocName = model.file.FileName;
                model.DocMimeType = model.file.ContentType;

                woDocBL.DMLProposalDocuments(UserManager.UserInfo, model.file.InputStream, model);

                CheckErrorForMessage(model, true);
            }

            ModelState.Clear();
            return View();
        }


        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ProposalDocuments.ProposalDocumentDelete)]
        public ActionResult ProposalDocumentsDelete(int woDocId)
        {
            ProposalDocumentsBL woDocBL = new ProposalDocumentsBL();
            ProposalDocumentsViewModel model = new ProposalDocumentsViewModel
            {
                ProposalDocId = woDocId,
                CommandType = ODMSCommon.CommonValues.DMLType.Delete
            };

            woDocBL.DMLProposalDocuments(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success, MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error, model.ErrorMessage);
        }


        //Download
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.ProposalDocuments.ProposalDocumentDownload)]
        public ActionResult ProposalDocumentsDownload(int id)
        {
            ProposalDocumentsBL woDocBL = new ProposalDocumentsBL();
            ProposalDocumentsViewModel model = new ProposalDocumentsViewModel { DocId = id };

            woDocBL.GetProposalDocument(model);

            return File(model.DocImage, model.DocMimeType, model.DocName);
        }
    }
}