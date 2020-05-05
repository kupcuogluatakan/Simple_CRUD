using System.Collections.Generic;
using ODMSModel.TechnicianOperationControl;
using ODMSCommon.Security;

namespace ODMSData.DataContracts
{
    public interface ITechnicianOperationControl<T>
    {
        T Get(UserInfo user, T model);

        IEnumerable<TechnicianOperationListModel> List(UserInfo user, TechnicianOperationListModel model, out int totalCnt);

        void Insert(UserInfo user, T model);
    }
}
