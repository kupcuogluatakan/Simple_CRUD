using ODMSModel.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Bank
{
    public class BankFilter : FilterBase
    {
        public PropertyFilter<bool> IsActive { get; set; }
        public BankFilter()
        {
            IsActive = new PropertyFilter<bool> { Name = "IsActive" };
        }
    }
}
