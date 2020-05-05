using System.Collections.Generic;
using System.Web.Mvc;

namespace ODMSModel.WorkOrderCard
{
    public class ChangeProcessTypeModel
    {
        public string CurrentProcessType { get; set; }
        public List<SelectListItem> ProcessTypeList { get; set; }

        public ChangeProcessTypeModel()
        {
            ProcessTypeList=new List<SelectListItem>();
        }
    }

}
