using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;

namespace ODMSModel.TechnicianOperationControl
{
    public class TechnicianOperationListModel : BaseListWithPagingModel
    {
        public TechnicianOperationListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {                    
                     {"CheckInDateString", "CHECKIN_DATE"},
                     {"CheckOutDateString", "CHECKOUT_DATE"},
                     {"CreateDate", "CREATE_DATE"}
                 };
            SetMapper(dMapper);
        }

        public TechnicianOperationListModel()
        {

        }

        public int UserId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime? CheckOutDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string ProcessTypeDesc { get; set; }             
    }
}
