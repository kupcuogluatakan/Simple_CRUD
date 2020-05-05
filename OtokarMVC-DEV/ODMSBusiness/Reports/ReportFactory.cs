using ODMSBusiness.Reports;
using System;

namespace ODMSBusiness.Reports
{
    internal static class ReportFactory
    {
        public static readonly object shared = new object();

        public static IReport Create(ReportType reportType)
        {

            lock (shared)
            {
                IReport reportBase;
                switch (reportType)
                {
                    case ReportType.VehicleLeaveReport:
                        reportBase = new VechicleLeaveReport();
                        break;
                    case ReportType.VehicleInvoiceReport:
                        reportBase = new VehicleInvoiceReport();
                        break;
                    case ReportType.SparePartSaleRealReport:
                        reportBase = new SparePartSaleRealReport();
                        break;
                    case ReportType.SparePartSaleCopyReport:
                        reportBase = new SparePartSaleCopyReport();
                        break;
                    case ReportType.SparePartPositionedReport:
                        reportBase = new SparePartPositionedReport();
                        break;
                    case ReportType.ClaimDismantledPartRealReport:
                        reportBase = new ClaimDismantledPartRealReport();
                        break;
                    case ReportType.ClaimDismantledPartCopyReport:
                        reportBase = new ClaimDismantledPartCopyReport();
                        break;
                    case ReportType.SparePartPickingReport:
                        reportBase = new SparePartPickingReport();
                        break;
                    case ReportType.ReturnPartPickingReport:
                        reportBase = new ReturnSparePartPickingReport();
                        break;
                    case ReportType.ClaimDismantledPartProformaReport:
                        reportBase = new ClaimDismantledPartProformaReport();
                        break;
                    case ReportType.SparePartSaleProformaReport:
                        reportBase = new SparePartSaleProformaReport();
                        break;
                    case ReportType.CycleCountReport:
                        reportBase = new CycleCountReport();
                        break;
                    case ReportType.WorkOrderReport:
                        reportBase = new WorkOrderReport();
                        break;
                    case ReportType.WorkOrderInvioceBreakDownProformaExcelReport:
                        reportBase = new WorkOrderInvioceBreakDownProformaExcelReport();
                        break;
                    case ReportType.WorkOrderInvoiceProformaExcelReport:
                        reportBase = new WorkOrderInvoiceProformaExcelReport();
                        break;
                    case ReportType.WorkOrderInvoiceWithHoldProformaExcelReport:
                        reportBase = new WorkOrderInvoiceWithHoldProformaExcelReport();
                        break;
                    case ReportType.SpecialWorkOrderInvoiceProformaExcelReport:
                        reportBase = new SpecialWorkOrderInvoiceProformaExcelReport();
                        break;
                    case ReportType.WorkOrderPrintFirstPart:
                        reportBase = new WorkOrderPrintFirstPartReport();
                        break;
                    case ReportType.SparePartSaleWaybillCopyReport:
                        reportBase = new SparePartSaleWaybillCopyReport();
                        break;
                    case ReportType.Proposal:
                        reportBase = new ProposalReport();
                        break;
                    case ReportType.ProposalDetail:
                        reportBase = new ProposalDetailReport();
                        break;
                    case ReportType.SSHReport:
                        reportBase = new SSHReport();
                        break;
                    case ReportType.AlternatePartReport:
                        reportBase = new AlternatePartReport();
                        break;
                    //case ReportType.SparePartSaleWaybillRealReport:
                    //    reportBase = new SparePartSaleWaybillRealReport();
                    //    break;
                    //case ReportType.SparePartSaleDeliveryReport:
                    //    reportBase = new SparePartSaleDeliveryReport();
                    //    break;
                    default: throw new NotImplementedException(String.Format("The report type:'{0}' is not implemented.", reportType));
                }
                return reportBase;
            }
        }
    }
}
