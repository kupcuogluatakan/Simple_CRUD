using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;
namespace ODMSModel.AppointmentDetailsMaintenance
{
    public class AppointmentDetailsMaintenanceListModel : BaseListWithPagingModel
    {
        public AppointmentDetailsMaintenanceListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"MaintId", "ID_MAINT"},
                    {"AppIndicId", "APPOINTMENT_INDICATOR_ID"},
                    {"AppId","APPOINTMENT_ID"},
                    {"Name", "NAME"},
                    {"IsRemoved","IS_REMOVED"},
                    {"Quantity","QUANTITY"},
                    {"Type","TYPE"},
                    {"LabourPartId","LABOUR_PART_ID"},
                    {"ObjType","OBJ_TYPE"}
                };
            SetMapper(dMapper);

        }

        public AppointmentDetailsMaintenanceListModel()
        {

        }

        public int Id { get; set; }

        public int MaintId { get; set; }

        [Display(Name = "AppointmentDetailsMaint_Display_Type", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int AppIndicId { get; set; }

        public int AppId { get; set; }

        [Display(Name = "AppointmentDetailsMaint_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Name { get; set; }

        public bool? IsRemoved { get; set; }

        [Display(Name = "Global_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int Quantity { get; set; }

        public int LabourPartId { get; set; }

        [Display(Name = "AppointmentDetailsMaint_Display_Type", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Type { get; set; }

        [Display(Name = "Global_Display_Price", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Price { get; set; }

        public int ObjType { get; set; }

        public bool IsMust { get; set; }

    }
}
