using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Web.Services.Protocols;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Dynamic;
using KingAOP;
using ReportService.Aspects;
using ReportService.Services;

namespace ReportService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ReportService : System.Web.Services.WebService, IReport
    {
        public UserCredentials consumer;

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] VehicleDeliveryVoucherReport(long workOrderId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.VehicleDeliveryVoucherReport(workOrderId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] SparePartPositionedReport(string langCode, int deliveryId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.SparePartPositionedReport(langCode, deliveryId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] ClaimDismantledPartReportCopy(string langCode, int claimWayBillId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.ClaimDismantledPartReportCopy(langCode, claimWayBillId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] ClaimDismantledPartProformaReport(string langCode, int claimWayBillId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.ClaimDismantledPartProformaReport(langCode, claimWayBillId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] ClaimDismantledPartReportReal(string langCode, int claimWayBillId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.ClaimDismantledPartReportReal(langCode, claimWayBillId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] SparePartSaleReportCopy(string langCode, int sparePartSaleId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.SparePartSaleReportCopy(langCode, sparePartSaleId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] SparePartSaleReportReal(string langCode, int sparePartSaleId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.SparePartSaleReportReal(langCode, sparePartSaleId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] WorkOrderInvoiceReportCopy(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderInvoiceReportCopy(langCode, workOrderInvoiceId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] WorkOrderInvoiceProformaReport(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderInvoiceProformaReport(langCode, workOrderInvoiceId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] WorkOrderInvoiceReportReal(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderInvoiceReportReal(langCode, workOrderInvoiceId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] WorkOrderInvoiceWithHoldReportCopy(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderInvoiceWithHoldReportCopy(langCode, workOrderInvoiceId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] WorkOrderInvoiceWithHoldProformaReport(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderInvoiceWithHoldProformaReport(langCode, workOrderInvoiceId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] WorkOrderInvoiceWithHoldReportReal(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderInvoiceWithHoldReportReal(langCode, workOrderInvoiceId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] SpecialWorkOrderInvoiceReportCopy(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.SpecialWorkOrderInvoiceReportCopy(langCode, workOrderInvoiceId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] SpecialWorkOrderInvoiceProformaReport(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.SpecialWorkOrderInvoiceProformaReport(langCode, workOrderInvoiceId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] SpecialWorkOrderInvoiceReportReal(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.SpecialWorkOrderInvoiceReportReal(langCode, workOrderInvoiceId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] SparePartPickingReport(string langCode, int workOrderPickingId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.SparePartPickingReport(langCode, workOrderPickingId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] WorkOrderInvioceBreakDownReportCopy(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderInvioceBreakDownReportCopy(langCode, workOrderInvoiceId);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] WorkOrderInvioceBreakDownProformaReport(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderInvioceBreakDownProformaReport(langCode, workOrderInvoiceId);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] WorkOrderInvioceBreakDownReportReal(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderInvioceBreakDownReportReal(langCode, workOrderInvoiceId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] ReturnSparePartPositionedReport(string langCode, int workOrderPickingId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.ReturnSparePartPositionedReport(langCode, workOrderPickingId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] SparePartSaleProformaReport(string langCode, int sparePartSaleId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.SparePartSaleProformaReport(langCode, sparePartSaleId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] CycleCountPlanReport(string langCode, int cycleCountId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.CycleCountPlanReport(langCode, cycleCountId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] WorkOrderReport(long workOrderId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderReport(workOrderId);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] WorkOrderInvioceBreakDownProformaExcelReport(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderInvioceBreakDownProformaExcelReport(langCode, workOrderInvoiceId);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] WorkOrderInvoiceProformaExcelReport(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderInvoiceProformaExcelReport(langCode, workOrderInvoiceId);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] WorkOrderInvoiceWithHoldProformaExcelReport(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderInvoiceProformaExcelReport(langCode, workOrderInvoiceId);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] SpecialWorkOrderInvoiceProformaExcelReport(string langCode, long workOrderInvoiceId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderInvoiceProformaExcelReport(langCode, workOrderInvoiceId);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] WorkOrderPrintFirstPart(long workOrderId)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.WorkOrderPrintAndProformaReport(workOrderId);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] Proposal(long id, int seq)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    var re = reportService.Proposal(id, seq);
                    return re;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] SparePartSaleWaybillCopyReport(long sparePartSaleWaybillId, int dealerId, string lang)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.SparePartSaleWaybillCopyReport(sparePartSaleWaybillId, dealerId, lang);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] SparePartSaleWaybillRealReport(long sparePartSaleWaybillId, int dealerId, string lang)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.SparePartSaleWaybillRealReport(sparePartSaleWaybillId, dealerId, lang);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] SaleOrderPrintReport(long soNumber, int dealerId, string lang)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.SaleOrderPrintReport(soNumber, dealerId, lang);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        [WebMethod]
        [SoapHeader("consumer", Required = true)]
        public byte[] ProposalDetails(long proposalId, int seq)
        {
            try
            {
                var loginService = new LoginServiceUtility();
                if (loginService.checkConsumer(consumer))
                {
                    dynamic reportService = new ReportServiceUtility();
                    return reportService.ProposalDetails(proposalId, seq);
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
