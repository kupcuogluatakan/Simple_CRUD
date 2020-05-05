using ODMSBusiness;
using ODMSModel.BreakdownDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class BreakdownDefinitionReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Teslim Öncesi Bakım Arıza Tanım Listesi";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new BreakdownDefinitionListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var appointmentIndicatorFailureCodeBo = new BreakdownDefinitionBL();
            int totalCnt = 0;
            return appointmentIndicatorFailureCodeBo.ListBreakdownDefinition(UserInfo,(Filter as BreakdownDefinitionListModel), out totalCnt).Data;
        }
    }
}
