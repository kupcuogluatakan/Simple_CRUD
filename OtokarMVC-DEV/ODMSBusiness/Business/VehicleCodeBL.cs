using System.Collections.Generic;
using System.Linq;
using ODMSCommon;
using ODMSData;
using ODMSModel.VehicleCode;
using ODMSModel.SpartInfo;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class VehicleCodeBL : BaseBusiness
    {
        private readonly VehicleCodeData data = new VehicleCodeData();

        public ResponseModel<VehicleCodeListModel> GetVehicleCodeList(UserInfo user, VehicleCodeListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleCodeListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetVehicleCodeList(user, filter, out totalCnt);
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

        public ResponseModel<VehicleCodeIndexViewModel> GetVehicleCode(UserInfo user, VehicleCodeIndexViewModel filter)
        {
            var response = new ResponseModel<VehicleCodeIndexViewModel>();
            try
            {
                data.GetVehicleCode(user, filter);
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

        public ResponseModel<VehicleCodeIndexViewModel> DMLVehicleCode(UserInfo user, VehicleCodeIndexViewModel model)
        {
            var response = new ResponseModel<VehicleCodeIndexViewModel>();
            try
            {
                data.DMLVehicleCode(user, model);
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

        public ResponseModel<List<VehicleCodeXMLViewModel>> XMLtoDBVehicleCode(List<VehicleCodeXMLViewModel> listModel)
        {
            var response = new ResponseModel<List<VehicleCodeXMLViewModel>>();
            try
            {
                var listOldModel = data.GetVehicleCodeListForXML();

                var deleteListModel = listOldModel.Where(
                    oldModel => !(listModel.FindIndex(q => q.CodeSSID == oldModel.CodeSSID) >= 0))
                    .Select(q => { q.CommandType = CommonValues.DMLType.Delete; return q; })
                    .ToList();

                listModel.AddRange(deleteListModel);

                data.XMLtoDBVehicleCode(listModel);
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

        public ResponseModel<List<VehicleCodeXMLViewModel>> XMLtoDBVehicleCode(List<VehicleCodeXMLViewModel> listModel, List<VehicleCodeLangXMLModel> listLangModel)
        {
            var response = new ResponseModel<List<VehicleCodeXMLViewModel>>();
            try
            {
                var listOldModel = data.GetVehicleCodeListForXML();

                var deleteListModel = listOldModel.Where(
                    oldModel => !(listModel.FindIndex(q => q.CodeSSID == oldModel.CodeSSID) >= 0))
                    .Select(q => { q.CommandType = CommonValues.DMLType.Delete; return q; })
                    .ToList();

                listModel.AddRange(deleteListModel);

                data.XMLtoDBVehicleCode(listModel);

                if (listModel.Any(x => x.ErrorNo > 0))
                    throw new System.Exception(listModel.FirstOrDefault().ErrorMessage);

                data.XMLtoDBVehicleCodeLang(listLangModel);

                if (listLangModel.Any(x => x.ErrorNo > 0))
                    throw new System.Exception(listLangModel.FirstOrDefault().ErrorMessage);

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
