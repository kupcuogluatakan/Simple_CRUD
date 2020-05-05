using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Terminal.Common
{
    public class ListResponse<T>:IResponse where T:class 
    {
        public List<T> Items { get; set; }
        public GridPagingInfo PageInfo { get; set; }
    }
}
