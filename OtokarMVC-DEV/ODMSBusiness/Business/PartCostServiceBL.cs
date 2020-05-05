using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.PartCostService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Business
{
    public class PartCostServiceBL : BaseBusiness
    {
        private readonly PartCostServiceData data = new PartCostServiceData();

        public ResponseModel<PartCostServiceModel> GetPart(Int64? id,int? seq)
        {
            var response = new ResponseModel<PartCostServiceModel>();
            try
            {
                response.Data = data.GetPart(id, seq);
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

        public ResponseModel<List<PartCostServiceModel>> SetPartCostVAlue(List<PartCostServiceModel> model)
        {
            var response = new ResponseModel<List<PartCostServiceModel>>();
            try
            {
                data.SetPartCostVAlue(model);
                response.Model = model;
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
