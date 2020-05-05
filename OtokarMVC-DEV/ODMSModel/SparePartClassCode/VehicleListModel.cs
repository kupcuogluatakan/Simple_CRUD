using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.SparePartClassCode
{
    public class VehicleListModel:BaseListWithPagingModel
    {


        public string VehicleSSID { get; set; }
        public string VehicleModel { get; set; }
    }
}
