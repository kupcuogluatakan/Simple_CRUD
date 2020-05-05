using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.VehicleModel
{
    public class VehicleModelListModel : BaseListWithPagingModel
    {
        public VehicleModelListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
            {
                {"VehicleModelKod", "MODEL_KOD"},
                {"VehicleModelName", "MODEL_NAME"},
                {"VehicleGroupName", "VHCL_GRP_NAME"},
                {"VehicleModelSSID", "MODEL_SSID"},
                {"IsPdiCheck", "IS_PDI_CHECK"},
                {"IsCouponCheck", "IS_COUPON_CHCK"},
                {"IsBodyWorkDetailCheck", "IS_BODYWORK_DETAIL_REQUIRED"},
                {"IsActiveS", "IS_ACTIVE"}

            };
            SetMapper(dMapper);
        }

        public VehicleModelListModel()
        {
        }

        [Display(Name = "VehicleModel_Display_Code", ResourceType = typeof(MessageResource))]
        public string VehicleModelKod { get; set; }

        [Display(Name = "VehicleModel_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleModelName { get; set; }

        [Display(Name = "VehicleGroup_Display_Name", ResourceType = typeof(MessageResource))]
        public string VehicleGroupName { get; set; }

        public int VehicleGroupId { get; set; }

        [Display(Name = "VehicleModel_Display_SSID", ResourceType = typeof(MessageResource))]
        public string VehicleModelSSID { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveS { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public bool IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActiveSearch { get; set; }

        [Display(Name = "VehicleModel_Display_PDICheck", ResourceType = typeof(MessageResource))]
        public string IsPdiCheck { get; set; }

        [Display(Name = "VehicleModel_Display_CouponCheck", ResourceType = typeof(MessageResource))]
        public string IsCouponCheck { get; set; }

        [Display(Name = "Vehicle_Display_BodyWork_Detailed_Required", ResourceType = typeof(MessageResource))]
        public string IsBodyWorkDetailCheck { get; set; }
    }
}
