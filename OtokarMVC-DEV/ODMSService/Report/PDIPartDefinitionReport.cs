using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.PDIPartDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class PDIPartDefinitionReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Teslim Öncesi Bakım Parça Tanım Listesi";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new PDIPartDefinitionListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var appointmentIndicatorFailureCodeBo = new PDIPartDefinitionBL();
            int totalCnt = 0;
            return appointmentIndicatorFailureCodeBo.ListPDIPartDefinition(UserInfo, (Filter as PDIPartDefinitionListModel), out totalCnt).Data;
        }
    }
}
