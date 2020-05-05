using System.Collections.Generic;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.Guarantee;
using System;
using System.Data;
using ODMSData.Utility;
using ODMSCommon;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class GuaranteeBL : BaseBusiness
    {
        private readonly GuaranteeData data = new GuaranteeData();
        private readonly CommonData dataCommon = new CommonData();

        public ResponseModel<GuaranteeXMLModel> GetGuarantee(UserInfo user)
        {
            var response = new ResponseModel<GuaranteeXMLModel>();
            try
            {
                response.Data = data.GetGuarantee(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;

                foreach (var model in response.Data)
                {
                    model.PartsColumn = data.GetGuaranteeParts(model.GuaranteeId, model.GuaranteeSeq);
                    model.LaboursColumn = data.GetGuaranteeLabours(model.GuaranteeId, model.GuaranteeSeq);
                    model.ExternalLaboursColumn = data.GetGuaranteeExternalLabours(model.GuaranteeId, model.GuaranteeSeq);
                }
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<string> SetSuccessResultGif(string guaranteeId, string guaranteeSeq, long gifNo)
        {
            var response = new ResponseModel<string>();
            try
            {
                data.SetSuccessResultGif(guaranteeId, guaranteeSeq, gifNo);
                response.Model = guaranteeId;
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

        public ResponseModel<string> SendFailMailDb(string gNo, string gSeq, string workOrderId, long logId, string innerText)
        {
            var response = new ResponseModel<string>();
            try
            {
                var mailList = dataCommon.GetGeneralParameterValue("GOS_FAIL_MAIL_LIST");
                string link = CommonBL.GetGeneralParameterValue("DMS_ROOT_URL").Model + "WebServiceErrorDisplay/WebServiceErrorDisplayIndex/" + logId;

                response.Model = dataCommon.SendDBMail(mailList, MessageResource.Mail_Subject_Gif, string.Format(MessageResource.Error_DB_DBUnexpected, gNo, gSeq, innerText, workOrderId, logId) + Environment.NewLine + link);
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

        public ResponseModel<string> GetGuaranteeCampaignSsidCsv()
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.GetGuaranteeCampaignSsidCsv();
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

        public ResponseModel<DataTable> UpdateCampaignPriceXml(DataTable model)
        {
            var response = new ResponseModel<DataTable>();
            try
            {
                data.UpdateCampaignPriceXml(model);
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


    }
}
