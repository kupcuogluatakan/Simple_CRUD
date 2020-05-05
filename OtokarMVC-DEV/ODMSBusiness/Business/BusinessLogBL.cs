using ODMSData;
using ODMSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Business
{
    public class BusinessLogBL : BaseBusiness
    {
        public BusinessLogModel GetBusinessLog(int? logId,string businessName ,string name)
        {
            return new BusinessLogData().GetBusinessLog(logId, businessName, name);
        }

        public void DMLBusinessLog(BusinessLogModel model)
        {
            var data = new BusinessLogData();
            data.DMLBusinessLog(model);
        }
    }
}
