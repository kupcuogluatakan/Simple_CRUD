using System.Collections.Generic;
using System.Text;

namespace ODMSModel.PurchaseOrderGroupRelation
{
    public class PurchaseGroupRelationSaveModel : ModelBase
    {
        public int PurchaseOrderGroupId { get; set; }
        
        public List<int> DealerIdListOfIncluded { get; set; }
        
        public string SerializedDealerIdsOfIncluded
        {
            get
            {
                if (DealerIdListOfIncluded == null || DealerIdListOfIncluded.Count == 0)
                    return "0";
                
                var builder = new StringBuilder();
                DealerIdListOfIncluded.ForEach(id =>
                {
                    builder.Append(id);
                    builder.Append(',');
                });
                return builder.ToString().Substring(0, builder.Length - 1);
            }
        }
    }
}
