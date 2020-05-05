using System.Collections.Generic;
using ODMSModel.PurchaseOrderGroupRelation;

namespace ODMSData.DataContracts
{
    public interface IPurchaseOrderGroupRelationList<T>
    {
        List<T> ListOfInclueded(T model);

        List<T> ListOfNotInclueded(T model);

        void Update(PurchaseGroupRelationSaveModel model);
    }
}
