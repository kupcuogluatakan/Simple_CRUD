using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.EducationRequests
{
    public class EducationRequestsListModel:BaseListWithPagingModel
    {

        public EducationRequestsListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
             var dMapper = new Dictionary<string, string>
                 {
                     {"CustomerName", "CUSTOMER_NAME"},
                     {"DealerName", "ER.ID_DEALER"},
                     {"TCIdentityNo", "U.TC_IDENTITY_NO"},
                     {"CreateDate", "ER.CREATE_DATE"}
                 };
            SetMapper(dMapper);

        }

        public EducationRequestsListModel()
        {
            
        }

        public int EducationRequestId { get; set; }
         [Display(Name = "Customer_Display_TCIdentityNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string TCIdentityNo { get; set; }
         [Display(Name = "Global_Display_WorkerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }
         [Display(Name = "User_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }
         [Display(Name = "EducationRequests_Display_RequestTime", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime RequestTime { get; set; }
        public string EducationCode { get; set; }
    }
}
