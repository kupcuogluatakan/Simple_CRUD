using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.WorkOrder;
using ODMSModel.VehicleBodywork;


namespace ODMSUnitTest
{

    [TestClass]
    public class WorkOrderBLTest
    {

        WorkOrderBL _WorkOrderBL = new WorkOrderBL();

        [TestMethod]
        public void WorkOrderBL_DMLWorkOrder_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkOrderViewModel();
            model.WorkOrderId = 1;
            model.CustomerId = 1;
            model.CustomerName = guid;
            model.CustomerSurName = guid;
            model.CustomerPhone = guid;
            model.VehiclePlate = guid;
            model.ModelKod = "ATLAS";
            model.ModelName = guid;
            model.VehicleTypeId = 1;
            model.VehicleTypeName = guid;
            model.VehicleId = 29627;
            model.AppointmentTypeId = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Stuff = guid;
            model.Note = guid;
            model.WorkOrderDate = DateTime.Now;
            model.WorkOrderStatusId = 1;
            model.VehicleKM = 1;
            model.VinNo = guid;
            model.DeliveryTime = DateTime.Now;
            model.VehicleModel = guid;
            model.AppointmentType = guid;
            model.FleetId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkOrderBL.DMLWorkOrder(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_DMLBodyWorkForWorkOrder_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleBodyworkViewModel();
            model.VehicleBodyworkId = 1;
            model.BodyworkCode = guid;
            model.BodyworkName = guid;
            model.VehicleId = 29627;
            model.VehiclePlate = guid;
            model.CountryId = 1;
            model.CountryName = guid;
            model.CityId = 1;
            model.CityName = guid;
            model.WorkOrderName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Manufacturer = guid;
            model.VehicleVinNo = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkOrderBL.DMLBodyWorkForWorkOrder(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_GetWorkOrder_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkOrderViewModel();
            model.WorkOrderId = 1;
            model.CustomerId = 1;
            model.CustomerName = guid;
            model.CustomerSurName = guid;
            model.CustomerPhone = guid;
            model.VehiclePlate = guid;
            model.ModelKod = "ATLAS";
            model.ModelName = guid;
            model.VehicleTypeId = 1;
            model.VehicleTypeName = guid;
            model.VehicleId = 29627;
            model.AppointmentTypeId = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Stuff = guid;
            model.Note = guid;
            model.WorkOrderDate = DateTime.Now;
            model.WorkOrderStatusId = 1;
            model.VehicleKM = 1;
            model.VinNo = guid;
            model.DeliveryTime = DateTime.Now;
            model.VehicleModel = guid;
            model.AppointmentType = guid;
            model.FleetId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkOrderBL.DMLWorkOrder(UserManager.UserInfo, model);


            var resultGet = _WorkOrderBL.GetWorkOrder(UserManager.UserInfo, result.Model.WorkOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.VinNo != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_GetWorkOrderData_GetModel()
        {
            var resultGet = _WorkOrderBL.GetWorkOrderData(1, "CUSTOMER");

            Assert.IsTrue(resultGet.Model != null && resultGet.Model != null && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_GetLastWorkOrderId_GetModel()
        {
            var resultGet = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_GetLastWorkOrderDetailId_GetModel()
        {

            var resultGet1 = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID);
            var resultGet = _WorkOrderBL.GetLastWorkOrderDetailId(resultGet1.Model.Value);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_GetLastWorkOrderPickingId_GetModel()
        {
            var resultGet = _WorkOrderBL.GetLastWorkOrderPickingId(UserManager.UserInfo.DealerID);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_GetLastWorkOrderPickingDetailId_GetModel()
        {
            var resultGet1 = _WorkOrderBL.GetLastWorkOrderPickingId(UserManager.UserInfo.DealerID);
            var resultGet = _WorkOrderBL.GetLastWorkOrderPickingDetailId(resultGet1.Model.Value);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_GetDealerUsers_GetModel()
        {
            var resultGet = _WorkOrderBL.GetDealerUsers(UserManager.UserInfo.DealerID);

            Assert.IsTrue(resultGet.Model != null && resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_GetWorkOrderViewModel_GetModel()
        {
            var lastWorkOrder = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID);
            var resultGet = _WorkOrderBL.GetWorkOrderViewModel(UserManager.UserInfo, lastWorkOrder.Model.Value);
            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_GetBodyworkFromVehicle_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleBodyworkViewModel();
            model.VehicleBodyworkId = 1;
            model.BodyworkCode = guid;
            model.BodyworkName = guid;
            model.VehicleId = 29627;
            model.VehiclePlate = guid;
            model.CountryId = 1;
            model.CountryName = guid;
            model.CityId = 1;
            model.CityName = guid;
            model.WorkOrderName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Manufacturer = guid;
            model.VehicleVinNo = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkOrderBL.DMLBodyWorkForWorkOrder(UserManager.UserInfo, model);

            var filter = new VehicleBodyworkViewModel();
            filter.BodyworkCode = guid;
            filter.VehicleId = 29627;
            filter.CountryId = 1;
            filter.CityId = 1;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _WorkOrderBL.GetBodyworkFromVehicle(filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CountryId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_GetVehicleCustomerId_GetModel()
        {
            var resultGet = _WorkOrderBL.GetVehicleCustomerId(29627);

            Assert.IsTrue(resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_GetCustomerChangeData_GetModel()
        {
            var resultGet = _WorkOrderBL.GetCustomerChangeData(21070, 21070);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CustomerName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_GetPeriodicMaintHistory_GetModel()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID);
            var resultGet = _WorkOrderBL.GetPeriodicMaintHistory(workOrderId.Model.Value);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.AdminDesc != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_ListWorkOrderAsSelectListItem_GetAll()
        {
            var resultGet = WorkOrderBL.ListWorkOrderAsSelectListItem(29627, UserManager.UserInfo.GetUserDealerId());

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void WorkOrderBL_ListWorkOrders_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleBodyworkViewModel();
            model.VehicleBodyworkId = 1;
            model.BodyworkCode = guid;
            model.BodyworkName = guid;
            model.VehicleId = 29627;
            model.VehiclePlate = guid;
            model.CountryId = 1;
            model.CountryName = guid;
            model.CityId = 1;
            model.CityName = guid;
            model.WorkOrderName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Manufacturer = guid;
            model.VehicleVinNo = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkOrderBL.DMLBodyWorkForWorkOrder(UserManager.UserInfo, model);

            int count = 0;
            var filter = new WorkOrderListModel();
            filter.VehicleId = 29627;
            filter.VehicleCode = guid;
            filter.DealerId = UserManager.UserInfo.DealerID;

            var resultGet = _WorkOrderBL.ListWorkOrders(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void WorkOrderBL_DMLBodyWorkForWorkOrder_Update()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleBodyworkViewModel();
            model.VehicleBodyworkId = 1;
            model.BodyworkCode = guid;
            model.BodyworkName = guid;
            model.VehicleId = 29627;
            model.VehiclePlate = guid;
            model.CountryId = 1;
            model.CountryName = guid;
            model.CityId = 1;
            model.CityName = guid;
            model.WorkOrderName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Manufacturer = guid;
            model.VehicleVinNo = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkOrderBL.DMLBodyWorkForWorkOrder(UserManager.UserInfo, model);

            var filter = new WorkOrderListModel();
            filter.VehicleId = 29627;
            filter.VehicleCode = guid;
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _WorkOrderBL.ListWorkOrders(UserManager.UserInfo, filter, out count);

            var modelUpdate = new VehicleBodyworkViewModel();
            modelUpdate.WorkOrderId = resultGet.Data.First().WorkOrderId;
            modelUpdate.BodyworkCode = guid;
            modelUpdate.BodyworkName = guid;
            modelUpdate.VehicleId = 29627;
            modelUpdate.VehiclePlate = guid;
            modelUpdate.CountryId = 1;
            modelUpdate.CountryName = guid;
            modelUpdate.CityId = 1;
            modelUpdate.CityName = guid;
            modelUpdate.WorkOrderName = guid;
            modelUpdate.DealerId = UserManager.UserInfo.DealerID;
            modelUpdate.DealerName = guid;
            modelUpdate.Manufacturer = guid;
            modelUpdate.VehicleVinNo = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _WorkOrderBL.DMLBodyWorkForWorkOrder(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_DMLBodyWorkForWorkOrder_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleBodyworkViewModel();
            model.VehicleBodyworkId = 1;
            model.BodyworkCode = guid;
            model.BodyworkName = guid;
            model.VehicleId = 29627;
            model.VehiclePlate = guid;
            model.CountryId = 1;
            model.CountryName = guid;
            model.CityId = 1;
            model.CityName = guid;
            model.WorkOrderName = guid;
            model.DealerId = UserManager.UserInfo.DealerID;
            model.DealerName = guid;
            model.Manufacturer = guid;
            model.VehicleVinNo = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkOrderBL.DMLBodyWorkForWorkOrder(UserManager.UserInfo, model);

            var filter = new WorkOrderListModel();
            filter.VehicleId = 29627;
            filter.VehicleCode = guid;
            filter.DealerId = UserManager.UserInfo.DealerID;

            int count = 0;
            var resultGet = _WorkOrderBL.ListWorkOrders(UserManager.UserInfo, filter, out count);

            var modelDelete = new VehicleBodyworkViewModel();
            modelDelete.WorkOrderId = resultGet.Data.First().WorkOrderId;
            modelDelete.BodyworkCode = guid;
            modelDelete.BodyworkName = guid;
            modelDelete.VehicleId = 29627;
            modelDelete.VehiclePlate = guid;
            modelDelete.CountryId = 1;
            modelDelete.CountryName = guid;
            modelDelete.CityId = 1;
            modelDelete.CityName = guid;
            modelDelete.WorkOrderName = guid;
            modelDelete.DealerId = UserManager.UserInfo.DealerID;
            modelDelete.DealerName = guid;
            modelDelete.Manufacturer = guid;
            modelDelete.VehicleVinNo = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _WorkOrderBL.DMLBodyWorkForWorkOrder(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderBL_ListAppointmentTypes_GetAll()
        {
            var resultGet = _WorkOrderBL.ListAppointmentTypes(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

      
        [TestMethod]
        public void WorkOrderBL_ListWorkOrderStatus_GetAll()
        {
            var resultGet = WorkOrderBL.ListWorkOrderStatus(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }
        

    }

}

