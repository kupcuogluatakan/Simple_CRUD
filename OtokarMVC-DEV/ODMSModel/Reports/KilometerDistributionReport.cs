using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSCommon.Resources;

namespace ODMSModel.Reports
{
    public class KilometerDistributionReport
    {
        [Display(Name = "KilometerDistributionReport_Display_GroupName", ResourceType = typeof(MessageResource))]
        public string GroupName { get; set; }
        [Display(Name = "KilometerDistributionReport_Display_TotalWorkOrderCardNumber", ResourceType = typeof(MessageResource))]
        public int TotalWorkOrderCardNumber { get; set; }
        [Display(Name = "KilometerDistributionReport_Display_TotalWinNumber", ResourceType = typeof(MessageResource))]
        public int TotalWinNumber { get; set; }
        [Display(Name = "KilometerDistributionReport_Display_AverageKilometer", ResourceType = typeof(MessageResource))]
        public double AverageKilometer { get; set; }
        
        public int GroupType { get; set; }
        public string GROUPCODE { get; set; }
        [Display(Name = "KilometerDistributionReport_Display_MaxKm", ResourceType = typeof(MessageResource))]
        public int MaxKM { get; set; }
    }
}
