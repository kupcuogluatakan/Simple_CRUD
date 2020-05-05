using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSData.Utility;
using ODMSModel;
using ODMSModel.AppointmentDetails;
using ODMSModel.AppointmentDetailsLabours;
using ODMSModel.AppointmentDetailsParts;
using ODMSModel.Common;
using ODMSModel.ProposalCard;
using ODMSModel.WorkOrderCard;
using ODMSModel.WorkOrderCard.CampaignDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ODMSBusiness.Business
{
    public class ProposalCardBL : BaseBusiness
    {
        private readonly ProposalCardData data = new ProposalCardData();
        private readonly ProposalData dataProposal = new ProposalData();
        public ResponseModel<ProposalCardModel> GetProposalCard(UserInfo user, long id, int seq)
        {
            var response = new ResponseModel<ProposalCardModel>();
            try
            {
                response.Model = data.GetProposalCard(user, id, seq);
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
        public ResponseModel<SelectListItem> ListProposalStats(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListProposalStats(user);
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
        public ResponseModel<CampaignCheckModel> GetCampaignCheckList(UserInfo user, long proposalId)
        {
            var response = new ResponseModel<CampaignCheckModel>();
            try
            {
                response.Data = data.GetCampaignCheckList(user, proposalId);
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
        public ResponseModel<bool> CheckProposalDealer(long id, int p, long invoiceid = 0)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.CheckProposalDealer(proposalId: id, dealerId: p, invoiceid: invoiceid);
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
        public ProposalCardModel GetProposalCardDetails(UserInfo user, long proposalId, int seq)
        {

            var model = new ProposalCardModel { Details = new ProposalCardData().GetProposalCardDetails(user, proposalId, seq) };
            var detailList = model.Details.Where(c => c.Type == "INDICATOR").Select(x => new ProposalDetailList
            {
                ProposalDetailId = x.ProposalDetailId,
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
                IndicatorType = ProposalCardData.GetIndicatorTypeFromIndicatorTypeCode(x.IndicatorTypeCode),
                AllowFailureCodeChange = x.AllowFailureCodeChange,
                GifNo = x.GifNo
            }).ToList();
           
            //maintenance adjustment
            detailList.ForEach(c =>
            {
                var meintenanceItem =
                    model.Details.FirstOrDefault(
                        d => d.ProposalDetailId == c.ProposalDetailId && d.MaintenanceId > 0);

                if (meintenanceItem != null)
                {
                    c.DetailType = DetailType.Meintenance;
                    c.Description = meintenanceItem.MaintenanceName;
                }
            });




            model.DetailList = detailList;

            return model;

        }
        public ResponseModel<ModelBase> CheckForOtherCampaigns(long id)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = dataProposal.CheckForOtherCampaigns(id);
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
        public ResponseModel<SelectListItem> ListIndicatorTypes(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListDetailProcessTypes(user);
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
        public ResponseModel<AppointmentDetailsViewModel> AddProposalDetail(UserInfo user, AppointmentDetailsViewModel model)
        {
            var response = new ResponseModel<AppointmentDetailsViewModel>();
            try
            {
                data.AddProposalDetail(user, model);
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
        public ResponseModel<long> GetLastLevelPartId(long partId)
        {
            var response = new ResponseModel<long>();
            try
            {
                response.Model = data.GetLastLevelPartId(partId);
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

        public ResponseModel<AppointmentDetailsPartsViewModel> AddProposalPart(UserInfo user,
            AppointmentDetailsPartsViewModel model)
        {
            var response = new ResponseModel<AppointmentDetailsPartsViewModel>();
            try
            {
                data.AddProposalPart(user, model);
                response.Model = model;
                response.Message = MessageResource.Global_Display_Success;
                if (model.ErrorNo > 0)
                    throw new System.Exception(model.ErrorMessage);
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error,
                    MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<bool?> CheckPartLastLevel(long proposalDetailId, string csPartIds)
        {
            var response = new ResponseModel<bool?>();
            try
            {
                response.Model = data.CheckPartLastLevel(proposalDetailId, csPartIds);
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
        public ResponseModel<AppointmentDetailsViewModel> GetDetailData(UserInfo user, long proposalId, long proposalDetailId)
        {
            var response = new ResponseModel<AppointmentDetailsViewModel>();
            try
            {
                response.Model = data.GetDetailData(user, proposalId, proposalDetailId);
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

        

        public ResponseModel<AppointmentDetailsLaboursViewModel> AddProposalLabour(UserInfo user, AppointmentDetailsLaboursViewModel model)
        {
            var response = new ResponseModel<AppointmentDetailsLaboursViewModel>();
            try
            {
                data.AddProposalLabour(user, model);
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
        public ResponseModel<ProposalMaintenanceModel> GetMaintenance(UserInfo user, ProposalMaintenanceModel filter)
        {
            var response = new ResponseModel<ProposalMaintenanceModel>();
            try
            {
                response.Data = dataProposal.GetMaintenance(user, filter);
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
        public ResponseModel<ProposalMaintenanceModel> AddProposalMaint(UserInfo user, ProposalMaintenanceModel model)
        {
            var response = new ResponseModel<ProposalMaintenanceModel>();
            try
            {
                data.AddProposalMaint(user, model);
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
        public ProposalDiscountModel GetProposalDetailItemDataForDiscount(long proposalId, long proposalDetailId, string type, long itemId)
        {
            var model = new ProposalCardData().GetProposalDetailItemDataForDiscount(proposalId, proposalDetailId,
                type, itemId);
            if (model.DisableDiscount)
            {
                model.DiscountRatio = model.TotalFleetDiscountRate;
            }

            return model;
        }
        public ResponseModel<ProposalQuantityDataModel> GetQuantityData(UserInfo user, long proposalId, long proposalDetailId, string type, long itemId)
        {
            var response = new ResponseModel<ProposalQuantityDataModel>();
            try
            {
                response.Model = data.GetQuantityData(user, proposalId, proposalDetailId, type, itemId);
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
        public ResponseModel<ProposalChangePriceListModel> ChangePriceList(UserInfo user, ProposalChangePriceListModel model)
        {
            var response = new ResponseModel<ProposalChangePriceListModel>();
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
        public ResponseModel<ProposalQuantityDataModel> UpdateQuantity(UserInfo user, ProposalQuantityDataModel model)
        {
            var response = new ResponseModel<ProposalQuantityDataModel>();
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
        public ResponseModel<ProposalMaintenanceQuantityDataModel> RemoveMaintenanceItem(UserInfo user, ProposalMaintenanceQuantityDataModel model)
        {
            var response = new ResponseModel<ProposalMaintenanceQuantityDataModel>();
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
        public ResponseModel<ProposalMaintenanceQuantityDataModel> ChangePart(UserInfo user, ProposalMaintenanceQuantityDataModel model)
        {
            var response = new ResponseModel<ProposalMaintenanceQuantityDataModel>();
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

        public ResponseModel<ProposalCancelModel> CancelProposal(UserInfo user, ProposalCancelModel model)
        {
            var response = new ResponseModel<ProposalCancelModel>();
            try
            {
                data.CancelProposal(user, model);
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

        public ResponseModel<ProposalDetailCancelModel> CancelDetail(UserInfo user, ProposalDetailCancelModel model)
        {
            var response = new ResponseModel<ProposalDetailCancelModel>();
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
        public ResponseModel<ProposalDiscountModel> AddDiscount(UserInfo user, ProposalDiscountModel model)
        {
            var response = new ResponseModel<ProposalDiscountModel>();
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
        public ResponseModel<long?> UpdateVehicleKM(UserInfo user, long id, int seq, long km, bool isHourMaint, int hour, out int errorNo, out string errorMessage)
        {
            var response = new ResponseModel<long?>();
            errorNo = 0;
            errorMessage = string.Empty;
            try
            {
                response.Model = data.UpdateVehicleKM(user, id, seq, km, isHourMaint, hour, out errorNo, out errorMessage);
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
        public ResponseModel<string> UpdateVehiclePlate(UserInfo user, long id, string plate, out int errorNo, out string errorMessage)
        {
            var response = new ResponseModel<string>();
            errorNo = 0;
            errorMessage = string.Empty;
            try
            {
                response.Model = data.UpdateVehiclePlate(user, id, plate, out errorNo, out errorMessage);
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
        public ResponseModel<object> GetMaintenanceQuantityData(UserInfo user, long proposalId, long proposalDetailId, string type, long itemId, int maintId)
        {
            var response = new ResponseModel<object>();
            try
            {
                response.Model = data.GetMaintenanceQuantityData(user, proposalId, proposalDetailId, maintId: maintId,
                    type: type, itemId: itemId);
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
        public ResponseModel<SelectListItem> ListAlternateParts(UserInfo user, long partId, int maintId, long proposalDetailId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListAlternateParts(user, partId, maintId, proposalDetailId);
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
        public ResponseModel<ProposalCampaignModel> GetCampaignData(UserInfo user, long id)
        {
            var response = new ResponseModel<ProposalCampaignModel>();
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
        public ResponseModel<ProposalCampaignModel> AddCampaign(UserInfo user, ProposalCampaignModel model)
        {
            var response = new ResponseModel<ProposalCampaignModel>();
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
        public ResponseModel<LabourModel> GetCampaignLabours(UserInfo user, string campaignCode, long proposalId)
        {
            var response = new ResponseModel<LabourModel>();
            try
            {
                response.Data = data.GetCampaignLabours(user, campaignCode, proposalId);
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
        public ResponseModel<VehicleNoteProposalDetailModel> GetVehicleNote(UserInfo user, int id, long proposalId)
        {
            var response = new ResponseModel<VehicleNoteProposalDetailModel>();
            try
            {
                response.Model = data.GetVehicleNote(user, id, proposalId);
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

        public ResponseModel<VehicleNoteProposalDetailModel> GetVehicleNotePopup(UserInfo user, int id, long proposalId)
        {
            var response = new ResponseModel<VehicleNoteProposalDetailModel>();
            try
            {
                response.Model = data.GetVehicleNotePopup(user, id, proposalId);
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


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        public ResponseModel<VehicleNoteDetailModel> GetDealerNote(UserInfo user, int id, long proposalId, int seq)
        {
            var response = new ResponseModel<VehicleNoteDetailModel>();
            try
            {
                response.Model = data.GetDealerNote(user, id, proposalId, seq);
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
        public ResponseModel<ProposalVehicleNoteModel> AddDealerNote(UserInfo user, ProposalVehicleNoteModel model)
        {
            var response = new ResponseModel<ProposalVehicleNoteModel>();
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
        public ResponseModel<ProposalVehicleNoteModel> AddVehicleNote(UserInfo user, ProposalVehicleNoteModel model)
        {
            var response = new ResponseModel<ProposalVehicleNoteModel>();
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
        public ResponseModel<bool> UpdateFailureCode(UserInfo user, long proposalId, long proposalDetailId, string failureCodeId, out int errorNo, out string errorMessage)
        {
            var response = new ResponseModel<bool>();
            errorNo = 0;
            errorMessage = String.Empty;
            try
            {
                data.UpdateFailureCode(user, proposalId, proposalDetailId, failureCodeId, out errorNo, out errorMessage);
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
        public ResponseModel<ProposalQuantityDataModel> UpdateDuration(UserInfo user, ProposalQuantityDataModel model)
        {
            var response = new ResponseModel<ProposalQuantityDataModel>();
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
        public ResponseModel<SelectListItem> ListDetailProcessTypes(UserInfo user, string indicatorTypeCode, long proposalId, int seq)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListDetailProcessTypes(user, indicatorTypeCode, proposalId, seq);
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
        public ResponseModel<string> GetProposalDetailDescription(long proposalDetailId)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.GetTechicalDescription(proposalDetailId);
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
        public ResponseModel<ModelBase> UpdateProposalDetailDescription(UserInfo user, long proposalDetailId, string desc)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.UpdateTechicalDescription(user, proposalDetailId, desc);
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
        public ResponseModel<string> GetProposalContactInfo(long id)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.GetProposalContactInfo(id);
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
        public ResponseModel<ModelBase> UpdateProposalContactInfo(UserInfo user, long id, string note)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.UpdateProposalContactInfo(user, id, note);
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
        public ResponseModel<ModelBase> UpdateCampaignDenyReason(UserInfo user, long id, string note)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.UpdateProposalCampaignDenyReason(user, id, note);
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
        public ResponseModel<string> GetProposalCampaignDenyReason(long id)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.GetProposalCampaignDenyReason(id);
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
        public ResponseModel<PartReturnModel> ListDetailPartReturnItems(UserInfo user, long proposalDetailId)
        {
            var response = new ResponseModel<PartReturnModel>();
            try
            {
                response.Data = data.ListDetailPartReturnItems(user, proposalDetailId);
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
        public ResponseModel<ModelBase> ReturnDetailParts(UserInfo user, List<PartReturnModel> list, long proposalDetailId)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Data = data.ReturnDetailParts(user, list, proposalDetailId);
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
        public ResponseModel<ModelBase> PickDetailParts(UserInfo user, long id, long proposalDetailId)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.PickDetailParts(user, id, proposalDetailId);
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
        public ResponseModel<ModelBase> ChangeProposalStat(UserInfo user, long proposalId, int stat, int seq)
        {

            var response = new ResponseModel<ModelBase>();
            try
            {
                int errorNo = 0;
                string errorMessage = String.Empty;
                data.ChangeProposalStatus(user, proposalId, stat, seq, out errorNo, out errorMessage);
                response.Model = new ModelBase()
                {
                    ErrorNo = errorNo,
                    ErrorMessage = errorMessage
                };
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }
        public ResponseModel<bool> UpdateProposalSparePartId(long proposalId, int sparePartSaleId, int seq)
        {
            var response = new ResponseModel<bool>();
            try
            {
                int errorNo = 0;
                string errorMessage = String.Empty;
                data.UpdateProposalSparePartId(proposalId, sparePartSaleId, seq);
                response.Model = true;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;

        }
        public ResponseModel<ProposalCardModel> ProposalRevision(UserInfo user, long id, int seq)
        {
            var response = new ResponseModel<ProposalCardModel>();
            try
            {
                response.Model = data.ProposalRevision(user, id, seq);
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
        public ResponseModel<ModelBase> CreateCampaignRequest(UserInfo user, long proposalDetailId)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.CreateCampaignRequest(user, proposalDetailId);
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
        public ResponseModel<PartModel> ListCampaignRequestDetails(UserInfo user, long proposalDetailId, ProposalCampaignRequestViewModel cr)
        {
            var response = new ResponseModel<PartModel>();
            try
            {
                response.Data = data.ListCampaignRequestDetails(user, proposalDetailId, cr);
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
        public ResponseModel<ChangeProcessTypeModel> GetProcessTypeData(UserInfo user, long proposalDetailId)
        {
            var response = new ResponseModel<ChangeProcessTypeModel>();
            try
            {
                response.Model = data.GetProcessTypeData(user, proposalDetailId);
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
        public ResponseModel<ModelBase> UpdateProcessType(UserInfo user, long proposalDetailId, string processType, bool confirmed)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.UpdateProcessType(user, proposalDetailId, processType, confirmed);
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
        public ResponseModel<DateTime?> GetVehicleLeaveDate(long proposalId)
        {
            var response = new ResponseModel<DateTime?>();
            try
            {
                response.Model = data.GetVehicleLeaveDate(proposalId);
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
        public ResponseModel<bool> CheckVehicleLeaveMandatoryFields(UserInfo user, long proposalId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.CheckVehicleLeaveMandatoryFields(user, proposalId);
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
        public ResponseModel<MandatoryRemovalPart> ListMustRemovedParts(UserInfo user, long proposalId)
        {
            var response = new ResponseModel<MandatoryRemovalPart>();
            try
            {
                response.Data = data.ListMandatoryRemovalPart(user, proposalId);
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
        public ResponseModel<ProposalPartRemovalDto> GetPartRemovalDto(UserInfo user,long proposalDetailId, long partId)
        {
            var response = new ResponseModel<ProposalPartRemovalDto>();
            try
            {
                response.Model = data.GetPartRemovalDto(user,proposalDetailId, partId);
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
                response.Data = data.ListRemovableParts(user, partId);
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
        public ResponseModel<ModelBase> UpdateRemovalInfo(UserInfo user, ProposalPartRemovalDto dto)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.UpdateRemovalInfo(user, dto);
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
        public ResponseModel<ProposalAddPdiPackageModel> AddPdiPackage(UserInfo user, ProposalAddPdiPackageModel model)
        {
            var response = new ResponseModel<ProposalAddPdiPackageModel>();
            try
            {
                data.AddPdiPackage(user, model);
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
        public ResponseModel<ProposalAddPdiPackageModel> UpdatePdiPackage(UserInfo user, ProposalAddPdiPackageModel model)
        {
            var response = new ResponseModel<ProposalAddPdiPackageModel>();
            try
            {
                data.UpdatePdiPackage(user, model);
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
        public ResponseModel<ProposalAddPdiPackageModel> GetPdiPackageData(long id)
        {
            var response = new ResponseModel<ProposalAddPdiPackageModel>();
            try
            {
                response.Model = data.GetPdiPackageData(id);
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
        public ResponseModel<bool> GetPdiVehicleIsControlled(long proposalId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                response.Model = data.GetPdiVehicleIsControlled(proposalId);
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
        public List<PdiResultItem> ListPdiResultItems(UserInfo user,long proposalId)
        {
            var list = new ProposalCardData().ListPdiResultItems(user,proposalId);
            list.ForEach(c => c.IsAppropriate = !c.IsGroupCode ? !c.IsAppropriate : c.IsAppropriate);
            return list;
        }
        public Tuple<string, string, List<SelectListItem>, List<SelectListItem>, List<SelectListItem>> GetPdiResultData(UserInfo user,long proposalId, string controlCode)
        {
            return new ProposalCardData().GetPdiResultData(user,proposalId, controlCode);
        }
        public ResponseModel<ModelBase> SavePdiResult(UserInfo user, ProposalPdiResultModel model, string type)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.SavePdiResult(user, model, type);
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
        public ResponseModel<ModelBase> PdiSendToApproval(UserInfo user,long id)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.PdiSendToApproval(user,id);
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
        public ResponseModel<PdiPrintModel> GetPdiPackageDetails(UserInfo  user,long id)
        {
            var response = new ResponseModel<PdiPrintModel>();
            try
            {
                response.Model = data.GetPdiPackageDetails(user,id);
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
        public ResponseModel<ModelBase> DeleteDetailItem(UserInfo user,long proposalId, long proposalDetailId, string type, long itemId)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                
                response.Model = data.DeleteDetailItem(user, proposalId, proposalDetailId, type, itemId);
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
        public string GetVehicleHistoryToolTipContent(int vehicleId)
        {
            var model = new ProposalCardData().GetGetVehicleHistoryLastItem(vehicleId);
            
            return model == null ? NoHistoryData() : GetToolTipHtml(model);
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
        public ResponseModel<SelectListItem> ListVehicleHourMaints(UserInfo user,int vehicleId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListVehicleHourMaints(user, vehicleId);
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
        public ResponseModel<ModelBase> CompletePdiVehicleResult(UserInfo user,long proposalId)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.CompletePdiVehicleResult(user, proposalId);
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
        public ResponseModel<PickingCancellationItem> ListPickingsForCancellation(UserInfo user,long proposalId)
        {
            var response = new ResponseModel<PickingCancellationItem>();
            try
            {
                response.Data = data.ListPickingsForCancellation(user,proposalId);
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
        public ResponseModel<bool> CancelPicking(UserInfo user,long pickingId, long? proposalId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.CancelPicking(user, proposalId, pickingId);
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
        public ResponseModel<ModelBase> CancelVehicleLeave(UserInfo user, long proposalId, string reason)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.CancelVehicleLeave(user, proposalId, reason);
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
        public ResponseModel<bool> SaveGeneralInfo(UserInfo user, GeneralInfo model)
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.SaveGeneralInfo(user, model);
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

        public ResponseModel<bool> SaveTechnicalInfo(UserInfo user, GeneralInfo model)
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.SaveTechnicalInfo(user, model);
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


    }
}
