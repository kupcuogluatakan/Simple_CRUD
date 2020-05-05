using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.WorkshopWorker
{
    public class WorkshopWorkerListModel : BaseListWithPagingModel
    {
        public int WorkshopId { get; set; }

        [Display(Name = "WorkshopWorker_Display_WorkshopName", ResourceType = typeof(MessageResource))]
        public string WorkshopName { get; set; }

        public int WorkerId { get; set; }

        [Display(Name = "WorkshopWorker_Display_WorkerName", ResourceType = typeof(MessageResource))]
        public string WorkerName { get; set; }

        [Display(Name = "Global_Display_StartDate", ResourceType = typeof(MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Global_Display_EndDate", ResourceType = typeof(MessageResource))]
        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public new string IsActiveString
        {
            get
            {
                if (IsActive)
                    return MessageResource.Global_Display_Active;
                return MessageResource.Global_Display_Passive;
            }
        }

        [Display(Name = "Global_Display_StartDate", ResourceType = typeof(MessageResource))]
        public string StartDateString { get; set; }

        [Display(Name = "Global_Display_EndDate", ResourceType = typeof(MessageResource))]
        public string EndDateString { get; set; }

        public WorkshopWorkerListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"WorkshopId", "ID_WORKSHOP"},
                     {"WorkshopName", "WORKSHOP_NAME"},
                     {"WorkerId", "ID_DMS_USER"},
                     {"WorkerName", "WORKER_NAME"},
                     {"IsActiveString", "IS_ACTIVE"},
                     {"StartDate", "VALID_START_DATE"},
                     {"EndDate", "VALID_END_DATE"}
                 };
            SetMapper(dMapper);
        }

        public WorkshopWorkerListModel() { }
    }
}
