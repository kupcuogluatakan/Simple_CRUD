using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.GuaranteeAuthorityGroupDealers;


namespace ODMSUnitTest
{

    [TestClass]
    public class GuaranteeAuthorityGroupDealersBLTest
    {

        GuaranteeAuthorityGroupDealersBL _GuaranteeAuthorityGroupDealersBL = new GuaranteeAuthorityGroupDealersBL();

        [TestMethod]
        public void GuaranteeAuthorityGroupDealersBL_SaveGuaranteeAuthorityGroupDealers_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeAuthorityGroupDealersSaveModel();
            model.id = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeAuthorityGroupDealersBL.SaveGuaranteeAuthorityGroupDealers(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeAuthorityGroupDealersBL_ListGuaranteeAuthorityGroupDealers_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeAuthorityGroupDealersSaveModel();
            model.id = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeAuthorityGroupDealersBL.SaveGuaranteeAuthorityGroupDealers(UserManager.UserInfo, model);

            var filter = new GuaranteeAuthorityGroupDealersListModel();
            var resultGet = _GuaranteeAuthorityGroupDealersBL.ListGuaranteeAuthorityGroupDealers(filter);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void GuaranteeAuthorityGroupDealersBL_SaveGuaranteeAuthorityGroupDealers_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeAuthorityGroupDealersSaveModel();
            model.id = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeAuthorityGroupDealersBL.SaveGuaranteeAuthorityGroupDealers(UserManager.UserInfo, model);

            var filter = new GuaranteeAuthorityGroupDealersListModel();

            var resultGet = _GuaranteeAuthorityGroupDealersBL.ListGuaranteeAuthorityGroupDealers(filter);

            var modelUpdate = new GuaranteeAuthorityGroupDealersSaveModel();
            modelUpdate.id = resultGet.Data.First().IdGroup ?? 0;
            modelUpdate.CommandType = "U";
            var resultUpdate = _GuaranteeAuthorityGroupDealersBL.SaveGuaranteeAuthorityGroupDealers(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeAuthorityGroupDealersBL_SaveGuaranteeAuthorityGroupDealers_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeAuthorityGroupDealersSaveModel();
            model.id = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeAuthorityGroupDealersBL.SaveGuaranteeAuthorityGroupDealers(UserManager.UserInfo, model);

            var filter = new GuaranteeAuthorityGroupDealersListModel();

            var resultGet = _GuaranteeAuthorityGroupDealersBL.ListGuaranteeAuthorityGroupDealers(filter);

            var modelDelete = new GuaranteeAuthorityGroupDealersSaveModel();
            modelDelete.id = resultGet.Data.First().IdGroup ?? 0;
            modelDelete.CommandType = "D";
            var resultDelete = _GuaranteeAuthorityGroupDealersBL.SaveGuaranteeAuthorityGroupDealers(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void GuaranteeAuthorityGroupDealersBL_ListGuaranteeAuthorityGroupDealersNotInclude_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GuaranteeAuthorityGroupDealersSaveModel();
            model.id = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _GuaranteeAuthorityGroupDealersBL.SaveGuaranteeAuthorityGroupDealers(UserManager.UserInfo, model);

            var filter = new GuaranteeAuthorityGroupDealersListModel();
            var resultGet = _GuaranteeAuthorityGroupDealersBL.ListGuaranteeAuthorityGroupDealersNotInclude(filter);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}

