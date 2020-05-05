using System;

namespace ODMSModel.ClaimDismantledParts
{
    public class ClaimWaybillViewModel : ModelBase
    {
        public int ClaimWaybillId { get; set; }

        public int DealerId { get; set; }
        public string WaybillText { get; set; }
        public string WaybillSerialNo { get; set; }
        public string WaybillNo { get; set; }
        public DateTime WaybillDate { get; set; }
        public string AcceptUser { get; set; }
        public DateTime AcceptDate { get; set; }
    }
}
