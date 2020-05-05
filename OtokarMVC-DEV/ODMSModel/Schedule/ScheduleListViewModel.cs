using System;
using System.Collections.Generic;

namespace ODMSModel.Schedule
{
    public class ScheduleListViewModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Qty { get; set; }
        public List<ScheduleViewModel> ScheduleList { get; set; }
    }
}
