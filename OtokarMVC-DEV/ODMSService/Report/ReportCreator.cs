using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSData;
using ODMSModel.Reports;
using ODMSModel;
using ODMSCommon;
using System.ComponentModel.DataAnnotations;
using System.IO;
using ODMSService.Helpers;
using System.Reflection;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSService.Report
{
    public enum ReportType
    {
        SaleReport = 1,                                          //Satış Raporu
        StockSearchReport = 2,                                   //Stok Arama
        AppointmentIndicatorMainCategoryReport = 3,              //Belirti Ana Grup Listesi
        AppointmentIndicatorCategoryReport = 4,                  //Belirti Kodu Alt Grup Listesi
        AppointmentIndicatorSubCategoryReport = 5,               //Belirti Kodu Son Grup Listesi
        AppointmentIndicatorFailureCodeReport = 6,               //Belirti Sebep Kodu Tanımlama
        LabourPriceIndexReport = 7,                              //İşcilik Fiyatı Tanımlamaİşcilik Fiyatı Tanımlama
        LabourSubGroupReport = 8,                                //İşçilik Alt Grup Listesi
        BreakdownDefinitionReport = 9,                           //Teslim Öncesi Bakım Arıza Tanım Listesi
        PDIPartDefinitionReport = 10,                            //Teslim Öncesi Bakım Parça Tanım Listesi
        PDIControlPartDefinitionReport = 11,                     //Teslim Öncesi Bakım Kontrol Parça Listesi
        PDIResultDefinitionReport = 12,                          //Teslim Öncesi Bakım Sonuç Listesi
        PDIControlDefinitionReport = 13,                         //Teslim Öncesi Bakım Kontrol Listesi
        UserReport = 14,                                         //Kullanıcılar
        DealerStartupInventoryLevelReport = 15,                  //Başlangıç Stok Seviyesi Listesi
        SparePartGuaranteeAuthorityNeedReport = 16,              //Parça Garanti Yetki Listesi
        CriticalStockCardReport = 17,                            //Kritik Stok Seviyesi Tanımlama
        MaintenanceReport = 18,                                  //Araç Bakım Paketleri
        RackReport = 19,                                         //Raf Listesi
        ClaimSupplierReport = 20,                                //Claim Tedarikçi Tanımlama
        CycleCountStockDiffSearchReport = 21,                    //Orjinal Parça Adet Değişim Sorgulama
        StockTypeDetailReport = 22,                              //Bayi Stok Arama
        OtokarStockSearchReport = 23,                            //Otokar Stok Sorgulama
        PriceReport = 24,                                        //Fiyat Listesi
        CustomerReport = 25,                                     //Müşteri Listesi
        WorkOrderDetailReport = 26,                              //İş Emri Kartı Detay Raporu
        WorkOrderMainReport = 27,                                //Servis Periyodik Bakım Fatura Sorgulama Raporu
        PurchaseOrderReport = 28,                                //Satın Alma Raporu
        PartExchangeReport = 29,                                 //Araç ve Servis Bazlı Parça Değişim Raporu
        CycleCountResultReport = 30,                             //Sayımla Değiştirilen Stok Raporu
        WorkOrderPartHistoryReport = 31,                         //İş Emri Kartı Parça Tarihçesi Raporu
        SentPartUsageReport = 32,                                //Acil ve Normal gönderilen Malzemelerin Serviste Kullanım Raporu
        PartStockReport = 33,                                    //Yedek Parça Stok Sorgulama Raporu
        CarServiceDurationReport = 34,                           //Araç Serviste Kalma Süresi Raporu
        ChargePerCarReport = 35,                                 //Araç Bazlı Gider Hesaplama Raporu
        WorkOrderProcessTypesTotalReport = 36,                   //İş Emri Kartı Adetsel – Tutarsal Dağılım Raporu
        FixAssetInventoryReport = 37,                            //Servis Ekipman Envanter Raporu
        WorkOrderPartsTotalReport = 38,                          //Parça Bazlı Tutarsal – Adetsel Rapor
        KilometerDistributionReport = 39,                        //Kilometre Dağılım Raporu
        PartStockActivityReport = 40,                            //Yedek Parça Hareketleri Raporu
        SparePartControlReport = 41,                             //Yedek Parça Kontrol Raporu
        CampaignSummaryReport = 42,                              //Kampanya Özet Raporu
        LaborCostPerVehicleReport = 43,                          //Araç Başı İşcilik Gideri Raporu
        PersonnelInfoReport = 44,                                //Personel Bilgileri Raporu
        InvoiceInfoReport = 45,                                  //Fatura Bilgileri Raporu
        GuaranteeReport = 46,                                    //Garanti Raporu
        WorkOrderPerformanceReport = 47,                         //İş Emri Performans Raporu
        WorkOrderReport = 48,                                    //İş Emirleri
        ProposalReport = 49,                                     //Teklifler
        LabourReport = 50,                                       //İşçilik Süreleri
        VehicleHistoryReport = 51,                               //Araç Geçmişi
        LabourDetailReport = 52,                                 //İşçilik Süreleri ve Detayları
        SSHReport = 53,                                          //SSH raporu
        LabourTechnicianReport = 54,                             //Labour Technician Report
        OtokarSparePartSaleInvoiceReport = 55,                   //Otokar Invoice Report
        FleetWorkOrderReport = 56,                               //Fleet Work Order Report
        AlternatePartReport = 57,                                 //Alternate Part Report
        VehicleHistoryDetailsReport = 58,                         //Araç Geçmişi Detay
        VehicleHistoryListWithDetailsReport = 59,                //Detaylı Araç Geçmişi Raporu
        CampaignRequestApproveReport = 60                        //Kampanya Tale Onay Raporu
    }

    public class ReportFactory
    {

        private ReportBase _report;

        public ReportFactory(ReportType reportType)
        {
            switch (reportType)
            {
                case ReportType.SaleReport:
                    _report = new SaleReport();
                    break;
                case ReportType.StockSearchReport:
                    _report = new StockSearchReport();
                    break;
                case ReportType.AppointmentIndicatorMainCategoryReport:
                    _report = new AppointmentIndicatorMainCategoryReport();
                    break;
                case ReportType.AppointmentIndicatorCategoryReport:
                    _report = new AppointmentIndicatorCategoryReport();
                    break;
                case ReportType.AppointmentIndicatorSubCategoryReport:
                    _report = new AppointmentIndicatorSubCategoryReport();
                    break;
                case ReportType.AppointmentIndicatorFailureCodeReport:
                    _report = new AppointmentIndicatorFailureCodeReport();
                    break;
                case ReportType.LabourPriceIndexReport:
                    _report = new LabourPriceIndexReport();
                    break;
                case ReportType.LabourSubGroupReport:
                    _report = new LabourSubGroupReport();
                    break;
                case ReportType.BreakdownDefinitionReport:
                    _report = new BreakdownDefinitionReport();
                    break;
                case ReportType.PDIPartDefinitionReport:
                    _report = new PDIPartDefinitionReport();
                    break;
                case ReportType.PDIControlPartDefinitionReport:
                    _report = new PDIControlPartDefinitionReport();
                    break;
                case ReportType.PDIResultDefinitionReport:
                    _report = new PDIResultDefinitionReport();
                    break;
                case ReportType.PDIControlDefinitionReport:
                    _report = new PDIControlDefinitionReport();
                    break;
                case ReportType.UserReport:
                    _report = new UserReport();
                    break;
                case ReportType.DealerStartupInventoryLevelReport:
                    _report = new DealerStartupInventoryLevelReport();
                    break;
                case ReportType.SparePartGuaranteeAuthorityNeedReport:
                    _report = new SparePartGuaranteeAuthorityNeedReport();
                    break;
                case ReportType.CriticalStockCardReport:
                    _report = new CriticalStockCardReport();
                    break;
                case ReportType.MaintenanceReport:
                    _report = new MaintenanceReport();
                    break;
                case ReportType.RackReport:
                    _report = new RackReport();
                    break;
                case ReportType.ClaimSupplierReport:
                    _report = new ClaimSupplierReport();
                    break;
                case ReportType.CycleCountStockDiffSearchReport:
                    _report = new CycleCountStockDiffSearchReport();
                    break;
                case ReportType.StockTypeDetailReport:
                    _report = new StockTypeDetailReport();
                    break;
                case ReportType.OtokarStockSearchReport:
                    _report = new OtokarStockSearchReport();
                    break;
                case ReportType.PriceReport:
                    _report = new PriceReport();
                    break;
                case ReportType.CustomerReport:
                    _report = new CustomerReport();
                    break;
                case ReportType.WorkOrderDetailReport:
                    _report = new WorkOrderDetailReport();
                    break;
                case ReportType.WorkOrderMainReport:
                    _report = new WorkOrderMainReport();
                    break;
                case ReportType.PurchaseOrderReport:
                    _report = new PurchaseOrderReport();
                    break;
                case ReportType.PartExchangeReport:
                    _report = new PartExchangeReport();
                    break;
                case ReportType.CycleCountResultReport:
                    _report = new CycleCountResultReport();
                    break;
                case ReportType.WorkOrderPartHistoryReport:
                    _report = new WorkOrderPartHistoryReport();
                    break;
                case ReportType.SentPartUsageReport:
                    _report = new SentPartUsageReport();
                    break;
                case ReportType.PartStockReport:
                    _report = new PartStockReport();
                    break;
                case ReportType.CarServiceDurationReport:
                    _report = new CarServiceDurationReport();
                    break;
                case ReportType.ChargePerCarReport:
                    _report = new ChargePerCarReport();
                    break;
                case ReportType.WorkOrderProcessTypesTotalReport:
                    _report = new WorkOrderProcessTypesTotalReport();
                    break;
                case ReportType.FixAssetInventoryReport:
                    _report = new FixAssetInventoryReport();
                    break;
                case ReportType.WorkOrderPartsTotalReport:
                    _report = new WorkOrderPartsTotalReport();
                    break;
                case ReportType.KilometerDistributionReport:
                    _report = new KilometerDistributionReport();
                    break;
                case ReportType.PartStockActivityReport:
                    _report = new PartStockActivityReport();
                    break;
                case ReportType.SparePartControlReport:
                    _report = new SparePartControlReport();
                    break;
                case ReportType.CampaignSummaryReport:
                    _report = new CampaignSummaryReport();
                    break;
                case ReportType.LaborCostPerVehicleReport:
                    _report = new LaborCostPerVehicleReport();
                    break;
                case ReportType.PersonnelInfoReport:
                    _report = new PersonnelInfoReport();
                    break;
                case ReportType.InvoiceInfoReport:
                    _report = new InvoiceInfoReport();
                    break;
                case ReportType.GuaranteeReport:
                    _report = new GuaranteeReport();
                    break;
                case ReportType.WorkOrderPerformanceReport:
                    _report = new WorkOrderPerformanceReport();
                    break;
                case ReportType.WorkOrderReport:
                    _report = new WorkOrderReport();
                    break;
                case ReportType.ProposalReport:
                    _report = new ProposalReport();
                    break;
                case ReportType.LabourReport:
                    _report = new LabourReport();
                    break;
                case ReportType.VehicleHistoryReport:
                    _report = new VehicleHistoryReport();
                    break;
                case ReportType.LabourDetailReport:
                    _report = new LabourDetailReport();
                    break;
                case ReportType.SSHReport:
                    _report = new SSHReport();
                    break;
                case ReportType.LabourTechnicianReport:
                    _report = new LabourTechnicianReport();
                    break;
                case ReportType.OtokarSparePartSaleInvoiceReport:
                    _report = new OtokarSparePartSaleInvoiceReport();
                    break;
                case ReportType.FleetWorkOrderReport:
                    _report = new FleetWorkOrderReport();
                    break;
                case ReportType.AlternatePartReport:
                    _report = new AlternatePartReport();
                    break;
                case ReportType.VehicleHistoryDetailsReport:
                    _report = new VehicleHistoryDetailsReport();
                    break;
                case ReportType.VehicleHistoryListWithDetailsReport:
                    _report = new VehicleHistoryListWithDetailsReport();
                    break;
                case ReportType.CampaignRequestApproveReport:
                    _report = new CampaignRequestApproveReport();
                    break;
                default:
                    break;
            }
        }

        public Tuple<bool, string> CreateExcel(UserInfo user, string path, out string fileName, Dictionary<dynamic, dynamic> param, string columns)
        {
            fileName = string.Format("{0}_{1}_{2}.xlsx", _report.ReportName.Replace(" ", "_").ToLower(), user.UserName, DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss"));

            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.REPORT_CREATE_SERVICE, string.Format("{0} raporu oluşturulacak", fileName));
            _report.UserInfo = user;
            var list = _report.CreateExcel(param).ToList();

            LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.REPORT_CREATE_SERVICE, string.Format("{0} raporu için {1} adet satır geldi", fileName, list.Count));

            if (list.Count > 0)
            {
                try
                {
                    object obj = list.FirstOrDefault();

                    var propertiesArr = obj.GetType().GetProperties();
                    var propList = new List<PropertyInfo>();

                    //Kolonlar alınır
                    columns.Split(',').ToList().ForEach(column =>
                    {
                        if (propertiesArr.Any(x => x.Name == column))
                            propList.Add(propertiesArr.FirstOrDefault(x => x.Name == column));
                    });


                    var header = new List<string>();
                    foreach (var prop in propList)
                    {
                        var attrs = obj.GetType().GetProperty(prop.Name).GetCustomAttributes(true);
                        foreach (var attr in attrs)
                        {
                            var cAttr = (attr as DisplayAttribute);
                            if (cAttr != null)
                            {
                                header.Add(CommonUtility.GetResourceValue(cAttr.Name));
                            }
                            else
                            {
                                header.Add(prop.Name);
                            }
                        }
                    }

                    var rows = new List<List<Tuple<object, string>>>();
                    foreach (var item in list)
                    {
                        var l = new List<Tuple<object, string>>();
                        foreach (var prop in propList)
                        {
                            var t = Tuple.Create((object)prop.GetValue(item), prop.Name);
                            l.Add(t);
                        }
                        rows.Add(l);
                    }

                    var excelBytes = new ExcelHelper().GenerateExcel(header, rows, null, reportName: fileName, reportObject: obj);
                    var fullPath = string.Format("{0}\\{1}", path, fileName);
                    File.WriteAllBytes(fullPath, excelBytes);

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.REPORT_CREATE_SERVICE, string.Format("{0} raporu için excel oluşturuldu.", fileName));

                    return Tuple.Create(true, string.Empty);
                }
                catch (Exception ex)
                {
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.REPORT_CREATE_SERVICE, string.Format("{0} raporunda hata alındı.Hata : {1}", fileName, ex.Message));

                    return Tuple.Create(false, ex.Message);
                }
            }
            else
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.REPORT_CREATE_SERVICE, string.Format("{0} raporu için oluşturulacak satır bulunamadı.", fileName));

                return Tuple.Create(false, MessageResource.Report_Create_NotFoundRows);
            }
        }

    }
}
