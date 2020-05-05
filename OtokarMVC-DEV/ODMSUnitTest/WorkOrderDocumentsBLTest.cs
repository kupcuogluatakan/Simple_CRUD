using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using System.IO;
using ODMSModel.WorkOrderDocuments;


namespace ODMSUnitTest
{

    [TestClass]
    public class WorkOrderDocumentsBLTest
    {

        WorkOrderDocumentsBL _WorkOrderDocumentsBL = new WorkOrderDocumentsBL();

        [TestMethod]
        public void WorkOrderDocumentsBL_DMLWorkOrderDocuments_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkOrderDocumentsViewModel();
            model.IsRequestRoot = true;
            model.WorkOrderDocId = 1;
            model.WorkOrderId = 1;
            model.DocId = 1;
            model.DocName = guid;
            model.DocMimeType = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkOrderDocumentsBL.DMLWorkOrderDocuments(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderDocumentsBL_GetWorkOrderDocument_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkOrderDocumentsViewModel();
            model.IsRequestRoot = true;
            model.WorkOrderDocId = 1;
            model.WorkOrderId = 1;
            model.DocId = 1;
            model.DocName = guid;
            model.DocMimeType = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkOrderDocumentsBL.DMLWorkOrderDocuments(UserManager.UserInfo, model);

            var filter = new WorkOrderDocumentsViewModel();
            filter.WorkOrderDocId = result.Model.WorkOrderDocId;

            var resultGet = _WorkOrderDocumentsBL.GetWorkOrderDocument(filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DocName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderDocumentsBL_ListWorkOrderDocuments_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkOrderDocumentsViewModel();
            model.IsRequestRoot = true;
            model.WorkOrderDocId = 1;
            model.WorkOrderId = 1;
            model.DocId = 1;
            model.DocName = guid;
            model.DocMimeType = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkOrderDocumentsBL.DMLWorkOrderDocuments(UserManager.UserInfo, model);

            int count = 0;
            var filter = new WorkOrderDocumentsListModel();

            var resultGet = _WorkOrderDocumentsBL.ListWorkOrderDocuments(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void WorkOrderDocumentsBL_DMLWorkOrderDocuments_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkOrderDocumentsViewModel();
            model.IsRequestRoot = true;
            model.WorkOrderDocId = 1;
            model.WorkOrderId = 1;
            model.DocId = 1;
            model.DocName = guid;
            model.DocMimeType = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkOrderDocumentsBL.DMLWorkOrderDocuments(UserManager.UserInfo, model);

            var filter = new WorkOrderDocumentsListModel();

            int count = 0;
            var resultGet = _WorkOrderDocumentsBL.ListWorkOrderDocuments(UserManager.UserInfo, filter, out count);

            var modelUpdate = new WorkOrderDocumentsViewModel();
            modelUpdate.WorkOrderDocId = (int)resultGet.Data.First().WorkOrderDocId;
            modelUpdate.DocName = guid;
            modelUpdate.DocMimeType = guid;
            modelUpdate.Description = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _WorkOrderDocumentsBL.DMLWorkOrderDocuments(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderDocumentsBL_DMLWorkOrderDocuments_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkOrderDocumentsViewModel();
            model.IsRequestRoot = true;
            model.WorkOrderDocId = 1;
            model.WorkOrderId = 1;
            model.DocId = 1;
            model.DocName = guid;
            model.DocMimeType = guid;
            model.Description = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkOrderDocumentsBL.DMLWorkOrderDocuments(UserManager.UserInfo, model);

            var filter = new WorkOrderDocumentsListModel();

            int count = 0;
            var resultGet = _WorkOrderDocumentsBL.ListWorkOrderDocuments(UserManager.UserInfo, filter, out count);

            var modelDelete = new WorkOrderDocumentsViewModel();
            modelDelete.WorkOrderDocId = (int)resultGet.Data.First().WorkOrderDocId;
            modelDelete.DocName = guid;
            modelDelete.DocMimeType = guid;
            modelDelete.Description = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _WorkOrderDocumentsBL.DMLWorkOrderDocuments(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

