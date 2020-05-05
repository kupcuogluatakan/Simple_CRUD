using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel
{
    public class BusinessLogModel : ModelBase
    {
        public int LogId { get; set; }
        public string BusinessName { get; set; }
        public string Name { get; set; }
        public string Parameters { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsSuccess { get; set; }
    }
}
