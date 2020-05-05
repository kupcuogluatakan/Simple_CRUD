using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.Vehicle;
using System.Collections.Generic;
using System.IO;
using ODMSModel.ListModel;


namespace ODMSUnitTest
{

    [TestClass]
    public class VehicleBLTest
    {

        VehicleBL _VehicleBL = new VehicleBL();

        [TestMethod]
        public void VehicleBL_DMLVehicle_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleIndexViewModel();
            model.VehicleId = 29627;
            model.VehicleCode = guid;
            model.VehicleModel = guid;
            model.VehicleType = guid;
            model.CustomerName = guid;
            model.VinNo = guid;
            model.EngineNo = guid;
            model.VatExcludeTypeName = guid;
            model.ContractNo = guid;
            model.Plate = guid;
            model.Color = guid;
            model.SpecialConditions = guid;
            model.Notes = guid;
            model.WarrantyStatusName = guid;
            model.OutOfWarrantyDescription = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.Description = guid;
            model.SSIDPriceList = guid;
            model.IsHourMaint = true;
            model.IsHourMaintName = guid;
            model.Location = guid;
            model.ResponsiblePerson = guid;
            model.ResponsiblePersonPhone = guid;
            model.PlateWillBeUpdated = true;
            model.VinCodeList = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleBL.DMLVehicle(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void VehicleBL_GetVehicle_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleIndexViewModel();
            model.VehicleId = 29627;
            model.VehicleCode = guid;
            model.VehicleModel = guid;
            model.VehicleType = guid;
            model.CustomerName = guid;
            model.VinNo = guid;
            model.EngineNo = guid;
            model.VatExcludeTypeName = guid;
            model.ContractNo = guid;
            model.Plate = guid;
            model.Color = guid;
            model.SpecialConditions = guid;
            model.Notes = guid;
            model.WarrantyStatusName = guid;
            model.OutOfWarrantyDescription = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.Description = guid;
            model.SSIDPriceList = guid;
            model.IsHourMaint = true;
            model.IsHourMaintName = guid;
            model.Location = guid;
            model.ResponsiblePerson = guid;
            model.ResponsiblePersonPhone = guid;
            model.PlateWillBeUpdated = true;
            model.VinCodeList = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleBL.GetVehicle(UserManager.UserInfo, model);

            var filter = new VehicleIndexViewModel();
            filter.VehicleId = 29627;
            filter.VehicleCode = guid;
            filter.VinCodeList = guid;
            var resultGet = _VehicleBL.GetVehicle(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CustomerName != string.Empty && resultGet.IsSuccess);
        }


