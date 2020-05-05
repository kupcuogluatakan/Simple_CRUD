using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSData.Utility;
using ODMSModel;
using ODMSModel.AppointmentDetails;
using ODMSModel.CampaignRequest;
using ODMSModel.Common;
using ODMSModel.WorkOrderCard;
using ODMSModel.WorkOrderCard.CampaignDetail;
using ODMSModel.AppointmentDetailsParts;
using ODMSModel.AppointmentDetailsLabours;
using ODMSModel.SparePart;

namespace ODMSBusiness
{
    public class WorkOrderCardBL : BaseBusiness
    {
        private readonly WorkOrderCardData data = new WorkOrderCardData();
        private readonly WorkOrderCardHelperData dataWorkOrderCardHelper = new WorkOrderCardHelperData();
        private readonly WorkOrderData dataWorkOrder = new WorkOrderData();
        private readonly SparePartData sparePart = new SparePartData();


        public ResponseModel<decimal> GetSparePartVatRatio(UserInfo user, long partId)
        {
            var response = new ResponseModel<decimal>();
            try
            {
                response.Model = data.GetSparePartVatRatio(user, partId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<string> GetCustomerNote(long workOrderId)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.GetCustomerNote(workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<WorkOrderCardModel> GetWorkOrderCard(UserInfo user, long workOrderId)
        {
            var response = new ResponseModel<WorkOrderCardModel>();
            try
            {
                response.Model = data.GetWorkOrderCard(user, workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<WorkOrderCustomerNoteUpdateModel> UpdateCustomerNote(WorkOrderCustomerNoteUpdateModel model)
        {
            var response = new ResponseModel<WorkOrderCustomerNoteUpdateModel>();
            try
            {
                data.UpdateCustomerNote(model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<long?> UpdateVehicleKM(UserInfo user, long workOrderId, long km, bool isHourMaint, int hour, int fromUpdateBtn, out int ErrorNo, out string ErrorMessage)
        {
            var response = new ResponseModel<long?>();
            ErrorNo = 0;
            ErrorMessage = string.Empty;
            try
            {
                response.Model = data.UpdateVehicleKM(user, workOrderId, km, isHourMaint, hour, fromUpdateBtn, out ErrorNo, out ErrorMessage);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<string> UpdateVehiclePlate(UserInfo user, long workOrderId, string plate, out int ErrorNo, out string ErrorMessage)
        {
            var response = new ResponseModel<string>();
            ErrorNo = 0;
            ErrorMessage = string.Empty;
            try
            {
                response.Model = data.UpdateVehiclePlate(user, workOrderId, plate, out ErrorNo, out ErrorMessage);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<AppointmentDetailsViewModel> AddWorkOrderDetail(UserInfo user, AppointmentDetailsViewModel model)
        {
            var response = new ResponseModel<AppointmentDetailsViewModel>();
            try
            {
                data.AddWorkOrderDetail(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<AppointmentDetailsPartsViewModel> AddWorkOrderPart(UserInfo user, AppointmentDetailsPartsViewModel model)
        {
            var response = new ResponseModel<AppointmentDetailsPartsViewModel>();
            try
            {
                data.AddWorkOrderPart(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<AppointmentDetailsViewModel> GetDetailData(UserInfo user, long workOrderId, long workOrderDetailId)
        {
            var response = new ResponseModel<AppointmentDetailsViewModel>();
            try
            {
                response.Model = data.GetDetailData(user, workOrderId, workOrderDetailId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<AppointmentDetailsLaboursViewModel> AddWorkOrderLabour(UserInfo user, AppointmentDetailsLaboursViewModel model)
        {
            var response = new ResponseModel<AppointmentDetailsLaboursViewModel>();
            try
            {
                data.AddWorkOrderLabour(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<LabourDataModel> GetLabourData(long labourId, int? vehicleId)
        {
            var response = new ResponseModel<LabourDataModel>();
            try
            {
                response.Model = data.GetLabourData(labourId, vehicleId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<WorkOrderMaintenanceModel> GetMaintenance(UserInfo user, WorkOrderMaintenanceModel model)
        {
            var response = new ResponseModel<WorkOrderMaintenanceModel>();
            try
            {
                response.Model = data.GetMaintenance(user, model);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<WorkOrderMaintenanceModel> AddWorkOrderMaint(UserInfo user, WorkOrderMaintenanceModel model)
        {
            var response = new ResponseModel<WorkOrderMaintenanceModel>();
            try
            {
                data.AddWorkOrderMaint(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<WorkOrderDetailCancelModel> CancelDetail(UserInfo user, WorkOrderDetailCancelModel model)
        {
            var response = new ResponseModel<WorkOrderDetailCancelModel>();
            try
            {
                data.CancelDetail(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<WorkOrderDiscountModel> GetWorkOrderDetailItemDataForDiscount(long workOrderId, long workOrderDetailId, string type, long itemId)
        {
            var response = new ResponseModel<WorkOrderDiscountModel>();
            try
            {
                var model = data.GetWorkOrderDetailItemDataForDiscount(workOrderId, workOrderDetailId, type, itemId);
                if (model.DisableDiscount)
                {
                    model.DiscountRatio = model.TotalFleetDiscountRate;
                }

                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<WorkOrderDiscountModel> AddDiscount(UserInfo user, WorkOrderDiscountModel model)
        {
            var response = new ResponseModel<WorkOrderDiscountModel>();
            try
            {
                data.AddDiscount(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<WorkOrderQuantityDataModel> GetQuantityData(UserInfo user, long workOrderId, long workOrderDetailId, string type, long itemId)
        {
            var response = new ResponseModel<WorkOrderQuantityDataModel>();
            try
            {
                response.Model = data.GetQuantityData(user, workOrderId, workOrderDetailId, type, itemId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<WorkOrderQuantityDataModel> UpdateQuantity(UserInfo user, WorkOrderQuantityDataModel model)
        {
            var response = new ResponseModel<WorkOrderQuantityDataModel>();
            try
            {
                data.UpdateQuantity(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<WorkOrderMaintenanceQuantityDataModel> GetMaintenanceQuantityData(UserInfo user, long workOrderId, long workOrderDetailId, string type, long itemId, int maintId)
        {
            var response = new ResponseModel<WorkOrderMaintenanceQuantityDataModel>();
            try
            {
                response.Model = data.GetMaintenanceQuantityData(user, workOrderId, workOrderDetailId, maintId: maintId, type: type, itemId: itemId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<SelectListItem> ListAlternateParts(UserInfo user, long partId, int maintId, long workOrderDetailId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListAlternateParts(user, partId, maintId, workOrderDetailId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<WorkOrderMaintenanceQuantityDataModel> RemoveMaintenanceItem(UserInfo user, WorkOrderMaintenanceQuantityDataModel model)
        {
            var response = new ResponseModel<WorkOrderMaintenanceQuantityDataModel>();
            try
            {
                data.RemoveLabourOrPartFromMaintenance(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<WorkOrderMaintenanceQuantityDataModel> ChangePart(UserInfo user, WorkOrderMaintenanceQuantityDataModel model)
        {
            var response = new ResponseModel<WorkOrderMaintenanceQuantityDataModel>();
            try
            {
                data.ChangePart(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<WorkOrderCampaignModel> GetCampaignData(UserInfo user, long id)
        {
            var response = new ResponseModel<WorkOrderCampaignModel>();
            try
            {
                response.Model = data.GetCampaignData(user, id);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<LabourModel> GetCampaignLabours(UserInfo user, string campaignCode, long workOrderId)
        {
            var response = new ResponseModel<LabourModel>();
            try
            {
                response.Data = data.GetCampaignLabours(user, campaignCode, workOrderId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<PartModel> GetCampaignParts(UserInfo user, string campaignCode)
        {
            var response = new ResponseModel<PartModel>();
            try
            {
                response.Data = data.GetCampaignParts(user, campaignCode);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<DocumentModel> GetCampaignDocuments(UserInfo user, string campaignCode)
        {
            var response = new ResponseModel<DocumentModel>();
            try
            {
                response.Data = data.GetCampaignDocuments(user, campaignCode);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<int> GetVehiclePlate(string id, string plate)
        {
            var response = new ResponseModel<int>();
            try
            {
                response.Model = data.GetVehiclePlate(id, plate);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<WorkOrderCampaignModel> AddCampaign(UserInfo user, WorkOrderCampaignModel model)
        {
            var response = new ResponseModel<WorkOrderCampaignModel>();
            try
            {
                data.AddCampaign(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<VehicleNoteDetailModel> GetVehicleNote(UserInfo user, int id, long workOrderId)
        {
            var response = new ResponseModel<VehicleNoteDetailModel>();
            try
            {
                response.Model = data.GetVehicleNote(user, id, workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<VehicleNoteDetailModel> GetVehicleNotePopup(UserInfo user, int id, long workOrderId)
        {
            var response = new ResponseModel<VehicleNoteDetailModel>();
            try
            {
                response.Model = data.GetVehicleNotePopup(user, id, workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<VehicleNoteDetailModel> GetDealerNote(UserInfo user, int id, long workOrderId)
        {
            var response = new ResponseModel<VehicleNoteDetailModel>();
            try
            {
                response.Model = data.GetDealerNote(user, id, workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<WorkOrderVehicleNoteModel> AddVehicleNote(UserInfo user, WorkOrderVehicleNoteModel model)
        {
            var response = new ResponseModel<WorkOrderVehicleNoteModel>();
            try
            {
                data.AddVehicleNote(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<WorkOrderVehicleNoteModel> AddDealerNote(UserInfo user, WorkOrderVehicleNoteModel model)
        {
            var response = new ResponseModel<WorkOrderVehicleNoteModel>();
            try
            {
                data.AddDealerNote(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<WorkOrderCardWarrantyModel> GetWarrantData(UserInfo user, long workOrderId, long workOrderDetailId)
        {
            var response = new ResponseModel<WorkOrderCardWarrantyModel>();
            try
            {
                response.Model = data.GetWarrantyData(user, workOrderId, workOrderDetailId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ChangePriceListModel> ChangePriceList(UserInfo user, ChangePriceListModel model)
        {
            var response = new ResponseModel<ChangePriceListModel>();
            try
            {
                data.ChangePriceList(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<AddLabourPrice> AddLabourPrice(UserInfo user, AddLabourPrice model)
        {
            var response = new ResponseModel<AddLabourPrice>();
            try
            {
                data.AddLabourPrice(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<ComboBoxModel> ListFailureCodes(UserInfo user)
        {
            var response = new ResponseModel<ComboBoxModel>();
            try
            {
                response.Data = data.ListFailureCodes(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<ComboBoxModel> ListFailureCodes2(UserInfo user)
        {
            var response = new ResponseModel<ComboBoxModel>();
            try
            {
                response.Data = data.ListFailureCodes2(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<bool> UpdateFailureCode(UserInfo user, long workOrderId, long workOrderDetailId, string failureCodeId, out int errorNo, out string errorMessage)
        {
            var response = new ResponseModel<bool>();
            errorNo = 0;
            errorMessage = string.Empty;
            try
            {
                data.UpdateFailureCode(user, workOrderId, workOrderDetailId, failureCodeId, out errorNo, out errorMessage);
                response.Model = true;
                response.Message = MessageResource.Global_Display_Success;
                if (errorNo > 0)
                    throw new System.Exception(errorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;

        }


        public ResponseModel<WorkOrderQuantityDataModel> UpdateDuration(UserInfo user, WorkOrderQuantityDataModel model)
        {
            var response = new ResponseModel<WorkOrderQuantityDataModel>();
            try
            {
                data.UpdateDuration(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<WorkOrderCardModel> GetWorkOrderCardDetails(UserInfo user, long workOrderId)
        {
            var response = new ResponseModel<WorkOrderCardModel>();
            try
            {
                var model = new WorkOrderCardModel { Details = data.GetWorkOrderCardDetails(user, workOrderId) };
                var detailList = model.Details.Where(c => c.Type == "INDICATOR").Select(x => new WorkOrderDetailList
                {
                    WorkOrderDetailId = x.WorkOrderDetailId,
                    Description = x.Description,
                    TechicalDescription = x.Name,
                    DetailType = DetailType.Indicator,
                    FailureCode = x.FailureCode,
                    ProcessType = x.ProcessType,
                    ProcessTypeCode = x.ProcessTypeCode,
                    GuarantyAuthorizationNeed = x.GuarantyAuthorizationNeed,
                    WarrantyStatus = x.WarrantyStatus,
                    InvoiceId = x.InvoiceId,
                    FailureCodeDescription = x.FailureCodeDescription,
                    WarrantyStatusDesc = x.WarrantyStatusDesc,
                    GosApprovalNeed = x.GosApprovalNeed,
                    GuaranteeConfirmDesc = x.GuaranteeConfirmDesc,
                    GosSendCheck = x.GosSendCheck,
                    IndicatorType = WorkOrderCardData.GetIndicatorTypeFromIndicatorTypeCode(x.IndicatorTypeCode),
                    AllowFailureCodeChange = x.AllowFailureCodeChange,
                    GifNo = x.GifNo,
                    IsFromProposal = x.IsFromProposal
                }).ToList();
                //maintenance adjustment
                detailList.ForEach(c =>
                {
                    var meintenanceItem =
                        model.Details.FirstOrDefault(
                            d => d.WorkOrderDetailId == c.WorkOrderDetailId && d.MaintenanceId > 0);

                    if (meintenanceItem != null)
                    {
                        c.DetailType = DetailType.Meintenance;
                        c.Description = meintenanceItem.MaintenanceName;
                    }
                });

                model.DetailList = detailList;
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;

        }

        public ResponseModel<CampaignCheckModel> GetCampaignCheckList(UserInfo user, long workOrderId)
        {
            var response = new ResponseModel<CampaignCheckModel>();
            try
            {
                response.Data = data.GetCampaignCheckList(user, workOrderId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<SelectListItem> ListDetailProcessTypes(UserInfo user, string indicatorTypeCode, long workOrderId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataWorkOrderCardHelper.ListDetailProcessTypes(user, indicatorTypeCode, workOrderId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }


        public ResponseModel<SelectListItem> ListIndicatorTypes(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataWorkOrderCardHelper.ListDetailProcessTypes(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<PartReservationDTO> GetPartReservationData(UserInfo user, long workOrderDetailId)
        {
            var response = new ResponseModel<PartReservationDTO>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetPartReservationData(user, workOrderDetailId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<ModelBase> ReservDetailParts(UserInfo user, long workOrderDetailId, string processType, out List<PartReservationInfo> reservationInfos)
        {
            var response = new ResponseModel<ModelBase>();
            reservationInfos = new List<PartReservationInfo>();
            try
            {
                response.Model = dataWorkOrderCardHelper.ReservDetailParts(user, workOrderDetailId, processType, out reservationInfos);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ModelBase> PickDetailParts(UserInfo user, long id, long workOrderDetailId)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.PickDetailParts(user, id, workOrderDetailId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<PartReturnModel> ListDetailPartReturnItems(UserInfo user, long workOrderDetailId)
        {
            var response = new ResponseModel<PartReturnModel>();
            try
            {
                response.Data = dataWorkOrderCardHelper.ListDetailPartReturnItems(user, workOrderDetailId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ModelBase> ReturnDetailParts(UserInfo user, List<PartReturnModel> list, long workOrderDetailId)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Data = dataWorkOrderCardHelper.ReturnDetailParts(user, list, workOrderDetailId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<string> GetWorkOrderDetailDescription(long workOrderDetailId)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetTechicalDescription(workOrderDetailId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ModelBase> UpdateWorkOrderDetailDescription(UserInfo user, long workOrderDetailId, string desc)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.UpdateTechicalDescription(user, workOrderDetailId, desc);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<string> GetWorkOrderContactInfo(long workOrderId)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetWorkOrderContactInfo(workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<ModelBase> UpdateWorkOrderContactInfo(UserInfo user, long workOrderId, string note)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.UpdateWorkOrderContactInfo(user, workOrderId, note);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<SelectListItem> ListWorkOrderStats(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataWorkOrderCardHelper.ListWorkOrderStats(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<SelectListItem> ListWorkOrderStatus(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataWorkOrder.ListWorkOrderStatus(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ModelBase> ChangeWorkOrderStat(UserInfo user, long workOrderId, int stat)
        {
            var response = new ResponseModel<ModelBase>();
            int errorNo;
            string errorMessage;
            try
            {
                dataWorkOrder.ChangeWorkOrderStatus(user, workOrderId, stat, out errorNo, out errorMessage);
                response.Model = new ModelBase { ErrorNo = errorNo, ErrorMessage = errorMessage };
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<string> GetWorkOrderCampaignDenyReason(long workOrderId)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetWorkOrderCampaignDenyReason(workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<string> GetWorkOrderCampaignDenyDealerReason(long workOrderId)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetWorkOrderCampaignDenyDealerReason(workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ModelBase> UpdateDeniedCampaigns(UserInfo user, long workOrderId, string deniedCamps) {

            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.UpdateDeniedCampaigns(user, workOrderId, deniedCamps);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ModelBase> CancelCampaignRejections(UserInfo user, long workOrderId) {

            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.CancelCampaignRejections(user, workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;

        }


        public ResponseModel<ModelBase> UpdateCampaignDenyReason(UserInfo user, long workOrderId, string note,string deniedCamps)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.UpdateWorkOrderCampaignDenyReason(user, workOrderId, note,deniedCamps);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ModelBase> UpdateCampaignDenyDealerReason(UserInfo user, long workOrderId, string note,string deniedCampaigns)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.UpdateWorkOrderCampaignDenyDealerReason(user, workOrderId, note, deniedCampaigns);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ModelBase> CheckForOtherCampaigns(long id)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.CheckForOtherCampaigns(id);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ModelBase> CreateCampaignRequest(UserInfo user, long workOrderDetailId)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.CreateCampaignRequest(user, workOrderDetailId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<PartModel> ListCampaignRequestDetails(UserInfo user, long workOrderDetailId, CampaignRequestViewModel cr)
        {
            var response = new ResponseModel<PartModel>();
            try
            {
                response.Data = dataWorkOrderCardHelper.ListCampaignRequestDetails(user, workOrderDetailId, cr);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<bool?> CheckPartLastLevel(long workOrderDetailId, string csPartIds)
        {
            var response = new ResponseModel<bool?>();
            try
            {
                response.Model = dataWorkOrderCardHelper.CheckPartLastLevel(workOrderDetailId, csPartIds);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<SparePartIndexViewModel> GetPart(UserInfo user,int partId, string partCode)
        {
            var response = new ResponseModel<SparePartIndexViewModel>();
            var filter = new SparePartIndexViewModel();
            filter.PartId = partId;
            filter.PartCode = partCode;
            try
            {
                response.Model = sparePart.GetSparePart(user, filter);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<long> GetLastLevelPartId(long partId)
        {
            var response = new ResponseModel<long>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetLastLevelPartId(partId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<string> GetPartsAsCsv(string id, string type)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetPartsAsCsv(id, type);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ChangeProcessTypeModel> GetProcessTypeData(UserInfo user, long workOrderDetailId)
        {
            var response = new ResponseModel<ChangeProcessTypeModel>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetProcessTypeData(user, workOrderDetailId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ModelBase> UpdateProcessType(UserInfo user, long workOrderDetailId, string processType, bool confirmed)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.UpdateProcessType(user, workOrderDetailId, processType, confirmed);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<DateTime?> GetVehicleLeaveDate(long workOrderId)
        {
            var response = new ResponseModel<DateTime?>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetVehicleLeaveDate(workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<SelectListItem> ListRemovableParts(UserInfo user, long partId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataWorkOrderCardHelper.ListRemovableParts(user, partId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<PdiResultItem> ListPdiResultItems(UserInfo user, long workOrderId)
        {
            var response = new ResponseModel<PdiResultItem>();
            try
            {
                var list = dataWorkOrderCardHelper.ListPdiResultItems(user, workOrderId);
                list.ForEach(c => c.IsAppropriate = !c.IsGroupCode ? !c.IsAppropriate : c.IsAppropriate);

                response.Data = list;
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<PartRemovalDto> GetPartRemovalDto(UserInfo user, long workOrderDetailId, long partId)
        {
            var response = new ResponseModel<PartRemovalDto>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetPartRemovalDto(user, workOrderDetailId, partId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<Tuple<string, string, List<SelectListItem>, List<SelectListItem>, List<SelectListItem>>> GetPdiResultData(UserInfo user, long workOrderId, string controlCode)
        {
            var response = new ResponseModel<Tuple<string, string, List<SelectListItem>, List<SelectListItem>, List<SelectListItem>>>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetPdiResultData(user, workOrderId, controlCode);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<ModelBase> UpdateRemovalInfo(UserInfo user, PartRemovalDto dto)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.UpdateRemovalInfo(user, dto);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<CommonValues.WorkOrderCardVehicleLeaveResult> CheckVehicleLeaveMandatoryFields(UserInfo user, long workOrderId)
        {
            var response = new ResponseModel<CommonValues.WorkOrderCardVehicleLeaveResult>();
            try
            {
                response.Model = dataWorkOrderCardHelper.CheckVehicleLeaveMandatoryFields(user, workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<MandatoryRemovalPart> ListMustRemovedParts(UserInfo user, long workOrderId)
        {
            var response = new ResponseModel<MandatoryRemovalPart>();
            try
            {
                response.Data = dataWorkOrderCardHelper.ListMandatoryRemovalPart(user, workOrderId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ModelBase> SendToWarrantyPreApproval(UserInfo user, long workOrderDetailId, string requestDescription)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                requestDescription = !string.IsNullOrEmpty(requestDescription) ? requestDescription.ToUpper(Thread.CurrentThread.CurrentCulture) : string.Empty;
                response.Model = dataWorkOrderCardHelper.SendToWarrantyApproval(user, workOrderDetailId, requestDescription, 1);
                if (response.Model.ErrorNo == 0)
                    response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }


        public ResponseModel<ModelBase> SendToWarrantyApproval(UserInfo user, long workOrderDetailId)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.SendToWarrantyApproval(user, workOrderDetailId, string.Empty, 4);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<bool> CheckWorkOrderDealer(long id, int p, long invoiceid = 0)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = dataWorkOrderCardHelper.CheckWorkOrderDealer(workOrderId: id, dealerId: p, invoiceid: invoiceid);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<AddPdiPackageModel> AddPdiPackage(UserInfo user, AddPdiPackageModel model)
        {
            var response = new ResponseModel<AddPdiPackageModel>();
            try
            {
                dataWorkOrderCardHelper.AddPdiPackage(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<AddPdiPackageModel> GetPdiPackageData(long workOrderId)
        {
            var response = new ResponseModel<AddPdiPackageModel>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetPdiPackageData(workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ModelBase> SavePdiResult(UserInfo user, PdiResultModel model, string type)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.SavePdiResult(user, model, type);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<AddPdiPackageModel> UpdatePdiPackage(UserInfo user, AddPdiPackageModel model)
        {
            var response = new ResponseModel<AddPdiPackageModel>();
            try
            {
                dataWorkOrderCardHelper.UpdatePdiPackage(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<ModelBase> PdiSendToApproval(UserInfo user, long id)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.PdiSendToApproval(user, id);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<PdiPrintModel> GetPdiPackageDetails(UserInfo user, long workOrderId)
        {
            var response = new ResponseModel<PdiPrintModel>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetPdiPackageDetails(user, workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        /// <summary>
        /// Sets CLAIM_DISMANTLED_PARTS BARCODE_FIRST_PRINT_DATE to DateTime.Now
        /// </summary>
        /// <param name="workOrderDetailId">Work Order Card detail id</param>
        /// <param name="printAll">Sets all matching results if true, otherwise only null values.</param>
        /// <returns>ModelBase</returns>
        public ResponseModel<ModelBase> PrintLabels(UserInfo user, long workOrderDetailId, bool printAll)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.PrintLabels(user, workOrderDetailId, printAll);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ModelBase> DeleteDetailItem(UserInfo user, long workOrderId, long workOrderDetailId, string type, long itemId)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.DeleteDetailItem(user, workOrderId, workOrderDetailId, type, itemId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<string> GetVehicleHistoryToolTipContent(int vehicleId)
        {
            var response = new ResponseModel<string>();
            try
            {
                var model = dataWorkOrderCardHelper.GetGetVehicleHistoryLastItem(vehicleId);

                response.Model = model == null ? NoHistoryData() : GetToolTipHtml(model);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        private string NoHistoryData()
        {
            return
                string.Format(@"
                <div class='tooltip-content'>
                    {0}
                </div>
            ", MessageResource.WorkOrderCard_Message_NoVehicleHistory
                    );

        }

        private string GetToolTipHtml(VehicleHistoryModel model)
        {
            return string.Format(@"
                <div class='tooltip-content'>
                    <table>
                        <tr>
                            <td class='bold'>{0}</td>
                            <td>{1}</td>
                        </tr>
                        <tr>
                            <td  class='bold' >{2}</td>
                            <td>{3}</td>
                        </tr>
                        <tr>
                            <td  class='bold'>{4}</td>
                            <td>{5}</td>
                        </tr>
                    </table>
                </div>
            ", MessageResource.Customer_Display_DealerName, model.DealerName,
                MessageResource.Vehicle_Display_VehicleKilometer, model.VehicleKm,
                MessageResource.VehicleHistory_Display_HistoryDate, model.HistoryDate == default(DateTime) ? String.Empty : model.HistoryDate.ToString("dd/MM/yyyy hh:mm")
                );
        }

        public ResponseModel<string> GetGuaranteeRequestDescription(long workOrderDetailId)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetGuaranteeRequestDescription(workOrderDetailId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<SelectListItem> ListVehicleHourMaints(UserInfo user, int vehicleId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataWorkOrderCardHelper.ListVehicleHourMaints(user, vehicleId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }


        public ResponseModel<ModelBase> CompletePdiVehicleResult(UserInfo user, long workOrderId)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.CompletePdiVehicleResult(user, workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<bool> GetPdiVehicleIsControlled(long workOrderId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = dataWorkOrderCardHelper.GetPdiVehicleIsControlled(workOrderId);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<PickingCancellationItem> ListPickingsForCancellation(UserInfo user, long workOrderId)
        {
            var response = new ResponseModel<PickingCancellationItem>();
            try
            {
                response.Data = dataWorkOrderCardHelper.ListPickingsForCancellation(user, workOrderId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<bool> CancelPicking(UserInfo user, long pickingId, long? workOrderId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                dataWorkOrderCardHelper.CancelPicking(user, workOrderId, pickingId);
                response.Model = true;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<ModelBase> CancelVehicleLeave(UserInfo user, long workOrderId, string reason)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataWorkOrderCardHelper.CancelVehicleLeave(user, workOrderId, reason);
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

    }
}