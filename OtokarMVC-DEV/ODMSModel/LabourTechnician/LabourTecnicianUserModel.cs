using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ODMSModel.LabourTechnician
{
    public class LabourTecnicianUserModel : ModelBase
    {
        public int TechnicianId { get; set; }
        [Display(Name = "LabourTechnician_Display_UserNameSurname", ResourceType = typeof(MessageResource))]
        //[UIHint("Teknisyen Kullanıcı Adı boş geçilemez !")]
        public int TechnicianUserId { get; set; }

        [Display(Name = "LabourTechnician_Display_UserNameSurname", ResourceType = typeof(MessageResource))]
        public string TechnicianUserName { get; set; }
        //[Required(ErrorMessage = "Teknisyen Çalışma Saati boş geçilemez !")]
        [Display(Name = "LabourTechnician_Display_WorkTimeReal", ResourceType = typeof(MessageResource))]
       
        public decimal WorkTime { get; set; }
        [Display(Name = "LabourTechnician_Display_StatusName", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

    }
    public class NewLabourTecnicianUserModel : ModelBase
    {
        public int TechnicianId { get; set; }
        [Display(Name = "LabourTechnician_Display_UserNameSurname", ResourceType = typeof(MessageResource))]
        public int TechnicianUserId { get; set; }

        [Display(Name = "LabourTechnician_Display_UserNameSurname", ResourceType = typeof(MessageResource))]
        [Required(ErrorMessage="Teknisyen Seçimi yapılmalı !")]
        public string TechnicianUserName { get; set; }
        [Required(ErrorMessage = "Teknisyen Çalışma Saati boş geçilemez !")]
        [Display(Name = "LabourTechnician_Display_WorkTimeReal", ResourceType = typeof(MessageResource))]
        [Range(1, 9999, ErrorMessage = "İş Süresi 0 dan büyük olmalıdır !")]
        public int WorkTime { get; set; }
        [Display(Name = "LabourTechnician_Display_StatusName", ResourceType = typeof(MessageResource))]
        public string StatusName { get; set; }

    }
}
