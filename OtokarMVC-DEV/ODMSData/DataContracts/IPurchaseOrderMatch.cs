using System.Collections.Generic;
using ODMSModel.PurchaseOrderMatch;

namespace ODMSData.DataContracts
{
    public interface IPurchaseOrderMatch<T>
    {
        List<PurchaseOrderMatchListModel> List(PurchaseOrderMatchListModel model, out int totalCnt);

        void Insert(T model);

        void Delete(T model);

        void Update(T model);

        T Get(T model);
    }
}
