using ODMSData;
using ODMSModel.SparePartSaleWaybill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSBusiness.Business
{
    public class SparePartSaleWaybillBL : BaseBusiness
    {
        private readonly SparePartSaleWaybillData data = new SparePartSaleWaybillData();

        public ResponseModel<SparePartSaleWaybillViewModel> DMLSparePartSaleWaybill(UserInfo user,SparePartSaleWaybillViewModel model)
        {
            var response = new ResponseModel<SparePartSaleWaybillViewModel>();
            try
            {
                data.DMLSparePartSaleWaybill(user, model);
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

        public ResponseModel<SparePartSaleWaybillListModel> ListSparePartSaleWaybills(UserInfo user,SparePartSaleWaybillListModel filter, out int totalCnt)
        {
            var response = new ResponseModel<SparePartSaleWaybillListModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.ListSparePartSaleWaybills(user,filter, out totalCnt);
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

        public ResponseModel<SparePartSaleWaybillViewModel> GetSparePartSaleWaybill(UserInfo user,int id)
        {
            var response = new ResponseModel<SparePartSaleWaybillViewModel>();
            try
            {
                response.Model = data.GetSparePartSaleWaybill(user, id);
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

