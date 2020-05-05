using System.Collections.Generic;
using System.Text;

namespace ODMSModel.GuaranteeAuthorityGroupVehicleModels
{
    public class GuaranteeAuthorityGroupVehicleSaveModel : ModelBase
    {
        public int id { get; set; }
        public List<string> ModelKodList { get; set; }

        public string SerializedModelKods
        {
            get
            {
                if (ModelKodList == null || ModelKodList.Count == 0)
                    return string.Empty;

                var builder = new StringBuilder();
                ModelKodList.ForEach(id =>
                {
                    builder.Append(id);
                    builder.Append(',');
                });
                return builder.ToString().Substring(0, builder.Length - 1);
            }
        }
    }
}
