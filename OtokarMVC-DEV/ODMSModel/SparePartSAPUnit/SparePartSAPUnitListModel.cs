using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.SparePartSAPUnit
{
    public class SparePartSAPUnitListModel : BaseListWithPagingModel
    {
        public SparePartSAPUnitListModel([DataSourceRequest] DataSourceRequest request) : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"SparePartCode", "SP.PART_CODE"},
                    {"SparePartName", "PART_NAME"},
                    {"UnitName", "SPSU.UNIT_LOOKVAL"},
                    {"ShipQuantity", "SHIP_QUANT"},
                    {"StateName", "IS_ACTIVE"}
                };
            SetMapper(dMapper);
        }

        public SparePartSAPUnitListModel()
        {

        }

        public int PartId { get; set; }

        [Display(Name = "SparePartSAPUnit_Display_SparePartCode", ResourceType = typeof(MessageResource))]
        public string SparePartCode { get; set; }

        [Display(Name = "SparePartSAPUnit_Display_SparePartName", ResourceType = typeof(MessageResource))]
        public string SparePartName { get; set; }

        public int UnitId { get; set; }

        [Display(Name = "SparePartSAPUnit_Display_UnitName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string UnitName { get; set; }

        [Display(Name = "SparePartSAPUnit_Display_ShipQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal ShipQuantity { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActive { get; set; }

        [Display(Name = "SparePartSAPUnit_Display_StateName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StateName { get; set; }
    }
}
