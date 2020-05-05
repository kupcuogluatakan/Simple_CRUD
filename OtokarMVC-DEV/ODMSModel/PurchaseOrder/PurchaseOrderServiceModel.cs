using System;
using System.Collections.Generic;

namespace ODMSModel.PurchaseOrder
{
    public class PurchaseOrderServiceModel : ModelBase
    {
        public Int64? PoNumber { get; set; }
        public List<PurchaseOrderServiceListModel> ListModel { get; set; }
        public string AllParts { get; set; }
        public int OrderNo { get; set; }

    }
}
