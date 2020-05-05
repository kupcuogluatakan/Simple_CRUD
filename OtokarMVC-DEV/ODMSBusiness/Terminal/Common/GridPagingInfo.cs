using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Terminal.Common
{
    public class GridPagingInfo
    {
        private int _currentPage = 1;

        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set {
                _currentPage = value <= 0 ? 1 : value;
            }
        }

        public int PageSize
        {
            get { return 10; }
        }

        public int Total { get; set; }
        public int TotalPages { get { return (int)Math.Ceiling((decimal)Total/PageSize); } }
    }
}
