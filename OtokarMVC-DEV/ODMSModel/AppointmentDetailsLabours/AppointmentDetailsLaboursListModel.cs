using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.AppointmentDetailsLabours
{
    public class AppointmentDetailsLaboursListModel : BaseListWithPagingModel
    {
        public int AppointmentIndicatorLabourId { get; set; }
        [Display(Name = "LabourDuration_Display_Labour", ResourceType = typeof(MessageResource))]
        public string LabourName { get; set; }
        [Display(Name = "Global_Display_Quantity", ResourceType = typeof(MessageResource))]
        public int Quantity { get; set; }
        [Display(Name = "Labour_Display_Code", ResourceType = typeof(MessageResource))]
        public string LabourCode { get; set; }

        public int AppointmentIndicatorId { get; set; }
        [Display(Name = "Global_Display_Price", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ListPrice { get; set; }

        public AppointmentDetailsLaboursListModel() { }

        public AppointmentDetailsLaboursListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"AppointmentIndicatorId", "APPOINTMENT_INDICATOR_ID"},
                     {"AppointmentIndicatorLabourId", "APPOINTMENT_INDICATOR_LABOUR_ID"},
                     {"LabourName", "LABOUR_ID"},
                     {"Quantity", "QUANTITY"} ,
                     {"LabourCode","LABOUR_CODE"} ,
                     {"ListPrice","LABOUR_PRICE"}
                 };
            SetMapper(dMapper);

        }



    }
}
