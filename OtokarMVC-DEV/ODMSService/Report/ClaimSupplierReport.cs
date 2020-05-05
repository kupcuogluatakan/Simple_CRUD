using ODMSBusiness;
using ODMSModel.ClaimSupplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService.Report
{
    class ClaimSupplierReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Claim Tedarikçi Tanımlama";
            }
        }

        private object _filter;
        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new ClaimSupplierListModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            var claimSupplierBo = new ClaimSupplierBL();
            int totalCnt = 0;
            return claimSupplierBo.ListClaimSupplier(UserInfo ,(Filter as ClaimSupplierListModel), out totalCnt).Data;
        }
    }
}
