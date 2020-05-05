using System.Collections.Generic;
using ODMSModel.LabourMainGroup;
using ODMSCommon.Security;

namespace ODMSData.DataContracts
{
    public interface ILabourMainGroup<T>
    {
        List<LabourMainGroupListModel> List(UserInfo user, LabourMainGroupListModel model, out int totalCnt);

        void Insert(UserInfo user, T model);

        void Delete(UserInfo user, T model);

        void Update(UserInfo user, T model);

        T Get(UserInfo user, T model);

        void Insert(UserInfo user, T model, List<T> listModel);
    }
}
