using System.Collections.Generic;
using ODMSData;
using ODMSModel.VehicleNote;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class VehicleNotesBL : BaseBusiness
    {
        private readonly VehicleNotesData data = new VehicleNotesData();

        public ResponseModel<VehicleNotesListModel> ListVehicleNotes(UserInfo user,VehicleNotesListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<VehicleNotesListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListVehicleNotes(user,filter, out totalCnt);
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

        public ResponseModel<VehicleNotesModel> GetVehicleNotes(UserInfo user,VehicleNotesModel filter)
        {
            var response = new ResponseModel<VehicleNotesModel>();
            try
            {
                data.GetVehicleNotes(user,filter);
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

        public ResponseModel<VehicleNotesModel> DMLVehicleNotes(UserInfo user,VehicleNotesModel model)
        {
            var response = new ResponseModel<VehicleNotesModel>();
            try
            {
                data.DMLVehicleNotes(user,model);
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


    }
}
