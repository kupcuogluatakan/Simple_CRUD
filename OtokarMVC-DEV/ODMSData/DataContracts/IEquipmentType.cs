using System.Collections.Generic;
using ODMSModel.Equipment;
using ODMSCommon.Security;

namespace ODMSData.DataContracts
{
    public interface IEquipmentType<T>
    {
        T Get(UserInfo user, T model);

        IEnumerable<EquipmentTypeListModel> List(UserInfo user, EquipmentTypeListModel model, out int totalCnt);

        void Insert(UserInfo user, T model);

        void Update(UserInfo user, T model);

        void Delete(UserInfo user, T model);
    }
}
