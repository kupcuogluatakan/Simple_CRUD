using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.AppointmentDetailsLabours;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class AppointmentDetailsLaboursBLTest
    {

        AppointmentDetailsLaboursBL _AppointmentDetailsLaboursBL = new AppointmentDetailsLaboursBL();

        [TestMethod]
        public void AppointmentDetailsLaboursBL_DMLAppointmentDetailLabours_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentDetailsLaboursViewModel();
            model.HideElements = true;
            model.AppointmentIndicatorId = 1;
            model.AppointmentId = 1;
            model.AppointmentIndicatorLabourId = 1;
            model.IndicType = guid;
            model.LabourId = 211;
            model.Duration = 1;
            model.txtLabourId = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentDetailsLaboursBL.DMLAppointmentDetailLabours(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void AppointmentDetailsLaboursBL_GetIndicatorData_GetModel()
        {
            var resultGet = _AppointmentDetailsLaboursBL.GetIndicatorData(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CategoryName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentDetailsLaboursBL_GetAppointmentDetailsLabour_GetModel()
        {
            var resultGet = _AppointmentDetailsLaboursBL.GetAppointmentDetailsLabour(UserManager.UserInfo, 0);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Quantity > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentDetailsLaboursBL_GetAppIndicType_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentDetailsLaboursViewModel();
            model.HideElements = true;
            model.AppointmentIndicatorId = 1;
            model.AppointmentId = 1;
            model.AppointmentIndicatorLabourId = 1;
            model.IndicType = guid;
            model.LabourId = 211;
            model.Duration = 1;
            model.txtLabourId = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentDetailsLaboursBL.DMLAppointmentDetailLabours(UserManager.UserInfo, model);

            var filter = new AppointmentDetailsLaboursViewModel();
            filter.LabourId = 211;

            var resultGet = _AppointmentDetailsLaboursBL.GetAppIndicType(filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.IndicType != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentDetailsLaboursBL_ListAppointmentIndicatorLabours_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentDetailsLaboursViewModel();
            model.HideElements = true;
            model.AppointmentIndicatorId = 1;
            model.AppointmentId = 1;
            model.AppointmentIndicatorLabourId = 1;
            model.IndicType = guid;
            model.LabourId = 211;
            model.Duration = 1;
            model.txtLabourId = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentDetailsLaboursBL.DMLAppointmentDetailLabours(UserManager.UserInfo, model);

            int count = 0;
            var filter = new AppointmentDetailsLaboursListModel();
            filter.LabourCode = guid;

            var resultGet = _AppointmentDetailsLaboursBL.ListAppointmentIndicatorLabours(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentDetailsLaboursBL_DMLAppointmentDetailLabours_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentDetailsLaboursViewModel();
            model.HideElements = true;
            model.AppointmentIndicatorId = 1;
            model.AppointmentId = 1;
            model.AppointmentIndicatorLabourId = 1;
            model.IndicType = guid;
            model.LabourId = 211;
            model.Duration = 1;
            model.txtLabourId = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentDetailsLaboursBL.DMLAppointmentDetailLabours(UserManager.UserInfo, model);

            var filter = new AppointmentDetailsLaboursListModel();
            filter.LabourCode = guid;

            int count = 0;
            var resultGet = _AppointmentDetailsLaboursBL.ListAppointmentIndicatorLabours(UserManager.UserInfo, filter, out count);

            var modelUpdate = new AppointmentDetailsLaboursViewModel();
            modelUpdate.IndicType = guid;
            modelUpdate.LabourId = 211;
            modelUpdate.txtLabourId = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _AppointmentDetailsLaboursBL.DMLAppointmentDetailLabours(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void AppointmentDetailsLaboursBL_DMLAppointmentDetailLabours_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentDetailsLaboursViewModel();
            model.HideElements = true;
            model.AppointmentIndicatorId = 1;
            model.AppointmentId = 1;
            model.AppointmentIndicatorLabourId = 1;
            model.IndicType = guid;
            model.LabourId = 211;
            model.Duration = 1;
            model.txtLabourId = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentDetailsLaboursBL.DMLAppointmentDetailLabours(UserManager.UserInfo, model);

            var filter = new AppointmentDetailsLaboursListModel();
            filter.LabourCode = guid;

            int count = 0;
            var resultGet = _AppointmentDetailsLaboursBL.ListAppointmentIndicatorLabours(UserManager.UserInfo, filter, out count);

            var modelDelete = new AppointmentDetailsLaboursViewModel();
            modelDelete.IndicType = guid;
            modelDelete.LabourId = 211;
            modelDelete.txtLabourId = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _AppointmentDetailsLaboursBL.DMLAppointmentDetailLabours(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

