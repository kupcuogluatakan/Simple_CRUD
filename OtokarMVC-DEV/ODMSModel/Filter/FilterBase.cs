using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Filter
{
    public class FilterBase
    {
        public PropertyFilter<int> PageSize { get; set; }
        public PropertyFilter<int> Offset { get; set; }
        public PropertyFilter<string> SortColumn { get; set; }
        public PropertyFilter<string> SortDirection { get; set; }
        public int TotalCount { get; set; }
        public int ErrorNo { get; set; }
        public string ErrorDesc { get; set; }
        public PropertyFilter<string> Code { get; set; }
        public PropertyFilter<string> Description { get; set; }

        public FilterBase()
        {
            PageSize = new PropertyFilter<int> { Name = "PageSize" };
            Offset = new PropertyFilter<int> { Name = "Offset" };
            SortColumn = new PropertyFilter<string> { Name = "SortColumn" };
            SortDirection = new PropertyFilter<string> { Name = "SortDirection" };
            Code = new PropertyFilter<string> { Name = "Code" };
            Description = new PropertyFilter<string> { Name = "Description" };
        }
    }
}
