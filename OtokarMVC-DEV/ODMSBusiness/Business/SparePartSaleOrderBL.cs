using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSData;
using ODMSData.Utility;
using ODMSModel;
using ODMSModel.SparePartSaleOrder;
using ODMSModel.SparePartSaleOrderDetail;

namespace ODMSBusiness.Business
{
    public class SparePartSaleOrderBL : BaseBusiness
    {
        private readonly SparePartSaleOrderData data = new SparePartSaleOrderData();
        private readonly DbHelper _dbHelper;

        public SparePartSaleOrderBL()
        {
            _dbHelper = new DbHelper();
        }
        public ResponseModel<SparePartSaleOrderViewModel> DMLSparePartSaleOrder(UserInfo user, SparePartSaleOrderViewModel model)
        {
            var response = new ResponseModel<SparePartSaleOrderViewModel>();
            try
            {
                data.DMLSparePartSaleOrder(user, model);
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

        public ResponseModel<SparePartSaleOrderListModel> ListSparePartSaleOrders(UserInfo user, SparePartSaleOrderListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<SparePartSaleOrderListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSparePartSaleOrders(user, filter, out totalCnt);
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

        public SparePartSaleOrderViewModel GetSparePartSaleOrder(UserInfo user, string soNumber)
        {
            return data.GetSparePartSaleOrder(user, soNumber);
        }

        public ResponseModel<string> GetSparePartSaleOrderLatestStatus(string soNumber)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.GetSparePartSaleOrderLatestStatus(soNumber);
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

        public ResponseModel<ModelBase> CreateSaleOrder(UserInfo user,List<SparePartSaleOrderDetailListModel> list)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.CreateSaleOrder(user,list);
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
