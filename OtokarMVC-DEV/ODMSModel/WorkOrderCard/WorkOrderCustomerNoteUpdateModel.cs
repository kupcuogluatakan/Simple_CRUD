using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.WorkOrderCard
{
    [Validator(typeof(WorkOrderCustomerNoteUpdateModelValidator))]
    public class WorkOrderCustomerNoteUpdateModel : ModelBase
    {
        public long WorkOrderId { get; set; }
        public string Note { get; set; }
    }
}
