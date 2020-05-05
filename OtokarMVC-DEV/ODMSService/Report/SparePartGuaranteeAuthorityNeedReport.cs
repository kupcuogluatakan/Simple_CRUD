using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness;
using ODMSModel.SparePartGuaranteeAuthorityNeed;
using ODMSCommon.Security;

namespace ODMSService.Report
{
    class SparePartGuaranteeAuthorityNeedReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Parça Garanti Yetki Listesi";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new SparePartGuaranteeAuthorityNeedListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            SparePartGuaranteeAuthorityNeedBL SsparePartGuaranteeAuthorityNeedBL = new SparePartGuaranteeAuthorityNeedBL();
            int totalCount = 0;
            return SsparePartGuaranteeAuthorityNeedBL.ListSparePartGuaranteeAuthorityNeeds(UserInfo, (Filter as SparePartGuaranteeAuthorityNeedListModel), out totalCount).Data;

        }
    }
}
