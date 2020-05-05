using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using Kendo.Mvc.UI;

namespace ODMSModel.VehicleNoteApprove
{
    public class VehicleNoteApproveListModel : BaseListWithPagingModel
    {
        public VehicleNoteApproveListModel([DataSourceRequest] DataSourceRequest request)
           : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"VehicleNotesId", "ID_VEHICLE_NOTE"},
                    {"DealerName", "DEALER_NAME"},
                    {"Note", "NOTE"},
                    {"CreateDate", "CREATE_DATE"},
                    {"IsActiveString", "IS_ACTIVE"},
                    {"VinId","VIN_NO" }

                };
            SetMapper(dMapper);
        }
        public VehicleNoteApproveListModel()
        {

        }
        public int VehicleNotesId { get; set; }

        public int VehicleId { get; set; }

        public string DealerId { get; set; }


        [Display(Name = "Vehicle_Display_VinNo", ResourceType = typeof(MessageResource))]
        public string VinId { get; set; }

        [Display(Name = "VehicleNote_Display_KM", ResourceType = typeof(MessageResource))]
        public decimal VehicleKm { get; set; }


        [Display(Name = "VehicleNote_Display_WarrantlyStartDate", ResourceType = typeof(MessageResource))]
        public DateTime WarrantlyStartDate { get; set; }


        [Display(Name = "VehicleNote_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "VehicleNote_Display_Note", ResourceType = typeof(MessageResource))]
        public string Note { get; set; }

        [Display(Name = "VehicleNote_Display_Date", ResourceType = typeof(MessageResource))]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool? IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public new string IsActiveString { get; set; }
    }
}
