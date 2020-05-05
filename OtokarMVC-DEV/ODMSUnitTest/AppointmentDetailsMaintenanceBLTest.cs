using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.AppointmentDetailsMaintenance;
using System;


namespace ODMSUnitTest
{

    [TestClass]
    public class AppointmentDetailsMaintenanceBLTest
    {

        AppointmentDetailsMaintenanceBL _AppointmentDetailsMaintenanceBL = new AppointmentDetailsMaintenanceBL();

        [TestMethod]
        public void AppointmentDetailsMaintenanceBL_DMLAppIndMainPartsLabours_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentDetailsMaintenanceViewModel();
            model.AppIndicId = 1;
            model.AppId = 1;
            model.Name = guid;
            model.IsRemoved = true;
            model.Quantity = 1;
            model.LabourPartId = 1;
            model.Type = guid;
            model.ObjType = 1;
            model.IsMust = true;
            model.CategoryString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentDetailsMaintenanceBL.DMLAppIndMainPartsLabours(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void AppointmentDetailsMaintenanceBL_DMLAppoinmentIndicatorMaint_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentDetailsMaintenanceViewModel();
            model.AppIndicId = 1;
            model.AppId = 1;
            model.Name = guid;
            model.IsRemoved = true;
            model.Quantity = 1;
            model.LabourPartId = 1;
            model.Type = guid;
            model.ObjType = 1;
            model.IsMust = true;
            model.CategoryString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentDetailsMaintenanceBL.DMLAppoinmentIndicatorMaint(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        /// <summary>
        /// TODO: Buna bakalım
        /// </summary>
        [TestMethod]
        public void AppointmentDetailsMaintenanceBL_GetSparePart_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentDetailsMaintenanceViewModel();
            model.AppIndicId = 1;
            model.AppId = 1;
            model.Name = guid;
            model.IsRemoved = true;
            model.Quantity = 1;
            model.LabourPartId = 1;
            model.Type = guid;
            model.ObjType = 1;
            model.IsMust = true;
            model.CategoryString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentDetailsMaintenanceBL.DMLAppoinmentIndicatorMaint(UserManager.UserInfo, model);

            var filter = new ChangePartViewModel();
            filter.AppIndPartId = result.Model.AppIndicId;
            filter.PartId = 39399;

            var resultGet = _AppointmentDetailsMaintenanceBL.GetSparePart(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartName != string.Empty && resultGet.IsSuccess);
        }

        /// <summary>
        /// TODO: Buna bakalım
        /// </summary>
        [TestMethod]
        public void AppointmentDetailsMaintenanceBL_GetMaintIdByAppIndicId_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentDetailsMaintenanceViewModel();
            model.AppIndicId = 1;
            model.AppId = 1;
            model.Name = guid;
            model.IsRemoved = true;
            model.Quantity = 1;
            model.LabourPartId = 1;
            model.Type = guid;
            model.ObjType = 1;
            model.IsMust = true;
            model.CategoryString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentDetailsMaintenanceBL.DMLAppoinmentIndicatorMaint(UserManager.UserInfo, model);

            var filter = new AppointmentDetailsMaintenanceViewModel();
            filter.AppIndicId = result.Model.MaintId ?? 0;

            var resultGet = _AppointmentDetailsMaintenanceBL.GetMaintIdByAppIndicId(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.MaintId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void AppointmentDetailsMaintenanceBL_GetAppDetailMaintList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AppointmentDetailsMaintenanceViewModel();
            model.AppIndicId = 1;
            model.AppId = 1;
            model.Name = guid;
            model.IsRemoved = true;
            model.Quantity = 1;
            model.LabourPartId = 1;
            model.Type = guid;
            model.ObjType = 1;
            model.IsMust = true;
            model.CategoryString = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _AppointmentDetailsMaintenanceBL.DMLAppoinmentIndicatorMaint(UserManager.UserInfo, model);

            int count = 0;
            var filter = new AppointmentDetailsMaintenanceListModel();

            var resultGet = _AppointmentDetailsMaintenanceBL.GetAppDetailMaintList(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void AppointmentDetailsMaintenanceBL_ListAppMaintenanceAsSelectItem_GetAll()
        {
            var resultGet = AppointmentDetailsMaintenanceBL.ListAppMaintenanceAsSelectItem(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Total > 0);
        }

    }

}

