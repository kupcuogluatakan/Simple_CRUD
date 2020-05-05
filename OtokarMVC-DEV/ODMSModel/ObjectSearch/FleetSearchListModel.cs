using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.ObjectSearch
{
    public class FleetSearchListModel:BaseListWithPagingModel
    {
           public FleetSearchListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"FleetName", "FLEET_NAME"},
                    {"FleetCode", "FLEET_CODE"},
                    {"IsPartConstricted", "IS_PART_CONSTRICTED"},
                    {"OtokarPartDiscountRate", "OTOKAR_PART_DISCOUNT_RATE"},
                    {"OtokarLabourDiscountRate", "OTOKAR_LABOUR_DISCOUNT_RATE"},
                    {"DealerPartDiscountRate", "DEALER_PART_DISCOUNT_RATE"},
                    {"DealerLabourDiscountRate", "DEALER_LABOUR_DISCOUNT_RATE"},
                    {"StartDateTime", "VALIDITY_START_DATE"},
                    {"EndDateTime", "VALIDITY_END_DATE"}
                };
            SetMapper(dMapper);
        }

        public FleetSearchListModel() { }

        public int FleetId { get; set; }
        [Display(Name = "Fleet_Display_FleetName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FleetName { get; set; }
        [Display(Name = "Fleet_Display_FleetCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FleetCode { get; set; }
        [Display(Name = "Fleet_Display_IsConstrictedName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool? IsPartConstricted { get; set; }
        [Display(Name = "Fleet_Display_OtokarPartDiscount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal OtokarPartDiscountRate { get; set; }
        [Display(Name = "Fleet_Display_OtokarLabourDiscount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal OtokarLabourDiscountRate { get; set; }
        [Display(Name = "Fleet_Display_DealerPartDiscount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal DealerPartDiscountRate { get; set; }
        [Display(Name = "Fleet_Display_DealerLabourDiscount", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal DealerLabourDiscountRate { get; set; }

        [Display(Name = "Fleet_Display_IsConstrictedName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string IsConstrictedName { get; set; }

        [Display(Name = "Fleet_Display_StartDateValid", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "Fleet_Display_EndDateValid", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime EndDateTime { get; set; }
    }
}
