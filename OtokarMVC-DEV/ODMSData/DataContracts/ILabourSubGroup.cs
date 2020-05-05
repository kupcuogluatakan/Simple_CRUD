using System.Collections.Generic;
using ODMSModel.LabourSubGroup;
using ODMSCommon.Security;

namespace ODMSData.DataContracts
{
    public interface ILabourSubGroup<T>
    {
        List<LabourSubGroupListModel> List(UserInfo user, LabourSubGroupListModel model, out int totalCnt);

        void Insert(UserInfo user, T model);

        void Delete(UserInfo user, T model);

        void Update(UserInfo user, T model);

        T Get(UserInfo user, T model);

        void Insert(UserInfo user, T model, List<T> listModel);
    }
}
