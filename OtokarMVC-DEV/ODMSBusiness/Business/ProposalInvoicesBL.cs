using ODMSData;
using ODMSModel.ProposalInvoice;
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
    public class ProposalInvoicesBL : BaseBusiness
    {
        private readonly ProposalInvoicesData data = new ProposalInvoicesData();
        public ResponseModel<ProposalInvoicesViewModel> GetProposalInvoice(UserInfo user,long workOrderInvoiceId)
        {
            var response = new ResponseModel<ProposalInvoicesViewModel>();
            try
            {
                response.Model = data.GetProposalInvoice(user, workOrderInvoiceId);
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
