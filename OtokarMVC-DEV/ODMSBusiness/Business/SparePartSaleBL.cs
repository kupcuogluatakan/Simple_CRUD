using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSData.Utility;
using ODMSModel.PaymentType;
using ODMSModel.SparePartSale;

namespace ODMSBusiness
{
    public class SparePartSaleBL : BaseBusiness
    {
        private readonly SparePartSaleData data = new SparePartSaleData();
        private readonly WorkorderInvoicePaymentsData dataWorkorderInvoicePayments = new WorkorderInvoicePaymentsData();
        private readonly CommonBL commonBL = new CommonBL();

        public ResponseModel<SparePartSaleViewModel> DMLSparePartSale(UserInfo user, SparePartSaleViewModel model)
        {
            var response = new ResponseModel<SparePartSaleViewModel>();
            try
            {
                data.DMLSparePartSale(user, model);
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
        public ResponseModel<bool> DMLSparePartSaleOtokar(UserInfo user, OtokarSparePartSaleViewModel model)
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.DMLSparePartSaleOtokar(user, model);
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
        public ResponseModel<SelectListItem> ListCustomerTypes(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                //TODO : Exception atılmış bakılacak
                response.Data = CommonBL.ListLookup(user, CommonValues.LookupKeys.CustomerTypeLookup).Data;
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

        public List<SelectListItem> ListCustomerAddresses(UserInfo user, int customerId, bool invoiceAddressOnly = false)
        {
            return
                new CustomerAddressData().ListCustomerAddressesAsSelectListItems(user, customerId, invoiceAddressOnly)
                    .Select(c => new SelectListItem { Text = c.Text.ToUpper(), Value = c.Value }).ToList();
        }

        public ResponseModel<PaymentTypeListModel> ListPaymentTypes(UserInfo user)
        {
            var response = new ResponseModel<PaymentTypeListModel>();
            try
            {
                response.Data = dataWorkorderInvoicePayments.GetPaymentTypeList(user);
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
        public ResponseModel<SelectListItem> ListBanks()
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataWorkorderInvoicePayments.GetBankList();
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

        public ResponseModel<SelectListItem> ListDueDurations()
        {
            return new WorkOrderInvoicesBL().ListDueDuration();
        }

        public ResponseModel<SparePartSaleListModel> ListSparePartSales(UserInfo user, SparePartSaleListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<SparePartSaleListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSparePartSales(user, filter, out totalCnt);
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

        public ResponseModel<SparePartSaleViewModel> GetSparePartSale(UserInfo user, int id)
        {
            var response = new ResponseModel<SparePartSaleViewModel>();
            try
            {
                response.Model = data.GetSparePartSale(user, id);
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
        public ResponseModel<OtokarSparePartSaleViewModel> GetSparePartSaleOtokar(UserInfo user, int id)
        {
            var response = new ResponseModel<OtokarSparePartSaleViewModel>();
            try
            {
                response.Model = data.GetSparePartSaleOtokar(user, id);
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
        public ResponseModel<string> ExecInvoiceOp(UserInfo user, int sparePartSaleId)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.ExecInvoiceOp(user, sparePartSaleId);
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

        public ResponseModel<string> ExecInvoiceOpMultiple(UserInfo user, int sparePartSaleWaybillId)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.ExecInvoiceOpMultiple(user, sparePartSaleWaybillId);
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

        public ResponseModel<bool> ChangeStatusAfterPickCancel(UserInfo user, int sparePartSaleId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                data.ChangeStatusAfterPickCancel(user, sparePartSaleId);
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

        public ResponseModel<SparePartSaleListModel> ListWayBill(UserInfo user, int sparePartSaleId)
        {
            var response = new ResponseModel<SparePartSaleListModel>();
            try
            {
                response.Data = data.ListSparePartSaleWaybill(user, sparePartSaleId);
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
