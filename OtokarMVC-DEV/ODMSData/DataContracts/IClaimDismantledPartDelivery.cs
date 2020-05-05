using System.Collections.Generic;
using ODMSModel.ClaimDismantledPartDelivery;
using ODMSCommon.Security;

namespace ODMSData.DataContracts
{
    public interface IClaimDismantledPartDelivery<T>
    {
        List<ClaimDismantledPartDeliveryListModel> List(UserInfo user, ClaimDismantledPartDeliveryListModel filter, out int totalCnt);

        void Insert(UserInfo user, T model);

        void Delete(UserInfo user, T model);

        void Update(UserInfo user, T model);

        T Get(UserInfo user, T model);      
    }
}
