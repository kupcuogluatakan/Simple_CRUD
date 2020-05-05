using System.Collections.Generic;
using ODMSData;
using ODMSModel.ObjectSearch;
using ODMSModel.PurchaseOrderDetail;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class ObjectSearchBL : BaseBusiness
    {
        private readonly ObjectSearchData data = new ObjectSearchData();

        public ResponseModel<CustomerSearchListModel> SearchCustomer(UserInfo user, CustomerSearchListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<CustomerSearchListModel>();
            totalCnt = 0;
            try
            {
                response.Data = (filter.OrgTypeId == 1) ? data.SearchDealer(user, filter, out totalCnt) : data.SearchCustomer(user, filter, out totalCnt);
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

        public ResponseModel<AppointmentIndicatorSubCategorySearchListModel> SearchAppointmentIndicatorSubCategory(UserInfo user, AppointmentIndicatorSubCategorySearchListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<AppointmentIndicatorSubCategorySearchListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.SearchAppointmentIndicatorSubCategory(user, filter, out totalCnt);
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

        public ResponseModel<VehicleSearchListModel> SearchVehicle(UserInfo user, VehicleSearchListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleSearchListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.SearchVehicle(user, filter, out totalCnt);
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

        public ResponseModel<string> GetObjectTextWithId(UserInfo user, CommonValues.ObjectSearchType filter, long objectId)
        {
            var response = new ResponseModel<string>();
            try
            {
                response.Model = data.GetObjectText(user, filter, objectId);
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

        public ResponseModel<AppointmentSearchListModel> SearchAppointment(UserInfo user, AppointmentSearchListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<AppointmentSearchListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.SearchAppointment(user, filter, out totalCnt);
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

        public ResponseModel<FleetSearchListModel> SearchFleet(UserInfo user, FleetSearchListModel filter)
        {
            var response = new ResponseModel<FleetSearchListModel>();
            try
            {
                response.Data = data.SearchFleet(user, filter);
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

        public ResponseModel<PurchaseOrderSearchListModel> SearchPurchaseOrder(UserInfo user, PurchaseOrderSearchListModel filter)
        {
            var response = new ResponseModel<PurchaseOrderSearchListModel>();
            try
            {
                response.Data = data.SearchPurchaseOrder(user, filter);
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

        public ResponseModel<PurchaseOrderDetailListModel> SearchPurchaseOrderDetails(UserInfo user, PurchaseOrderDetailListModel filter)
        {
            var response = new ResponseModel<PurchaseOrderDetailListModel>();
            var total = 0;
            try
            {
                response.Data = data.ListPurchaseOrderDetails(user, filter, out total);
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
