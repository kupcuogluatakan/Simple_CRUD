using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Reports
{
    public class WorkOrderInfoDetailParameter
    {
        public int id { get; set; }
        public string StatusIds { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public int GroupType { get; set; }
        public string DealerIdList { get; set; }
        public string RegionIdList { get; set; }
        public string CustomerIdList { get; set; }
        public string VehicleModelList { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string VinNo { get; set; }
        public string CustTypeList { get; set; }
       
    }
}
