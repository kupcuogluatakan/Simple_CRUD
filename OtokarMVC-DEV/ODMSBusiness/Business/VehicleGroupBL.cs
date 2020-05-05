using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSModel.VehicleGroup;
using ODMSCommon.Security;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class VehicleGroupBL : BaseBusiness
    {
        private readonly VehicleGroupData data = new VehicleGroupData();

        public ResponseModel<VehicleGroupListModel> GetVehicleGroupList(UserInfo user, VehicleGroupListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleGroupListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetVehicleGroupList(user, filter, out totalCnt);
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

        public ResponseModel<VehicleGroupIndexViewModel> GetVehicleGroup(UserInfo user, VehicleGroupIndexViewModel filter)
        {

            var response = new ResponseModel<VehicleGroupIndexViewModel>();
            try
            {
                response.Model = data.GetVehicleGroup(user, filter);
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

        public ResponseModel<VehicleGroupIndexViewModel> DMLVehicleGroup(UserInfo user, VehicleGroupIndexViewModel model)
        {
            var response = new ResponseModel<VehicleGroupIndexViewModel>();
            try
            {
                data.DMLVehicleGroup(user, model);
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


        public ResponseModel<List<VehicleGroupXMLViewModel>> XMLtoDBVehicleGroup(List<VehicleGroupXMLViewModel> listModel)
        {
            var response = new ResponseModel<List<VehicleGroupXMLViewModel>>();
            try
            {
                data.XMLtoDBVehicleGroup(listModel);
                response.Model = listModel;
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

        public static ResponseModel<SelectListItem> ListVehicleGroupAsSelectList(UserInfo user)
        {
            VehicleGroupData data = new VehicleGroupData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListVehicleGroupAsSelectList(user);
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
