using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSData.Utility;
using ODMSModel.WorkOrder;
using ODMSModel.WorkOrderCard;
using ODMSModel;
using ODMSModel.GuaranteeRequestApproveDetail;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.VehicleBodywork;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class WorkOrderBL : BaseBusiness
    {
        private readonly WorkOrderData data = new WorkOrderData();
        private readonly AppointmentTypeData dataAppointmentType = new AppointmentTypeData();
        private readonly DealerData dataDealer = new DealerData();
        

        public static ResponseModel<SelectListItem> ListWorkOrderAsSelectListItem(int? vehicleId, int? dealerId)
        {
            var data = new WorkOrderData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListWorkOrderAsSelectListItem(vehicleId, dealerId);
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

        public ResponseModel<WorkOrderListModel> ListWorkOrders(UserInfo user,WorkOrderListModel model, out int totalCnt)
        {
            var response = new ResponseModel<WorkOrderListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListWorkOrders(user,model, out totalCnt);
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

        public ResponseModel<WorkOrderViewModel> DMLWorkOrder(UserInfo user, WorkOrderViewModel model)
        {
            var response = new ResponseModel<WorkOrderViewModel>();
            try
            {
                data.DMLWorkOrder(user, model);
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

        public ResponseModel<WorkOrderViewModel> GetWorkOrder(UserInfo user, long workOrderId)
        {
            var response = new ResponseModel<WorkOrderViewModel>();
            try
            {
                response.Model = data.GetWorkOrder(user, workOrderId);
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

        public ResponseModel<SelectListItem> ListAppointmentTypes(UserInfo user)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataAppointmentType.ListAppointmentTypeAsSelectListItems(user);
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

        public ResponseModel<object> GetWorkOrderData(int id, string type)
        {
            var response = new ResponseModel<object>();
            try
            {
                response.Model = data.GetWorkOrderData(id, type);
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
        /// Bayinin son iş emri numarasını verir.
        /// </summary>
        /// <param name="dealerId">Bayi Id</param>
        public ResponseModel<int?> GetLastWorkOrderId(int dealerId)
        {
            var response = new ResponseModel<int?>();
            try
            {
                response.Model = data.GetLastWorkOrderId(dealerId);
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
        ///  Bayinin son iş emri detay numarasını verir.
        /// </summary>
        /// <param name="workOrderId">İş emri no</param>
        /// <returns></returns>
        public ResponseModel<int?> GetLastWorkOrderDetailId(int workOrderId)
        {
            var response = new ResponseModel<int?>();
            try
            {
                response.Model = data.GetLastWorkOrderDetailId(workOrderId);
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
        /// Bayinin son bekleyen toplama emri numarasını verir
        /// </summary>
        /// <param name="dealerId">Bayi Id</param>
        /// <returns></returns>
        public ResponseModel<int?> GetLastWorkOrderPickingId(int dealerId)
        {
            var response = new ResponseModel<int?>();
            try
            {
                response.Model = data.GetLastWorkOrderPickingId(dealerId);
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
        /// Bayinin son bekleyen toplama emri detay numarasını verir
        /// </summary>
        /// <param name="dealerId">Bayi Id</param>
        /// <returns></returns>
        public ResponseModel<int?> GetLastWorkOrderPickingDetailId(int pickingId)
        {
            var response = new ResponseModel<int?>();
            try
            {
                response.Model = data.GetLastWorkOrderPickingDetailId(pickingId);
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

        public ResponseModel<SelectListItem> GetDealerUsers(int dealerId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = dataDealer.GetDealerUsersAsSelectListItem(dealerId);
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

        public ResponseModel<WorkOrderCardModel> GetWorkOrderViewModel(UserInfo user, long workOrderId)
        {
            var response = new ResponseModel<WorkOrderCardModel>();
            try
            {
                response.Model = data.GetWorkOrderCard(user, workOrderId);
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

        public ResponseModel<int> CheckFleet(UserInfo user, int customerId, int vehicleId)
        {
            var response = new ResponseModel<int>();
            try
            {
                response.Model = data.CheckFleet(user, customerId, vehicleId);
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

        public ResponseModel<ModelBase> CancelWorkOrder(UserInfo user, WorkOrderCancelModel model)
        {
            var response = new ResponseModel<ModelBase>();
            try
            {
                response.Model = data.CancelWorkOrder(user, model);
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

        public ResponseModel<VehicleBodyworkViewModel> GetBodyworkFromVehicle(VehicleBodyworkViewModel filter)
        {

            var response = new ResponseModel<VehicleBodyworkViewModel>();
            try
            {
                data.GetBodyworkFromVehicle(filter);
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

        public ResponseModel<VehicleBodyworkViewModel> DMLBodyWorkForWorkOrder(UserInfo user, VehicleBodyworkViewModel model)
        {
            var response = new ResponseModel<VehicleBodyworkViewModel>();
            try
            {
                data.DMLBodyWorkForWorkOrder(user, model);
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

        public ResponseModel<int> GetVehicleCustomerId(int vehicleId)
        {
            var response = new ResponseModel<int>();
            try
            {
                response.Model = data.GetVehicleCustomerId(vehicleId);
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

        public ResponseModel<CustomerChangeModel> GetCustomerChangeData(int customerId, int vehicleCustomerId)
        {
            var response = new ResponseModel<CustomerChangeModel>();
            try
            {
                response.Model = data.GetCustomerChangeData(customerId, vehicleCustomerId);
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

        public static ResponseModel<SelectListItem> ListWorkOrderStatus(UserInfo user)
        {
            var data = new WorkOrderData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListWorkOrderStatus(user);
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

        public ResponseModel<PeriodicMaintHistoryModel> GetPeriodicMaintHistory(long workOrderId)
        {
            var response = new ResponseModel<PeriodicMaintHistoryModel>();
            try
            {
                response.Data = data.GetPeriodicMaintHistory(workOrderId);
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
