using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSModel.CampaignVehicle;


namespace ODMSUnitTest
{

    [TestClass]
    public class CampaignVehicleBLTest
    {

        CampaignVehicleBL _CampaignVehicleBL = new CampaignVehicleBL();

        [TestMethod]
        public void CampaignVehicleBL_DMLCampaignVehicle_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignVehicleViewModel();
            model.CampaignCode = "508";
            model.VehicleId = 29627;
            model.VinNo = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IsUtilized = true;
            model.IsUtilizedName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CampaignVehicleBL.DMLCampaignVehicle(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void CampaignVehicleBL_GetCampaignVehicle_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignVehicleViewModel();
            model.CampaignCode = "508";
            model.VehicleId = 29627;
            model.VinNo = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IsUtilized = true;
            model.IsUtilizedName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CampaignVehicleBL.DMLCampaignVehicle(UserManager.UserInfo, model);

            var filter = new CampaignVehicleViewModel();
            filter.CampaignCode = "508";
            filter.VehicleId = 29627;

            var resultGet = _CampaignVehicleBL.GetCampaignVehicle(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.VinNo != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void CampaignVehicleBL_ListCampaignVehicles_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignVehicleViewModel();
            model.CampaignCode = "508";
            model.VehicleId = 29627;
            model.VinNo = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IsUtilized = true;
            model.IsUtilizedName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CampaignVehicleBL.DMLCampaignVehicle(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CampaignVehicleListModel();
            filter.CampaignCode = "508";
            filter.VehicleId = 29627;

            var resultGet = _CampaignVehicleBL.ListCampaignVehicles(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void CampaignVehicleBL_ListCampaignVehiclesForDealer_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new CampaignVehicleViewModel();
            model.CampaignCode = "508";
            model.VehicleId = 29627;
            model.VinNo = guid;
            model.IsActive = true;
            model.IsActiveName = guid;
            model.IsUtilized = true;
            model.IsUtilizedName = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.CommandType = "I";
            var result = _CampaignVehicleBL.DMLCampaignVehicle(UserManager.UserInfo, model);

            int count = 0;
            var filter = new CampaignVehicleListModel();
            filter.CampaignCode = "508";
            filter.VehicleId = 29627;

            var resultGet = _CampaignVehicleBL.ListCampaignVehiclesForDealer(UserManager.UserInfo, filter, out count);

            Assert.IsTrue(resultGet.Total > 0);
        }

       

    }

}

