using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.CampaignRequestApprove;
using System.Collections.Generic;

namespace ODMSService.Report
{
    public class CampaignRequestApproveReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Kampanya Talep Onay Sayfası";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new CampaignRequestApproveListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            //Filtre'nin Filter as CampaignRequestApproveListModel bu şekilde verilmesi kilit nokta. model new'leyip vermeyin.
            var campaignRequestApproveBo = new CampaignRequestApproveBL();
            var totalCnt = 0;
            return campaignRequestApproveBo.ListCampaignRequestApprove(UserManager.UserInfo, Filter as CampaignRequestApproveListModel, out totalCnt).Data;


        }
    }
}