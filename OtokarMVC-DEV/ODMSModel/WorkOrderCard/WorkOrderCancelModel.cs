using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.WorkOrderCard
{
    [Validator(typeof(WorkOrderCancelModelValidator))]
    public class WorkOrderCancelModel : ModelBase
    {
        public long WorkOrderId { get; set; }
        public string CancelReason { get; set; }
        public int VehicleId { get; set; }
    }
}
