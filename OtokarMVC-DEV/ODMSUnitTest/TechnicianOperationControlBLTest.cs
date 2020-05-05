using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.TechnicianOperationControl;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class TechnicianOperationControlBLTest
    {

        TechnicianOperationControlBL _TechnicianOperationControlBL = new TechnicianOperationControlBL();

        [TestMethod]
        public void TechnicianOperationControlBL_Insert_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new TechnicianOperationViewModel();
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _TechnicianOperationControlBL.Insert(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void TechnicianOperationControlBL_Get_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new TechnicianOperationViewModel();
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _TechnicianOperationControlBL.Insert(UserManager.UserInfo, model);

            var filter = new TechnicianOperationViewModel();
            var resultGet = _TechnicianOperationControlBL.Get(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.IsSuccess);
        }

        [TestMethod]
        public void TechnicianOperationControlBL_List_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new TechnicianOperationViewModel();
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _TechnicianOperationControlBL.Insert(UserManager.UserInfo, model);

            int count = 0;
            var filter = new TechnicianOperationListModel();
            var resultGet = _TechnicianOperationControlBL.List(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void TechnicianOperationControlBL_Update_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new TechnicianOperationViewModel();
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _TechnicianOperationControlBL.Update(UserManager.UserInfo, model);

            var filter = new TechnicianOperationListModel();

            int count = 0;
            var resultGet = _TechnicianOperationControlBL.List(UserManager.UserInfo, filter, out count);

            var modelUpdate = new TechnicianOperationViewModel();
            modelUpdate.CommandType = "U";
            var resultUpdate = _TechnicianOperationControlBL.Update(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }


    }

}

