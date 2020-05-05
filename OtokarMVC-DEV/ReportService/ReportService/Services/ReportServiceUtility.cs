using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using ReportService.Aspects;
using KingAOP;
using System.IO;
using CrystalDecisions.Shared;
using System.Configuration;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;

namespace ReportService.Services
{
    public class ReportServiceUtility : IDynamicMetaObjectProvider, IReport
    {

        Logger logger = new Logger();
        public DynamicMetaObject GetMetaObject(System.Linq.Expressions.Expression parameter)
        {
            return new AspectWeaver(parameter, this);
        }

        [ExceptionHandlingAspect]
        public byte[] VehicleDeliveryVoucherReport(long workOrderId)
        {

            using (Reports.VehicleDeliveryVoucher report = new Reports.VehicleDeliveryVoucher())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "VehicleDeliveryVoucher.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@WorkOrderID", workOrderId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }

        }

        [ExceptionHandlingAspect]
        public byte[] SparePartPositionedReport(string langCode, int deliveryId)
        {
            using (Reports.SparePartPositionedForm report = new Reports.SparePartPositionedForm())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "SparePartPositionedForm.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_DELIVERY", deliveryId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] ClaimDismantledPartReportCopy(string langCode, int claimWayBillId)
        {
            using (Reports.ClaimDismantledPart report = new Reports.ClaimDismantledPart())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "ClaimDismantledPart.pdf"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@ID_CLAIM_WAYBILL", claimWayBillId);
                report.SetParameterValue("@LANGUAGE_CODE", langCode);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }


        }

        [ExceptionHandlingAspect]
        public byte[] ClaimDismantledPartProformaReport(string langCode, int claimWayBillId)
        {
            using (Reports.ClaimDismantledPartProforma report = new Reports.ClaimDismantledPartProforma())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "ClaimDismantledPartProforma.pdf"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@ID_CLAIM_WAYBILL", claimWayBillId);
                report.SetParameterValue("@LANGUAGE_CODE", langCode);


                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] ClaimDismantledPartReportReal(string langCode, int claimWayBillId)
        {
            using (Reports.ClaimDismantledPartReal report = new Reports.ClaimDismantledPartReal())
            {

                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "ClaimDismantledPartReal.pdf"), OpenReportMethod.OpenReportByDefault);


                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@ID_CLAIM_WAYBILL", claimWayBillId);
                report.SetParameterValue("@LANGUAGE_CODE", langCode);


                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }

        }

        [ExceptionHandlingAspect]
        public byte[] SparePartSaleReportCopy(string langCode, int sparePartSaleId)
        {

            using (Reports.SparePartSaleCopy report = new Reports.SparePartSaleCopy())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "SparePartSaleCopy.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_PART_SALE", sparePartSaleId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);

            }


        }

        [ExceptionHandlingAspect]
        public byte[] SparePartSaleProformaReport(string langCode, int sparePartSaleId)
        {
            using (Reports.SparePartSaleProforma report = new Reports.SparePartSaleProforma())
            {

                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "SparePartSaleProforma.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_PART_SALE", sparePartSaleId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] SparePartSaleReportReal(string langCode, int sparePartSaleId)
        {
            using (Reports.SparePartSaleReal report = new Reports.SparePartSaleReal())
            {

                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "SparePartSaleReal.rpt"), OpenReportMethod.OpenReportByDefault);


                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_PART_SALE", sparePartSaleId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] WorkOrderInvoiceReportCopy(string langCode, long workOrderInvoiceId)
        {
            using (Reports.WorkOrderInvoiceCopy report = new Reports.WorkOrderInvoiceCopy())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "WorkOrderInvoiceCopy.rpt"), OpenReportMethod.OpenReportByDefault);
                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }

        }

        [ExceptionHandlingAspect]
        public byte[] WorkOrderInvoiceProformaReport(string langCode, long workOrderInvoiceId)
        {
            using (Reports.WorkOrderInvoiceProforma report = new Reports.WorkOrderInvoiceProforma())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "WorkOrderInvoiceProforma.rpt"), OpenReportMethod.OpenReportByDefault);
                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] WorkOrderInvoiceReportReal(string langCode, long workOrderInvoiceId)
        {
            using (Reports.WorkOrderInvoiceReal report = new Reports.WorkOrderInvoiceReal())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "WorkOrderInvoiceReal.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] WorkOrderInvoiceWithHoldReportCopy(string langCode, long workOrderInvoiceId)
        {
            using (Reports.WorkOrderInvoiceWithHoldCopy report = new Reports.WorkOrderInvoiceWithHoldCopy())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "WorkOrderInvoiceWithHoldCopy.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] WorkOrderInvoiceWithHoldProformaReport(string langCode, long workOrderInvoiceId)
        {
            using (Reports.WorkOrderInvoiceWithHoldProforma report = new Reports.WorkOrderInvoiceWithHoldProforma())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "WorkOrderInvoiceWithHoldProforma.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }



        [ExceptionHandlingAspect]
        public byte[] WorkOrderInvoiceWithHoldReportReal(string langCode, long workOrderInvoiceId)
        {
            using (Reports.WorkOrderInvoiceWithHoldReal report = new Reports.WorkOrderInvoiceWithHoldReal())
            {

                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "WorkOrderInvoiceWithHoldReal.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] SpecialWorkOrderInvoiceReportCopy(string langCode, long workOrderInvoiceId)
        {
            using (Reports.SprecialWorkOrderInvoiceCopy report = new Reports.SprecialWorkOrderInvoiceCopy())
            {

                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "SprecialWorkOrderInvoiceCopy.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] SpecialWorkOrderInvoiceProformaReport(string langCode, long workOrderInvoiceId)
        {
            using (Reports.SprecialWorkOrderInvoiceProforma report = new Reports.SprecialWorkOrderInvoiceProforma())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "SprecialWorkOrderInvoiceProforma.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] SpecialWorkOrderInvoiceReportReal(string langCode, long workOrderInvoiceId)
        {
            using (Reports.SprecialWorkOrderInvoiceReal report = new Reports.SprecialWorkOrderInvoiceReal())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "SprecialWorkOrderInvoiceReal.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] SparePartPickingReport(string langCode, int workOrderPickingId)
        {
            using (Reports.SparePartPickingForm report = new Reports.SparePartPickingForm())
            {

                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "SparePartPickingForm.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_WORK_ORDER_PICKING_MST", workOrderPickingId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] WorkOrderInvioceBreakDownReportCopy(string langCode, long workOrderInvoiceId)
        {
            using (Reports.WorkOrderInvoiceBreakdownCopy report = new Reports.WorkOrderInvoiceBreakdownCopy())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "WorkOrderInvoiceBreakdownCopy.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);
                report.SetParameterValue("@LANGUAGE_CODE", langCode);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] WorkOrderInvioceBreakDownProformaReport(string langCode, long workOrderInvoiceId)
        {

            using (Reports.WorkOrderInvoiceBreakdownProforma report = new Reports.WorkOrderInvoiceBreakdownProforma())
            {

                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "WorkOrderInvoiceBreakdownProforma.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);
                report.SetParameterValue("@LANGUAGE_CODE", langCode);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] WorkOrderInvioceBreakDownReportReal(string langCode, long workOrderInvoiceId)
        {
            using (Reports.WorkOrderInvoiceBreakdownReal report = new Reports.WorkOrderInvoiceBreakdownReal())
            {

                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "WorkOrderInvoiceBreakdownReal.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);
                report.SetParameterValue("@LANGUAGE_CODE", langCode);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] ReturnSparePartPositionedReport(string langCode, int workOrderPickingId)
        {
            using (Reports.ReturnSparePartPositionedForm report = new Reports.ReturnSparePartPositionedForm())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "ReturnSparePartPositionedForm.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@ID_WORK_ORDER_PICKING_MST", workOrderPickingId);
                report.SetParameterValue("@LANGUAGE_CODE", langCode);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] CycleCountPlanReport(string langCode, int cycleCountId)
        {
            using (Reports.CycleCount report = new Reports.CycleCount())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "CycleCount.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@ID_CYCLE_COUNT", cycleCountId);
                report.SetParameterValue("@LANGUAGE_CODE", langCode);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] WorkOrderReport(long workOrderId)
        {
            using (Reports.WorkOrderReport report = new Reports.WorkOrderReport())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "WorkOrderReport.rpt"), OpenReportMethod.OpenReportByDefault);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@WorkOrderID", workOrderId);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        private byte[] RunReportAndGetBytes(ReportClass report, ExportFormatType formatType = ExportFormatType.PortableDocFormat)
        {
            ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
            ReportUtility.SetDBLogonForReport(cInfo, report);
            ReportUtility.SetDBLogonForSubreports(cInfo, report);

            Stream stream = report.ExportToStream(formatType);

            return Extentions.ReadFully(stream);
        }

        [ExceptionHandlingAspect]
        public byte[] WorkOrderInvioceBreakDownProformaExcelReport(string langCode, long workOrderInvoiceId)
        {

            using (Reports.WorkOrderInvoiceBreakdownProforma report = new Reports.WorkOrderInvoiceBreakdownProforma())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "WorkOrderInvoiceBreakdownProforma.rpt"), OpenReportMethod.OpenReportByDefault);
                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);
                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                return RunReportAndGetBytes(report, ExportFormatType.Excel);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] WorkOrderInvoiceProformaExcelReport(string langCode, long workOrderInvoiceId)
        {
            using (Reports.WorkOrderInvoiceProforma report = new Reports.WorkOrderInvoiceProforma())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "WorkOrderInvoiceProforma.rpt"), OpenReportMethod.OpenReportByDefault);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);

                return RunReportAndGetBytes(report, ExportFormatType.Excel);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] WorkOrderInvoiceWithHoldProformaExcelReport(string langCode, long workOrderInvoiceId)
        {
            using (Reports.WorkOrderInvoiceWithHoldProforma report = new Reports.WorkOrderInvoiceWithHoldProforma())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "WorkOrderInvoiceWithHoldProforma.rpt"), OpenReportMethod.OpenReportByDefault);
                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);
                return RunReportAndGetBytes(report, ExportFormatType.Excel);
            }
        }
        [ExceptionHandlingAspect]
        public byte[] SpecialWorkOrderInvoiceProformaExcelReport(string langCode, long workOrderInvoiceId)
        {
            using (Reports.SprecialWorkOrderInvoiceProforma report = new Reports.SprecialWorkOrderInvoiceProforma())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "SprecialWorkOrderInvoiceProforma.rpt"), OpenReportMethod.OpenReportByDefault);

                report.SetParameterValue("@LANGUAGE_CODE", langCode);
                report.SetParameterValue("@ID_WORK_ORDER_INV", workOrderInvoiceId);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                return RunReportAndGetBytes(report, ExportFormatType.Excel);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] WorkOrderPrintAndProformaReport(long workOrderId)
        {
            using (Reports.WorkOrderPrintFirstPart report = new Reports.WorkOrderPrintFirstPart())
            {
                report.Load(Path.Combine(HttpContext.Current.Server.MapPath("~/Reports"), "WorkOrderPrintFirstPart.rpt"), OpenReportMethod.OpenReportByDefault);

                report.SetParameterValue("@WorkOrderID", workOrderId);

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                return RunReportAndGetBytes(report, ExportFormatType.PortableDocFormat);
            }
        }
        [ExceptionHandlingAspect]
        public byte[] Proposal(long proposalId, int seq)
        {
            using (Reports.Proposal report = new Reports.Proposal())
            {
                report.Load(Path.Combine(Path.GetDirectoryName("~/Reports"), "Proposal.rpt"));

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@PROPOSAL_ID", proposalId);
                report.SetParameterValue("@PROPOSAL_SEQ", seq);

                report.SetParameterValue("@ID_PROPOSAL", proposalId,report.Subreports[0].Name);
                report.SetParameterValue("@PROPOSAL_SEQ", seq, report.Subreports[0].Name);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] SparePartSaleWaybillCopyReport(long sparePartSaleWaybillId, int dealerId, string lang)
        {
            using (Reports.SaleOrderWaybillCopy report = new Reports.SaleOrderWaybillCopy())
            {
                report.Load(Path.Combine(Path.GetDirectoryName("~/Reports"), "SaleOrderWaybillCopy.rpt"));

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@ID_SPARE_PART_SALE_WAYBILL", sparePartSaleWaybillId);
                report.SetParameterValue("@ID_DEALER", dealerId);
                report.SetParameterValue("@LANGUAGE_CODE", lang);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }


        [ExceptionHandlingAspect]
        public byte[] SparePartSaleWaybillRealReport(long sparePartSaleWaybillId, int dealerId, string lang)
        {
            using (Reports.SaleOrderWaybillReal report = new Reports.SaleOrderWaybillReal())
            {
                report.Load(Path.Combine(Path.GetDirectoryName("~/Reports"), "SaleOrderWaybillReal.rpt"));

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@ID_SPARE_PART_SALE_WAYBILL", sparePartSaleWaybillId);
                report.SetParameterValue("@ID_DEALER", dealerId);
                report.SetParameterValue("@LANGUAGE_CODE", lang);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }

        [ExceptionHandlingAspect]
        public byte[] SaleOrderPrintReport(long soNumber, int dealerId, string lang)
        {
            using (Reports.SaleProposalPrint report = new Reports.SaleProposalPrint())
            {
                report.Load(Path.Combine(Path.GetDirectoryName("~/Reports"), "SaleProposalPrint.rpt"));

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@SO_NUMBER", soNumber);
                report.SetParameterValue("@DEALER_ID", dealerId);
                report.SetParameterValue("@LANGUAGE_CODE", lang);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }
        [ExceptionHandlingAspect]
        public byte[] ProposalDetails(long proposalId, int seq)
        {
            using (Reports.ProposalDetail report = new Reports.ProposalDetail())
            {
                report.Load(Path.Combine(Path.GetDirectoryName("~/Reports"), "ProposalDetail.rpt"));

                ConnectionInfo cInfo = ReportUtility.GetConnectionInfo();
                ReportUtility.SetDBLogonForReport(cInfo, report);
                ReportUtility.SetDBLogonForSubreports(cInfo, report);

                report.SetParameterValue("@PROPOSAL_ID", proposalId);
                report.SetParameterValue("@PROPOSAL_SEQ", seq);

                Stream stream = report.ExportToStream(ExportFormatType.PortableDocFormat);

                return Extentions.ReadFully(stream);
            }
        }
    }
}