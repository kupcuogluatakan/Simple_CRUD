using ODMSBusiness;
using ODMSModel.ServiceCallSchedule;
using ODMSService.Helpers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Quartz.Impl;

namespace ODMSService.Jobs
{
    internal class RunImmediatelyJob : JobBase, IJob
    {

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
                scheduler.Start();

                int totalService = 0;
                var bl = new ServiceScheduleBL();
                List<ServiceScheduleModel> listModel = bl.GetServiceScheduleList(out totalService).Data;
                foreach (var service in listModel.Where(x => x.IsTrigger).ToList())
                {

                    if (service.Name == "VEHICLE_GROUP_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<VehicleGroupJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "VEHICLE_MODEL_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<VehicleModelJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "VEHICLE_TYPE_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<VehicleTypeJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "VEHICLE_CODE_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<VehicleCodeJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "SPART_INFO_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<PartClassInfoJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "SPARE_PART_SPLITTING")
                    {
                        IJobDetail job = JobBuilder.Create<SparePartSplittingJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();
                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "RETAIL_PRICE_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<RetailPriceJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "SPARE_PART_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<SparePartJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "GIF_SAVE")
                    {
                        IJobDetail job = JobBuilder.Create<GetGifNoJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                           .StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "MRP_CONTROL")
                    {
                        IJobDetail job = JobBuilder.Create<ControlMrpJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();

                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "VEHICLE_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<VehicleJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();
                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "VEHICLE_SALES_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<VehicleSalesJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                           .StartNow()
                            .Build();
                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "FREE_PO_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<FreePurchaseOrderJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();
                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "DENY_REASON_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<DenyReasonJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();
                        scheduler.ScheduleJob(job, trigger);
                    }

                    else if (service.Name == "ZDMS_YP_ISKONTO")
                    {
                        IJobDetail job = JobBuilder.Create<SparePartSupplyDiscountRatioJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                           .StartNow()
                            .Build();
                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "VEHICLE_WARRANTY_END_CONTROL")
                    {
                        IJobDetail job = JobBuilder.Create<VehicleWarrantyEndControlJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                           .StartNow()
                            .Build();
                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "CAMPAING_PRICE_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<GuaranteeCampaignPartPriceJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();
                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "WORK_ORDER_DAY_CONTROL")
                    {
                        IJobDetail job = JobBuilder.Create<WorkOrderDetailReportDayControlJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                           .StartNow()
                            .Build();
                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "REPORT_CREATE_SERVICE")
                    {
                        IJobDetail job = JobBuilder.Create<ReportCreatorJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();
                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "CLAIM_PERIOD_LAST_DAY_CONTROL")
                    {
                        IJobDetail job = JobBuilder.Create<ControlClaimPeriodLastDayJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();
                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "WORK_ORDER_HOUR_CONTROL")
                    {
                        IJobDetail job = JobBuilder.Create<WorkOrderDetailReportHourControlJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                           .StartNow()
                            .Build();
                        scheduler.ScheduleJob(job, trigger);
                    }
                    else if (service.Name == "CLAIM_PERIOD_PART_LIST_CONTROL")
                    {
                        IJobDetail job = JobBuilder.Create<ControlClaimPeriodPartListApproveJob>().Build();
                        ITrigger trigger = TriggerBuilder.Create()
                            .StartNow()
                            .Build();
                        scheduler.ScheduleJob(job, trigger);
                    }

                }

                //scheduler.Shutdown();
            }
            catch (Exception ex)
            {
                LoggerFactory.Logger.AddLog(LoggerType.File, ServiceType.RUN_IMMEDIATELY, string.Format("Hata : {0}", ex.Message));
            }

            return Task.CompletedTask;
        }
    }
}
