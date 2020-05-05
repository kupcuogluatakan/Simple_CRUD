using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ODMSCommon.Resources;

namespace ODMSModel.EducationRequest
{
    public class EducationRequestIndexModel
    {
        [Display(Name = "Education_Display_EducationList", ResourceType = typeof(MessageResource))]
        public List<SelectListItem> EducationList { get; set; }
    }
}
