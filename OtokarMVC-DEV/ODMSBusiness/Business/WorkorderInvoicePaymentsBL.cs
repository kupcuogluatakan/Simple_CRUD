using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.PaymentType;
using ODMSModel.WorkorderInvoicePayments;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class WorkorderInvoicePaymentsBL : BaseBusiness
    {
        private readonly WorkorderInvoicePaymentsData data = new WorkorderInvoicePaymentsData();

        public ResponseModel<WorkorderInvoicePaymentsIndexModel> GetWorkorderInvoicePaymentsIndexModel(UserInfo user, int workorderInvoiceId, int workorderId)
        {
            var response = new ResponseModel<WorkorderInvoicePaymentsIndexModel>();
            try
            {
                response.Model = data.GetWorkorderInvoicePaymentsIndexModel(user, workorderInvoiceId, workorderId);
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

        public ResponseModel<PaymentTypeListModel> GetPaymentTypeList(UserInfo user)
        {
            var response = new ResponseModel<PaymentTypeListModel>();
            try
            {
                response.Data = data.GetPaymentTypeList(user);
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

        public ResponseModel<SelectListItem> GetBankList()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetBankList();
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

        public ResponseModel<WorkorderInvoicePaymentsDetailModel> DMLWorkorderInvoicePayments(UserInfo user, WorkorderInvoicePaymentsDetailModel model)
        {
            var response = new ResponseModel<WorkorderInvoicePaymentsDetailModel>();
            try
            {
                data.DMLWorkorderInvoicePayments(user, model);
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

        public ResponseModel<WorkorderInvoicePaymentsDetailModel> GetWorkorderInvoicePayments(UserInfo user, WorkorderInvoicePaymentsDetailModel filter)
        {
            var response = new ResponseModel<WorkorderInvoicePaymentsDetailModel>();
            try
            {
                response.Model = data.GetWorkorderInvoicePayments(user, filter);
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

        public ResponseModel<WorkorderInvoicePaymentsListModel> ListWorkorderInvoicePayments(UserInfo user, WorkorderInvoicePaymentsListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<WorkorderInvoicePaymentsListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkorderInvoicePayments(user, filter, out totalCnt);
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


    }
}
