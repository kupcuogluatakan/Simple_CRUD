using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.Scrap
{
    [Validator(typeof(ScrapViewModelValidator))]
    public class ScrapViewModel : ModelBase
    {
        [Display(Name = "Scrap_Display_ScrapId", ResourceType = typeof(MessageResource))]
        public int ScrapId { get; set; }

        public int? DealerId { get; set; }
        [Display(Name = "Scrap_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "Scrap_Display_ScrapDate", ResourceType = typeof(MessageResource))]
        public DateTime? ScrapDate { get; set; }

        public int? DocId { get; set; }
        [Display(Name = "Scrap_Display_DocName", ResourceType = typeof(MessageResource))]
        public string DocName { get; set; }

        [Display(Name = "Scrap_Display_ScrapReasonDesc", ResourceType = typeof(MessageResource))]
        public string ScrapReasonDesc { get; set; }

        public int? ScrapReasonId { get; set; }
        [Display(Name = "Scrap_Display_ScrapReasonName", ResourceType = typeof(MessageResource))]
        public string ScrapReasonName { get; set; }
    }
}
