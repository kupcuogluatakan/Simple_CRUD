using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.VehicleNote
{
    public class VehicleNotesListModel : BaseListWithPagingModel
    {
        public VehicleNotesListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"VehicleNotesId", "ID_VEHICLE_NOTE"},
                    {"DealerName", "DEALER_NAME"},
                    {"Note", "NOTE"},
                    {"IsActiveName", "IS_ACTIVE_STRING"},
                    {"VehicleKm","VEHICLE_KM"}
                };
            SetMapper(dMapper);
        }

        public VehicleNotesListModel()
        {
        }

        [Display(Name = "VehicleNote_Display_KM", ResourceType = typeof(MessageResource))]
        public decimal VehicleKm { get; set; }

        public int VehicleNotesId { get; set;}

        public int VehicleId { get; set;}
         [Display(Name = "VehicleNote_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
         [Display(Name = "VehicleNote_Display_Note", ResourceType = typeof(MessageResource))]
        public string Note { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
         public int? IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
         public string IsActiveName { get; set; }

        public DateTime? ApproveDate { get; set; }

    }
}
