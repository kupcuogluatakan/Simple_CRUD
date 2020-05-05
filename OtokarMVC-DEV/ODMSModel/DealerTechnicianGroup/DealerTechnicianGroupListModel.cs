using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.DealerTechnicianGroup
{
    public class DealerTechnicianGroupListModel : BaseListWithPagingModel
    {
        public int DealerTechnicianGroupId { get; set; }
        [Display(Name = "DealerTechnicianGroup_Display_TechnicianGroupName", ResourceType = typeof(MessageResource))]
        public string TechnicianGroupName { get; set; }

        public int? WorkshopTypeId { get; set; }
        [Display(Name = "DealerTechnicianGroup_Display_WorkshopTypeName", ResourceType = typeof(MessageResource))]
        public string WorkshopTypeName { get; set; }

        [Display(Name = "DealerTechnicianGroup_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        public int? DealerId { get; set; }
        [Display(Name = "DealerTechnicianGroup_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "DealerTechnicianGroup_Display_VehicleModelKod", ResourceType = typeof(MessageResource))]
        public string VehicleModelKod { get; set; }

        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }

        public DealerTechnicianGroupListModel()
        {
        }

        public DealerTechnicianGroupListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
             var dMapper = new Dictionary<string, string>
                 {
                     {"DealerTechnicianGroupId", "ID_DEALER_TECHNICIAN_GROUP"},
                     {"WorkshopTypeName", "WORKSHOP_TYPE_NAME"},
                     {"DealerName", "DEALER_NAME"},
                     {"Description", "DESCRIPTION"},
                     {"VehicleModelKod", "VEHICLE_MODEL_KOD"},
                     {"TechnicianGroupName", "TECHNICIAN_GROUP_NAME"},
                     {"IsActiveName","IS_ACTIVE"}
                 };
            SetMapper(dMapper);

        }
    }
}
