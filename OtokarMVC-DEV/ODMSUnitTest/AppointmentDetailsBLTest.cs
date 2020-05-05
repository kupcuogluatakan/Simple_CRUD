using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.AppointmentDetails;
using ODMSModel.Appointment;

namespace ODMSUnitTest
{

    [TestClass]
    public class AppointmentDetailsBLTest
    {

        AppointmentDetailsBL _AppointmentDetailsBL = new AppointmentDetailsBL();
        AppointmentBL _AppointmentBL = new AppointmentBL();

        [TestMethod]
        public void AppointmentDetailsBL_DMLAppointmentDetails_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);

            var model1 = new AppointmentDetailsViewModel();
            model1.AppointmentIndicatorId = 1;
            model1.TotalPrice = 1;
            model1.MainCategoryName = guid;
            model1.CategoryName = guid;
            model1.SubCategoryName = guid;
            model1.VehicleId = 29627;
            model1.IndicatorTypeCode = "IT_C";
            model1.CampaignCode = "508";
            model1.AppointmentId =  586;
            model1.HideElements = true;
            model1.FailureCodeId = guid;
            model1.ProcessTypeId = guid;
            model1.ProposalSeq = 1;
            model1.UpdateUser = 1;
            model1.UpdateDate = DateTime.Now;
            model1.IsActive = true;
            model1.CommandType = "I";
            var result1 = _AppointmentDetailsBL.DMLAppointmentDetails(UserManager.UserInfo, model1);

            Assert.IsTrue(result1.IsSuccess);
        }

        [TestMethod]
        public void AppointmentDetailsBL_GetAppointmentDetails_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentDetailsViewModel();
            model.AppointmentIndicatorId = 1;
            model.TotalPrice = 1;
            model.MainCategoryName = guid;
            model.CategoryName = guid;
            model.SubCategoryName = guid;
            model.VehicleId = 29627;
            model.IndicatorTypeCode = "IT_C";
            model.CampaignCode = "508";
            model.AppointmentId =  586;
            model.HideElements = true;
            model.FailureCodeId = guid;
            model.ProcessTypeId = guid;
            model.ProposalSeq = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentDetailsBL.DMLAppointmentDetails(UserManager.UserInfo, model);

            var resultGet = _AppointmentDetailsBL.GetAppointmentDetails(UserManager.UserInfo, result.Model.AppointmentIndicatorId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CategoryName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentDetailsBL_GetAppointmentIndicType_GetModel()
        {
            var vehicleId = 0;
            var resultGet = _AppointmentDetailsBL.GetAppointmentIndicType(UserManager.UserInfo, 586, out vehicleId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Value != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentDetailsBL_GetCampaignCodeByVehicleId_GetModel()
        {
            var resultGet = _AppointmentDetailsBL.GetCampaignCodeByVehicleId(UserManager.UserInfo, 29627);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Value != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentDetailsBL_GetMaintCoupon_GetModel()
        {
            var resultGet = _AppointmentDetailsBL.GetMaintCoupon(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Value != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentDetailsBL_GetMaintByVehicle_GetModel()
        {
            var resultGet = _AppointmentDetailsBL.GetMaintByVehicle(UserManager.UserInfo, 29627);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Value != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentDetailsBL_ListAppointmentDetails_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentDetailsViewModel();
            model.AppointmentIndicatorId = 1;
            model.TotalPrice = 1;
            model.MainCategoryName = guid;
            model.CategoryName = guid;
            model.SubCategoryName = guid;
            model.VehicleId = 29627;
            model.IndicatorTypeCode = "IT_C";
            model.CampaignCode = "508";
            model.AppointmentId = 586;
            model.HideElements = true;
            model.FailureCodeId = guid;
            model.ProcessTypeId = guid;
            model.ProposalSeq = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentDetailsBL.DMLAppointmentDetails(UserManager.UserInfo, model);

            int count = 0;
            var filter = new AppointmentDetailsListModel();
            filter.FailureCode = guid;

            var resultGet = _AppointmentDetailsBL.ListAppointmentDetails(UserManager.UserInfo,filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentDetailsBL_DMLAppointmentDetails_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentDetailsViewModel();
            model.AppointmentIndicatorId = 1;
            model.TotalPrice = 1;
            model.MainCategoryName = guid;
            model.CategoryName = guid;
            model.SubCategoryName = guid;
            model.VehicleId = 29627;
            model.IndicatorTypeCode = "IT_C";
            model.CampaignCode = "508";
            model.AppointmentId =  586;
            model.HideElements = true;
            model.FailureCodeId = guid;
            model.ProcessTypeId = guid;
            model.ProposalSeq = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentDetailsBL.DMLAppointmentDetails(UserManager.UserInfo, model);

            var filter = new AppointmentDetailsListModel();
            filter.FailureCode = guid;

            int count = 0;
            var resultGet = _AppointmentDetailsBL.ListAppointmentDetails(UserManager.UserInfo, filter, out count);

            var modelUpdate = new AppointmentDetailsViewModel();
            modelUpdate.AppointmentIndicatorId = resultGet.Data.First().AppointmentIndicatorId;

            modelUpdate.MainCategoryName = guid;
            modelUpdate.CategoryName = guid;
            modelUpdate.SubCategoryName = guid;
            modelUpdate.VehicleId = 29627;
            modelUpdate.IndicatorTypeCode = "IT_C";
            modelUpdate.CampaignCode = "508";
            modelUpdate.FailureCodeId = guid;
            modelUpdate.ProcessTypeId = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _AppointmentDetailsBL.DMLAppointmentDetails(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void AppointmentDetailsBL_DMLAppointmentDetails_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentDetailsViewModel();
            model.AppointmentIndicatorId = 1;
            model.TotalPrice = 1;
            model.MainCategoryName = guid;
            model.CategoryName = guid;
            model.SubCategoryName = guid;
            model.VehicleId = 29627;
            model.IndicatorTypeCode = "IT_C";
            model.CampaignCode = "508";
            model.AppointmentId =  586;
            model.HideElements = true;
            model.FailureCodeId = guid;
            model.ProcessTypeId = guid;
            model.ProposalSeq = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentDetailsBL.DMLAppointmentDetails(UserManager.UserInfo, model);

            var filter = new AppointmentDetailsListModel();
            filter.FailureCode = guid;

            int count = 0;
            var resultGet = _AppointmentDetailsBL.ListAppointmentDetails(UserManager.UserInfo, filter, out count);

            var modelDelete = new AppointmentDetailsViewModel();
            modelDelete.AppointmentIndicatorId = resultGet.Data.First().AppointmentIndicatorId;


            modelDelete.MainCategoryName = guid;
            modelDelete.CategoryName = guid;
            modelDelete.SubCategoryName = guid;
            modelDelete.VehicleId = 29627;
            modelDelete.IndicatorTypeCode = "IT_C";
            modelDelete.CampaignCode = "508";
            modelDelete.FailureCodeId = guid;
            modelDelete.ProcessTypeId = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _AppointmentDetailsBL.DMLAppointmentDetails(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

