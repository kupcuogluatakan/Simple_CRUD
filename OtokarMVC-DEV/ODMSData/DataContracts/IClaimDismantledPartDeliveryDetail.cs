using ODMSCommon.Security;
using System.Collections.Generic;

namespace ODMSData.DataContracts
{
    public interface IClaimDismantledPartDeliveryDetail<T>
    {
        List<T> List(UserInfo user, T model, out int totalCnt);

        T Exists(UserInfo user, T model);

        void SetClaimWayBill(UserInfo user, int claimDismantledPartId, int claimWayBillId);
    }
}
