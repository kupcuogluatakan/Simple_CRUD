using System;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.ClaimWaybill.Dtos
{
    public class ClaimWaybillInfo:IResponse
    {
        public long ClaimRecallPeriodId { get; set; }
        public DateTime WaybillDate { get; set; }
        public string WaybillNo { get; set; }
        public string WaybillSerialNo { get; set; }
        public int ClaimWaybillId { get; set; }
    }
}
