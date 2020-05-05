using System;

namespace ODMSModel.ServiceCallSchedule
{
    public class ServiceScheduleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int CallInterval { get; set; }
        public DateTime LastSuccessCall { get; set; }
        public DateTime LastCallKS { get; set; } 
        public DateTime NextCallKS { get; set; }
        public bool IsTrigger { get; set; }
        public bool IsLogged { get; set; }
        public bool IsActive { get; set; }

        public int ErrorNo { get; set; }
        public string ErrorDesc { get; set; }
    }
}