        [TestMethod]
        public void VehicleBL_GetVehicleContactInfo_GetModel()
        {
            var resultGet = _VehicleBL.GetVehicleContactInfo(29627);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.VehicleId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void VehicleBL_GetLastVehicleKm_GetModel()
        {
            var resultGet = _VehicleBL.GetLastVehicleKm(29627);

            Assert.IsTrue(resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void VehicleBL_ListVehicles_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleIndexViewModel();
            model.VehicleId = 29627;
            model.VehicleCode = guid;
            model.VehicleModel = guid;
            model.VehicleType = guid;
            model.CustomerName = guid;
            model.VinNo = guid;
            model.EngineNo = guid;
            model.VatExcludeTypeName = guid;
            model.ContractNo = guid;
            model.Plate = guid;
            model.Color = guid;
            model.SpecialConditions = guid;
            model.Notes = guid;
            model.WarrantyStatusName = guid;
            model.OutOfWarrantyDescription = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.Description = guid;
            model.SSIDPriceList = guid;
            model.IsHourMaint = true;
            model.IsHourMaintName = guid;
            model.Location = guid;
            model.ResponsiblePerson = guid;
            model.ResponsiblePersonPhone = guid;
            model.PlateWillBeUpdated = true;
            model.VinCodeList = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleBL.DMLVehicle(UserManager.UserInfo, model);

            int count = 0;
            var filter = new VehicleListModel();
            filter.VehicleId = 29627;
            filter.VehicleCode = guid;
            filter.VehicleCodeDesc = guid;
            filter.VinCodeList = guid;
            filter.ModelCode = guid;

            var resultGet = _VehicleBL.ListVehicles(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }


        [TestMethod]
        public void VehicleBL_ListVehicleWorkOrderMaint_GetAll()
        {
            var resultGet = _VehicleBL.ListVehicleWorkOrderMaint("NLRTMR130CA002078", UserManager.UserInfo.LanguageCode);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VehicleBL_ListVehicleHistory_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleIndexViewModel();
            model.VehicleId = 29627;
            model.VehicleCode = guid;
            model.VehicleModel = guid;
            model.VehicleType = guid;
            model.CustomerName = guid;
            model.VinNo = guid;
            model.EngineNo = guid;
            model.VatExcludeTypeName = guid;
            model.ContractNo = guid;
            model.Plate = guid;
            model.Color = guid;
            model.SpecialConditions = guid;
            model.Notes = guid;
            model.WarrantyStatusName = guid;
            model.OutOfWarrantyDescription = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.Description = guid;
            model.SSIDPriceList = guid;
            model.IsHourMaint = true;
            model.IsHourMaintName = guid;
            model.Location = guid;
            model.ResponsiblePerson = guid;
            model.ResponsiblePersonPhone = guid;
            model.PlateWillBeUpdated = true;
            model.VinCodeList = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleBL.DMLVehicle(UserManager.UserInfo, model);

            int count = 0;
            var filter = new VehicleHistoryListModel();
            filter.VehicleId = 29627;
            filter.DealerId = UserManager.UserInfo.DealerID;
            filter.CampaignNameCode = guid;
            filter.AppIndicCode = guid;

            var resultGet = _VehicleBL.ListVehicleHistory(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VehicleBL_ListVehicleHistoryDetails_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleIndexViewModel();
            model.VehicleId = 29627;
            model.VehicleCode = guid;
            model.VehicleModel = guid;
            model.VehicleType = guid;
            model.CustomerName = guid;
            model.VinNo = guid;
            model.EngineNo = guid;
            model.VatExcludeTypeName = guid;
            model.ContractNo = guid;
            model.Plate = guid;
            model.Color = guid;
            model.SpecialConditions = guid;
            model.Notes = guid;
            model.WarrantyStatusName = guid;
            model.OutOfWarrantyDescription = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.Description = guid;
            model.SSIDPriceList = guid;
            model.IsHourMaint = true;
            model.IsHourMaintName = guid;
            model.Location = guid;
            model.ResponsiblePerson = guid;
            model.ResponsiblePersonPhone = guid;
            model.PlateWillBeUpdated = true;
            model.VinCodeList = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleBL.DMLVehicle(UserManager.UserInfo, model);

            int count = 0;
            var filter = new VehicleHistoryDetailListModel();
            filter.LabourId = 211;
            filter.PartId = 39399;
            filter.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            filter.DetailCode = guid;

            var resultGet = _VehicleBL.ListVehicleHistoryDetails(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VehicleBL_ListVehicleCodeAsSelectListItem_GetAll()
        {
            var resultGet = VehicleBL.ListVehicleCodeAsSelectListItem(UserManager.UserInfo, "123");

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VehicleBL_ParseExcel_UpdateNotesForVehicle_Update()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);

            var modelUpdate = new VehicleIndexViewModel();
            modelUpdate.VehicleId = 29627;
            modelUpdate.VehicleCode = guid;
            modelUpdate.VehicleModel = guid;
            modelUpdate.VehicleType = guid;
            modelUpdate.CustomerName = guid;
            modelUpdate.VinNo = guid;
            modelUpdate.EngineNo = guid;
            modelUpdate.VatExcludeTypeName = guid;
            modelUpdate.ContractNo = guid;
            modelUpdate.Plate = guid;
            modelUpdate.Color = guid;
            modelUpdate.SpecialConditions = guid;
            modelUpdate.Notes = guid;
            modelUpdate.WarrantyStatusName = guid;
            modelUpdate.OutOfWarrantyDescription = guid;
            modelUpdate.IsActiveName = guid;
            modelUpdate.Description = guid;
            modelUpdate.SSIDPriceList = guid;
            modelUpdate.IsHourMaintName = guid;
            modelUpdate.Location = guid;
            modelUpdate.ResponsiblePerson = guid;
            modelUpdate.ResponsiblePersonPhone = guid;
            modelUpdate.VinCodeList = guid;
            modelUpdate.CommandType = "U";
            var resultUpdate = _VehicleBL.DMLVehicle(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void VehicleBL_ListVehicleEngineTypesAsSelectListItem_GetAll()
        {
            var resultGet = VehicleBL.ListVehicleEngineTypesAsSelectListItem(UserManager.UserInfo, null);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VehicleBL_ListVehicleHistoryTotalPrice_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleIndexViewModel();
            model.VehicleId = 29627;
            model.VehicleCode = guid;
            model.VehicleModel = guid;
            model.VehicleType = guid;
            model.CustomerName = guid;
            model.VinNo = guid;
            model.EngineNo = guid;
            model.VatExcludeTypeName = guid;
            model.ContractNo = guid;
            model.Plate = guid;
            model.Color = guid;
            model.SpecialConditions = guid;
            model.Notes = guid;
            model.WarrantyStatusName = guid;
            model.OutOfWarrantyDescription = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.Description = guid;
            model.SSIDPriceList = guid;
            model.IsHourMaint = true;
            model.IsHourMaintName = guid;
            model.Location = guid;
            model.ResponsiblePerson = guid;
            model.ResponsiblePersonPhone = guid;
            model.PlateWillBeUpdated = true;
            model.VinCodeList = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleBL.DMLVehicle(UserManager.UserInfo, model);

            int count = 0;
            var filter = new VehicleHistoryTotalPriceListModel();
            filter.VehicleId = 29627;
            filter.CurrencyCode = guid;
            filter.ProcessTypeCode = guid;
            filter.IndicatorTypeCode = "IT_C";

            var resultGet = _VehicleBL.ListVehicleHistoryTotalPrice(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void VehicleBL_ParseExcel_UpdateNotesForVehicle_Delete()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new VehicleIndexViewModel();
            model.VehicleId = 29627;
            model.VehicleCode = guid;
            model.VehicleModel = guid;
            model.VehicleType = guid;
            model.CustomerName = guid;
            model.VinNo = guid;
            model.EngineNo = guid;
            model.VatExcludeTypeName = guid;
            model.ContractNo = guid;
            model.Plate = guid;
            model.Color = guid;
            model.SpecialConditions = guid;
            model.Notes = guid;
            model.WarrantyStatusName = guid;
            model.OutOfWarrantyDescription = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.Description = guid;
            model.SSIDPriceList = guid;
            model.IsHourMaint = true;
            model.IsHourMaintName = guid;
            model.Location = guid;
            model.ResponsiblePerson = guid;
            model.ResponsiblePersonPhone = guid;
            model.PlateWillBeUpdated = true;
            model.VinCodeList = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _VehicleBL.DMLVehicle(UserManager.UserInfo, model);

            var filter = new VehicleHistoryTotalPriceListModel();
            filter.VehicleId = 29627;
            filter.CurrencyCode = guid;
            filter.ProcessTypeCode = guid;
            filter.IndicatorTypeCode = "IT_C";

            int count = 0;
            var resultGet = _VehicleBL.ListVehicleHistoryTotalPrice(UserManager.UserInfo, filter, out count);

            var modelDelete = new VehicleIndexViewModel();
            modelDelete.VehicleId = 29627;
            modelDelete.VehicleCode = guid;
            modelDelete.VehicleModel = guid;
            modelDelete.VehicleType = guid;
            modelDelete.CustomerName = guid;
            modelDelete.VinNo = guid;
            modelDelete.EngineNo = guid;
            modelDelete.VatExcludeTypeName = guid;
            modelDelete.ContractNo = guid;
            modelDelete.Plate = guid;
            modelDelete.Color = guid;
            modelDelete.SpecialConditions = guid;
            modelDelete.Notes = guid;
            modelDelete.WarrantyStatusName = guid;
            modelDelete.OutOfWarrantyDescription = guid;
            modelDelete.IsActiveName = guid;
            modelDelete.Description = guid;
            modelDelete.SSIDPriceList = guid;
            modelDelete.IsHourMaintName = guid;
            modelDelete.Location = guid;
            modelDelete.ResponsiblePerson = guid;
            modelDelete.ResponsiblePersonPhone = guid;
            modelDelete.VinCodeList = guid;
            modelDelete.CommandType = "D";
            var resultDelete = _VehicleBL.DMLVehicle(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

