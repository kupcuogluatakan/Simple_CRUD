using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel
{
    public interface IPagingFilter
    {
        string LanguageCode { get; set; }
        string SortColumn { get; set; }
        string SortDirection { get; set; }
        int Offset { get; set; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
        int ErrorNo { get; set; }
        string ErrorDesc { get; set; }
    }
}
