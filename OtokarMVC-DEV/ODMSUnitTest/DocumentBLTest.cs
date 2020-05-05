using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Common;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class DocumentBLTest
    {

        DocumentBL _DocumentBL = new DocumentBL();

        [TestMethod]
        public void DocumentBL_DMLDocument_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DocumentInfo();
            model.DocId = 1;
            model.DocMimeType = guid;
            model.DocName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DocumentBL.DMLDocument(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void DocumentBL_GetDocumentById_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new DocumentInfo();
            model.DocId = 1;
            model.DocMimeType = guid;
            model.DocName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _DocumentBL.DMLDocument(UserManager.UserInfo, model);

            var resultGet = _DocumentBL.GetDocumentById(result.Model.DocId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DocName != string.Empty && resultGet.IsSuccess);
        }


    }

}

