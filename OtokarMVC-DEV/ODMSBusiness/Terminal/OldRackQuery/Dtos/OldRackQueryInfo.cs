using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.OldRackQuery.Dtos
{
    public class OldRackQueryInfo:IResponse
    {
        public string PartCode { get; set; }
        public long PartId { get; set; }
        public string PartName { get; set; }
        public string Unit { get; set; }
        public EnumerableResponse<OldRackQueryListItem> ResultList { get; set; }
    }
}
