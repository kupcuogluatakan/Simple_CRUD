using System;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using FluentValidation.Attributes;

namespace ODMSModel.WorkshopWorker
{
    [Validator(typeof(WorkshopWorkerDetailModelValidator))]
    public class WorkshopWorkerDetailModel : ModelBase
    {
        public int? WorkshopId { get; set; }

        public int? WorkerId { get; set; }

        [Display(Name = "WorkshopWorker_Display_WorkshopName", ResourceType = typeof(MessageResource))]
        public string WorkshopName { get; set; }

        [Display(Name = "WorkshopWorker_Display_WorkerName", ResourceType = typeof(MessageResource))]
        public string WorkerName { get; set; }

        [Display(Name = "Global_Display_StartDate", ResourceType = typeof(MessageResource))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Global_Display_EndDate", ResourceType = typeof(MessageResource))]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]

        public string IsActiveString
        {
            get
            {
                if (IsActive)
                    return MessageResource.Global_Display_Active;
                return MessageResource.Global_Display_Passive;
            }
        }

        [Display(Name = "Global_Display_StartDate", ResourceType = typeof(MessageResource))]
        public string StartDateString { get { return StartDate == null ? string.Empty : StartDate.Value.ToString("dd/MM/yyyy"); } }

        [Display(Name = "Global_Display_EndDate", ResourceType = typeof(MessageResource))]
        public string EndDateString { get { return EndDate == null ? string.Empty : EndDate.Value.ToString("dd/MM/yyyy"); } }
    }
}
