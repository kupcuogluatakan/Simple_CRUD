using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSModel.ListModel;

namespace ODMSModel.Reports
{
    public class ReportListModelBase:BaseListWithPagingModel
    {
        public ReportListModelBase(Kendo.Mvc.UI.DataSourceRequest request) :base(request)
        {
        }

        public ReportListModelBase()
        {   
        }

        protected override string GetDbColumnName(string key)
        {
            return key;
        }
    }
}
