using System.Collections.Generic;
using System.Web.Mvc;

namespace ODMSModel.WorkOrderCard
{
    public class PartReservationDTO:ModelBase
    {
        public string CurrentProcessType { get; set; }
        public List<SelectListItem> ProcessTypeList { get; set; }

        public PartReservationDTO()
        {
            ProcessTypeList=new List<SelectListItem>();
        }
    }
}
