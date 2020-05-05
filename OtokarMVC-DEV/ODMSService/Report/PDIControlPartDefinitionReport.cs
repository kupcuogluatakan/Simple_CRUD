using ODMSBusiness;
using ODMSCommon.Security;
using ODMSModel.PDIControlPartDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class PDIControlPartDefinitionReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Teslim Öncesi Bakım Kontrol Parça Listesi";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new PDIControlPartDefinitionListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var pdiControlDefinitionBo = new PDIControlPartDefinitionBL();
            int totalCnt = 0;
            return pdiControlDefinitionBo.ListPDIControlPartDefinition(UserInfo, (Filter as PDIControlPartDefinitionListModel), out totalCnt).Data;
        }
    }
}
