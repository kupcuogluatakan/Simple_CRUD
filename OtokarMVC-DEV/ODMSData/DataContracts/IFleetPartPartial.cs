using System.Collections.Generic;
using ODMSModel.FleetPartPartial;
using ODMSCommon.Security;

namespace ODMSData.DataContracts
{
    public interface IFleetPartPartial<in T>
    {
        IEnumerable<FleetPartPartialListModel> List(UserInfo user, FleetPartPartialListModel model, out int totalCnt);

        void Insert(UserInfo user, T model);
      
        void Delete(UserInfo user, T model);

        bool Exists(UserInfo user, T model);

        bool IsPartConstricted(UserInfo user, FleetPartViewModel model);
    }
}
