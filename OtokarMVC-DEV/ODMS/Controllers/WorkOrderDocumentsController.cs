using System.Web.Mvc;
using Kendo.Mvc.UI;
using ODMS.Filters;
using ODMSBusiness;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.WorkOrderDocuments;

namespace ODMS.Controllers
{
    [PreventDirectFilter]
    public class WorkOrderDocumentsController : ControllerBase
    {
        //Index
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.WorkOrderDocuments.WorkOrderDocumentIndex)]
        public ActionResult WorkOrderDocumentsIndex(int id)
        {
            WorkOrderDocumentsViewModel model = new WorkOrderDocumentsViewModel();

            if (id > 0)
            {
                model.WorkOrderId = id;
                model.IsRequestRoot = true;
            }
            return PartialView(model);
        }

        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.WorkOrderDocuments.WorkOrderDocumentIndex)]
        public JsonResult ListWorkOrderDocuments([DataSourceRequest]DataSourceRequest request, WorkOrderDocumentsListModel model)
        {
            WorkOrderDocumentsBL woDocBL = new WorkOrderDocumentsBL();
            WorkOrderDocumentsListModel docModel = new WorkOrderDocumentsListModel(request);
            int totalCount = 0;
            docModel.WorkOrderId = model.WorkOrderId;

            var rValue = woDocBL.ListWorkOrderDocuments(UserManager.UserInfo, docModel, out totalCount).Data;

            return Json(new
            {
                Data = rValue,
                Total = totalCount
            });
        }

        //Create
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.WorkOrderDocuments.WorkOrderDocumentCreate)]
        public ActionResult WorkOrderDocumentsCreate(int wOrderId)
        {
            WorkOrderDocumentsViewModel model = new WorkOrderDocumentsViewModel();
            if (wOrderId > 0)
            {
                model.WorkOrderId = wOrderId;
            }
            return View(model);
        }

        [HttpPost]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.WorkOrderDocuments.WorkOrderDocumentCreate)]
        [ValidateAntiForgeryToken]
        public ActionResult WorkOrderDocumentsCreate(WorkOrderDocumentsViewModel model)
        {
            if (model.WorkOrderId > 0 && model.file != null)
            {

                WorkOrderDocumentsBL woDocBL = new WorkOrderDocumentsBL();
                model.CommandType = ODMSCommon.CommonValues.DMLType.Insert;
                model.DocName = model.file.FileName;
                model.DocMimeType = model.file.ContentType;

                woDocBL.DMLWorkOrderDocuments(UserManager.UserInfo, model.file.InputStream, model);

                CheckErrorForMessage(model, true);
            }
            else
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Global_Display_FileValid;
                CheckErrorForMessage(model, true);
            }

            ModelState.Clear();
            return View();
        }


        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.WorkOrderDocuments.WorkOrderDocumentDelete)]
        public ActionResult WorkOrderDocumentsDelete(int woDocId)
        {
            WorkOrderDocumentsBL woDocBL = new WorkOrderDocumentsBL();
            WorkOrderDocumentsViewModel model = new WorkOrderDocumentsViewModel
                {
                    WorkOrderDocId = woDocId,
                    CommandType = ODMSCommon.CommonValues.DMLType.Delete
                };

            woDocBL.DMLWorkOrderDocuments(UserManager.UserInfo, model);

            if (model.ErrorNo == 0)
                return GenerateAsyncOperationResponse(AsynOperationStatus.Success,
                    MessageResource.Global_Display_Success);
            return GenerateAsyncOperationResponse(AsynOperationStatus.Error,
                model.ErrorMessage);
        }


        //Download
        [HttpGet]
        [AuthorizationFilter(ODMSCommon.CommonValues.PermissionCodes.WorkOrderDocuments.WorkOrderDocumentDownload)]
        public ActionResult WorkOrderDocumentsDownload(int id)
        {
            WorkOrderDocumentsBL woDocBL = new WorkOrderDocumentsBL();
            WorkOrderDocumentsViewModel model = new WorkOrderDocumentsViewModel {DocId = id};

            woDocBL.GetWorkOrderDocument(model);

            return File(model.DocImage,model.DocMimeType, model.DocName);
        }

    }
}
