using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.SparePartClassCode
{
    public class SaveModel : ModelBase
    {
        public string Code { get; set; }
        public List<string> VehicleIdList { get; set; }

        public string SerializedVehicleIds
        {
            get
            {
                if (VehicleIdList == null || VehicleIdList.Count == 0)
                    return string.Empty;
                var builder = new StringBuilder();
                VehicleIdList.ForEach(id => {
                    builder.Append(id);
                    builder.Append(',');
                });
                return builder.ToString().Substring(0, builder.Length - 1);
            }
        }
    }
}
