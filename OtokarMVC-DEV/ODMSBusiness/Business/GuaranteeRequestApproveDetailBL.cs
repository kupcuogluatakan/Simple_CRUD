using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Mvc;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.GuaranteeRequestApproveDetail;
using ODMSCommon.Security;
using ODMSCommon;

namespace ODMSBusiness
{
    public class GuaranteeRequestApproveDetailBL : BaseBusiness
    {
        private readonly GuaranteeRequestApproveDetailData data = new GuaranteeRequestApproveDetailData();

        public ResponseModel<bool> UpdatePricesOnOpen(UserInfo user, long guaranteeId, int guaranteeSeq)
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.UpdatePricesOnOpen(user, guaranteeId, guaranteeSeq);
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

        [BusinessLog]
        public ResponseModel<GRADMstViewModel> GetGuaranteeInfo(UserInfo user, GRADMstViewModel filter)
        {
            var response = new ResponseModel<GRADMstViewModel>();
            try
            {
                data.GetGuaranteeInfo(user, filter);
                response.Model = filter;
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

        [BusinessLog]
        public ResponseModel<GuaranteePartsListModel> ListGuaranteeParts(UserInfo user, GuaranteePartsListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<GuaranteePartsListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListGuaranteeParts(user, filter, out totalCnt);
                response.Total = totalCnt;
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

        [BusinessLog]
        public ResponseModel<GuaranteeLaboursListModel> ListGuaranteeLabours(UserInfo user, GuaranteeLaboursListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<GuaranteeLaboursListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListGuaranteeLabours(user, filter, out totalCnt);
                response.Total = totalCnt;
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

        [BusinessLog]
        public ResponseModel<GuaranteeDescriptionHistoryListModel> ListGuaranteeDescriptionHistory(GuaranteeDescriptionHistoryListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<GuaranteeDescriptionHistoryListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListGuaranteeDescriptionHistory(filter, out totalCnt);
                response.Total = totalCnt;
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

        [BusinessLog]
        public ResponseModel<SelectListItem> ListRemovalPart(UserInfo user, int partId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListRemovalPart(user, partId);
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

        [BusinessLog]
        public ResponseModel<GuaranteePartsLabourViewModel> DMLSaveGuaranteeParts(UserInfo user, GuaranteePartsLabourViewModel model)
        {
            var response = new ResponseModel<GuaranteePartsLabourViewModel>();
            try
            {
                data.DMLSaveGuaranteeParts(user, model);
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

        [BusinessLog]
        public ResponseModel<GuaranteePartsLabourViewModel> DMLSaveGuaranteeLabour(UserInfo user, GuaranteePartsLabourViewModel model)
        {
            var response = new ResponseModel<GuaranteePartsLabourViewModel>();
            try
            {
                data.DMLSaveGuaranteeLabour(user, model);
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

        [BusinessLog]
        public ResponseModel<GRADMstViewModel> CompleteGuaranteeApprove(UserInfo user, GRADMstViewModel model)
        {
            var response = new ResponseModel<GRADMstViewModel>();
            try
            {
                data.CompleteGuaranteeApprove(user, model);
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

        [BusinessLog]
        public ResponseModel<GRADMstViewModel> GuaranteeUpdateDescription(UserInfo user, GRADMstViewModel model)
        {
            var response = new ResponseModel<GRADMstViewModel>();
            try
            {
                data.GuaranteeUpdateDescription(user, model);
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

        [BusinessLog]
        public ResponseModel<GRADGifHistoryModel> ListGRADGifHistory(UserInfo user, GRADGifHistoryModel filter, out int totalCnt)
        {
            var response = new ResponseModel<GRADGifHistoryModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListGRADGifHistory(user, filter, out totalCnt);
                response.Total = totalCnt;
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

        [BusinessLog]
        public ResponseModel<GRADGifHistoryDetModel> ListGRADGifHistoryDet(UserInfo user, long guaranteeId, out int totalCnt)
        {
            var response = new ResponseModel<GRADGifHistoryDetModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListGRADGifHistoryDet(user, guaranteeId, out totalCnt);
                response.Total = totalCnt;
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

        [BusinessLog]
        public ResponseModel<GRADMstViewModel> CompleteGuaranteeCancel(UserInfo user, GRADMstViewModel model)
        {
            var response = new ResponseModel<GRADMstViewModel>();
            try
            {

                bool IsServiceCalled = true;

                if (!General.IsTest)
                {
                    if (!string.IsNullOrEmpty(model.GifInfo.GifNo))
                    {
                        try
                        {
                            var client = GetClient();
                            DataSet result = client.ZDMS_GIF_IPTAL(ConfigurationManager.AppSettings.Get("PSSCUser"), ConfigurationManager.AppSettings.Get("PSSCPass"), model.GifInfo.GifNo);

                            if (result != null && result.Tables[0].Rows.Count > 0)
                            {
                                IsServiceCalled = result.Tables[0].Rows[0][1].ToString().ToUpper() == "X";
                                if (!IsServiceCalled)
                                {
                                    model.ErrorNo = 1;
                                    model.ErrorMessage = result.Tables[0].Rows[0][0].ToString();
                                }
                            }
                        }
                        catch
                        {
                            IsServiceCalled = false;
                            model.ErrorNo = 1;
                            model.ErrorMessage = MessageResource.Global_Display_Service_Error_Message;
                        }
                    }
                }

                if (IsServiceCalled)
                    data.CompleteGuaranteeCancel(user, model);

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

        [BusinessLog]
        public ResponseModel<Part_Infos> GetPartInfos(long id)
        {
            var response = new ResponseModel<Part_Infos>();
            try
            {
                response.Data = data.GetPartInfos(id);
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

    }
}
