using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ODMSCommon;
using ODMSData;
using ODMSModel.VehicleModel;
using ODMSCommon.Security;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class VehicleModelBL : BaseBusiness
    {
        private readonly VehicleModelData data = new VehicleModelData();

        public ResponseModel<VehicleModelListModel> GetVehicleModelList(UserInfo user, VehicleModelListModel vehicleMModel, out int totalCnt)
        {

            var response = new ResponseModel<VehicleModelListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetVehicleModelList(user, vehicleMModel, out totalCnt);
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

        public ResponseModel<VehicleModelIndexViewModel> GetVehicleModel(UserInfo user, VehicleModelIndexViewModel filter)
        {
            var response = new ResponseModel<VehicleModelIndexViewModel>();
            try
            {
                data.GetVehicleModel(user, filter);
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

        public ResponseModel<VehicleModelIndexViewModel> DMLVehicleModel(UserInfo user, VehicleModelIndexViewModel model)
        {
            var response = new ResponseModel<VehicleModelIndexViewModel>();
            try
            {
                data.DMLVehicleModel(user, model);
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

        public static ResponseModel<SelectListItem> ListVehicleModelAsSelectList(UserInfo user)
        {
            VehicleModelData vehicleMData = new VehicleModelData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = vehicleMData.ListVehicleModelAsSelectList(user);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), vehicleMData.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public static ResponseModel<SelectListItem> ListVehicleModelAsSelectList(UserInfo user, int vehicleGroupId)
        {
            VehicleModelData vehicleMData = new VehicleModelData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = vehicleMData.ListVehicleModelAsSelectList(user, vehicleGroupId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), vehicleMData.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }

            return response;
        }

        public ResponseModel<List<VehicleModelXMLViewModel>> XMLtoDBVehicleModel(List<VehicleModelXMLViewModel> listModel)
        {
            var response = new ResponseModel<List<VehicleModelXMLViewModel>>();
            try
            {
                var listOldModel = data.GetVehicleModelListForXML();

                var deleteListModel = listOldModel.Where(
                    oldModel => !(listModel.FindIndex(q => q.ModelSSID == oldModel.ModelSSID) >= 0))
                    .Select(q => { q.CommandType = CommonValues.DMLType.Delete; return q; })
                    .ToList();

                listModel.AddRange(deleteListModel);

                data.XMLtoDBVehicleModel(listModel);

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



    }
}
