using ODMSCommon;
using ODMSCommon.Resources;
using ODMSData;
using ODMSModel.TechnicianOperation;

namespace ODMSBusiness
{
    public class TechnicianOperationBL : BaseBusiness
    {
        private readonly TechnicianOperationData data = new TechnicianOperationData();
        public ResponseModel<TechnicianOperationViewModel> CheckTechnicianLogin(TechnicianOperationViewModel model)
        {
            var response = new ResponseModel<TechnicianOperationViewModel>();
            try
            {
                data.CheckTechnicianLogin(model);
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
