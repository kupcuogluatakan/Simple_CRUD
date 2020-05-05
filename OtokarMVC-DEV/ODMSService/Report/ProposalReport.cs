using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Business;
using ODMSModel.Proposal;

namespace ODMSService.Report
{
    class ProposalReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Teklifler";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new ProposalListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var reportsBo = new ProposalBL();
            var totalCnt = 0;
            return reportsBo.ListProposal(UserInfo,Filter as ProposalListModel, out totalCnt).Data;
        }
    }
}
