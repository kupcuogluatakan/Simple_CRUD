using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.CampaignRequestApprove
{
    public class CampaignRequestApproveJsonModel: BaseListWithPagingModel
    {
        //Search
        public int CampaignRequestId { get; set; }
        public string VinCodes { get; set; }

        //Results
        public string VinCode { get; set; }
        public int Count { get; set; }
    }
}
