using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ODMSBusiness.Reports;
using ODMSBusiness.ReportService;

namespace ReportsServiceTests
{
    [TestFixture]
    public class ReportsTests
    {
        [TestCase("TR",134)]
        [Test]
        public void Get_WorkOrderInvioceBreakDownProformaExcelReport_Successfully(string languageCode, int invoiceId)
        {
            var client = new ReportServiceSoapClient("ReportServiceSoap");

            var report= client.WorkOrderInvioceBreakDownProformaExcelReport(Helper.GetCredentials(), languageCode, invoiceId);
            if (report != null)
            {
                if (!File.Exists(Helper.GetReportsSavePath()))
                {
                    Directory.CreateDirectory(Helper.GetReportsSavePath());
                }

                File.WriteAllBytes(string.Format("{0}\\WorkOrderInvioceBreakDownProformaExcelReport_{1}_{2}.xls", Helper.GetReportsSavePath(),invoiceId, Guid.NewGuid()), report);
            }
            Assert.IsNotNull(report,
                string.Format(
                    "WorkOrderInvioceBreakDownProformaExcelReport get failed for parameters languageCode={0}, invoiceId{1}",
                    languageCode, invoiceId));
        }

        [TestCase("TR", 14549)]
        [Test]
        public void Get_WorkOrderInvoiceProformaExcelReport_Successfully(string languageCode, int invoiceId)
        {
            var client = new ReportServiceSoapClient("ReportServiceSoap");

            var report = client.WorkOrderInvoiceProformaExcelReport(Helper.GetCredentials(), languageCode, invoiceId);
            if (report != null)
            {
                if (!File.Exists(Helper.GetReportsSavePath()))
                {
                    Directory.CreateDirectory(Helper.GetReportsSavePath());
                }

                File.WriteAllBytes(string.Format("{0}\\WorkOrderInvoiceProformaExcelReport_{1}_{2}.xls", Helper.GetReportsSavePath(),invoiceId, Guid.NewGuid()), report);
            }
            Assert.IsNotNull(report,
                string.Format(
                    "WorkOrderInvoiceProformaExcelReport get failed for parameters languageCode={0}, invoiceId{1}",
                    languageCode, invoiceId));
        }
        [TestCase("TR", 134)]
        [Test]
        public void Get_WorkOrderInvoiceWithHoldProformaExcelReport_Successfully(string languageCode, int invoiceId)
        {
            var client = new ReportServiceSoapClient("ReportServiceSoap");

            var report = client.WorkOrderInvoiceWithHoldProformaExcelReport(Helper.GetCredentials(), languageCode, invoiceId);
            if (report != null)
            {
                if (!File.Exists(Helper.GetReportsSavePath()))
                {
                    Directory.CreateDirectory(Helper.GetReportsSavePath());
                }

                File.WriteAllBytes(string.Format("{0}\\WorkOrderInvoiceWithHoldProformaExcelReport{1}_{2}.xls", Helper.GetReportsSavePath(), invoiceId, Guid.NewGuid()), report);
            }
            Assert.IsNotNull(report,
                string.Format(
                    "WorkOrderInvoiceWithHoldProformaExcelReport get failed for parameters languageCode={0}, invoiceId{1}",
                    languageCode, invoiceId));
        }
        [TestCase("TR", 134)]
        [Test]
        public void Get_SpecialWorkOrderInvoiceProformaExcelReport_Successfully(string languageCode, int invoiceId)
        {
            var client = new ReportServiceSoapClient("ReportServiceSoap");

            var report = client.SpecialWorkOrderInvoiceProformaExcelReport(Helper.GetCredentials(), languageCode, invoiceId);
            if (report != null)
            {
                if (!File.Exists(Helper.GetReportsSavePath()))
                {
                    Directory.CreateDirectory(Helper.GetReportsSavePath());
                }

                File.WriteAllBytes(string.Format("{0}\\SpecialWorkOrderInvoiceProformaExcelReport{1}_{2}.xls", Helper.GetReportsSavePath(), invoiceId, Guid.NewGuid()), report);
            }
            Assert.IsNotNull(report,
                string.Format(
                    "SpecialWorkOrderInvoiceProformaExcelReport get failed for parameters languageCode={0}, invoiceId{1}",
                    languageCode, invoiceId));
        }
    }
}
