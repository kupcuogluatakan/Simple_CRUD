using System.Collections.Generic;
using System.Web.Mvc;
using ODMSData;
using ODMSCommon;
using System;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.AnnouncementDealer;

namespace ODMSBusiness
{
    public class AnnouncementDealerBL : BaseBusiness
    {
        private readonly AnnouncementDealerData data = new AnnouncementDealerData();

        public ResponseModel<SelectListItem> ListAnnouncementDealersIncluded(int announcementId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListAnnouncementDealersIncluded(announcementId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<SelectListItem> ListAnnouncementDealersExcluded(int announcementId, int customerGroupId, string vehicleModelId)
        {
            var response = new ResponseModel<SelectListItem>();
            try
            {
                response.Data = data.ListAnnouncementDealersExcluded(announcementId, customerGroupId, vehicleModelId);
                response.Total = response.Data.Count;
                response.Message = MessageResource.Global_Display_Success;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false; 
 				 AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
                response.Message = string.Format("{0} {1} : {2}", MessageResource.Global_Display_Error, MessageResource.lblGeneralErrorDetail, ex.Message);
            }
            return response;
        }

        public ResponseModel<AnnouncementDealerModel> Save(UserInfo user, ODMSModel.AnnouncementDealer.AnnouncementDealerModel model)
        {
            var response = new ResponseModel<AnnouncementDealerModel>();
            try
            {
                data.Save(user, model);
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
