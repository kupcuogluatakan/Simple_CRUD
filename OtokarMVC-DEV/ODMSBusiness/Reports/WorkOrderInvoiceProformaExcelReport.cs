﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Reports
{
    class WorkOrderInvoiceProformaExcelReport : ReportBase, IReport
    {
        public byte[] FetchBytes(params object[] parameters)
        {
            return Client.WorkOrderInvoiceProformaExcelReport(Credentials, (string)parameters[0], (long)parameters[1]);
        }

        public byte[] FetchBytesAsync(params object[] parameters)
        {
            throw new NotImplementedException();
            //return Client.VehicleDeliveryVoucherReportAsync(Credentials, (long)parameters[0]).Result.VehicleDeliveryVoucherReportResult;
        }

    }
}
