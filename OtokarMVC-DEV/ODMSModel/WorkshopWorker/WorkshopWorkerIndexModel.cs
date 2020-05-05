using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ODMSCommon.Resources;

namespace ODMSModel.WorkshopWorker
{
    public class WorkshopWorkerIndexModel : IndexModelBase
    {
        public List<SelectListItem> WorkshopList { get; set; }
        
        public List<SelectListItem> WorkerList { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(MessageResource))]
        public bool? IsActive { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public WorkshopWorkerIndexModel()
        {
            WorkshopList = new List<SelectListItem>();
            WorkerList = new List<SelectListItem>();
        }
    }
}
