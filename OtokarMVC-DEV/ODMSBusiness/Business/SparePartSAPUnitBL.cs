using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.SparePartSAPUnit;
using ODMSCommon.Security;

namespace ODMSBusiness
{
    public class SparePartSAPUnitBL : BaseService<SparePartSAPUnitViewModel>
    {
        private readonly SparePartSAPUnitData data = new SparePartSAPUnitData();

        public ResponseModel<SparePartSAPUnitListModel> List(UserInfo user, SparePartSAPUnitListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<SparePartSAPUnitListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.List(user, filter, out totalCnt);
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

        public ResponseModel<SparePartSAPUnitViewModel> Get(UserInfo user, SparePartSAPUnitViewModel filter)
        {
            var response = new ResponseModel<SparePartSAPUnitViewModel>();
            try
            {
                response.Model = data.Get(user, filter);
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

        public ResponseModel<SparePartSAPUnitViewModel> Insert(UserInfo user, SparePartSAPUnitViewModel model)
        {
            var response = new ResponseModel<SparePartSAPUnitViewModel>();
            try
            {
                data.Insert(user, model);
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

        public ResponseModel<SparePartSAPUnitViewModel> Update(UserInfo user, SparePartSAPUnitViewModel model)
        {
            var response = new ResponseModel<SparePartSAPUnitViewModel>();
            try
            {
                data.Update(user, model);
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

        public ResponseModel<SparePartSAPUnitViewModel> Delete(UserInfo user, SparePartSAPUnitViewModel model)
        {
            var response = new ResponseModel<SparePartSAPUnitViewModel>();
            try
            {
                data.Delete(user, model);
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

