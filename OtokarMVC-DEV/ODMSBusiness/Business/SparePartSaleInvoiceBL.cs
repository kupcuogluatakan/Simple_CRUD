using ODMSData;
using ODMSModel.SparePartSaleInvoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness.Business
{
    public class SparePartSaleInvoiceBL : BaseBusiness
    {
        private readonly SparePartSaleInvoiceData data = new SparePartSaleInvoiceData();

        public ResponseModel<SparePartSaleInvoiceViewModel> DMLSparePartSaleInvoice(UserInfo user,SparePartSaleInvoiceViewModel model)
        {
            var response = new ResponseModel<SparePartSaleInvoiceViewModel>();
            try
            {
                data.DMLSparePartSaleInvoice(user, model);
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

        public ResponseModel<SparePartSaleInvoiceListModel> ListSparePartSaleInvoices(UserInfo user,SparePartSaleInvoiceListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<SparePartSaleInvoiceListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSparePartSaleInvoices(user,filter, out totalCnt);
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

        public ResponseModel<SparePartSaleInvoiceViewModel> GetSparePartSaleInvoice(UserInfo user,int id)
        {
            var response = new ResponseModel<SparePartSaleInvoiceViewModel>();
            try
            {
                response.Model = data.GetSparePartSaleInvoice(user, id);
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
