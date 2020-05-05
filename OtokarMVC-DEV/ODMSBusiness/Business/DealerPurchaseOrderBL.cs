using System.Collections.Generic;
using ODMSData;
using ODMSModel.DealerPurchaseOrder;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.DealerSaleSparepart;

namespace ODMSBusiness
{
    public class DealerPurchaseOrderBL : BaseBusiness
    {
        private readonly DealerPurchaseOrderData data = new DealerPurchaseOrderData();

        public ResponseModel<DealerPurchaseOrderViewModel> DMLDealerPurchaseOrder(UserInfo user, DealerPurchaseOrderViewModel model)
        {
            var response = new ResponseModel<DealerPurchaseOrderViewModel>();
            try
            {
                data.DMLDealerPurchaseOrder(user, model);
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

        public ResponseModel<DealerPurchaseOrderViewModel> GetDealerPurchaseOrder(UserInfo user, DealerPurchaseOrderViewModel filter)
        {
            var response = new ResponseModel<DealerPurchaseOrderViewModel>();
            try
            {
                data.GetDealerPurchaseOrder(user, filter);
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

        public ResponseModel<DealerPurchaseOrderViewModel> GetPartDetails(UserInfo user, DealerPurchaseOrderViewModel filter)
        {
            var response = new ResponseModel<DealerPurchaseOrderViewModel>();
            try
            {
                data.GetPartDetails(user, filter);
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

        public ResponseModel<DealerPurchaseOrderViewModel> CreateDealerPurchasePart(UserInfo user, DealerPurchaseOrderViewModel model)
        {
            var response = new ResponseModel<DealerPurchaseOrderViewModel>();
            try
            {
                model.CommandType = ODMSCommon.CommonValues.DMLType.Insert;
                model.PoDetSeqNo = 0;
                data.DMLDealerPurchasePart(user, model);
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

        public ResponseModel<DealerSaleSparepartListModel> ListDealerOrderPart(UserInfo user,DealerSaleSparepartListModel model, out int totalCount)
        {
            var response = new ResponseModel<DealerSaleSparepartListModel>();
            totalCount = 0;
            try
            {
                response.Data = data.ListDealerOrderPart(user,model, out totalCount);
                response.Total = totalCount;
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

        public ResponseModel<DealerPurchaseOrderViewModel> DeleteDealerPurhcasePart(UserInfo user, DealerPurchaseOrderViewModel model)
        {
            var response = new ResponseModel<DealerPurchaseOrderViewModel>();
            try
            {
                model.CommandType = ODMSCommon.CommonValues.DMLType.Delete;
                model.PurchaseOrderId = 0;
                model.PartId = 0;
                model.Quantity = 0;
                data.DMLDealerPurchasePart(user, model);
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

        public ResponseModel<int> GetVehicleMustByPOType(int poType)
        {
            var response = new ResponseModel<int>();
            try
            {
                response.Model = data.GetVehicleMustByPOType(poType);
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
