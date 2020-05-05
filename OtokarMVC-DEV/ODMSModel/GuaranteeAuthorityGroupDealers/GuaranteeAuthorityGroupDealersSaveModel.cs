using System.Collections.Generic;
using System.Text;

namespace ODMSModel.GuaranteeAuthorityGroupDealers
{
    public class GuaranteeAuthorityGroupDealersSaveModel : ModelBase
    {
        public int id { get; set; }
        public List<int> IdDealerList { get; set; }

        public string SerializedDealerIds
        {
            get
            {
                if (IdDealerList == null || IdDealerList.Count == 0)
                    return string.Empty;

                var builder = new StringBuilder();
                IdDealerList.ForEach(id =>
                {
                    builder.Append(id);
                    builder.Append(',');
                });
                return builder.ToString().Substring(0, builder.Length - 1);
            }
        }
    }
}
