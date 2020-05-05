using System.Collections.Generic;
using System.Web.Mvc;
using ODMSModel.PurchaseOrderType;

namespace ODMSData.DataContracts
{
    public interface IPurchaseOrderType<T>
    {
        List<PurchaseOrderTypeListModel> List(PurchaseOrderTypeListModel model, out int totalCnt);

        void Insert(T model);

        void Delete(T model);

        void Update(T model);

        T Get(T model);

        List<SelectListItem> ListPurchaseOrderTypeAsSelectListItem(int? dealerId);

        List<SelectListItem> PurchaseOrderTypeList();
    }
}

