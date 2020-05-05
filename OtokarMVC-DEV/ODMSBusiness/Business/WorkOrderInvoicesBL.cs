using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ODMSCommon;
using ODMSData;
using ODMSData.Utility;
using ODMSModel;
using ODMSModel.WorkOrderCard;
using ODMSModel.WorkOrderInvoice;
using System;
using ODMSCommon.Security;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class WorkOrderInvoicesBL : BaseBusiness
    {
        private readonly WorkOrderInvoicesData data = new WorkOrderInvoicesData();
        private readonly WorkOrderData dataWorkOrder = new WorkOrderData();
        private readonly CustomerAddressData dataCustomerAddress = new CustomerAddressData();

        [BusinessLog]
        public ResponseModel<WorkOrderInvoicesListModel> ListWorkOrderInvoices(UserInfo user,WorkOrderInvoicesListModel model, out int totalCnt)
        {
            var response = new ResponseModel<WorkOrderInvoicesListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkOrderInvoices(user,model, out totalCnt);
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
        public ResponseModel<WorkOrderInvoicesViewModel> DMLWorkOrderInvoices(UserInfo user, WorkOrderInvoicesViewModel model)
        {
            var response = new ResponseModel<WorkOrderInvoicesViewModel>();
            try
            {
                model.InvoiceNo = model.InvoiceNo.ToUpperInvariant();
                model.InvoiceSerialNo = model.InvoiceSerialNo.ToUpperInvariant();
                data.DMLWorkOrderInvoices(user, model);
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
        public ResponseModel<WorkOrderInvoicesViewModel> ChangeWorkOrderStatusToFinish(UserInfo user, WorkOrderInvoicesViewModel model)
        {
            var response = new ResponseModel<WorkOrderInvoicesViewModel>();
            try
            {
                const int statusId = CommonValues.WorkOrderStatusInvoiceFinishId;
                string errorMessage;
                int errorNo;
                dataWorkOrder.ChangeWorkOrderStatus(user, model.WorkOrderId, statusId, out errorNo, out errorMessage);
                response.Model = model;
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

        [BusinessLog]
        public ResponseModel<WorkOrderInvoicesViewModel> GetWorkOrderInvoices(UserInfo user, long workOrderInvoiceId)
        {
            var response = new ResponseModel<WorkOrderInvoicesViewModel>();
            try
            {
                response.Model = data.GetWorkOrderInvoice(user, workOrderInvoiceId);
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
        public ResponseModel<WorkOrderInvoiceDTO> GetWorkOrderInvoiceAmount(long workOrderId, int customerId, long invoiceId, string workOrderDetailIds)
        {
            var response = new ResponseModel<WorkOrderInvoiceDTO>();
            try
            {
                response.Model = data.GetWorkOrderInvoiceAmount(workOrderId, customerId, invoiceId, workOrderDetailIds);
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
        public ResponseModel<SelectListItem> ListCutomerAddresses(UserInfo user, int customerId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataCustomerAddress.ListCustomerAddressesAsSelectListItems(user, customerId, invoiceAddressOnly: false).Select(c => new SelectListItem { Text = c.Text.ToUpper(), Value = c.Value }).ToList();
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

        public ResponseModel<SelectListItem> ListDueDuration()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                var list = new List<SelectListItem>
                    {
                            new SelectListItem {Text = "0",Value = "0"},
                            new SelectListItem{Text = "1",Value = "1"},
                            new SelectListItem{Text = "2",Value = "2"},
                            new SelectListItem{Text = "3",Value = "3"},
                            new SelectListItem{Text = "4",Value = "4"},
                            new SelectListItem{Text = "5",Value = "5"},
                            new SelectListItem{Text = "6",Value = "6"},
                            new SelectListItem{Text = "7",Value = "7"},
                            new SelectListItem{Text = "8",Value = "8"},
                            new SelectListItem{Text = "9",Value = "9"},
                            new SelectListItem{Text = "10",Value = "10"},
                            new SelectListItem{Text = "11",Value = "11"},
                            new SelectListItem{Text = "12",Value = "12"}
                    };

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

        [BusinessLog]
        //dealerdan countryid bilgisini alıp o ülke için tevkifatları çekiyoruz....
        public ResponseModel<SelectListItem> GetWitholdingListForDealer(int dealerId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.GetWitholdingListForDealer(dealerId);
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
        public ResponseModel<WorkOrderInvoiceItem> ListWorkOrderInvoiceItems(UserInfo user, long workOrderId, string workOrderDetailds)
        {
            var response = new ResponseModel<WorkOrderInvoiceItem>();
            try
            {
                response.Data = data.ListWorkOrderInvoiceItems(user, workOrderId, workOrderDetailds);
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
        public ResponseModel<ModelBase> UpdateInvoiceIds(UserInfo user, long workOrderId, long invoiceId, string worOrderDetailIds, string commandType, out string invType)
        {
            var response = new ResponseModel<ModelBase>();
            invType = string.Empty;
            try
            {
                response.Model = data.UpdateInvoiceIds(user, workOrderId, invoiceId, worOrderDetailIds, commandType, out invType);
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
        public ResponseModel<WorkOrderInvoicesListModel> ListInvoices(UserInfo user, long id, string workOrderDetailds)
        {
            var response = new ResponseModel<WorkOrderInvoicesListModel>();
            try
            {
                response.Data = data.ListInvoices(user, id, workOrderDetailds);
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
        public ResponseModel<Tuple<string, string, decimal>> GetSuggestedInvoiceData(long workOrderId, string workOrderDetailIds)
        {
            var response = new ResponseModel<Tuple<string, string, decimal>>();
            try
            {
                response.Model = data.GetSuggestedInvoiceData(workOrderId, workOrderDetailIds);
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
        public ResponseModel<ModelBase> SetBillingStatus(UserInfo user, long workOrderDetailId, bool invoiceCancel)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.SetBillingStatus(user, workOrderDetailId, invoiceCancel);
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
        /// İş emri detayının faturasını siler
        /// </summary>
        /// <param name="workOrderDetailId">İş emri detay Id</param>
        [BusinessLog]
        public ResponseModel<ModelBase> WorkOrderDetailInvoiceDelete(int workOrderDetailId)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.WorkOrderDetailInvoiceDelete(workOrderDetailId);
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
        /// İş emrinin son faturasını verir.
        /// </summary>
        /// <param name="workOrderId">İş Emri No</param>
        [BusinessLog]
        public ResponseModel<int> GetLastWorkOrderInvoiceId(int workOrderId)
        {
            var response = new ResponseModel<int>();
            try
            {
                response.Model = data.GetLastWorkOrderInvoiceId(workOrderId);
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
