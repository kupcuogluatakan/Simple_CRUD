using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODMSWebService.Model
{
    public class VehicleMaint
    {
        public string VinNo { get; set; }
        public int WorkOrderId { get; set; }
        public DateTime WarrantStartDate { get; set; }
        public DateTime WarrantyEndDate { get; set; }
        public DateTime WorkOrderCreateDate { get; set; }
        public DateTime VehicleLeaveDate { get; set; }
        public int DealerId { get; set; }
        public string DealerName { get; set; }
        public string MaintName { get; set; }
    }
}