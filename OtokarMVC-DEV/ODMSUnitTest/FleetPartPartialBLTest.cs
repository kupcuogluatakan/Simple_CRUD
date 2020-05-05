using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.FleetPartPartial;


namespace ODMSUnitTest
{

    [TestClass]
    public class FleetPartPartialBLTest
    {

        FleetPartPartialBL _FleetPartPartialBL = new FleetPartPartialBL();

        [TestMethod]
        public void FleetPartPartialBL_Insert_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetPartViewModel();
            model.FleetId = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCode = "M.162127";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetPartPartialBL.Insert(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void FleetPartPartialBL_Insert_Insert_1()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetPartViewModel();
            model.FleetId = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCode = "M.162127";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetPartPartialBL.Insert(UserManager.UserInfo, model, new System.Collections.Generic.List<FleetPartViewModel>());

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void FleetPartPartialBL_Get_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetPartViewModel();
            model.FleetId = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCode = "M.162127";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetPartPartialBL.Update(UserManager.UserInfo, model);

            var filter = new FleetPartViewModel();
            filter.PartId = 39399;

            var resultGet = _FleetPartPartialBL.Get(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void FleetPartPartialBL_List_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetPartViewModel();
            model.FleetId = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCode = "M.162127";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetPartPartialBL.Update(UserManager.UserInfo, model);

            int count = 0;
            var filter = new FleetPartPartialListModel();
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.PartCode = "M.162127";

            var resultGet = _FleetPartPartialBL.List(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void FleetPartPartialBL_Update_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetPartViewModel();
            model.FleetId = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCode = "M.162127";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetPartPartialBL.Update(UserManager.UserInfo, model);

            var filter = new FleetPartPartialListModel();
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.PartCode = "M.162127";

            int count = 0;
            var resultGet = _FleetPartPartialBL.List(UserManager.UserInfo, filter, out count);

            var modelUpdate = new FleetPartViewModel();
            modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelUpdate.PartCode = "M.162127";
            modelUpdate.CommandType = "U";
            var resultUpdate = _FleetPartPartialBL.Update(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void FleetPartPartialBL_Update_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new FleetPartViewModel();
            model.FleetId = 1;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartCode = "M.162127";
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _FleetPartPartialBL.Update(UserManager.UserInfo, model);

            var filter = new FleetPartPartialListModel();
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.PartCode = "M.162127";

            int count = 0;
            var resultGet = _FleetPartPartialBL.List(UserManager.UserInfo, filter, out count);

            var modelDelete = new FleetPartViewModel();
            modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelDelete.PartCode = "M.162127";
            modelDelete.CommandType = "D";
            var resultDelete = _FleetPartPartialBL.Update(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

    }

}

