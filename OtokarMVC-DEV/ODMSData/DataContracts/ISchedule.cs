using System.Collections.Generic;
using ODMSModel.Schedule;

namespace ODMSData.DataContracts
{
    public interface ISchedule<in T>
    {
        List<ScheduleViewModel> List(ScheduleViewModel model, out int totalCnt);

        void Insert(T model);

        void Delete(T model);

        void Update(T model);

        ScheduleViewModel Get(T model);
    }
}
