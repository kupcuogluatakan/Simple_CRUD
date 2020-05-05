using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODMSBusiness;
using ODMSCommon.Security;
using System.Linq;
using ODMSCommon.Security;
using System;
using ODMSModel.WorkOrderCard;
using ODMSModel.CampaignRequest;


namespace ODMSUnitTest
{

    [TestClass]
    public class WorkOrderCardBLTest
    {

        WorkOrderCardBL _WorkOrderCardBL = new WorkOrderCardBL();
        WorkOrderBL _WorkOrderBL = new WorkOrderBL();

        [TestMethod]
        public void WorkOrderCardBL_UpdateProcessType()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;
            var result = _WorkOrderCardBL.UpdateProcessType(UserManager.UserInfo, workOrderDetailId, "IT_M", true);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_UpdatePdiPackage()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;
            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new AddPdiPackageModel();
            model.PdiCheckNote = guid;
            model.WorkOrderId = workOrderId;
            model.TransmissionSerialNo = guid;
            model.DifferencialSerialNo = guid;
            model.ApprovalNote = guid;
            model.WorkOrderId = 1;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkOrderCardBL.UpdatePdiPackage(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_UpdateCustomerNote()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkOrderCustomerNoteUpdateModel();
            model.WorkOrderId = workOrderId;
            model.Note = guid;
            model.UpdateUser = 1;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;
            model.CommandType = "I";
            var result = _WorkOrderCardBL.UpdateCustomerNote(model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_UpdateVehicleKM()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;
            int error = 0;
            string errorDesc = string.Empty;
            var result = _WorkOrderCardBL.UpdateVehicleKM(UserManager.UserInfo, workOrderId, 1, true, 1, 1, out error, out errorDesc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_UpdateVehiclePlate()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;
            int error = 0;
            string errorDesc = string.Empty;

            var result = _WorkOrderCardBL.UpdateVehiclePlate(UserManager.UserInfo, workOrderId, "33BM999", out error, out errorDesc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_UpdateQuantity()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkOrderQuantityDataModel();
            model.WorkOrderId = workOrderId;
            model.WorkOrderDetailId = workOrderDetailId;
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
            var result = _WorkOrderCardBL.UpdateQuantity(UserManager.UserInfo,model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_UpdateFailureCode()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;
            int error = 0;
            string errorDesc = string.Empty;

            var result = _WorkOrderCardBL.UpdateFailureCode(UserManager.UserInfo, workOrderId, workOrderDetailId, "A", out error, out errorDesc);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_UpdateDuration()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var model = new WorkOrderQuantityDataModel();
            model.WorkOrderId = workOrderId;
            model.WorkOrderDetailId = workOrderDetailId;
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
            var result = _WorkOrderCardBL.UpdateDuration(UserManager.UserInfo, model);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_UpdateWorkOrderDetailDescription()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var result = _WorkOrderCardBL.UpdateWorkOrderDetailDescription(UserManager.UserInfo, workOrderDetailId, "UNIT TEST");

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_UpdateWorkOrderContactInfo()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var result = _WorkOrderCardBL.UpdateWorkOrderContactInfo(UserManager.UserInfo, workOrderId, "UNIT TEST");

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_UpdateCampaignDenyReason_Insert()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var result = _WorkOrderCardBL.UpdateCampaignDenyReason(UserManager.UserInfo, workOrderId, "UNIT TEST");

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_UpdateCampaignDenyDealerReason_Insert()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var result = _WorkOrderCardBL.UpdateCampaignDenyDealerReason(UserManager.UserInfo, workOrderId, "UNIT TEST");

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetLastLevelPartId()
        {
            var resultGet = _WorkOrderCardBL.GetLastLevelPartId(331213);

            Assert.IsTrue(resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetPartsAsCsv()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetPartsAsCsv(workOrderDetailId.ToString(), "WORK_ORDER_DETAIL");

            Assert.IsTrue(resultGet.Model != null && resultGet.Model != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetProcessTypeData()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetProcessTypeData(UserManager.UserInfo, workOrderDetailId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CurrentProcessType != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetVehicleLeaveDate()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetVehicleLeaveDate(workOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetPartRemovalDto()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetPartRemovalDto(UserManager.UserInfo, workOrderDetailId, 331213);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.PartName != string.Empty && resultGet.IsSuccess);
        }

        //TODO : Buna bakalım
        [TestMethod]
        public void WorkOrderCardBL_GetPdiResultData()
        {
            //var resultGet = _WorkOrderCardBL.GetPdiResultData(UserManager.UserInfo, filter);

            //Assert.IsTrue(resultGet.Model != null && resultGet.Model.Id > 0 && resultGet.IsSuccess);
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetPdiPackageData()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;


            var resultGet = _WorkOrderCardBL.GetPdiPackageData(workOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.WorkOrderId > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetPdiPackageDetails()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetPdiPackageDetails(UserManager.UserInfo, workOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Dealer != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetVehicleHistoryToolTipContent()
        {
            var resultGet = _WorkOrderCardBL.GetVehicleHistoryToolTipContent(29627);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetGuaranteeRequestDescription()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;


            var resultGet = _WorkOrderCardBL.GetGuaranteeRequestDescription(workOrderDetailId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetPdiVehicleIsControlled()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetPdiVehicleIsControlled(workOrderId);

            Assert.IsTrue(resultGet.Model && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetSparePartVatRatio()
        {
            var resultGet = _WorkOrderCardBL.GetSparePartVatRatio(UserManager.UserInfo, 331213);

            Assert.IsTrue(resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetCustomerNote()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetCustomerNote(workOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetWorkOrderCard()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;


            var resultGet = _WorkOrderCardBL.GetWorkOrderCard(UserManager.UserInfo, workOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DealerName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetDetailData()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetDetailData(UserManager.UserInfo, workOrderId, workOrderDetailId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.CategoryName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetLabourData()
        {
            var resultGet = _WorkOrderCardBL.GetLabourData(211, 29627);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Duration > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetMaintenance()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var filter = new WorkOrderMaintenanceModel();
            filter.WorkOrderId = workOrderId;
            var resultGet = _WorkOrderCardBL.GetMaintenance(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.MaintenancName != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetWorkOrderDetailItemDataForDiscount()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetWorkOrderDetailItemDataForDiscount(workOrderId, workOrderDetailId, "PART", 331213);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Quantity > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetQuantityData()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetQuantityData(UserManager.UserInfo,workOrderId, workOrderDetailId, "PART", 331213);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Quantity > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetMaintenanceQuantityData()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetMaintenanceQuantityData(UserManager.UserInfo, workOrderId, workOrderDetailId, "PART", 331213, 1);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Name != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetCampaignData()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;


            var resultGet = _WorkOrderCardBL.GetCampaignData(UserManager.UserInfo, workOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Campaigns.Count > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetCampaignLabours()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;


            var resultGet = _WorkOrderCardBL.GetCampaignLabours(UserManager.UserInfo, "508", workOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetCampaignParts()
        {
            var resultGet = _WorkOrderCardBL.GetCampaignParts(UserManager.UserInfo, "508");

            Assert.IsTrue(resultGet.Model != null && resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetCampaignDocuments()
        {
            var resultGet = _WorkOrderCardBL.GetCampaignDocuments(UserManager.UserInfo, "508");

            Assert.IsTrue(resultGet.Model != null && resultGet.Total > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetVehiclePlate()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetVehiclePlate(workOrderId.ToString(), "35DNM35");

            Assert.IsTrue(resultGet.Model > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetVehicleNote()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetVehicleNote(UserManager.UserInfo, 3, workOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Note != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetVehicleNotePopup()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;


            var resultGet = _WorkOrderCardBL.GetVehicleNotePopup(UserManager.UserInfo, 3, workOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Note != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetDealerNote()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetDealerNote(UserManager.UserInfo, 3, workOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Note != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetWarrantData()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetWarrantData(UserManager.UserInfo, workOrderId, workOrderDetailId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.Items.Count > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetWorkOrderCardDetails()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetWorkOrderCardDetails(UserManager.UserInfo, workOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.DetailList.Count > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetPartReservationData()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;


            var resultGet = _WorkOrderCardBL.GetPartReservationData(UserManager.UserInfo, workOrderDetailId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model.ProcessTypeList.Count > 0 && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetWorkOrderDetailDescription()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;


            var resultGet = _WorkOrderCardBL.GetWorkOrderDetailDescription(workOrderDetailId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetWorkOrderContactInfo()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetWorkOrderContactInfo(workOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetWorkOrderCampaignDenyReason()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;


            var resultGet = _WorkOrderCardBL.GetWorkOrderCampaignDenyReason(workOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetWorkOrderCampaignDenyDealerReason()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetWorkOrderCampaignDenyDealerReason(workOrderId);

            Assert.IsTrue(resultGet.Model != null && resultGet.Model != string.Empty && resultGet.IsSuccess);
        }

        [TestMethod]
        public void WorkOrderCardBL_ListRemovableParts()
        {
            var resultGet = _WorkOrderCardBL.ListRemovableParts(UserManager.UserInfo, 331213);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void WorkOrderCardBL_UpdateCampaignDenyDealer()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultUpdate = _WorkOrderCardBL.UpdateCampaignDenyDealerReason(UserManager.UserInfo, workOrderId, "UNIT TEST");
            Assert.IsTrue(resultUpdate.Model != null && resultUpdate.IsSuccess);
        }



        [TestMethod]
        public void WorkOrderCardBL_ListPdiResultItems()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.ListPdiResultItems(UserManager.UserInfo, workOrderId);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void WorkOrderCardBL_ListMustRemovedParts()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.ListMustRemovedParts(UserManager.UserInfo, workOrderId);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void WorkOrderCardBL_ListVehicleHourMaints()
        {
            var resultGet = _WorkOrderCardBL.ListVehicleHourMaints(UserManager.UserInfo, 29627);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void WorkOrderCardBL_ListPickingsForCancellation()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.ListPickingsForCancellation(UserManager.UserInfo, workOrderId);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void WorkOrderCardBL_ListAlternateParts()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.ListAlternateParts(UserManager.UserInfo, 331213, 1, workOrderDetailId);

            Assert.IsTrue(resultGet.Total > 0);
        }


        [TestMethod]
        public void WorkOrderCardBL_ChangePriceList()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var result = _WorkOrderCardBL.UpdateCampaignDenyDealerReason(UserManager.UserInfo, workOrderId, "UNUIT TEST");

            var filter = new ChangePriceListModel();

            var resultGet = _WorkOrderCardBL.ChangePriceList(UserManager.UserInfo, filter);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void WorkOrderCardBL_GetCampaignCheckList()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.GetCampaignCheckList(UserManager.UserInfo, workOrderId);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void WorkOrderCardBL_ListDetailProcessTypes()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.ListDetailProcessTypes(UserManager.UserInfo, "IT_A", workOrderId);

            Assert.IsTrue(resultGet.Total > 0);
        }


        [TestMethod]
        public void WorkOrderCardBL_ListIndicatorTypes()
        {
            var resultGet = _WorkOrderCardBL.ListIndicatorTypes(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }


        [TestMethod]
        public void WorkOrderCardBL_ListDetailPartReturnItems()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var resultGet = _WorkOrderCardBL.ListDetailPartReturnItems(UserManager.UserInfo, workOrderDetailId);

            Assert.IsTrue(resultGet.Total > 0);
        }


        [TestMethod]
        public void WorkOrderCardBL_ListWorkOrderStats()
        {
            var resultGet = _WorkOrderCardBL.ListWorkOrderStats(UserManager.UserInfo);

            Assert.IsTrue(resultGet.Total > 0);
        }

        [TestMethod]
        public void WorkOrderCardBL_ListCampaignRequestDetails()
        {
            var workOrderId = _WorkOrderBL.GetLastWorkOrderId(UserManager.UserInfo.DealerID).Model.Value;
            var workOrderDetailId = _WorkOrderBL.GetLastWorkOrderDetailId(UserManager.UserInfo.DealerID).Model.Value;

            var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            var filter = new CampaignRequestViewModel();
            filter.CampaignCode = "508";
            filter.VerihcleModelCode = guid;

            var resultGet = _WorkOrderCardBL.ListCampaignRequestDetails(UserManager.UserInfo, workOrderDetailId, filter);

            Assert.IsTrue(resultGet.Total > 0);
        }


    }

}

