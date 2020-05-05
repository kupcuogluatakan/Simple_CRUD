using System.Collections.Generic;
using ODMSModel.SupplierDispatchPart;
using ODMSCommon.Security;

namespace ODMSData.DataContracts
{
    public interface ISupplierDispatchPart<T>
    {
        T Get(UserInfo user, T model);

        IEnumerable<SupplierDispatchPartListModel> List(UserInfo user, SupplierDispatchPartListModel model, out int totalCnt);

        void Insert(UserInfo user, T model);

        void Insert(UserInfo user, SupplierDispatchPartOrderViewModel model);

        void Update(UserInfo user, T model);

        void Delete(UserInfo user, T model);

        SupplierDispatchPartViewModel Update(UserInfo user, IEnumerable<SupplierDispatchPartListModel> listModel);
    }
}
