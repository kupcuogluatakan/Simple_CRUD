using ODMSBusiness;
using ODMSModel.OtokarSparePartSaleInvoiceSearch;
using ODMSService.Helpers;
using System;
using System.Collections.Generic;

namespace ODMSService.Report
{
    class OtokarSparePartSaleInvoiceReport : ReportBase
    {
        public override string ReportName
        {
            get
            {
                return "Otokar Fatura Listesi";
            }
        }

        private object _filter;

        public override object Filter
        {
            get
            {
                if (_filter == null)
                    _filter = new OtokarSparePartSaleInvoiceSearchViewModel();
                return _filter;
            }
        }

        public override IEnumerable<dynamic> FillData()
        {
            try
            {
                var otokarSparePartSaleInvoiceBo = new OtokarSparePartSaleInvoiceSearchBL();
                int totalCnt = 0;
                return otokarSparePartSaleInvoiceBo.ListInvoices(UserInfo, (Filter as OtokarSparePartSaleInvoiceSearchViewModel), out totalCnt).Data;
            }
            catch(Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.REPORT_CREATE_SERVICE, ex.InnerException.ToString());
            }
            return null;
        }
    }
}
