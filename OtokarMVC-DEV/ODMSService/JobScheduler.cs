using ODMSBusiness;
using ODMSModel.ServiceCallSchedule;
using ODMSService.Helpers;
using ODMSService.Jobs;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSService
{
    public class JobScheduler
    {
        public static void Start()
        {
            /*SparePartJob deneme = new SparePartJob();
            deneme.Execute(null);*/

            /*
            Emre - Windows Servisteki Excel Raporu kaydı için bu satırlar açılır diğerleri kapanır.
            app.config altında connection string DEV'e döndürülür.
            ExcelFolder path'i kendi bilgisayarındaki yolla değiştirilir.
            Kullanım: Başlangıç projesi ODMSService ayarla. debug olmadan sayfadaki excel butonu bas. Sonra projeyi debug mod'ta çalıştır.

            //ReportCreatorJob job = new ReportCreatorJob();
            //job.Execute(null);
             
             */

            //WorkOrderDetailReportHourControlJob jb = new WorkOrderDetailReportHourControlJob();
            //jb.Execute(null);


            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            scheduler.Start();

            #region Immediately Execute 

            int totalService = 0;
            var bl = new ServiceScheduleBL();
            List<ServiceScheduleModel> listModel = bl.GetServiceScheduleList(out totalService).Data;

            IJobDetail jobNow = JobBuilder.Create<RunImmediatelyJob>().Build();
            ITrigger triggerNow = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s
                    .OnEveryDay()
                    .WithIntervalInSeconds(10)
                  )
                .Build();
            scheduler.ScheduleJob(jobNow, triggerNow);

            #endregion

            #region Scheduled Execute

            if (listModel.Any())
            {
                foreach (var service in listModel)
                {
                    if (service.Name == "VEHICLE_GROUP_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<VehicleGroupJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "VEHICLE_MODEL_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<VehicleModelJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "VEHICLE_TYPE_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<VehicleTypeJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "VEHICLE_CODE_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<VehicleCodeJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "SPART_INFO_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<PartClassInfoJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "SPARE_PART_SPLITTING")
                    {
                        IJobDetail job = JobBuilder.Create<SparePartSplittingJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "RETAIL_PRICE_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<RetailPriceJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "SPARE_PART_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<SparePartJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "GIF_SAVE")
                    {
                        IJobDetail job = JobBuilder.Create<GetGifNoJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "MRP_CONTROL")
                    {
                        IJobDetail job = JobBuilder.Create<ControlMrpJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "VEHICLE_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<VehicleJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "VEHICLE_SALES_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<VehicleSalesJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "FREE_PO_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<FreePurchaseOrderJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "DENY_REASON_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<DenyReasonJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "ZDMS_YP_ISKONTO")
                    {
                        IJobDetail job = JobBuilder.Create<SparePartSupplyDiscountRatioJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "VEHICLE_WARRANTY_END_CONTROL")
                    {
                        IJobDetail job = JobBuilder.Create<VehicleWarrantyEndControlJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "CAMPAING_PRICE_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<GuaranteeCampaignPartPriceJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "WORK_ORDER_DAY_CONTROL")
                    {
                        IJobDetail job = JobBuilder.Create<WorkOrderDetailReportDayControlJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "REPORT_CREATE_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<ReportCreatorJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "CLAIM_PERIOD_LAST_DAY_CONTROL")
                    {
                        IJobDetail job = JobBuilder.Create<ControlClaimPeriodLastDayJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);

                    }
                    else if (service.Name == "CLAIM_PERIOD_PART_LIST_CONTROL")
                    {
                        IJobDetail job = JobBuilder.Create<ControlClaimPeriodPartListApproveJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "WORK_ORDER_HOUR_CONTROL")
                    {
                        IJobDetail job = JobBuilder.Create<WorkOrderDetailReportHourControlJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .WithDailyTimeIntervalSchedule
                              (s =>
                                 s
                                .OnEveryDay()
                                .WithIntervalInMinutes(service.CallInterval)
                              ).StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                }
            }

            #endregion


        }
    }
}
