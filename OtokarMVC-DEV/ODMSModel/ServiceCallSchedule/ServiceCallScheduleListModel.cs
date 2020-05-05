using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.ServiceCallSchedule
{
    public class ServiceCallScheduleListModel : BaseListWithPagingModel
    {
        public ServiceCallScheduleListModel()
        {

        }

        public ServiceCallScheduleListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"ServiceId", "ID_SERVICE"},
                     {"ServiceDescription", "SERVICE_DESCRIPTION"},
                     {"CallIntervalMinute", "CALL_INTERVAL_MINUTE"},
                     {"LastCallDate", "LAST_CALL_DATE_KS"},
                     {"NextCallDate", "NEXT_CALL_DATE_KS"},
                     {"IsTriggerService", "TRIGGER_SERVICE"},
                     {"TriggerServiceName","TRIGGER_SERVICE_NAME"},
                     {"IsResponseLogged","IS_RESPONSE_LOGGED"},
                     {"IsResponseLoggedString","IS_RESPONSE_LOGGED_STRING"},
                 };
            SetMapper(dMapper);
        }


        public int ServiceId { get; set; }


        [Display(Name = "ServiceCallSchedule_View_Description", ResourceType = typeof(MessageResource))]
        public string ServiceDescription { get; set; }

        [Display(Name = "ServiceCallSchedule_View_InvervalMinute", ResourceType = typeof(MessageResource))]
        public string CallIntervalMinute { get; set; }

        [Display(Name = "ServiceCallSchedule_View_LastCallDate", ResourceType = typeof(MessageResource))]
        public string LastCallDate { get; set; }

        [Display(Name = "ServiceCallSchedule_View_NextCallDate", ResourceType = typeof(MessageResource))]
        public string NextCallDate { get; set; }

        public bool IsTriggerService { get; set; }

        [Display(Name = "ServiceCallSchedule_View_IsTriggerService", ResourceType = typeof(MessageResource))]
        public string TriggerServiceName { get; set; }

        public bool IsResponseLogged { get; set; }

        [Display(Name = "ServiceCallSchedule_View_IsResponseLogged", ResourceType = typeof(MessageResource))]
        public string IsResponseLoggedString { get; set; }
    }
}
