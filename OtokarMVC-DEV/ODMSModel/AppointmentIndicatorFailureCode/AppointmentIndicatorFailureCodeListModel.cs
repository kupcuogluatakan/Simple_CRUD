using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.AppointmentIndicatorFailureCode
{
    public class AppointmentIndicatorFailureCodeListModel : BaseListWithPagingModel
    {
                public AppointmentIndicatorFailureCodeListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"IdAppointmentIndicatorFailureCode", "ID_APPOINTMENT_INDICATOR_FAILURE_CODE"},
                    {"Code", "CODE"},
                    {"AdminDesc", "ADMIN_DESC"},
                    {"Description","DESCRIPTION"},
                    {"IsActive", "IS_ACTIVE"}
                };
            SetMapper(dMapper);

        }
        public AppointmentIndicatorFailureCodeListModel()
        {
        }

        [Display(Name = "AppointmentIndicatorFailureCode_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int IdAppointmentIndicatorFailureCode { get; set; }

        [Display(Name = "AppointmentIndicatorFailureCode_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Code { get; set; }

        [Display(Name = "AppointmentIndicatorFailureCode_Display_AdminDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string AdminDesc { get; set; }

        [Display(Name = "AppointmentIndicatorFailureCode_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsActiveName { get; set; }

        //[Display(Name = "Global_Display_Active", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        //public new bool? IsActive { get; set; }
    }
    
}
