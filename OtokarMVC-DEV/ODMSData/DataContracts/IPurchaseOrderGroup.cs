using System.Collections.Generic;
using System.Web.Mvc;
using ODMSModel.PurchaseOrderGroup;

namespace ODMSData.DataContracts
{
    public interface IPurchaseOrderGroup<T>
    {
        List<PurchaseOrderGroupListModel> List(PurchaseOrderGroupListModel model, out int totalCnt);

        void Insert(T model);

        void Delete(T model);

        void Update(T model);

        T Get(T model);

        List<SelectListItem> PurchaseOrderGroupList();
    }
}
