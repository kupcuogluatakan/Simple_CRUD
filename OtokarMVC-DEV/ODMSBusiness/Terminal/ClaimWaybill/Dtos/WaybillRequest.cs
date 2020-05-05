using System;
using ODMSBusiness.Terminal.ClaimWaybill.Handlers;
using ODMSBusiness.Terminal.Common;
using ODMSCommon.Exception;
using ODMSCommon.Resources;

namespace ODMSBusiness.Terminal.ClaimWaybill.Dtos
{

    /// <summary>
    /// burası  create ve update baseden yapılsın diye yapıldı
    /// </summary>
    public class WaybillRequest
    {
        public int DealerId { get; }
        public int UserId { get; }
        public long ClaimPeriodId { get; }
        public DateTime WaybillDate { get; }
        public string WaybillNo { get; }
        public string WaybillSerialNo { get; }
        public int? ClaimWaybillId { get; }

        protected internal WaybillRequest(long claimPeriodId, int? claimWaybillId, DateTime waybillDate, string waybillNo, string waybillSerialNo,int dealerId,int userId)
        {
            DealerId = dealerId;
            UserId = userId;
            ClaimWaybillId = claimWaybillId;
            WaybillDate = waybillDate;
            WaybillNo = waybillNo;
            WaybillSerialNo = waybillSerialNo;
            ClaimPeriodId = claimPeriodId;
        }
    }
 
    public class CreateWaybillRequest : WaybillRequest, IRequest<CreateWaybillRequest, ClaimWaybillResponse>
    {
        public CreateWaybillRequest(long claimPeriodId, DateTime waybillDate, string waybillNo, string waybillSerialNo,int dealerId,int userId)
            :base(claimPeriodId,null,waybillDate,waybillNo,waybillSerialNo,dealerId,userId)
        {
            if(waybillDate==DateTime.MinValue)
                throw new ODMSException(MessageResource.Terminal_ClaimWaybill_Exception_InvalidWaybillDate);

            if (string.IsNullOrEmpty(waybillNo))
                throw new ODMSException(MessageResource.Terminal_ClaimWaybill_Exception_InvalidWaybillNo);

            if (string.IsNullOrEmpty(waybillSerialNo))
                throw new ODMSException(MessageResource.Terminal_ClaimWaybill_Exception_InvalidWaybillNo);
        }
    }

}
