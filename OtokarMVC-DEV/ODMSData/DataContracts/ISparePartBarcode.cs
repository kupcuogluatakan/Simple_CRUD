using System.Collections.Generic;

namespace ODMSData.DataContracts
{
    public interface ISparePartBarcode<T>
    {
        List<T> List(int workOrderId,bool isPrinted);
    }
}
