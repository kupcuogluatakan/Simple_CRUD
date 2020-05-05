using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportService
{
    public interface IReport
    {
        byte[] VehicleDeliveryVoucherReport(long workOrderId);

        byte[] SparePartPositionedReport(string langCode, int deliveryId);

        byte[] ClaimDismantledPartReportCopy(string langCode, int claimWayBillId);

        byte[] ClaimDismantledPartReportReal(string langCode, int claimWayBillId);

        byte[] SparePartSaleReportCopy(string langCode, int sparePartSaleId);

        byte[] SparePartSaleReportReal(string langCode, int sparePartSaleId);

        byte[] WorkOrderInvoiceReportCopy(string langCode, long workOrderInvoiceId);

        byte[] WorkOrderInvoiceReportReal(string langCode, long workOrderInvoiceId);

        byte[] WorkOrderInvoiceWithHoldReportCopy(string langCode, long workOrderInvoiceId);

        byte[] WorkOrderInvoiceWithHoldReportReal(string langCode, long workOrderInvoiceId);

        byte[] SpecialWorkOrderInvoiceReportCopy(string langCode, long workOrderInvoiceId);

        byte[] SpecialWorkOrderInvoiceReportReal(string langCode, long workOrderInvoiceId);

        byte[] SparePartPickingReport(string langCode, int workOrderPickingId);

        byte[] WorkOrderInvioceBreakDownReportCopy(string langCode, long workOrderInvoiceId);

        byte[] WorkOrderInvioceBreakDownReportReal(string langCode, long workOrderInvoiceId);

        byte[] ReturnSparePartPositionedReport(string langCode, int workOrderPickingId);

        byte[] ClaimDismantledPartProformaReport(string langCode, int claimWayBillId);

        byte[] SparePartSaleProformaReport(string langCode, int sparePartSaleId);

        byte[] WorkOrderInvoiceProformaReport(string langCode, long workOrderInvoiceId);

        byte[] WorkOrderInvoiceWithHoldProformaReport(string langCode, long workOrderInvoiceId);

        byte[] SpecialWorkOrderInvoiceProformaReport(string langCode, long workOrderInvoiceId);

        byte[] WorkOrderInvioceBreakDownProformaReport(string langCode, long workOrderInvoiceId);

        byte[] CycleCountPlanReport(string langCode, int cycleCountId);
        byte[] WorkOrderReport(long workOrderId);
        byte[] Proposal(long proposalId, int seq);
        byte[] ProposalDetails(long proposalId, int seq);
        byte[] SaleOrderPrintReport(long soNumber, int dealerId, string lang);
        byte[] SparePartSaleWaybillCopyReport(long sparePartSaleWaybillId, int dealerId, string lang);
        byte[] SparePartSaleWaybillRealReport(long sparePartSaleWaybillId, int dealerId, string lang);
    }
}
