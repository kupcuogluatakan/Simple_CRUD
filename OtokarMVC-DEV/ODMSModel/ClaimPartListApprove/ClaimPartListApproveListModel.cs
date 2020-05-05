using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSCommon.Resources;

namespace ODMSModel.ClaimPartListApprove
{
    public class ClaimPartListApproveListModel
    {
        public sbyte Type { get; set; }

        [Display(Name = "Global_Display_Select", ResourceType = typeof(MessageResource))]
        public bool IsSelected { get; set; }
        [Display(Name = "Global_Display_Status", ResourceType = typeof(MessageResource))]
        public bool Status { get; set; }

        public long PartId { get; set; }

        [Display(Name = "ClaimRecallPeriodPart_Display_PartCode", ResourceType = typeof(MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "ClaimRecallPeriodPart_Display_PartName", ResourceType = typeof(MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "Appointment_Display_Qty", ResourceType = typeof(MessageResource))]
        public decimal Quantity { get; set; }
    }
}
