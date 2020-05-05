using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.PDIControlDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class PDIControlDefinitionReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Teslim Öncesi Bakım Kontrol Listesi";
            }
        }

        private object _filter;

        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new PDIControlDefinitionListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var pdiControlDefinitionBo = new PDIControlDefinitionBL();
            int totalCnt = 0;
            return pdiControlDefinitionBo.ListPDIControlDefinition(UserInfo, (Filter as PDIControlDefinitionListModel), out totalCnt).Data;
        }
    }
}
