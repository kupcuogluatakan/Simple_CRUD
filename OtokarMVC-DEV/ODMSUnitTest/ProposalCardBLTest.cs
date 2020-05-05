using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using ODMSModel.ProposalCard;
using System;
using ODMSBusiness.Business;


namespace ODMSUnitTest
{

    [TestClass]
    public class ProposalCardBLTest
    {

        ProposalCardBL _ProposalCardBL = new ProposalCardBL();

        [TestMethod]
        public void ProposalCardBL_UpdatePdiPackage_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ProposalAddPdiPackageModel();
            model.PdiCheckNote = guid;
            model.TransmissionSerialNo = guid;
            model.DifferencialSerialNo = guid;
            model.ApprovalNote = guid;
            model.ProposalId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ProposalCardBL.UpdatePdiPackage(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_SavePdiResult_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ProposalPdiResultModel();
            model.ProposalId = 1;
            model.ControlCode = guid;
            model.PartCode = "M.162127";
            model.BreakDownCode = guid;
            model.ResultCode = guid;
            var result = _ProposalCardBL.SavePdiResult(UserManager.UserInfo, model, "C");

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_SaveGeneralInfo_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GeneralInfo();
            model.ProposalId = 1;
            model.ProposalSeq = 1;
            model.Matter1 = guid;
            model.Matter2 = guid;
            model.Matter3 = guid;
            var result = _ProposalCardBL.SaveGeneralInfo(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_SaveTechnicalInfo_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new GeneralInfo();
            model.ProposalId = 1;
            model.ProposalSeq = 1;
            model.Matter1 = guid;
            model.Matter2 = guid;
            model.Matter3 = guid;
            var result = _ProposalCardBL.SaveTechnicalInfo(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_UpdateQuantity_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ProposalQuantityDataModel();
            model.ProposalId = 1;
            model.ProposalDetailId = 1;
            model.Type = guid;
            model.ItemId = 1;
            model.Quantity = 1;
            model.Duration = 1;
            model.LabourDealerDurationCheck = true;
            model.Name = guid;
            model.Code = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ProposalCardBL.UpdateQuantity(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_UpdateVehicleKM_Insert()
        {
            int errorNo = 0;
            string errorMessage = String.Empty;
            var result = _ProposalCardBL.UpdateVehicleKM(UserManager.UserInfo, 102, 1, 125, true, 0, out errorNo, out errorMessage);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_UpdateVehiclePlate_Insert()
        {
            int errorNo = 0;
            string errorMessage = String.Empty;
            var result = _ProposalCardBL.UpdateVehiclePlate(UserManager.UserInfo, 38, "41J4324", out errorNo, out errorMessage);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_UpdateFailureCode_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            int errorNo = 0;
            string errorMessage = String.Empty;
            var result = _ProposalCardBL.UpdateFailureCode(UserManager.UserInfo,102, 508,guid, out errorNo, out errorMessage);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_UpdateDuration_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ProposalQuantityDataModel();
            model.ProposalId = 1;
            model.ProposalDetailId = 1;
            model.Type = guid;
            model.ItemId = 1;
            model.Quantity = 1;
            model.Duration = 1;
            model.LabourDealerDurationCheck = true;
            model.Name = guid;
            model.Code = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _ProposalCardBL.UpdateDuration(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_UpdateProposalDetailDescription_Insert()
        {
            var result = _ProposalCardBL.UpdateProposalDetailDescription(UserManager.UserInfo, 508,null);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_UpdateProposalContactInfo_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var result = _ProposalCardBL.UpdateProposalContactInfo(UserManager.UserInfo, 102,guid);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_UpdateCampaignDenyReason_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var result = _ProposalCardBL.UpdateCampaignDenyReason(UserManager.UserInfo, 102,guid);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_UpdateProposalSparePartId_Insert()
        {
            var result = _ProposalCardBL.UpdateProposalSparePartId(36,1,1);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_UpdateProcessType_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var result = _ProposalCardBL.UpdateProcessType(UserManager.UserInfo, 508,guid,true);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_UpdateRemovalInfo_Insert()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ProposalPartRemovalDto();
            model.ProposalDetailId = 1;
            model.PartId = 1;
            model.DismentledPartSerialNo = guid;
            model.DismentledPartName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartSerialNo = guid;
            model.PartCode = "M.162127";
            var result = _ProposalCardBL.UpdateRemovalInfo(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetPdiPackageData_GetModel()
        {
            var resultGet = _ProposalCardBL.GetPdiPackageData(102);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.ProposalId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetPdiVehicleIsControlled_GetModel()
        {
            var resultGet = _ProposalCardBL.GetPdiVehicleIsControlled(102);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetPdiPackageDetails_GetModel()
        {
            var resultGet = _ProposalCardBL.GetPdiPackageDetails(UserManager.UserInfo, 102);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetProposalCard_GetModel()
        {
            var resultGet = _ProposalCardBL.GetProposalCard(UserManager.UserInfo, 102,1);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.ProposalId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetLabourData_GetModel()
        {
            var resultGet = _ProposalCardBL.GetLabourData(1,251);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetLastLevelPartId_GetModel()
        {
            var resultGet = _ProposalCardBL.GetLastLevelPartId(1);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetDetailData_GetModel()
        {
            var resultGet = _ProposalCardBL.GetDetailData(UserManager.UserInfo, 508,102);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetMaintenance_GetModel()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ProposalPartRemovalDto();
            model.ProposalDetailId = 1;
            model.PartId = 1;
            model.DismentledPartSerialNo = guid;
            model.DismentledPartName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartSerialNo = guid;
            model.PartCode = "M.162127";
            var result = _ProposalCardBL.UpdateRemovalInfo(UserManager.UserInfo, model);

            var filter = new ProposalMaintenanceModel();
            

            var resultGet = _ProposalCardBL.GetMaintenance(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.ProposalId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetQuantityData_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);

            var resultGet = _ProposalCardBL.GetQuantityData(UserManager.UserInfo, 102, 508,"LABOUR",1);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.ProposalId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetMaintenanceQuantityData_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);

            var resultGet = _ProposalCardBL.GetMaintenanceQuantityData(UserManager.UserInfo, 102,508,guid,1,1);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetCampaignData_GetModel()
        {
            var resultGet = _ProposalCardBL.GetCampaignData(UserManager.UserInfo, 102);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.ProposalId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetCampaignLabours_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var resultGet = _ProposalCardBL.GetCampaignLabours(UserManager.UserInfo, guid, 102);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.LabourId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetCampaignParts_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var resultGet = _ProposalCardBL.GetCampaignParts(UserManager.UserInfo, guid);
            Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetCampaignDocuments_GetModel()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var resultGet = _ProposalCardBL.GetCampaignDocuments(UserManager.UserInfo, guid);
            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DocumentId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetVehicleNote_GetModel()
        {
            var resultGet = _ProposalCardBL.GetVehicleNote(UserManager.UserInfo, 1,102);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.NoteId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetVehicleNotePopup_GetModel()
        {
            var resultGet = _ProposalCardBL.GetVehicleNotePopup(UserManager.UserInfo, 18,102);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.NoteId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetDealerNote_GetModel()
        {
            var resultGet = _ProposalCardBL.GetDealerNote(UserManager.UserInfo, 18,105,4);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.NoteId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetProposalDetailDescription_GetModel()
        {
            var resultGet = _ProposalCardBL.GetProposalDetailDescription(508);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetProposalContactInfo_GetModel()
        {
            var resultGet = _ProposalCardBL.GetProposalContactInfo(102);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetProposalCampaignDenyReason_GetModel()
        {
            var resultGet = _ProposalCardBL.GetProposalCampaignDenyReason(102);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetProcessTypeData_GetModel()
        {
            var resultGet = _ProposalCardBL.GetProcessTypeData(UserManager.UserInfo, 508);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetVehicleLeaveDate_GetModel()
        {
            var resultGet = _ProposalCardBL.GetVehicleLeaveDate(102);

            Assert.IsTrue(resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_GetPartRemovalDto_GetModel()
        {
            var resultGet = _ProposalCardBL.GetPartRemovalDto(UserManager.UserInfo, 102,1);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_ListVehicleHourMaints_GetAll()
        {
            var resultGet = _ProposalCardBL.ListVehicleHourMaints(UserManager.UserInfo, 100961);

            Assert.IsTrue(resultGet.Total > 0);
        }
        
        [TestMethod]
        public void ProposalCardBL_ListPickingsForCancellation_GetAll()
        {
            var resultGet = _ProposalCardBL.ListPickingsForCancellation(UserManager.UserInfo, 102);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void ProposalCardBL_ListProposalStats_GetAll()
        {
            var resultGet = _ProposalCardBL.ListProposalStats(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }
        
        [TestMethod]
        public void ProposalCardBL_GetCampaignCheckList_GetAll()
        {
            var resultGet = _ProposalCardBL.GetCampaignCheckList(UserManager.UserInfo, 102);

            Assert.IsTrue(resultGet.Total > 0);
        }
        
        [TestMethod]
        public void ProposalCardBL_ListFailureCodes_GetAll()
        {
            var resultGet = _ProposalCardBL.ListFailureCodes(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void ProposalCardBL_ListIndicatorTypes_GetAll()
        {
            var resultGet = _ProposalCardBL.ListIndicatorTypes(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }
        
        [TestMethod]
        public void ProposalCardBL_ChangePriceList_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ProposalPartRemovalDto();
            model.ProposalDetailId = 1;
            model.PartId = 1;
            model.DismentledPartSerialNo = guid;
            model.DismentledPartName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartSerialNo = guid;
            model.PartCode = "M.162127";
            var result = _ProposalCardBL.UpdateRemovalInfo(UserManager.UserInfo, model);

            int count = 0;
            var filter = new ProposalChangePriceListModel();

            var resultGet = _ProposalCardBL.ChangePriceList(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void ProposalCardBL_ListAlternateParts_GetAll()
        {
            var resultGet = _ProposalCardBL.ListAlternateParts(UserManager.UserInfo, 1,1,102);

            Assert.IsTrue(resultGet.Total > 0);
        }
        
        [TestMethod]
        public void ProposalCardBL_ListDetailProcessTypes_GetAll()
        {
            var resultGet = _ProposalCardBL.ListDetailProcessTypes(UserManager.UserInfo, "IT_A",102,1);

            Assert.IsTrue(resultGet.Total > 0);
        }
        
        [TestMethod]
        public void ProposalCardBL_ListDetailPartReturnItems_GetAll()
        {
            var resultGet = _ProposalCardBL.ListDetailPartReturnItems(UserManager.UserInfo, 508);

            Assert.IsTrue(resultGet.Total > 0);
        }
        
        [TestMethod]
        public void ProposalCardBL_ListCampaignRequestDetails_GetAll()
        {

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new ProposalPartRemovalDto();
            model.ProposalDetailId = 1;
            model.PartId = 1;
            model.DismentledPartSerialNo = guid;
            model.DismentledPartName = guid;
            model.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            model.PartSerialNo = guid;
            model.PartCode = "M.162127";
            var result = _ProposalCardBL.UpdateRemovalInfo(UserManager.UserInfo, model);

            int count = 0;
            var filter = new ProposalCampaignRequestViewModel();
            filter.CampaignCode = "508";
            filter.VerihcleModelCode = guid;

            var resultGet = _ProposalCardBL.ListCampaignRequestDetails(UserManager.UserInfo,508, filter);

            Assert.IsTrue(resultGet.Total > 0);
        }
        
        [TestMethod]
        public void ProposalCardBL_ListMustRemovedParts_GetAll()
        {
            var resultGet = _ProposalCardBL.ListMustRemovedParts(UserManager.UserInfo, 102);

            Assert.IsTrue(resultGet.Total > 0);
        }
        
        [TestMethod]
        public void ProposalCardBL_ListRemovableParts_GetAll()
        {
            var resultGet = _ProposalCardBL.ListRemovableParts(UserManager.UserInfo, 1);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void ProposalCardBL_UpdateRemovalInfo_Update()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var modelUpdate = new ProposalPartRemovalDto();
            modelUpdate.PartId = 1;
            modelUpdate.DismentledPartSerialNo = guid;
            modelUpdate.DismentledPartName = guid;
            modelUpdate.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelUpdate.PartSerialNo = guid;
            modelUpdate.PartCode = "M.162127";
            var resultUpdate = _ProposalCardBL.UpdateRemovalInfo(UserManager.UserInfo, modelUpdate);
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }

        [TestMethod]
        public void ProposalCardBL_UpdateRemovalInfo_Delete()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var modelDelete = new ProposalPartRemovalDto();
            modelDelete.DismentledPartSerialNo = guid;
            modelDelete.DismentledPartName = guid;
            modelDelete.PartName = "DİFERANSİYEL/DİŞLİ YAĞI-85W140";
            modelDelete.PartSerialNo = guid;
            modelDelete.PartCode = "M.162127";
            var resultDelete = _ProposalCardBL.UpdateRemovalInfo(UserManager.UserInfo, modelDelete);
            Assert.IsTrue(resultDelete.IsSuccess);
        }


    }

}

