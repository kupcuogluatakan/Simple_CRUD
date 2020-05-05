using System.Collections.Generic;
using ODMSModel.ServiceCallSchedule;

namespace ODMSData.DataContracts
{
    public interface IServiceCallSchedule<T>
    {
        IEnumerable<ServiceCallScheduleListModel> List(ServiceCallScheduleListModel model, out int totalCnt);
      
        void Update(T model);

        T Get(T model);
    }
}
