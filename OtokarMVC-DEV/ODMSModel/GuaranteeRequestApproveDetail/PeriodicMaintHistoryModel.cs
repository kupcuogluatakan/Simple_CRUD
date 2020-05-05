using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class PeriodicMaintHistoryModel:ModelBase
    {
        [Display(Name = "AppointmentDetails_Display_PerMaint", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }
    }
}
