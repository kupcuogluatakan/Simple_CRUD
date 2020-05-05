using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.WorkOrder
{
    public class CustomerChangeModel
    {
        public string VehicleCustomerName { get; set; }
        public string CustomerName { get; set; }
        public int  CustomerId { get; set; }
        public int VehicleCustomerId { get; set; }
    }
}
