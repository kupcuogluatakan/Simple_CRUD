using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.PartCostPriceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Business
{
    public class PartCostBL : BaseBusiness
    {
        private readonly PartCostData data = new PartCostData();

        public ResponseModel<PartCostPriceXMLModel> GetGuaranteeDetPart(int partID, int totalCnt)
        {
            var response = new ResponseModel<PartCostPriceXMLModel>();
            totalCnt = 0;
            try
            {
                response.Data = data.GetGuaranteeDetPart(partID, out totalCnt);
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

    }
}
