using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.LabourTechnician;


namespace ODMSUnitTest
{

    [TestClass]
    public class LabourTechnicianBLTest
    {

        LabourTechnicianBL _LabourTechnicianBL = new LabourTechnicianBL();

        [TestMethod]
        public void LabourTechnicianBL_DMLLabourTechnician_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourTechnicianViewModel();
            model.LabourTechnicianId = 1;
            model.WorkOrderDetailId = 1;
            model.LabourId = 211;
            model.LabourCode = guid;
            model.LabourName = guid;
            model._UserID = guid;
            model.UserNameSurname = guid;
            model.StatusName = guid;
            model.VinNo = guid;
            model.Plate = guid;
            model.CustomerName = guid;
            model.WorkshopPlanTypeId = 1;
            model.IsDealerDuration = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourTechnicianBL.DMLLabourTechnician(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void LabourTechnicianBL_DMLLabourTechnicianStartFinish_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourTechnicianViewModel();
            model.LabourTechnicianId = 1;
            model.WorkOrderDetailId = 1;
            model.LabourId = 211;
            model.LabourCode = guid;
            model.LabourName = guid;
            model._UserID = guid;
            model.UserNameSurname = guid;
            model.StatusName = guid;
            model.VinNo = guid;
            model.Plate = guid;
            model.CustomerName = guid;
            model.WorkshopPlanTypeId = 1;
            model.IsDealerDuration = true;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourTechnicianBL.DMLLabourTechnicianStartFinish(model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void LabourTechnicianBL_DMLLabourTechnicianUser_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourTecnicianUserModel();
            model.TechnicianId = 1;
            model.TechnicianUserId = 1;
            model.TechnicianUserName = guid;
            model.WorkTime = 1;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourTechnicianBL.DMLLabourTechnicianUser(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void LabourTechnicianBL_GetLabourTechnician_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourTecnicianUserModel();
            model.TechnicianId = 1;
            model.TechnicianUserId = 1;
            model.TechnicianUserName = guid;
            model.WorkTime = 1;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourTechnicianBL.DMLLabourTechnicianUser(UserManager.UserInfo, model);

            var filter = new LabourTechnicianViewModel();
            filter.LabourTechnicianId = result.Model.TechnicianId;
            filter.LabourId = 211;
            filter.LabourCode = guid;

            var resultGet = _LabourTechnicianBL.GetLabourTechnician(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.LabourName != String.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void LabourTechnicianBL_GetLabourTecnicianInfo_GetModel()
        {
            var resultGet = _LabourTechnicianBL.GetLabourTecnicianInfo(1);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.TechnicianUserName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void LabourTechnicianBL_ListLabourTechnicians_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new LabourTecnicianUserModel();
            model.TechnicianId = 1;
            model.TechnicianUserId = 1;
            model.TechnicianUserName = guid;
            model.WorkTime = 1;
            model.StatusName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _LabourTechnicianBL.DMLLabourTechnicianUser(UserManager.UserInfo, model);

            int count = 0;
            var filter = new LabourTechnicianListModel();
            filter.LabourCode = guid;
            filter.LabourId = 211;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _LabourTechnicianBL.ListLabourTechnicians(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void LabourTechnicianBL_ListTechnicianAsSelectList_GetAll()
        {
            var resultGet = LabourTechnicianBL.ListTechnicianAsSelectList(UserManager.UserInfo.GetUserDealerId());

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

