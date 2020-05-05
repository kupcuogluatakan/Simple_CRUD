using System.Collections.Generic;
using System.Linq;
using ODMSCommon;
using ODMSData;
using ODMSModel.VehicleType;
using System.Web.Mvc;
using ODMSCommon.Security;
using ODMSCommon.Resources;

namespace ODMSBusiness
{
    public class VehicleTypeBL : BaseBusiness
    {
        private readonly VehicleTypeData data = new VehicleTypeData();

        public ResponseModel<VehicleTypeListModel> GetVehicleTypeList(UserInfo user, VehicleTypeListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleTypeListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetVehicleTypeList(user, filter, out totalCnt);
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

        public ResponseModel<VehicleTypeIndexViewModel> GetVehicleType(UserInfo user, VehicleTypeIndexViewModel filter)
        {
            var response = new ResponseModel<VehicleTypeIndexViewModel>();
            try
            {
                response.Model = data.GetVehicleType(user, filter);
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

        public ResponseModel<VehicleTypeIndexViewModel> DMLVehicleType(UserInfo user, VehicleTypeIndexViewModel model)
        {
            var response = new ResponseModel<VehicleTypeIndexViewModel>();
            try
            {
                data.DMLVehicleType(user, model);
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

        public static ResponseModel<SelectListItem> ListVehicleTypeAsSelectList(UserInfo user, string vehicleModel)
        {
            var data = new VehicleTypeData();
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListVehicleTypeAsSelectList(user, vehicleModel);
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

        public ResponseModel<List<VehicleTypeXMLViewModel>> XMLtoDBVehicleType(List<VehicleTypeXMLViewModel> listModel)
        {

            var response = new ResponseModel<List<VehicleTypeXMLViewModel>>();
            try
            {
                var listOldModel = data.GetVehicleTypeListForXML();

                var deleteListModel = listOldModel.Where(
                    oldModel => !(listModel.FindIndex(q => q.ModelSSID == oldModel.ModelSSID) >= 0))
                    .Select(q => { q.CommandType = CommonValues.DMLType.Delete; return q; })
                    .ToList();

                listModel.AddRange(deleteListModel);

                data.XMLtoDBVehicleType(listModel);
                if (listModel.Any(x => x.ErrorNo > 0))
                    throw new System.Exception(listModel.FirstOrDefault().ErrorMessage);

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
