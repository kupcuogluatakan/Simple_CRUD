using ODMSBusiness;
using ODMSCommon;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.Vehicle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ODMSService.Helpers;
using Quartz;

namespace ODMSService.Jobs
{
    [DisallowConcurrentExecution]
    internal class VehicleSalesJob : JobBase, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var serviceCallSchedule = serviceCallScheduleBL.Get("VEHICLE_SALES_SERVICE").Model;
            base.StartService(serviceCallSchedule);
            if (serviceCallSchedule == null || serviceCallSchedule.ServiceId == 0)
                return Task.CompletedTask;

            var serviceCallLog = new ServiceCallLogModel();
            var mainBL = new VehicleBL();

            var lastSuccessCallDateTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd HH:mm:ss") : DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            var lastSuccessCallDate = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("yyyyMMdd") : DateTime.Now.ToString("yyyyMMdd");
            var lastSuccessCallTime = serviceCallSchedule.LastSuccessCallDate.HasValue ? serviceCallSchedule.LastSuccessCallDate.Value.ToString("HH:mm:ss") : DateTime.Now.ToString("HH:mm:ss");

            try
            {
                using (var pssc = GetClient())
                {
                    var listModel = new List<VehicleSalesXMLModel>();
                    var listCustModel = new List<VehicleCustomerXMLModel>();

                    #region Request XML Logging

                    if (serviceCallSchedule.IsResponseLogged)
                    {
                        serviceCallLog.ReqResDic = new Dictionary<string, string>()
                                        {
                                            {"DateTime", lastSuccessCallDateTime}
                                        };
                        serviceCallLog.ServiceName = serviceCallSchedule.TriggerServiceName; // "SPARE_PART_SERVICE";
                        serviceCallLog.IsManuel = serviceCallSchedule.IsTriggerService;
                        serviceCallLog.LogType = CommonValues.LogType.Request;
                        base.LogRequestService(serviceCallLog);
                    }

                    #endregion Request XML Logging

                    #region Calling Vehicle Sales Service & Customer Service & Filling Model

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SALES_SERVICE, "ZDMS_ARAC_SATIS_BILGILERI servisi çağırılıyor");
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SALES_SERVICE, "LastSuccessCall -> " + lastSuccessCallDate);
                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SALES_SERVICE, "DateTimeNow -> " + DateTime.Now.ToString("yyyyMMdd"));

                    //Call Vehicle Sales Service
                    DataSet rValue = pssc.ZDMS_ARAC_SATIS_BILGILERI(UserName, Password,
                                        lastSuccessCallDate,
                                        lastSuccessCallTime,
                                        DateTime.Now.ToString("yyyyMMdd"),
                                        "");

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SALES_SERVICE, "ZDMS_ARAC_SATIS_BILGILERI servisi bitti");

                    DataTable dtMain = rValue.Tables["t_data"];//For vehicle sales

                    listModel = dtMain.AsEnumerable().Select(rowMain => new VehicleSalesXMLModel
                    {
                        VinNo = rowMain.Field<string>("sasi_no"),
                        DealerSSID = rowMain.Field<string>("sap_musteri_kodu"),
                        DealerCode = rowMain.Field<string>("bayi_kodu"),
                        DealerType = rowMain.Field<string>("bayi_tipi"),
                        EngineNo = rowMain.Field<string>("motor_no").ToString(),
                        WarrantyStartDate = rowMain.Table.Columns.Contains("satis_tr") ? rowMain.Field<DateTime?>("satis_tr") : null,
                        WarrantyEndDate = rowMain.Table.Columns.Contains("garanti_bitis_tr") ? rowMain.Field<DateTime?>("garanti_bitis_tr") : null,
                        CustomerNo = rowMain.Field<int>("musteri_no").ToString(),
                        Plate = string.IsNullOrEmpty(rowMain.Field<string>("plaka_no")) ? string.Empty : rowMain.Field<string>("plaka_no").ToUpper().Replace(" ", ""),
                        WarrantyKm = rowMain.Table.Columns.Contains("garantisurekm") ? rowMain.Field<int>("garantisurekm").ToString() : "0",
                        WarrantyColorEndDate = rowMain.Table.Columns.Contains("boya_grn_bitis_tr") ? rowMain.Field<DateTime?>("boya_grn_bitis_tr") : null,
                        WarrantyCorrosionEndDate = rowMain.Table.Columns.Contains("korozyon_grn_bitis_tr") ? rowMain.Field<DateTime?>("korozyon_grn_bitis_tr") : null
                    }).ToList();

                    foreach (var cModel in listModel)
                    {
                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SALES_SERVICE, "ZDMS_MUSTERI_BILGILERI servisi çağırılıyor");
                        DataSet customerDs = pssc.ZDMS_MUSTERI_BILGILERI(UserName, Password, cModel.CustomerNo,
                            cModel.CustomerNo);
                        LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SALES_SERVICE, "ZDMS_MUSTERI_BILGILERI servisi bitti");

                        DataTable dtCust = new DataTable();

                        dtCust = customerDs.Tables["t_data"]; //For vehicle customer

                        var dto = dtCust.AsEnumerable().Select(row => new VehicleCustomerXMLModel
                        {
                            Address = row.Table.Columns.Contains("adres") ? row.Field<string>("adres") : null,
                            CityPlateCode =
                                row.Table.Columns.Contains("il_plaka_kodu")
                                    ? row.Field<string>("il_plaka_kodu")
                                    : null,
                            Company = row.Table.Columns.Contains("firma") ? row.Field<string>("firma") : null,
                            CustomerName =
                                row.Table.Columns.Contains("musteri_adi")
                                    ? row.Field<string>("musteri_adi")
                                    : null,
                            CustomerNo = cModel.CustomerNo,
                            CustomerSSID = cModel.CustomerNo, //row.Field<string>("sap_musteri_kodu"),
                            CountryCode =
                                row.Table.Columns.Contains("ulke_kodu") ? row.Field<string>("ulke_kodu") : null,
                            DealerName =
                                row.Table.Columns.Contains("bayi_adi") ? row.Field<string>("bayi_adi") : null,
                            Desc = row.Table.Columns.Contains("aciklama") ? row.Field<string>("aciklama") : null,
                            MobilePhone =
                                row.Table.Columns.Contains("cep_tel") ? row.Field<string>("cep_tel") : null,
                            Phone = row.Table.Columns.Contains("telefon") ? row.Field<string>("telefon") : null,
                            //row.Field<string>("telefon"),
                            TCIdentity =
                                row.Table.Columns.Contains("tc_kimlik_no")
                                    ? row.Field<string>("tc_kimlik_no")
                                    : null,
                            VatNo =
                                row.Table.Columns.Contains("vergi_no")
                                    ? row.Field<string>("vergi_no")
                                    : null,
                            VatOffice =
                                row.Table.Columns.Contains("vergi_dairesi")
                                    ? row.Field<string>("vergi_dairesi")
                                    : null,
                            IsPublic = row.Table.Columns.Contains("kamu") ? row.Field<string>("kamu") : null,
                            Email = row.Table.Columns.Contains("email") ? row.Field<string>("email") : null,
                            Fax = row.Table.Columns.Contains("fax") ? row.Field<string>("fax") : null,
                            CityName = row.Table.Columns.Contains("il_adi") ? row.Field<string>("il_adi") : null,
                            VinNo = cModel.VinNo,
                            EngineNo = cModel.EngineNo,
                            WarrantyStartDate = cModel.WarrantyStartDate,
                            WarrantyEndDate = cModel.WarrantyEndDate,
                            WarrantyColorEndDate = cModel.WarrantyColorEndDate,
                            WarrantyCorrosionEndDate = cModel.WarrantyCorrosionEndDate,
                            WarrantyKm = cModel.WarrantyKm,
                            Plate = cModel.Plate
                        }).SingleOrDefault();
                        if (dto != null)
                        {
                            if (!string.IsNullOrEmpty(dto.VinNo))
                                dto.VinNo.Replace(" ", "");
                            if (dto.VinNo == "NLRTMF100JA022450")
                                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SALES_SERVICE, "NLRTMF100JA022450 geldi");

                            listCustModel.Add(dto);
                        }
                    }
                    // vergi no boşluk al


                    #endregion Calling Vehicle Sales Service & Customer Service & Filling Model

                    #region DB Update

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SALES_SERVICE, "XMLtoDBVehicleCustomer metodu çağırılıyor");

                    var re = mainBL.XMLtoDBVehicleCustomer(listCustModel).Model;
                    serviceCallLog.IsSuccess = re.IsSuccess;
                    serviceCallLog.ErrorMessage = re.ErrorMessage;
                    serviceCallLog.ErrorModel = re.ErrorModel;

                    LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SALES_SERVICE, "XMLtoDBVehicleCustomer metodu bitti");

                    #endregion

                    #region Response & Error XML Logging

                    if (serviceCallSchedule.IsResponseLogged)
                    {
                        serviceCallLog.LogType = CommonValues.LogType.Response;
                        serviceCallLog.LogXml = XElement.Parse(rValue.ToXml());
                        base.LogResponseService(serviceCallLog);
                    }

                    #endregion Response & Error XML Logging
                }

                if (!serviceCallLog.IsSuccess && serviceCallLog.ErrorModel != null && serviceCallLog.ErrorModel.Any())
                {
                    SendErrorsMail(serviceCallLog.ErrorModel);
                }

            }
            catch (Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.VEHICLE_SALES_SERVICE, string.Format("Hata : {0}", ex.Message));
                serviceCallLog.ErrorMessage = ex.Message;
                base.LogResponseService(serviceCallLog);
            }
            finally
            {
                base.EndService(serviceCallSchedule);
            }
            return Task.CompletedTask;
        }

        private void SendErrorsMail(IEnumerable<ServiceCallScheduleErrorListModel> errors)
        {
            string title = string.Format("ARAÇ SATIŞ SERVİSİ HATALARI - {0}", DateTime.Now.ToString("dd.MM.yyyy hh:mm"));
            new GosErrorEmailSender().Send(errors, title);
        }
    }
}