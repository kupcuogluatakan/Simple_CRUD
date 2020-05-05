using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.Fleet
{
    public class FleetListModel : BaseListWithPagingModel
    {
        public FleetListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"FleetName","FLEET_NAME"},
                    {"FleetCode","FLEET_CODE"},
                    {"OtokarPartDiscount","OTOKAR_PART_DISCOUNT_RATE"},
                    {"OtokarLabourDiscount","OTOKAR_LABOUR_DISCOUNT_RATE"},
                    {"DealerPartDiscount","Dealer_PART_DISCOUNT_RATE"},
                    {"DealerLabourDiscount","Dealer_LABOUR_DISCOUNT_RATE"},
                    {"IsConstrictedName","IS_CONSTRICTED_NAME"},
                     {"IsVinControl","VIN_CONTROL"}
                };
            SetMapper(dMapper);
        }

        public FleetListModel()
        {
        }

        public int? IdFleet { get; set; }

        [Display(Name = "Fleet_Display_FleetName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FleetName { get; set; }

        [Display(Name = "Fleet_Display_FleetCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FleetCode { get; set; }

        [Display(Name = "Fleet_Display_OtokarPartDiscount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? OtokarPartDiscount { get; set; }

        [Display(Name = "Fleet_Display_OtokarLabourDiscount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? OtokarLabourDiscount { get; set; }

        [Display(Name = "Fleet_Display_DealerPartDiscount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? DealerPartDiscount { get; set; }

        [Display(Name = "Fleet_Display_DealerLabourDiscount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal? DealerLabourDiscount { get; set; }

        [Display(Name = "Fleet_Display_IsConstrictedName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsConstrictedName { get; set; }

        [Display(Name = "Fleet_Display_IsConstrictedName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IsConstricted { get; set; }

        [Display(Name = "Fleet_Display_IsVinControl", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsVinControl { get; set; }

        [Display(Name = "Fleet_Display_StartDateValid", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime StartDateValid { get; set; }

        [Display(Name = "Fleet_Display_EndDateValid", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime EndDateValid { get; set; }

        [Display(Name = "Global_Display_Active", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsActive { get; set; }
    }
}
