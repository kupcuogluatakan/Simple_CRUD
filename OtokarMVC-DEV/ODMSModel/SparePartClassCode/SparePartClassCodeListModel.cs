using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.SparePartClassCode
{
    public class SparePartClassCodeListModel
    {
        public string Code { get; set; }
        public string Desc { get; set; }
        public bool IsActive { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsJanpol { get; set; }
    }
}
