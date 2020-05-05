using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.GuaranteeRequestApproveDetail
{
    public class GuaranteeCompleteViewModel
    {

        public Int64 Id { get; set; }
        public int seq { get; set; }
        public int type { get; set; }
        public string desc { get; set; }

        public string category { get; set; }

        public List<GuaranteeLaboursListModel> labourList { get; set; }
        public List<GuaranteePartsListModel> partList { get; set; }

    }

}
