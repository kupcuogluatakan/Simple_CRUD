using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.PDIResultDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class PDIResultDefinitionReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Teslim Öncesi Bakım Sonuç Listesi";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new PDIResultDefinitionListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var pdiResultDefinitionBo = new PDIResultDefinitionBL();
            int totalCnt = 0;
            return pdiResultDefinitionBo.ListPDIResultDefinition(UserInfo, (Filter as PDIResultDefinitionListModel), out totalCnt).Data;
        }
    }
}
